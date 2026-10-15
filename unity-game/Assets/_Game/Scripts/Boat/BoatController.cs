using UnityEngine;

namespace OldManAndTheSea.Boat
{
    /// <summary>
    /// 16英尺旧木小艇控制器
    /// 依据 docs/04_OPEN_WORLD_EXPLORATION_DESIGN.md 实现
    /// 具有浮力阻尼、划桨推进与受钓线/巨鱼拖曳物理特性
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class BoatController : MonoBehaviour
    {
        [Header("Movement & Rowing")]
        [SerializeField] private float rowForce = 15f;
        [SerializeField] private float turnTorque = 8f;
        [SerializeField] private float waterDrag = 1.5f;
        [SerializeField] private float angularWaterDrag = 2.0f;

        [Header("Buoyancy Simulation")]
        [SerializeField] private float waterLevel = 0f;
        [SerializeField] private float floatStrength = 18f;
        [SerializeField] private Transform[] floatPoints;

        [Header("Fish Towing")]
        [SerializeField] private Transform sternAnchorPoint; // 船尾/船艏系线受力点
        [SerializeField] private Vector3 externalTowingForce = Vector3.zero;

        private Rigidbody rb;

        public Rigidbody Rb => rb;
        public Transform SternAnchorPoint => sternAnchorPoint != null ? sternAnchorPoint : transform;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            rb.linearDamping = waterDrag;
            rb.angularDamping = angularWaterDrag;
        }

        private void FixedUpdate()
        {
            ApplyBuoyancy();

            // 受巨鱼拖曳外力作用
            if (externalTowingForce != Vector3.zero)
            {
                rb.AddForceAtPosition(externalTowingForce, SternAnchorPoint.position);
            }
        }

        public void ApplyRowInput(float moveInput, float turnInput)
        {
            if (Mathf.Abs(moveInput) > 0.05f)
            {
                rb.AddForce(transform.forward * moveInput * rowForce, ForceMode.Acceleration);
            }

            if (Mathf.Abs(turnInput) > 0.05f)
            {
                rb.AddTorque(transform.up * turnInput * turnTorque, ForceMode.Acceleration);
            }
        }

        public void SetExternalTowingForce(Vector3 force)
        {
            externalTowingForce = force;
        }

        private void ApplyBuoyancy()
        {
            if (floatPoints == null || floatPoints.Length == 0)
            {
                // 默认单点简单浮力
                float diff = waterLevel - transform.position.y;
                if (diff > 0)
                {
                    rb.AddForce(Vector3.up * diff * floatStrength, ForceMode.Acceleration);
                }
                return;
            }

            foreach (var point in floatPoints)
            {
                if (point == null) continue;
                float diff = waterLevel - point.position.y;
                if (diff > 0)
                {
                    rb.AddForceAtPosition(Vector3.up * diff * (floatStrength / floatPoints.Length), point.position, ForceMode.Acceleration);
                }
            }
        }
    }
}
