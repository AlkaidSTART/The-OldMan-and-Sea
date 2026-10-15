using UnityEngine;
using OldManAndTheSea.Fishing;
using OldManAndTheSea.Survival;
using OldManAndTheSea.AI;
using OldManAndTheSea.Dialogue;

namespace OldManAndTheSea.UI
{
    /// <summary>
    /// 即开即用的原型 HUD，原生 GUI 渲染（无需复杂 UI 预制体即可运行）
    /// </summary>
    public class GameHUD : MonoBehaviour
    {
        [SerializeField] private FishingLinePhysics linePhysics;
        [SerializeField] private OldManSurvivalStats stats;
        [SerializeField] private MarlinAI marlin;
        [SerializeField] private WeaponDegradationSystem weaponSystem;

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
            // 顶层风格与颜色
            GUI.backgroundColor = new Color(0.1f, 0.15f, 0.2f, 0.85f);
            GUI.contentColor = Color.white;

            // 1. 左上角：老人四维生存体征
            GUILayout.BeginArea(new Rect(20, 20, 260, 220), GUI.skin.box);
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

            // 2. 屏幕正上方：钓线张力计 (Tension Gauge)
            GUILayout.BeginArea(new Rect(Screen.width / 2f - 200, 20, 400, 110), GUI.skin.box);
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
                GUILayout.Label($"剩余储备线: {linePhysics.RemainingLine:F1} 寻 (总380寻) | 放出深度: {linePhysics.CurrentLineOut:F1} 寻");
            }
            GUILayout.EndArea();

            // 3. 右上角：大马林鱼与武器装备状态
            GUILayout.BeginArea(new Rect(Screen.width - 280, 20, 260, 160), GUI.skin.box);
            GUILayout.Label("<b>【猎获与武器】</b>");
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

            // 4. 底部居中：原著中英双语心理独白
            if (quoteTimer > 0)
            {
                float boxWidth = Mathf.Min(700, Screen.width - 40);
                GUILayout.BeginArea(new Rect((Screen.width - boxWidth) / 2f, Screen.height - 110, boxWidth, 90), GUI.skin.box);
                GUI.color = new Color(1f, 0.95f, 0.8f);
                GUILayout.Label($"<size=15><b>{currentQuoteText}</b></size>");
                GUI.color = new Color(0.8f, 0.8f, 0.8f);
                GUILayout.Label($"<i><size=12>{currentQuoteSub}</size></i>");
                GUI.color = Color.white;
                GUILayout.EndArea();
            }

            // 5. 左下角：按键说明操作指南
            GUILayout.BeginArea(new Rect(20, Screen.height - 140, 300, 120), GUI.skin.box);
            GUILayout.Label("<b>【操作指南】</b>");
            GUILayout.Label("• W/S/A/D: 划桨前行与转向");
            GUILayout.Label("• 按住 [空格键]: 锁线用力收线 (Reel In)");
            GUILayout.Label("• 松开 [空格键]: 顺应放线泄力 (Give Slack)");
            GUILayout.Label("• [E] 键喝水 | [Q] 键揉手缓解痉挛");
            GUILayout.EndArea();
        }

        private void DrawStatBar(string label, float current, float max, Color barColor)
        {
            GUILayout.Label($"{label}: {current:F0} / {max:F0}");
            Rect r = GUILayoutUtility.GetRect(180, 12);
            GUI.DrawTexture(r, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, Color.black, 0, 0);

            float fill = Mathf.Clamp01(current / max);
            Rect fillRect = new Rect(r.x, r.y, r.width * fill, r.height);
            GUI.DrawTexture(fillRect, Texture2D.whiteTexture, ScaleMode.StretchToFill, true, 0, barColor, 0, 0);
        }
    }
}
