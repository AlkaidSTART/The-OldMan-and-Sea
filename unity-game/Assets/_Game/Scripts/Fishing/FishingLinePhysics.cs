using System;
using UnityEngine;

namespace OldManAndTheSea.Fishing
{
    public enum TensionState
    {
        Slack,       // < 30% 脱钩危险区
        Optimal,     // 30% - 75% 安全消耗区
        Strain,      // 75% - 90% 高负荷僵持区
        SnapDanger   // 90% - 100% 断线临界区
    }

    /// <summary>
    /// 《老人与海》深海钓线张力力学系统
    /// 依据 docs/03_GAME_SYSTEMS_AND_MECHANICS.md 1.1 节实现
    /// </summary>
    public class FishingLinePhysics : MonoBehaviour
    {
        [Header("Tension Tuning")]
        [Range(0f, 100f)]
        [SerializeField] private float currentTension = 45f;
        [SerializeField] private float alpha = 1.2f; // 相对速度影响系数
        [SerializeField] private float beta = 25f;   // 玩家刹车阻尼系数

        [Header("Line Capacity (Fathoms)")]
        [Tooltip("总储备线长（寻），原著总计约380寻（约695米）")]
        [SerializeField] private float totalLineCapacity = 380f;
        [SerializeField] private float remainingLine = 380f;
        [SerializeField] private float currentLineOut = 100f; // 初始布饵深度 100寻

        [Header("Danger Timers")]
        [SerializeField] private float maxSlackTime = 3.0f;
        private float currentSlackTimer = 0f;

        [SerializeField] private float maxSnapDangerTime = 1.8f;
        private float currentSnapTimer = 0f;

        [Header("Line Visuals")]
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private Transform boatAnchorPoint;
        [SerializeField] private Transform targetFishPoint;

        // Events
        public event Action<TensionState> OnTensionStateChanged;
        public event Action<float> OnTensionUpdated;
        public event Action OnLineSnapped;
        public event Action OnFishUnhooked;
        public event Action OnLineExhausted;

        private TensionState currentState = TensionState.Optimal;

        public float CurrentTension => currentTension;
        public TensionState CurrentState => currentState;
        public float RemainingLine => remainingLine;
        public float CurrentLineOut => currentLineOut;

        private void Awake()
        {
            if (lineRenderer == null)
            {
                lineRenderer = GetComponent<LineRenderer>();
            }
        }

        public void Initialize(Transform boatAnchor, Transform fishTarget)
        {
            boatAnchorPoint = boatAnchor;
            targetFishPoint = fishTarget;
            remainingLine = totalLineCapacity - currentLineOut;
        }

        public void UpdatePhysics(float fishSpeed, float boatSpeed, float playerDampingInput, float deltaTime)
        {
            // T = alpha * (V_fish - V_skiff) + beta * D
            float relativeVelocity = Mathf.Max(0f, fishSpeed - boatSpeed);
            float targetTension = (alpha * relativeVelocity * 10f) + (beta * playerDampingInput);

            // 平滑插值计算张力
            currentTension = Mathf.MoveTowards(currentTension, Mathf.Clamp(targetTension, 0f, 100f), deltaTime * 40f);

            // 更新线长与出线
            if (playerDampingInput < 0.2f && relativeVelocity > 0.1f)
            {
                // 自由出线
                float lineGaveOut = relativeVelocity * deltaTime * 2f;
                currentLineOut += lineGaveOut;
                remainingLine -= lineGaveOut;

                if (remainingLine <= 0f)
                {
                    remainingLine = 0f;
                    OnLineExhausted?.Invoke();
                    return;
                }
            }
            else if (playerDampingInput > 0.8f && currentTension < 75f)
            {
                // 玩家用力收线 (Reeling in)
                float reeledIn = 1.5f * deltaTime;
                if (currentLineOut > 10f)
                {
                    currentLineOut -= reeledIn;
                    remainingLine += reeledIn;
                }
            }

            EvaluateState(deltaTime);
            UpdateLineRenderer();

            OnTensionUpdated?.Invoke(currentTension);
        }

        private void EvaluateState(float deltaTime)
        {
            TensionState newState;

            if (currentTension < 30f)
            {
                newState = TensionState.Slack;
                currentSlackTimer += deltaTime;
                if (currentSlackTimer >= maxSlackTime)
                {
                    OnFishUnhooked?.Invoke();
                    return;
                }
            }
            else
            {
                currentSlackTimer = 0f;

                if (currentTension < 75f)
                {
                    newState = TensionState.Optimal;
                }
                else if (currentTension < 90f)
                {
                    newState = TensionState.Strain;
                }
                else
                {
                    newState = TensionState.SnapDanger;
                    currentSnapTimer += deltaTime;
                    if (currentSnapTimer >= maxSnapDangerTime)
                    {
                        OnLineSnapped?.Invoke();
                        return;
                    }
                }
            }

            if (newState != TensionState.SnapDanger)
            {
                currentSnapTimer = 0f;
            }

            if (newState != currentState)
            {
                currentState = newState;
                OnTensionStateChanged?.Invoke(currentState);
            }
        }

        private void UpdateLineRenderer()
        {
            if (lineRenderer == null || boatAnchorPoint == null || targetFishPoint == null) return;

            lineRenderer.positionCount = 3;
            lineRenderer.SetPosition(0, boatAnchorPoint.position);

            // 依据张力计算弧垂（下垂度）
            Vector3 midPoint = (boatAnchorPoint.position + targetFishPoint.position) * 0.5f;
            float sagAmount = Mathf.Lerp(3f, 0.2f, currentTension / 100f);
            midPoint.y -= sagAmount;

            lineRenderer.SetPosition(1, midPoint);
            lineRenderer.SetPosition(2, targetFishPoint.position);
        }
    }
}
