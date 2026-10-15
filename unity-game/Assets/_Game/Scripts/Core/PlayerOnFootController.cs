using UnityEngine;

namespace OldManAndTheSea.Core
{
    /// <summary>
    /// 老人圣地亚哥陆地漫步与剧情交互控制器
    /// 支持 WASD 平滑行走、朝向旋转与地标交互触发
    /// </summary>
    public class PlayerOnFootController : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float moveSpeed = 4.2f;
        [SerializeField] private float turnSpeed = 12f;

        [Header("Interaction Targets")]
        [SerializeField] private Transform terraceTarget;
        [SerializeField] private Transform shackTarget;
        [SerializeField] private Transform skiffTarget;

        [Header("State")]
        [SerializeField] private float interactRadius = 3.8f;

        private CharacterController charController;
        private string promptText = "";
        private bool isNearInteractable = false;

        public string PromptText => promptText;
        public bool IsNearInteractable => isNearInteractable;

        private void Awake()
        {
            charController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            var flow = StoryFlowManager.Instance;

            // 如果正在对话中，按 空格/E/F 推进对话
            if (flow != null && flow.IsDialogueActive)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F))
                {
                    flow.AdvanceDialogue();
                }
                return; // 对话中禁止角色乱跑
            }

            // 1. 玩家移动逻辑
            HandleMovement();

            // 2. 地标交互检测
            CheckInteractionProximity();

            // 3. 响应交互按键
            if (isNearInteractable && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.F)))
            {
                ExecuteInteraction();
            }
        }

        private void HandleMovement()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");

            Vector3 moveDir = new Vector3(h, 0, v);
            if (moveDir.magnitude > 1f) moveDir.Normalize();

            if (moveDir.magnitude > 0.05f)
            {
                // 平滑旋转朝向
                Quaternion targetRot = Quaternion.LookRotation(moveDir);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);

                // 位移
                Vector3 moveDelta = moveDir * moveSpeed * Time.deltaTime;
                if (charController != null && charController.enabled)
                {
                    charController.Move(moveDelta + Vector3.down * 9.8f * Time.deltaTime);
                }
                else
                {
                    transform.position += moveDelta;
                }
            }
        }

        private void CheckInteractionProximity()
        {
            var flow = StoryFlowManager.Instance;
            if (flow == null) return;

            isNearInteractable = false;
            promptText = "";

            switch (flow.CurrentPhase)
            {
                case StoryPhase.Prologue_TerraceBeer:
                    if (terraceTarget != null && Vector3.Distance(transform.position, terraceTarget.position) <= interactRadius)
                    {
                        isNearInteractable = true;
                        promptText = "🍺 露台饭店 [按 E/F 键坐下与马诺林喝啤酒]";
                    }
                    break;

                case StoryPhase.Prologue_SantiagoShack:
                    if (shackTarget != null && Vector3.Distance(transform.position, shackTarget.position) <= interactRadius)
                    {
                        isNearInteractable = true;
                        promptText = "🏠 圣地亚哥陋室 [按 E/F 键进屋喝苦咖啡与整理渔具]";
                    }
                    break;

                case StoryPhase.Prologue_BoardSkiff:
                    if (skiffTarget != null && Vector3.Distance(transform.position, skiffTarget.position) <= interactRadius)
                    {
                        isNearInteractable = true;
                        promptText = "⛵ 16英尺小木船 [按 E/F 键登船启航，驶向深海]";
                    }
                    break;
            }
        }

        public void ExecuteInteraction()
        {
            var flow = StoryFlowManager.Instance;
            if (flow == null) return;

            switch (flow.CurrentPhase)
            {
                case StoryPhase.Prologue_TerraceBeer:
                    flow.TriggerTerraceBeerDialogue();
                    break;
                case StoryPhase.Prologue_SantiagoShack:
                    flow.TriggerShackDialogue();
                    break;
                case StoryPhase.Prologue_BoardSkiff:
                    flow.TriggerBoardSkiffDialogue();
                    break;
            }
        }

        public void SetTargets(Transform terrace, Transform shack, Transform skiff)
        {
            terraceTarget = terrace;
            shackTarget = shack;
            skiffTarget = skiff;
        }
    }
}
