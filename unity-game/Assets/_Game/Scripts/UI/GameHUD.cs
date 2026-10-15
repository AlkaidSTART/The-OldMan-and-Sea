using UnityEngine;
using OldManAndTheSea.Fishing;
using OldManAndTheSea.Survival;
using OldManAndTheSea.AI;
using OldManAndTheSea.Dialogue;
using OldManAndTheSea.Core;

namespace OldManAndTheSea.UI
{
    /// <summary>
    /// 全局综合剧情与海战 HUD 渲染器
    /// 支持陆地探索交互、双语剧情对白、开放海域多深度布饵与深海拉锯博弈
    /// </summary>
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private FishingLinePhysics linePhysics;
        [SerializeField] private OldManSurvivalStats stats;
        [SerializeField] private MarlinAI marlin;
        [SerializeField] private WeaponDegradationSystem weaponSystem;
        [SerializeField] private Transform boatTransform;

        private string currentQuoteText = "“人不是为失败而生的。一个人可以被毁灭，但不能被打败。”";
        private string currentQuoteSub = "“Man is not made for defeat. A man can be destroyed but not defeated.”";
        private float quoteTimer = 8f;

        private void Start()
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.OnQuoteTriggered += OnQuote;
            }
        }

        private void OnDestroy()
        {
            if (DialogueManager.Instance != null)
            {
                DialogueManager.Instance.OnQuoteTriggered -= OnQuote;
            }
        }

        private void OnQuote(GameQuote quote)
        {
            currentQuoteText = quote.chinese;
            currentQuoteSub = quote.english;
            quoteTimer = quote.displayDuration;
        }

        private void Update()
        {
            if (quoteTimer > 0)
            {
                quoteTimer -= Time.deltaTime;
            }
        }

        private void OnGUI()
        {
            var flow = StoryFlowManager.Instance;
            if (flow == null) return;

            GUI.backgroundColor = new Color(0.08f, 0.12f, 0.18f, 0.92f);
            GUI.contentColor = Color.white;

            // 1. 顶部全局剧情目标栏 (Objective Banner)
            DrawObjectiveBanner(flow);

            // 2. 如果正在进行剧情对白，绘制原著台词弹窗
            if (flow.IsDialogueActive)
            {
                DrawDialogueModal(flow.CurrentLine);
                return; // 对白时专心看故事
            }

            // 3. 根据当前剧情阶段绘制专属界面
            if (flow.CurrentPhase == StoryPhase.Prologue_TerraceBeer || 
                flow.CurrentPhase == StoryPhase.Prologue_SantiagoShack || 
                flow.CurrentPhase == StoryPhase.Prologue_BoardSkiff)
            {
                DrawLandPrologueHUD();
            }
            else if (flow.CurrentPhase == StoryPhase.Act1_OpenSeaExploration)
            {
                DrawOpenSeaHUD(flow);
            }
            else if (flow.CurrentPhase == StoryPhase.Act2_MarlinTowingBattle)
            {
                DrawMarlinBattleHUD();
            }
        }

        #region 顶部主线目标栏
        private void DrawObjectiveBanner(StoryFlowManager flow)
        {
            float width = Mathf.Min(820, Screen.width - 40);
            GUILayout.BeginArea(new Rect((Screen.width - width) / 2f, 15, width, 44), GUI.skin.box);
            GUILayout.BeginHorizontal();
            GUI.color = new Color(1f, 0.85f, 0.4f);
            GUILayout.Label($"<size=13><b>{flow.GetCurrentObjectiveText()}</b></size>");
            GUI.color = Color.white;
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
        #endregion

        #region 剧情对白弹窗
        private void DrawDialogueModal(DialogueLine line)
        {
            if (line == null) return;

            float boxWidth = Mathf.Min(780, Screen.width - 60);
            float boxHeight = 170;
            Rect r = new Rect((Screen.width - boxWidth) / 2f, Screen.height - boxHeight - 40, boxWidth, boxHeight);

            GUI.Box(r, "");
            GUILayout.BeginArea(r);

            GUILayout.Space(8);
            GUILayout.BeginHorizontal();
            GUI.color = new Color(1f, 0.85f, 0.3f);
            GUILayout.Label($"<size=16><b>【{line.speaker}】</b></size> <size=11><i>{line.speakerTitle}</i></size>");
            GUI.color = Color.white;
            GUILayout.EndHorizontal();

            GUILayout.Space(6);
            GUI.color = new Color(1f, 0.98f, 0.92f);
            GUILayout.Label($"<size=14>{line.textCn}</size>");

            GUILayout.Space(4);
            GUI.color = new Color(0.75f, 0.82f, 0.9f);
            GUILayout.Label($"<i><size=12>{line.textEn}</size></i>");

            GUILayout.FlexibleSpace();
            GUI.color = new Color(0.4f, 1f, 0.6f);
            GUILayout.Label("<size=11><b>[ 按 空格键 / E / F 推进剧情对白 ]</b></size>", GUILayout.Height(20));
            GUI.color = Color.white;

            GUILayout.EndArea();
        }
        #endregion

        #region 陆地序幕探索 HUD
        private void DrawLandPrologueHUD()
        {
            // 交互提示 (如果站在地标附近)
            var player = FindAnyObjectByType<PlayerOnFootController>();
            if (player != null && player.IsNearInteractable)
            {
                float w = 480;
                Rect pr = new Rect((Screen.width - w) / 2f, Screen.height - 110, w, 50);
                GUI.backgroundColor = new Color(0.2f, 0.5f, 0.3f, 0.95f);
                GUILayout.BeginArea(pr, GUI.skin.box);
                GUI.color = Color.white;
                GUILayout.Label($"<size=14><b>{player.PromptText}</b></size>");
                GUILayout.EndArea();
                GUI.backgroundColor = new Color(0.08f, 0.12f, 0.18f, 0.92f);
            }

            // 左下角：漫步操作提示
            GUILayout.BeginArea(new Rect(20, Screen.height - 130, 280, 110), GUI.skin.box);
            GUILayout.Label("<b>【陆地漫步与探索操作】</b>");
            GUILayout.Label("• <b>W / A / S / D</b>: 老人漫步行走");
            GUILayout.Label("• <b>E / F 键</b>: 与地标/人物交互");
            GUILayout.Label("• 探索点: 露台饭店 ➔ 陋室 ➔ 码头");
            GUILayout.EndArea();
        }
        #endregion

        #region 开放海域航海 HUD
        private void DrawOpenSeaHUD(StoryFlowManager flow)
        {
            float boatZ = boatTransform != null ? boatTransform.position.z : 0f;
            string zoneText = "Zone 0: 科希马尔避风港 (内湾水域)";
            Color zoneColor = Color.green;

            if (boatZ > 140f)
            {
                zoneText = "Zone 2: 墨西哥湾流大井区 (墨黑深渊，巨鱼出没！)";
                zoneColor = new Color(0.4f, 0.7f, 1f);
            }
            else if (boatZ > 50f)
            {
                zoneText = "Zone 1: 大陆架浅滩海域 (翡翠绿浅滩，飞鱼穿梭)";
                zoneColor = Color.yellow;
            }

            // 1. 左上角：水文与洋流状态
            GUILayout.BeginArea(new Rect(20, 70, 320, 160), GUI.skin.box);
            GUILayout.Label("<b>【航海罗盘与水文区域】</b>");
            GUI.color = zoneColor;
            GUILayout.Label($"<b>{zoneText}</b>");
            GUI.color = Color.white;
            GUILayout.Label($"离岸纵深: {boatZ:F0} 米 | 洋流速度: 2.4 节");
            GUILayout.Space(6);
            GUILayout.Label("<b>【四层钓线垂直布饵状态】</b>");
            GUILayout.Label($"• [1键] 40寻 浅层线: {(flow.bait40Fathoms ? "<color=green>已布设 (沙丁鱼)</color>" : "<color=gray>未放线</color>")}");
            GUILayout.Label($"• [2键] 75寻 中层线: {(flow.bait75Fathoms ? "<color=green>已布设 (鲜鱼条)</color>" : "<color=gray>未放线</color>")}");
            GUILayout.Label($"• [3键] 100寻 深渊井口: {(flow.bait100Fathoms ? "<color=cyan>★已布设 (长鳍金枪鱼重饵)</color>" : "<color=orange>未放线 (马林鱼诱饵)</color>")}");
            GUILayout.Label($"• [4键] 125寻 冥府冷水: {(flow.bait125Fathoms ? "<color=green>已布设 (冷水饵)</color>" : "<color=gray>未放线</color>")}");
            GUILayout.EndArea();

            // 2. 右上角：大鱼诱饵与快速咬钩指令
            GUILayout.BeginArea(new Rect(Screen.width - 280, 70, 260, 110), GUI.skin.box);
            GUILayout.Label("<b>【诱捕大马林鱼】</b>");
            GUILayout.Label("驶入大井区深水并布设100寻线，或直接按空格键召唤咬钩！");
            if (GUILayout.Button("🎣 诱引百寻大鱼咬钩 [空格]", GUILayout.Height(30)))
            {
                flow.TriggerMarlinStrike();
            }
            GUILayout.EndArea();

            // 3. 左下角：航海操作指南
            GUILayout.BeginArea(new Rect(20, Screen.height - 140, 300, 120), GUI.skin.box);
            GUILayout.Label("<b>【开放海域航行指南】</b>");
            GUILayout.Label("• <b>W / S</b>: 奋力划木双桨前行 / 倒桨");
            GUILayout.Label("• <b>A / D</b>: 划单桨控制小船航向");
            GUILayout.Label("• <b>1 / 2 / 3 / 4 键</b>: 开关对应深度钓线");
            GUILayout.Label("• <b>[空格键]</b>: 触发百寻巨鱼咬钩");
            GUILayout.EndArea();
        }
        #endregion

        #region 大马林鱼拉锯搏杀 HUD
        private void DrawMarlinBattleHUD()
        {
            // 1. 左上角：老人四维生存体征
            GUILayout.BeginArea(new Rect(20, 70, 260, 220), GUI.skin.box);
            GUILayout.Label("<b>【圣地亚哥·体征】</b>");
            if (stats != null)
            {
                DrawStatBar("体力 (Stamina)", stats.Stamina, 100f, Color.green);
                DrawStatBar("水分 (Hydration)", stats.Hydration, 100f, Color.cyan);
                DrawStatBar("手伤 (Hands)", stats.HandHealth, 100f, Color.red);
                DrawStatBar("神志 (Clarity)", stats.Clarity, 100f, Color.yellow);
                GUILayout.Label($"棕色玻璃水瓶: 剩余 {stats.WaterSipsRemaining} 口 [按 E 饮水]");

                if (stats.IsLeftHandCramped)
                {
                    GUI.color = Color.red;
                    GUILayout.Label("⚠️ 警告: 左手肌肉痉挛抽筋中！[按 Q 搓揉]");
                    GUI.color = Color.white;
                }
            }
            GUILayout.EndArea();

            // 2. 正上方：钓线张力计
            GUILayout.BeginArea(new Rect(Screen.width / 2f - 200, 65, 400, 105), GUI.skin.box);
            GUILayout.Label("<size=14><b>【深海钓线张力 (Tension)】</b></size>");
            if (linePhysics != null)
            {
                Color tensionColor = Color.green;
                string stateText = "安全耐力消耗区 (Optimal)";
                if (linePhysics.CurrentTension < 30f)
                {
                    tensionColor = Color.yellow;
                    stateText = "⚠️ 虚线松脱危险区 (Slack Warning)";
                }
                else if (linePhysics.CurrentTension > 90f)
                {
                    tensionColor = Color.red;
                    stateText = "🚨 崩断绝境！轻放阻尼让线 (Snap Danger!)";
                }
                else if (linePhysics.CurrentTension > 75f)
                {
                    tensionColor = new Color(1f, 0.5f, 0f);
                    stateText = "高负荷僵持区 (Strain)";
                }

                DrawStatBar(stateText, linePhysics.CurrentTension, 100f, tensionColor);
                GUILayout.Label($"剩余储备线: {linePhysics.RemainingLine:F1} 寻 | 放出深度: {linePhysics.CurrentLineOut:F1} 寻");
            }
            GUILayout.EndArea();

            // 3. 右上角：大马林鱼状态
            GUILayout.BeginArea(new Rect(Screen.width - 280, 70, 260, 150), GUI.skin.box);
            GUILayout.Label("<b>【巨物猎获与武器】</b>");
            if (marlin != null)
            {
                GUILayout.Label($"18英尺大马林鱼状态: {marlin.CurrentState}");
                DrawStatBar("巨鱼体力", marlin.StaminaRatio * 100f, 100f, new Color(0.6f, 0.4f, 0.9f));
            }
            if (weaponSystem != null)
            {
                GUILayout.Label($"防卫武器: {weaponSystem.CurrentWeapon}");
                GUILayout.Label($"耐久剩余: {weaponSystem.CurrentDurability} 次");
            }
            GUILayout.EndArea();

            // 4. 底部居中：原著心理独白
            if (quoteTimer > 0)
            {
                float boxWidth = Mathf.Min(700, Screen.width - 40);
                GUILayout.BeginArea(new Rect((Screen.width - boxWidth) / 2f, Screen.height - 105, boxWidth, 85), GUI.skin.box);
                GUI.color = new Color(1f, 0.95f, 0.8f);
                GUILayout.Label($"<size=15><b>{currentQuoteText}</b></size>");
                GUI.color = new Color(0.8f, 0.8f, 0.8f);
                GUILayout.Label($"<i><size=12>{currentQuoteSub}</size></i>");
                GUI.color = Color.white;
                GUILayout.EndArea();
            }

            // 5. 左下角：搏杀按键指南
            GUILayout.BeginArea(new Rect(20, Screen.height - 140, 300, 120), GUI.skin.box);
            GUILayout.Label("<b>【大鱼拉锯操作指南】</b>");
            GUILayout.Label("• W/S/A/D: 顺应拖曳划桨");
            GUILayout.Label("• 按住 [空格键]: 锁线用力收线 (Reel In)");
            GUILayout.Label("• 松开 [空格键]: 顺应放线泄力 (Give Slack)");
            GUILayout.Label("• [E] 键喝水 | [Q] 键揉手缓解抽筋");
            GUILayout.EndArea();
        }
        #endregion

        private void DrawStatBar(string label, float current, float max, Color barColor)
        {
            GUILayout.Label($"{label}: {current:F0} / {max:F0}");
            Rect r = GUILayoutUtility.GetRect(180, 11);
            GUI.DrawTexture(r, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, Color.black, 0, 0);

            float fill = Mathf.Clamp01(current / max);
            Rect fillRect = new Rect(r.x, r.y, r.width * fill, r.height);
            GUI.DrawTexture(fillRect, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, barColor, 0, 0);
        }
    }
}
