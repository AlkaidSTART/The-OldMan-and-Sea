using System;
using System.Collections.Generic;
using UnityEngine;
using OldManAndTheSea.Boat;
using OldManAndTheSea.Fishing;
using OldManAndTheSea.Survival;
using OldManAndTheSea.AI;
using OldManAndTheSea.Dialogue;

namespace OldManAndTheSea.Core
{
    public enum StoryPhase
    {
        Prologue_TerraceBeer,       // 第0幕：露台饭店喝啤酒、聊84天厄运与迪马吉奥
        Prologue_SantiagoShack,     // 第0幕：回到木棚屋陋室，检视家当与圣母像，准备行装
        Prologue_BoardSkiff,        // 第0幕：来到沙滩栈桥，推船入海，告别男孩马诺林
        Act1_OpenSeaExploration,    // 第1幕：开放海域自由航行探索（观天察海、布设4层钓线）
        Act2_MarlinTowingBattle     // 第2幕：百寻深渊大马林鱼咬钩！进入深渊意志力拉锯战
    }

    [Serializable]
    public class DialogueLine
    {
        public string speaker;
        public string speakerTitle;
        [TextArea(2, 4)] public string textCn;
        [TextArea(2, 4)] public string textEn;

        public DialogueLine(string speaker, string title, string cn, string en)
        {
            this.speaker = speaker;
            this.speakerTitle = title;
            this.textCn = cn;
            this.textEn = en;
        }
    }

    /// <summary>
    /// 全局主线剧情与开放自由探索流程控制器
    /// 严格遵循原著《老人与海》全书序幕、启航、深海自由探索到咬钩的完整推进
    /// </summary>
    public class StoryFlowManager : MonoBehaviour
    {
        public static StoryFlowManager Instance { get; private set; }

        [Header("Current Phase")]
        [SerializeField] private StoryPhase currentPhase = StoryPhase.Prologue_TerraceBeer;

        [Header("Scene Actors & Transforms")]
        [SerializeField] private Transform santiagoPlayer;
        [SerializeField] private Transform boat;
        [SerializeField] private BoatController boatController;
        [SerializeField] private MarlinAI marlin;
        [SerializeField] private FishingLinePhysics linePhysics;
        [SerializeField] private OldManSurvivalStats stats;

        [Header("Land Landmarks")]
        [SerializeField] private Transform terraceTablePoint;
        [SerializeField] private Transform shackPoint;
        [SerializeField] private Transform pierBoatPoint;

        [Header("Open Sea Multi-Depth Baits")]
        public bool bait40Fathoms = false;
        public bool bait75Fathoms = false;
        public bool bait100Fathoms = false;
        public bool bait125Fathoms = false;

        // Dialogue state
        private bool isDialogueActive = false;
        private List<DialogueLine> currentDialogueSequence = new List<DialogueLine>();
        private int currentDialogueIndex = 0;
        private Action onDialogueCompletedCallback;

        public StoryPhase CurrentPhase => currentPhase;
        public bool IsDialogueActive => isDialogueActive;
        public DialogueLine CurrentLine => (isDialogueActive && currentDialogueIndex < currentDialogueSequence.Count) 
            ? currentDialogueSequence[currentDialogueIndex] 
            : null;

        public event Action<StoryPhase> OnPhaseChanged;

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);
        }

        private void Start()
        {
            SetPhase(StoryPhase.Prologue_TerraceBeer);
        }

        public void SetPhase(StoryPhase phase)
        {
            currentPhase = phase;
            OnPhaseChanged?.Invoke(phase);

            if (phase == StoryPhase.Prologue_TerraceBeer || phase == StoryPhase.Prologue_SantiagoShack || phase == StoryPhase.Prologue_BoardSkiff)
            {
                // 陆地探索阶段：圣地亚哥步行激活，小船静止在岸边
                if (santiagoPlayer != null) santiagoPlayer.gameObject.SetActive(true);
                if (boatController != null) boatController.enabled = false;
                if (linePhysics != null) linePhysics.gameObject.SetActive(false);
                if (marlin != null) marlin.gameObject.SetActive(false);
            }
            else if (phase == StoryPhase.Act1_OpenSeaExploration)
            {
                // 登船启航自由探索阶段：隐藏陆地步行体，小船划桨与航海物理激活
                if (santiagoPlayer != null) santiagoPlayer.gameObject.SetActive(false);
                if (boatController != null)
                {
                    boatController.enabled = true;
                    boatController.transform.position = new Vector3(0, 0.4f, 5f);
                }
                if (linePhysics != null) linePhysics.gameObject.SetActive(true);
                if (marlin != null) marlin.gameObject.SetActive(true);
            }
            else if (phase == StoryPhase.Act2_MarlinTowingBattle)
            {
                // 大鱼咬钩搏杀阶段
                if (marlin != null && boatController != null && linePhysics != null)
                {
                    marlin.gameObject.SetActive(true);
                    linePhysics.gameObject.SetActive(true);
                    linePhysics.Initialize(boatController.SternAnchorPoint, marlin.transform);
                    marlin.Initialize(boatController.transform);
                    marlin.HookAndTow();
                }
            }
        }

        #region 剧情对话系统与触发器

        public void StartDialogue(List<DialogueLine> lines, Action onComplete = null)
        {
            if (lines == null || lines.Count == 0) return;
            currentDialogueSequence = lines;
            currentDialogueIndex = 0;
            isDialogueActive = true;
            onDialogueCompletedCallback = onComplete;
        }

        public void AdvanceDialogue()
        {
            if (!isDialogueActive) return;

            currentDialogueIndex++;
            if (currentDialogueIndex >= currentDialogueSequence.Count)
            {
                isDialogueActive = false;
                var callback = onDialogueCompletedCallback;
                onDialogueCompletedCallback = null;
                callback?.Invoke();
            }
        }

        /// <summary>
        /// 触发【露台饭店喝啤酒】剧情对话
        /// </summary>
        public void TriggerTerraceBeerDialogue()
        {
            var lines = new List<DialogueLine>
            {
                new DialogueLine("马诺林", "忠诚的男孩",
                    "“圣地亚哥，到‘露台’去喝杯啤酒吧，然后我们把渔具搬回家。”",
                    "“Santiago,” the boy said. “Let me go get you a beer at the Terrace and then we’ll take the stuff home.”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“好啊，为什么不呢？都是打鱼人，喝一杯啤酒解解渴。”",
                    "“Why not?” the old man said. “Between fishermen.”"),

                new DialogueLine("年轻渔夫甲", "邻桌嘲笑者",
                    "“瞧瞧那老头，整整八十四天没逮着一条鱼了！他已经彻底被‘Salao’（最严重的倒霉）缠死了！”",
                    "“Look at the old man. Eighty-four days without a fish! He is definitely salao, the worst form of unlucky.”"),

                new DialogueLine("马诺林", "忠诚的男孩",
                    "“别听他们的，老头子。今天洋基队赢了！迪马吉奥在脚后跟骨刺剧痛下，依然打出了全垒打！”",
                    "“Do not mind them, old man. The Yankees won today! The great DiMaggio hit a home run, even with the bone spur in his heel!”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“伟大的迪马吉奥……真正的硬汉在痛苦中从不退缩。明天是第八十五天，八十五是个吉利数字。我要驶向远方深海。”",
                    "“The great DiMaggio... A true man does not yield to pain. Tomorrow is eighty-five. Eighty-five is a lucky number. I will go far out.”"),

                new DialogueLine("马诺林", "忠诚的男孩",
                    "“给，喝完这杯清凉的哈瓦那啤酒。今晚回你的小棚屋好好睡一觉，明天清晨四点，我来叫醒你，给你带鲜沙丁鱼当重饵！”",
                    "“Drink up this cold Hatuey beer. Sleep well in your shack tonight. At four in the morning, I’ll wake you and bring fresh sardines for deep bait!”")
            };

            StartDialogue(lines, () =>
            {
                SetPhase(StoryPhase.Prologue_SantiagoShack);
            });
        }

        /// <summary>
        /// 触发【木棚屋整理行装与喝苦咖啡】剧情对话
        /// </summary>
        public void TriggerShackDialogue()
        {
            var lines = new List<DialogueLine>
            {
                new DialogueLine("圣地亚哥", "老渔夫",
                    "“棚屋里还是老样子。弹簧床上铺着旧报纸，墙上挂着耶稣圣心像和加布里埃尔圣母像……”",
                    "“The shack is quiet. Newspapers on the springs of the bed, the Sacred Heart of Jesus and the Virgin of Cobre on the wall...”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“用白铁皮罐煮上一罐热苦咖啡。没有牛奶，没有糖，但能驱散海风的寒气。”",
                    "“Boil up some black coffee in the condensed milk tin. No milk, no sugar, but it warms a man against the night chill.”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“装满一瓶淡水，检查铁飞叉和柳条钓线篮。明天的八十五天，运气来的时候，我必须做到分毫不差。”",
                    "“Fill the water bottle. Check the iron harpoon and the wicker coiled lines. When luck comes tomorrow, I must be exact.”")
            };

            StartDialogue(lines, () =>
            {
                SetPhase(StoryPhase.Prologue_BoardSkiff);
            });
        }

        /// <summary>
        /// 触发【清晨栈桥登船启航】剧情对话
        /// </summary>
        public void TriggerBoardSkiffDialogue()
        {
            var lines = new List<DialogueLine>
            {
                new DialogueLine("马诺林", "忠诚的男孩",
                    "“老头子！两包清晨刚捕上的新鲜沙丁鱼和两尾金枪鱼重饵都在船舱里了！水壶也放稳了。”",
                    "“Old man! Two packages of fresh sardines and two albacore for deep baits are in the skiff! The water jug is secure.”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“谢谢你，孩子。你是个好孩子。替我照看好港湾里的其他渔具。”",
                    "“Thank you, boy. You are a good boy. Look after the gear on the dock for me.”"),

                new DialogueLine("马诺林", "忠诚的男孩",
                    "“祝你好运，老头子！划得远一些，去墨西哥湾流的大井区，抓一条震惊整个港湾的巨鱼！”",
                    "“Good luck, old man! Go far out into the Great Well of the Gulf Stream, and bring home a fish to amaze the whole village!”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“好孩子，再见！大海上见真章！”",
                    "“Goodbye, boy! A man will see what he can do upon the great sea!”")
            };

            StartDialogue(lines, () =>
            {
                SetPhase(StoryPhase.Act1_OpenSeaExploration);
            });
        }

        /// <summary>
        /// 触发【百寻深海巨鱼咬钩】突发剧情
        /// </summary>
        public void TriggerMarlinStrike()
        {
            var lines = new List<DialogueLine>
            {
                new DialogueLine("旁白", "深海回响",
                    "【正午·墨西哥湾流大井区 100寻深渊】系在船舷上的深绿树枝猛地向下一弯，随后急速颤抖点头！",
                    "【Noon, The Great Well, 100 Fathoms】The delicate green stick on the gunwale dips down sharply, shivering violently!"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“沉住气……不要立即提竿！百寻深渊的大物在轻触试探……让钓绳轻轻从拇指和食指间滑过去几十码……吃下去吧，大鱼，把它咽下去！”",
                    "“Easy now... Do not jerk! A giant thing a hundred fathoms down is tasting the bait... Let the line slip smoothly through the fingers... Eat it, fish, swallow it whole!”"),

                new DialogueLine("圣地亚哥", "老渔夫",
                    "“现在！它咬稳了！拉直身子——一击！二击！三击！钩牢了！！”",
                    "“Now! He has taken it! Straighten the back—One! Two! Three! Struck home!”")
            };

            StartDialogue(lines, () =>
            {
                SetPhase(StoryPhase.Act2_MarlinTowingBattle);
            });
        }

        #endregion

        public string GetCurrentObjectiveText()
        {
            switch (currentPhase)
            {
                case StoryPhase.Prologue_TerraceBeer:
                    return "【第0幕·出航前夜】前往露台饭店，坐下与男孩马诺林喝一杯啤酒 [按 E/F 交互]";
                case StoryPhase.Prologue_SantiagoShack:
                    return "【第0幕·出航前夜】返回木棚屋陋室，检视家当与圣母像，喝清晨苦咖啡 [按 E/F 交互]";
                case StoryPhase.Prologue_BoardSkiff:
                    return "【第0幕·出航前夜】前往栈桥沙滩，与男孩告别，登上 16 英尺小木船 [按 E/F 启航]";
                case StoryPhase.Act1_OpenSeaExploration:
                    return "【第1幕·破晓深航】划桨驶向墨西哥湾流大井区，按 [1~4] 键在深渊布饵，或按 [空格] 诱鱼咬钩";
                case StoryPhase.Act2_MarlinTowingBattle:
                    return "【第2幕·深渊拖曳】18英尺大马林鱼正在拖拽小船！长按 [空格] 锁线加阻尼，按 [E] 饮水，按 [Q] 搓揉左手痉挛";
                default:
                    return "";
            }
        }
    }
}
