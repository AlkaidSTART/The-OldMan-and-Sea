using UnityEngine;
using OldManAndTheSea.Boat;
using OldManAndTheSea.Fishing;
using OldManAndTheSea.Survival;
using OldManAndTheSea.AI;
using OldManAndTheSea.Dialogue;
using OldManAndTheSea.UI;

namespace OldManAndTheSea.Core
{
    /// <summary>
    /// 游戏一键自启动与组装引导器 (Game Bootstrap)
    /// 挂载在主摄像机或全局游戏根对象上，统筹全主线剧情状态机与输入流
    /// </summary>
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Scene References")]
        [SerializeField] private BoatController boat;
        [SerializeField] private MarlinAI marlin;
        [SerializeField] private FishingLinePhysics linePhysics;
        [SerializeField] private OldManSurvivalStats stats;
        [SerializeField] private WeaponDegradationSystem weaponSystem;

        private void Awake()
        {
            EnsureCoreComponents();
        }

        private void Update()
        {
            var flow = StoryFlowManager.Instance;
            if (flow == null) return;

            // 如果当前在对话界面，禁止航行输入
            if (flow.IsDialogueActive) return;

            float dt = Time.deltaTime;

            if (flow.CurrentPhase == StoryPhase.Act1_OpenSeaExploration)
            {
                // 1. 开放海域自由航行阶段：划双桨推进与小艇转向
                HandleOpenSeaNavigation(dt);
            }
            else if (flow.CurrentPhase == StoryPhase.Act2_MarlinTowingBattle)
            {
                // 2. 深海大鱼拉锯搏杀阶段
                HandleMarlinStruggle(dt);
            }
        }

        private void HandleOpenSeaNavigation(float dt)
        {
            var flow = StoryFlowManager.Instance;

            // 自由划桨与转向
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");
            if (boat != null)
            {
                boat.ApplyRowInput(moveInput, turnInput);
            }

            // 按键 1~4 自由布饵
            if (Input.GetKeyDown(KeyCode.Alpha1)) flow.bait40Fathoms = !flow.bait40Fathoms;
            if (Input.GetKeyDown(KeyCode.Alpha2)) flow.bait75Fathoms = !flow.bait75Fathoms;
            if (Input.GetKeyDown(KeyCode.Alpha3)) flow.bait100Fathoms = !flow.bait100Fathoms;
            if (Input.GetKeyDown(KeyCode.Alpha4)) flow.bait125Fathoms = !flow.bait125Fathoms;

            // 按空格键，或划入大井区 (Z > 120米) 且布有深线时，触发大鱼咬钩
            if (Input.GetKeyDown(KeyCode.Space) || (boat != null && boat.transform.position.z > 140f && flow.bait100Fathoms))
            {
                flow.TriggerMarlinStrike();
            }
        }

        private void HandleMarlinStruggle(float dt)
        {
            // 小船划桨与受巨鱼拖曳外力
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");
            if (boat != null)
            {
                boat.ApplyRowInput(moveInput, turnInput);

                if (marlin != null && linePhysics != null)
                {
                    Vector3 pullDir = (marlin.transform.position - boat.transform.position).normalized;
                    float tensionForce = linePhysics.CurrentTension * 0.4f;
                    boat.SetExternalTowingForce(pullDir * tensionForce);
                }
            }

            // 钓线阻尼与收线（按住空格）
            float dampingInput = Input.GetKey(KeyCode.Space) ? 1.0f : 0.05f;
            if (linePhysics != null && marlin != null && boat != null)
            {
                float fishSpd = marlin.CurrentState == MarlinState.DeepTowing ? marlin.SwimSpeed : 0f;
                float boatSpd = boat.Rb != null ? boat.Rb.linearVelocity.magnitude : 0f;

                linePhysics.UpdatePhysics(fishSpd, boatSpd, dampingInput, dt);

                if (Input.GetKey(KeyCode.Space) && stats != null)
                {
                    stats.ConsumeStamina(8f * dt);
                    if (linePhysics.CurrentTension > 85f)
                    {
                        stats.TakeHandDamage(5f * dt);
                    }
                }
            }

            // 生存快捷键
            if (stats != null)
            {
                if (Input.GetKeyDown(KeyCode.E)) stats.DrinkWater();
                if (Input.GetKey(KeyCode.Q)) stats.MassageCramp(40f * dt);
            }
        }

        private void EnsureCoreComponents()
        {
            if (DialogueManager.Instance == null) gameObject.AddComponent<DialogueManager>();
            if (stats == null) stats = gameObject.AddComponent<OldManSurvivalStats>();
            if (weaponSystem == null) weaponSystem = gameObject.AddComponent<WeaponDegradationSystem>();
        }
    }
}
