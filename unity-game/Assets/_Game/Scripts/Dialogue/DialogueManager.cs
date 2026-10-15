using System;
using System.Collections.Generic;
using UnityEngine;

namespace OldManAndTheSea.Dialogue
{
    [Serializable]
    public class GameQuote
    {
        public string id;
        public string context;
        [TextArea(2, 4)] public string chinese;
        [TextArea(2, 4)] public string english;
        public float displayDuration = 5f;

        public GameQuote(string id, string context, string cn, string en, float duration = 5f)
        {
            this.id = id;
            this.context = context;
            this.chinese = cn;
            this.english = en;
            this.displayDuration = duration;
        }
    }

    /// <summary>
    /// 全书经典中英双语台词与独白文库
    /// 依据 docs/03_GAME_SYSTEMS_AND_MECHANICS.md 第 4 节实现
    /// </summary>
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }

        public event Action<GameQuote> OnQuoteTriggered;

        private readonly List<GameQuote> quotes = new List<GameQuote>();

        private void Awake()
        {
            if (Instance == null) Instance = this;
            else Destroy(gameObject);

            InitializeQuotes();
        }

        private void InitializeQuotes()
        {
            quotes.Add(new GameQuote(
                "DEF_01",
                "SharkAttack",
                "人不是为失败而生的。一个人可以被毁灭，但不能被打败。",
                "Man is not made for defeat. A man can be destroyed but not defeated."
            ));

            quotes.Add(new GameQuote(
                "RES_01",
                "TowingStruggle",
                "鱼啊，我非常爱你，也十分尊敬你。但今天太阳落山前，我一定要杀死你。",
                "I love you and respect you very much. But I will kill you dead before this day ends."
            ));

            quotes.Add(new GameQuote(
                "PAIN_01",
                "HandCut",
                "疼痛对一个汉子来说算不上什么。",
                "Pain does not matter to a man."
            ));

            quotes.Add(new GameQuote(
                "BOY_01",
                "CrampLeftHand",
                "我真希望那男孩在这儿。能帮我一把，还能看看这番景象。",
                "I wish the boy were here. To help me and to see this."
            ));

            quotes.Add(new GameQuote(
                "WILL_01",
                "ExhaustionMax",
                "拉吧，双手，我想。撑住吧，两腿。替我顶住，脑袋。替我顶住。你从没垮过。",
                "Pull, hands, I thought. Hold up, legs. Last for me, head. Last for me. You never went."
            ));

            quotes.Add(new GameQuote(
                "END_01",
                "EpilogueDream",
                "老头儿正在梦见那些狮子。",
                "The old man was dreaming about the lions."
            ));
        }

        public GameQuote TriggerQuoteByContext(string context)
        {
            var matched = quotes.Find(q => q.context.Equals(context, StringComparison.OrdinalIgnoreCase));
            if (matched != null)
            {
                OnQuoteTriggered?.Invoke(matched);
                return matched;
            }
            return null;
        }

        public GameQuote TriggerRandomQuote()
        {
            if (quotes.Count == 0) return null;
            int idx = UnityEngine.Random.Range(0, quotes.Count);
            var q = quotes[idx];
            OnQuoteTriggered?.Invoke(q);
            return q;
        }
    }
}
