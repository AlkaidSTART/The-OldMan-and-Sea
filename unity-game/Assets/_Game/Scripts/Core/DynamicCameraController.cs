using UnityEngine;

namespace OldManAndTheSea.Core
{
    /// <summary>
    /// 动态多阶段第三人称摄像机
    /// 在陆地漫步与海面航行间平滑切换跟随目标与视角构图
    /// </summary>
    public class DynamicCameraController : MonoBehaviour
    {
        [Header("Follow Targets")]
        [SerializeField] private Transform landTarget;  // 圣地亚哥步行
        [SerializeField] private Transform boatTarget;  // 16英尺小船

        [Header("Offsets")]
        [SerializeField] private Vector3 landOffset = new Vector3(0, 3.2f, -5.5f);
        [SerializeField] private Vector3 boatOffset = new Vector3(0, 5.5f, -8.5f);

        [Header("Smoothing")]
        [SerializeField] private float smoothSpeed = 5.0f;

        private void LateUpdate()
        {
            var flow = StoryFlowManager.Instance;
            bool isSailing = flow != null && (flow.CurrentPhase == StoryPhase.Act1_OpenSeaExploration || flow.CurrentPhase == StoryPhase.Act2_MarlinTowingBattle);

            Transform currentTarget = isSailing ? boatTarget : landTarget;
            Vector3 targetOffset = isSailing ? boatOffset : landOffset;

            if (currentTarget == null) return;

            Vector3 desiredPosition = currentTarget.position + targetOffset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smoothSpeed);

            // 观察点稍微偏向目标上方
            Vector3 lookTarget = currentTarget.position + Vector3.up * 1.2f;
            transform.LookAt(lookTarget);
        }

        public void SetTargets(Transform land, Transform boat)
        {
            landTarget = land;
            boatTarget = boat;
        }
    }
}
