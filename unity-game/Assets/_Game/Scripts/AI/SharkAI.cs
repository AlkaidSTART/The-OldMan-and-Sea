using System;
using UnityEngine;

namespace OldManAndTheSea.AI
{
    public enum SharkType
    {
        Mako,       // 灰鲭鲨：深青蓝色、游速极快、直线猎杀
        Galano      // 铲头角鲨：成群、食腐、绕船偷袭
    }

    /// <summary>
    /// 鲨鱼攻击 AI
    /// 依据 docs/03_GAME_SYSTEMS_AND_MECHANICS.md 3.1 节实现
    /// </summary>
    public class SharkAI : MonoBehaviour
    {
        [Header("Shark Spec")]
        [SerializeField] private SharkType sharkType = SharkType.Mako;
        [SerializeField] private float health = 100f;
        [SerializeField] private float speed = 14f;
        [SerializeField] private float biteDamage = 25f;

        [Header("Target")]
        [SerializeField] private Transform boatHullTarget;
        private bool isAttacking = false;

        public event Action<SharkAI> OnSharkDefeated;
        public event Action OnBiteLanded;

        public SharkType Type => sharkType;
        public float Health => health;

        public void Initialize(SharkType type, Transform target)
        {
            sharkType = type;
            boatHullTarget = target;
            if (sharkType == SharkType.Mako)
            {
                speed = 18f;
                health = 150f;
            }
            else
            {
                speed = 10f;
                health = 75f;
            }
            isAttacking = true;
        }

        private void Update()
        {
            if (!isAttacking || boatHullTarget == null) return;

            // 逼近小船船舷马林鱼肉
            Vector3 targetDir = (boatHullTarget.position - transform.position).normalized;
            transform.position += targetDir * speed * Time.deltaTime;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(targetDir), Time.deltaTime * 5f);

            // 距离小于 2米 进行撕咬
            if (Vector3.Distance(transform.position, boatHullTarget.position) < 2.0f)
            {
                OnBiteLanded?.Invoke();
                // 咬完短暂迂回
                transform.position -= transform.forward * 4f;
            }
        }

        public void TakeDamage(float damage)
        {
            health -= damage;
            if (health <= 0)
            {
                isAttacking = false;
                OnSharkDefeated?.Invoke(this);
                Destroy(gameObject, 2f);
            }
        }
    }
}
