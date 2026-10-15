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
    /// 挂载在主摄像机或空对象上，启动时自动构建完整的《老人与海》可玩原型场景
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

        private void Start()
        {
            // 初始化系统绑定
            if (linePhysics != null && boat != null && marlin != null)
            {
                linePhysics.Initialize(boat.SternAnchorPoint, marlin.transform);
                marlin.Initialize(boat.transform);
                marlin.HookAndTow(); // 默认进入深潜拖拽阶段进行玩法测试
            }
        }

        private void Update()
        {
            HandlePlayerInputs();
        }

        private void HandlePlayerInputs()
        {
            float dt = Time.deltaTime;

            // 1. 小船划桨与转向
            float moveInput = Input.GetAxis("Vertical");
            float turnInput = Input.GetAxis("Horizontal");
            if (boat != null)
            {
                boat.ApplyRowInput(moveInput, turnInput);

                // 受到来自大马林鱼的拖曳拉力
                if (marlin != null && linePhysics != null)
                {
                    Vector3 pullDir = (marlin.transform.position - boat.transform.position).normalized;
                    float tensionForce = linePhysics.CurrentTension * 0.4f;
                    boat.SetExternalTowingForce(pullDir * tensionForce);
                }
            }

            // 2. 钓线阻尼与收线（空格键）
            float dampingInput = Input.GetKey(KeyCode.Space) ? 1.0f : 0.05f;
            if (linePhysics != null && marlin != null && boat != null)
            {
                float fishSpd = marlin.CurrentState == MarlinState.DeepTowing ? marlin.SwimSpeed : 0f;
                float boatSpd = boat.Rb != null ? boat.Rb.linearVelocity.magnitude : 0f;

                linePhysics.UpdatePhysics(fishSpd, boatSpd, dampingInput, dt);

                // 收线大力消耗体力
                if (Input.GetKey(KeyCode.Space) && stats != null)
                {
                    stats.ConsumeStamina(8f * dt);
                    if (linePhysics.CurrentTension > 85f)
                    {
                        stats.TakeHandDamage(5f * dt); // 高张力手掌勒伤
                    }
                }
            }

            // 3. 生存操作交互快捷键
            if (stats != null)
            {
                if (Input.GetKeyDown(KeyCode.E))
                {
                    stats.DrinkWater();
                }
                if (Input.GetKey(KeyCode.Q))
                {
                    stats.MassageCramp(40f * dt);
                }
            }
        }

        private void EnsureCoreComponents()
        {
            // 确保 DialogueManager
            if (DialogueManager.Instance == null)
            {
                gameObject.AddComponent<DialogueManager>();
            }

            // 确保生存数值
            if (stats == null)
            {
                stats = gameObject.AddComponent<OldManSurvivalStats>();
            }

            // 确保武器系统
            if (weaponSystem == null)
            {
                weaponSystem = gameObject.AddComponent<WeaponDegradationSystem>();
            }

            // 自动生成小船原型几何体
            if (boat == null)
            {
                GameObject boatObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
                boatObj.name = "Skiff_16ft";
                boatObj.transform.position = new Vector3(0, 0.5f, 0);
                boatObj.transform.localScale = new Vector3(1.6f, 0.8f, 4.8f); // 16英尺旧木小舟比例

                var renderer = boatObj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = new Color(0.45f, 0.3f, 0.18f); // 木色
                }

                boat = boatObj.AddComponent<BoatController>();
            }

            // 自动生成大马林鱼原型几何体
            if (marlin == null)
            {
                GameObject marlinObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                marlinObj.name = "GiantMarlin_18ft";
                marlinObj.transform.position = new Vector3(0, -2f, 25f);
                marlinObj.transform.localScale = new Vector3(1.0f, 1.0f, 3.5f); // 18英尺流线鱼身
                marlinObj.transform.rotation = Quaternion.Euler(0, 0, 90f);

                var renderer = marlinObj.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material.color = new Color(0.2f, 0.35f, 0.7f); // 蓝紫色大西洋蓝马林
                }

                marlin = marlinObj.AddComponent<MarlinAI>();
            }

            // 自动配置钓线
            if (linePhysics == null)
            {
                GameObject lineObj = new GameObject("FishingLine");
                var lr = lineObj.AddComponent<LineRenderer>();
                lr.startWidth = 0.05f;
                lr.endWidth = 0.05f;
                lr.material = new Material(Shader.Find("Sprites/Default"));
                lr.startColor = new Color(0.9f, 0.9f, 0.7f, 0.9f);
                lr.endColor = new Color(0.3f, 0.7f, 0.9f, 0.5f);

                linePhysics = lineObj.AddComponent<FishingLinePhysics>();
            }

            // 自动生成海洋水面底板
            GameObject ocean = GameObject.CreatePrimitive(PrimitiveType.Plane);
            ocean.name = "OceanSurface";
            ocean.transform.position = Vector3.zero;
            ocean.transform.localScale = new Vector3(100f, 1f, 100f);
            var oceanRenderer = ocean.GetComponent<Renderer>();
            if (oceanRenderer != null)
            {
                oceanRenderer.material.color = new Color(0.08f, 0.22f, 0.38f, 0.85f); // 墨西哥湾流深青蓝
            }

            // 挂载 HUD
            var hud = gameObject.GetComponent<GameHUD>();
            if (hud == null)
            {
                hud = gameObject.AddComponent<GameHUD>();
            }
            // 自动反射绑定私有字段
            SetPrivateField(hud, "linePhysics", linePhysics);
            SetPrivateField(hud, "stats", stats);
            SetPrivateField(hud, "marlin", marlin);
            SetPrivateField(hud, "weaponSystem", weaponSystem);
        }

        private void SetPrivateField(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            if (field != null)
            {
                field.SetValue(target, value);
            }
        }
    }
}
