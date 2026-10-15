using System;
using UnityEngine;

namespace OldManAndTheSea.Survival
{
    public enum MemoryBuffType
    {
        None,
        ElCampeon,       // 冠军之力：30秒体力锁定不扣除，抓握力翻倍
        GreatDiMaggio,   // 迪马吉奥骨刺：手部伤情惩罚免疫，张力控制精度提升50%
        LionsOnBeach     // 幼狮之梦：神志清醒度回满，清除所有黑视
    }

    /// <summary>
    /// 《老人与海》老人真实生理与负面状态系统
    /// 依据 docs/03_GAME_SYSTEMS_AND_MECHANICS.md 1.2 节实现
    /// </summary>
    public class OldManSurvivalStats : MonoBehaviour
    {
        [Header("Four Core Metrics (0 - 100)")]
        [SerializeField] private float stamina = 100f;
        [SerializeField] private float hydration = 100f;
        [SerializeField] private float handHealth = 100f;
        [SerializeField] private float clarity = 100f;

        [Header("Hydration Bottle")]
        [Tooltip("船底棕色玻璃水瓶中的淡水（全游戏仅限 5 口）")]
        [SerializeField] private int waterSipsRemaining = 5;

        [Header("Left Hand Cramp (Calambre)")]
        [SerializeField] private bool isLeftHandCramped = false;
        [Range(0f, 100f)]
        [SerializeField] private float crampSeverity = 0f;

        [Header("Memory Buff")]
        [SerializeField] private MemoryBuffType activeBuff = MemoryBuffType.None;
        [SerializeField] private float buffTimer = 0f;

        // Events
        public event Action<float, float, float, float> OnStatsChanged;
        public event Action OnLeftHandCrampTriggered;
        public event Action OnLeftHandCrampRecovered;
        public event Action<MemoryBuffType> OnMemoryBuffActivated;
        public event Action OnExhaustionCollapse;

        public float Stamina => stamina;
        public float Hydration => hydration;
        public float HandHealth => handHealth;
        public float Clarity => clarity;
        public int WaterSipsRemaining => waterSipsRemaining;
        public bool IsLeftHandCramped => isLeftHandCramped;
        public MemoryBuffType ActiveBuff => activeBuff;

        private void Update()
        {
            float dt = Time.deltaTime;

            // 持续自然代谢衰减
            float hydrationDecayRate = 3f / 60f; // 烈日下每分钟衰减3点
            hydration = Mathf.Max(0f, hydration - hydrationDecayRate * dt);

            // 神志缓慢损耗
            clarity = Mathf.Max(0f, clarity - 0.05f * dt);

            // 活跃 Buff 倒计时
            if (activeBuff != MemoryBuffType.None)
            {
                buffTimer -= dt;
                if (buffTimer <= 0f)
                {
                    activeBuff = MemoryBuffType.None;
                    OnMemoryBuffActivated?.Invoke(MemoryBuffType.None);
                }
            }

            // 痉挛恢复进度
            if (isLeftHandCramped)
            {
                if (crampSeverity <= 0f)
                {
                    isLeftHandCramped = false;
                    OnLeftHandCrampRecovered?.Invoke();
                }
            }

            OnStatsChanged?.Invoke(stamina, hydration, handHealth, clarity);
        }

        public void ConsumeStamina(float amount)
        {
            if (activeBuff == MemoryBuffType.ElCampeon) return; // 冠军之力锁定体力

            stamina = Mathf.Max(0f, stamina - amount);
            if (stamina <= 0f)
            {
                OnExhaustionCollapse?.Invoke();
            }
        }

        public void RestoreStamina(float amount)
        {
            stamina = Mathf.Min(100f, stamina + amount);
        }

        public void TakeHandDamage(float damage)
        {
            if (activeBuff == MemoryBuffType.GreatDiMaggio) return; // 迪马吉奥免疫骨刺剧痛

            handHealth = Mathf.Max(0f, handHealth - damage);

            // 高负荷拉扯可能诱发左手抽筋
            if (!isLeftHandCramped && UnityEngine.Random.value < 0.15f)
            {
                TriggerCramp();
            }
        }

        public void TriggerCramp()
        {
            isLeftHandCramped = true;
            crampSeverity = 100f;
            OnLeftHandCrampTriggered?.Invoke();
        }

        /// <summary>
        /// 搓揉与借日光回暖化解痉挛
        /// </summary>
        public void MassageCramp(float progress)
        {
            if (!isLeftHandCramped) return;
            crampSeverity = Mathf.Max(0f, crampSeverity - progress);
        }

        /// <summary>
        /// 喝一口宝贵的淡水（限5口）
        /// </summary>
        public bool DrinkWater()
        {
            if (waterSipsRemaining <= 0) return false;

            waterSipsRemaining--;
            hydration = Mathf.Min(100f, hydration + 25f);
            stamina = Mathf.Min(100f, stamina + 5f);
            return true;
        }

        /// <summary>
        /// 咀嚼生金枪鱼/鲯鳅片
        /// </summary>
        public void EatRawFish()
        {
            stamina = Mathf.Min(100f, stamina + 20f);
            // 盐分与蛋白加速缓解痉挛
            if (isLeftHandCramped)
            {
                crampSeverity = Mathf.Max(0f, crampSeverity - 35f);
            }
        }

        /// <summary>
        /// 双手浸入清凉海水中盐疗止血
        /// </summary>
        public void SaltWaterSoak()
        {
            handHealth = Mathf.Min(100f, handHealth + 15f);
        }

        /// <summary>
        /// 激活记忆殿堂 Buff
        /// </summary>
        public void ActivateMemoryBuff(MemoryBuffType buff)
        {
            activeBuff = buff;
            switch (buff)
            {
                case MemoryBuffType.ElCampeon:
                    buffTimer = 30f;
                    break;
                case MemoryBuffType.GreatDiMaggio:
                    buffTimer = 45f;
                    break;
                case MemoryBuffType.LionsOnBeach:
                    clarity = 100f;
                    buffTimer = 60f;
                    break;
            }
            OnMemoryBuffActivated?.Invoke(activeBuff);
        }
    }
}
