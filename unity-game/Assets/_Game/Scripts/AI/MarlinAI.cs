using System;
using UnityEngine;

namespace OldManAndTheSea.AI
{
    public enum MarlinState
    {
        Cruising,       // 深海自由巡游
        Biting,         // 试探与重口咬钩
        DeepTowing,     // 无声深海拖曳小船
        Breaching,      // 破水腾空飞跃（原著经典瞬间）
        Circling,       // 疲竭生死大回旋
        Defeated        // 被处决绑舷
    }

    /// <summary>
    /// 18英尺大西洋巨型大马林鱼 AI
    /// 依据 docs/01_PROMPT_LIBRARY.md 与 docs/03_GAME_SYSTEMS_AND_MECHANICS.md 实现
    /// </summary>
    public class MarlinAI : MonoBehaviour
    {
        [Header("Marlin Specs")]
        [SerializeField] private float length = 18f; // 英尺
        [SerializeField] private float currentStamina = 1000f;
        [SerializeField] private float maxStamina = 1000f;
        [SerializeField] private float swimSpeed = 12f;

        [Header("State")]
        [SerializeField] private MarlinState currentState = MarlinState.Cruising;
        [SerializeField] private Transform targetBoat;

        [Header("Breaching Mechanics")]
        [SerializeField] private float breachChance = 0.08f;
        [SerializeField] private float breachDuration = 2.5f;
        private float breachTimer = 0f;

        // Events
        public event Action<MarlinState> OnMarlinStateChanged;
        public event Action OnMarlinBreached;
        public event Action OnMarlinExhausted;

        public MarlinState CurrentState => currentState;
        public float StaminaRatio => currentStamina / maxStamina;
        public float SwimSpeed => swimSpeed;

        public void Initialize(Transform boat)
        {
            targetBoat = boat;
        }

        private void Update()
        {
            float dt = Time.deltaTime;

            switch (currentState)
            {
                case MarlinState.DeepTowing:
                    UpdateDeepTowing(dt);
                    break;

                case MarlinState.Breaching:
                    UpdateBreaching(dt);
                    break;

                case MarlinState.Circling:
                    UpdateCircling(dt);
                    break;
            }
        }

        public void StartBiting()
        {
            currentState = MarlinState.Biting;
            OnMarlinStateChanged?.Invoke(currentState);
        }

        public void HookAndTow()
        {
            currentState = MarlinState.DeepTowing;
            OnMarlinStateChanged?.Invoke(currentState);
        }

        private void UpdateDeepTowing(float dt)
        {
            // 向西北方向（远离陆地深渊）直线拖曳
            Vector3 towDirection = (transform.forward + Vector3.down * 0.2f).normalized;
            transform.position += towDirection * swimSpeed * dt;

            // 偶尔触发破水腾空跳跃
            if (UnityEngine.Random.value < breachChance * dt)
            {
                TriggerBreach();
                return;
            }

            // 体力自然消耗
            currentStamina -= 5f * dt;
            if (currentStamina <= maxStamina * 0.25f)
            {
                // 进入大回旋疲惫阶段
                currentState = MarlinState.Circling;
                OnMarlinStateChanged?.Invoke(currentState);
                OnMarlinExhausted?.Invoke();
            }
        }

        private void TriggerBreach()
        {
            currentState = MarlinState.Breaching;
            breachTimer = breachDuration;
            OnMarlinStateChanged?.Invoke(currentState);
            OnMarlinBreached?.Invoke();
        }

        private void UpdateBreaching(float dt)
        {
            breachTimer -= dt;
            // 抛物线跳跃视觉表现
            transform.position += (transform.forward + Vector3.up * 1.5f) * (swimSpeed * 1.4f) * dt;

            if (breachTimer <= 0f)
            {
                // 重回水下拖拽
                currentState = MarlinState.DeepTowing;
                OnMarlinStateChanged?.Invoke(currentState);
            }
        }

        private void UpdateCircling(float dt)
        {
            if (targetBoat == null) return;

            // 绕着小船半径 20米 旋转
            transform.RotateAround(targetBoat.position, Vector3.up, 15f * dt);
            transform.LookAt(transform.position + transform.forward);

            // 持续体力见底
            currentStamina = Mathf.Max(0f, currentStamina - 8f * dt);
        }

        public void TakeHarpoonHit()
        {
            currentState = MarlinState.Defeated;
            OnMarlinStateChanged?.Invoke(currentState);
        }
    }
}
