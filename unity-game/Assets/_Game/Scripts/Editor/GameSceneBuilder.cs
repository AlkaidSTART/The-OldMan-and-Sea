using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using OldManAndTheSea.Core;
using OldManAndTheSea.Boat;
using OldManAndTheSea.Fishing;
using OldManAndTheSea.Survival;
using OldManAndTheSea.AI;
using OldManAndTheSea.Dialogue;
using OldManAndTheSea.UI;

namespace OldManAndTheSea.Editor
{
    public static class GameSceneBuilder
    {
        [MenuItem("Old Man & Sea/Generate Prototype Scene (一键生成实体化原型场景)")]
        public static void GenerateScene()
        {
            SetupURPPipeline();

            var newScene = EditorSceneManager.NewScene(NewSceneSetup.DefaultGameObjects, NewSceneMode.Single);

            // 1. 材质准备
            if (!AssetDatabase.IsValidFolder("Assets/_Game/Materials"))
            {
                AssetDatabase.CreateFolder("Assets/_Game", "Materials");
            }

            Shader litShader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");

            Material oceanMat = GetOrCreateMaterial("Assets/_Game/Materials/M_OceanSurface.mat", litShader, new Color(0.05f, 0.20f, 0.36f, 0.92f), 0.85f, 0.1f);
            Material woodMat = GetOrCreateMaterial("Assets/_Game/Materials/M_WoodSkiff.mat", litShader, new Color(0.40f, 0.24f, 0.14f), 0.2f, 0f);
            Material sandMat = GetOrCreateMaterial("Assets/_Game/Materials/M_SandBeach.mat", litShader, new Color(0.85f, 0.74f, 0.52f), 0.1f, 0f);
            Material thatchMat = GetOrCreateMaterial("Assets/_Game/Materials/M_ThatchedRoof.mat", litShader, new Color(0.65f, 0.52f, 0.30f), 0.15f, 0f);
            Material beerMat = GetOrCreateMaterial("Assets/_Game/Materials/M_BeerBottle.mat", litShader, new Color(0.12f, 0.52f, 0.25f, 0.9f), 0.9f, 0.2f);
            Material marlinMat = GetOrCreateMaterial("Assets/_Game/Materials/M_BlueMarlin.mat", litShader, new Color(0.15f, 0.32f, 0.72f), 0.7f, 0.1f);
            Material oldManMat = GetOrCreateMaterial("Assets/_Game/Materials/M_Santiago.mat", litShader, new Color(0.35f, 0.42f, 0.55f), 0.2f, 0f);
            Material boyMat = GetOrCreateMaterial("Assets/_Game/Materials/M_ManolinBoy.mat", litShader, new Color(0.80f, 0.65f, 0.45f), 0.2f, 0f);

            // 2. 地形环境构建：沙滩与海洋
            // 沙滩与陆地（Z轴在 -50 到 0 之间）
            GameObject beachObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            beachObj.name = "Cojimar_Beach_Land";
            beachObj.transform.position = new Vector3(0, 0.05f, -25f);
            beachObj.transform.localScale = new Vector3(6f, 1f, 5f);
            beachObj.GetComponent<Renderer>().sharedMaterial = sandMat;

            // 墨西哥湾流广阔海洋水面（Z轴延伸到远海 +600 米）
            GameObject oceanObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            oceanObj.name = "OceanSurface";
            oceanObj.transform.position = new Vector3(0, 0f, 250f);
            oceanObj.transform.localScale = new Vector3(100f, 1f, 60f);
            oceanObj.GetComponent<Renderer>().sharedMaterial = oceanMat;

            // 木质栈桥码头
            GameObject pierObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            pierObj.name = "FishingPier";
            pierObj.transform.position = new Vector3(0, 0.25f, -6f);
            pierObj.transform.localScale = new Vector3(2.4f, 0.4f, 14f);
            pierObj.GetComponent<Renderer>().sharedMaterial = woodMat;

            // 3. 核心建筑一：【露台饭店 The Terrace】(黄昏小酌与听流言)
            GameObject terraceRoot = new GameObject("TheTerrace_Restaurant");
            terraceRoot.transform.position = new Vector3(-14f, 0f, -22f);

            // 露台木质地台
            GameObject terraceFloor = GameObject.CreatePrimitive(PrimitiveType.Cube);
            terraceFloor.name = "TerraceFloor";
            terraceFloor.transform.parent = terraceRoot.transform;
            terraceFloor.transform.localPosition = new Vector3(0, 0.2f, 0);
            terraceFloor.transform.localScale = new Vector3(8f, 0.3f, 8f);
            terraceFloor.GetComponent<Renderer>().sharedMaterial = woodMat;

            // 露台遮阳棚顶
            GameObject terraceRoof = GameObject.CreatePrimitive(PrimitiveType.Cube);
            terraceRoof.name = "TerraceAwning";
            terraceRoof.transform.parent = terraceRoot.transform;
            terraceRoof.transform.localPosition = new Vector3(0, 3.4f, 0);
            terraceRoof.transform.localScale = new Vector3(8.5f, 0.2f, 8.5f);
            terraceRoof.GetComponent<Renderer>().sharedMaterial = thatchMat;

            // 露天酒桌与长凳
            GameObject table = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            table.name = "BarTable";
            table.transform.parent = terraceRoot.transform;
            table.transform.localPosition = new Vector3(0, 0.9f, 0);
            table.transform.localScale = new Vector3(1.8f, 0.45f, 1.8f);
            table.GetComponent<Renderer>().sharedMaterial = woodMat;

            // 哈瓦那啤酒瓶道具
            GameObject beerBottle = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            beerBottle.name = "Hatuey_BeerBottle";
            beerBottle.transform.parent = terraceRoot.transform;
            beerBottle.transform.localPosition = new Vector3(0.2f, 1.55f, 0.1f);
            beerBottle.transform.localScale = new Vector3(0.18f, 0.35f, 0.18f);
            beerBottle.GetComponent<Renderer>().sharedMaterial = beerMat;

            // 男孩马诺林 NPC 站在桌旁
            GameObject manolinNpc = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            manolinNpc.name = "Manolin_TheBoy_NPC";
            manolinNpc.transform.parent = terraceRoot.transform;
            manolinNpc.transform.localPosition = new Vector3(0.9f, 1.3f, 0.8f);
            manolinNpc.transform.localScale = new Vector3(0.65f, 0.85f, 0.65f);
            manolinNpc.GetComponent<Renderer>().sharedMaterial = boyMat;

            // 4. 核心建筑二：【圣地亚哥的木棚屋陋室 Shack】(检视家当与清晨咖啡)
            GameObject shackRoot = new GameObject("Santiago_Shack");
            shackRoot.transform.position = new Vector3(14f, 0f, -22f);

            // 木棚墙壁与地面
            GameObject shackBox = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shackBox.name = "ShackWalls";
            shackBox.transform.parent = shackRoot.transform;
            shackBox.transform.localPosition = new Vector3(0, 1.6f, 0);
            shackBox.transform.localScale = new Vector3(6f, 3.2f, 6f);
            shackBox.GetComponent<Renderer>().sharedMaterial = woodMat;

            // 茅草屋顶
            GameObject shackRoof = GameObject.CreatePrimitive(PrimitiveType.Cube);
            shackRoof.name = "ShackThatchRoof";
            shackRoof.transform.parent = shackRoot.transform;
            shackRoof.transform.localPosition = new Vector3(0, 3.4f, 0);
            shackRoof.transform.localScale = new Vector3(6.8f, 0.5f, 6.8f);
            shackRoof.GetComponent<Renderer>().sharedMaterial = thatchMat;

            // 破旧弹簧床与报纸
            GameObject cotBed = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cotBed.name = "CotBedWithNewspapers";
            cotBed.transform.parent = shackRoot.transform;
            cotBed.transform.localPosition = new Vector3(-1.8f, 0.4f, 1.2f);
            cotBed.transform.localScale = new Vector3(1.6f, 0.5f, 3.0f);
            cotBed.GetComponent<Renderer>().sharedMaterial = thatchMat;

            // 白铁皮罐苦咖啡与桌子
            GameObject coffeeTable = GameObject.CreatePrimitive(PrimitiveType.Cube);
            coffeeTable.name = "CoffeeTable";
            coffeeTable.transform.parent = shackRoot.transform;
            coffeeTable.transform.localPosition = new Vector3(1.5f, 0.6f, -1.0f);
            coffeeTable.transform.localScale = new Vector3(1.2f, 0.8f, 1.2f);
            coffeeTable.GetComponent<Renderer>().sharedMaterial = woodMat;

            // 5. 核心载具：【16英尺小木船 Skiff】(停泊在栈桥浅滩)
            GameObject boatObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boatObj.name = "OldMan_Skiff_16ft";
            boatObj.transform.position = new Vector3(0, 0.35f, 3.5f);
            boatObj.transform.localScale = new Vector3(1.6f, 0.7f, 4.8f);
            boatObj.GetComponent<Renderer>().sharedMaterial = woodMat;

            var boatCtrl = boatObj.AddComponent<BoatController>();
            var boatRb = boatObj.GetComponent<Rigidbody>();
            if (boatRb != null) boatRb.mass = 350f;

            // 船尾受力桩点
            GameObject sternPoint = new GameObject("SternAnchorPoint");
            sternPoint.transform.parent = boatObj.transform;
            sternPoint.transform.localPosition = new Vector3(0, 0.3f, 2.2f);

            // 桅杆与补丁帆
            GameObject mastObj = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            mastObj.name = "MastWithFlourSackSail";
            mastObj.transform.parent = boatObj.transform;
            mastObj.transform.localPosition = new Vector3(0, 1.8f, -0.6f);
            mastObj.transform.localScale = new Vector3(0.08f, 1.8f, 0.08f);
            mastObj.GetComponent<Renderer>().sharedMaterial = woodMat;

            // 6. 核心猎物：【18英尺大西洋蓝马林鱼】(潜伏在深渊大井区 Z=260)
            GameObject marlinObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            marlinObj.name = "GiantMarlin_18ft";
            marlinObj.transform.position = new Vector3(0, -2.5f, 260f);
            marlinObj.transform.localScale = new Vector3(1.1f, 1.1f, 4.5f);
            marlinObj.transform.rotation = Quaternion.Euler(0, 0, 90f);
            marlinObj.GetComponent<Renderer>().sharedMaterial = marlinMat;
            var marlinAI = marlinObj.AddComponent<MarlinAI>();

            // 7. 钓线系统
            GameObject lineObj = new GameObject("FishingLine_380Fathoms");
            var lr = lineObj.AddComponent<LineRenderer>();
            lr.startWidth = 0.04f;
            lr.endWidth = 0.04f;
            lr.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = new Color(1f, 0.95f, 0.8f, 0.95f);
            lr.endColor = new Color(0.4f, 0.75f, 0.95f, 0.6f);

            var linePhysics = lineObj.AddComponent<FishingLinePhysics>();
            linePhysics.Initialize(sternPoint.transform, marlinObj.transform);

            // 8. 老人圣地亚哥玩家实体 (陆地漫步)
            GameObject playerObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            playerObj.name = "Player_Santiago";
            playerObj.transform.position = new Vector3(-4f, 1.0f, -14f); // 初始站在码头通往露台饭店的道路上
            playerObj.transform.localScale = new Vector3(0.85f, 1.0f, 0.85f);
            playerObj.GetComponent<Renderer>().sharedMaterial = oldManMat;

            var charCtrl = playerObj.AddComponent<CharacterController>();
            charCtrl.height = 1.8f;
            charCtrl.radius = 0.45f;
            charCtrl.center = new Vector3(0, 0, 0);

            var footCtrl = playerObj.AddComponent<PlayerOnFootController>();
            footCtrl.SetTargets(terraceRoot.transform, shackRoot.transform, boatObj.transform);

            // 9. 全局系统管理器 (Root Manager)
            GameObject rootObj = new GameObject("GameRoot_Manager");
            rootObj.AddComponent<DialogueManager>();
            var stats = rootObj.AddComponent<OldManSurvivalStats>();
            var weaponSystem = rootObj.AddComponent<WeaponDegradationSystem>();
            var flow = rootObj.AddComponent<StoryFlowManager>();
            var bootstrap = rootObj.AddComponent<GameBootstrap>();
            var hud = rootObj.AddComponent<GameHUD>();

            // 绑定 StoryFlowManager 引用
            SetField(flow, "santiagoPlayer", playerObj.transform);
            SetField(flow, "boat", boatObj.transform);
            SetField(flow, "boatController", boatCtrl);
            SetField(flow, "marlin", marlinAI);
            SetField(flow, "linePhysics", linePhysics);
            SetField(flow, "stats", stats);
            SetField(flow, "terraceTablePoint", terraceRoot.transform);
            SetField(flow, "shackPoint", shackRoot.transform);
            SetField(flow, "pierBoatPoint", boatObj.transform);

            // 绑定 Bootstrap 引用
            SetField(bootstrap, "boat", boatCtrl);
            SetField(bootstrap, "marlin", marlinAI);
            SetField(bootstrap, "linePhysics", linePhysics);
            SetField(bootstrap, "stats", stats);
            SetField(bootstrap, "weaponSystem", weaponSystem);

            // 绑定 HUD 引用
            SetField(hud, "linePhysics", linePhysics);
            SetField(hud, "stats", stats);
            SetField(hud, "marlin", marlinAI);
            SetField(hud, "weaponSystem", weaponSystem);
            SetField(hud, "boatTransform", boatObj.transform);

            // 10. 主摄像机与动态跟随控制器
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                mainCam.transform.position = new Vector3(-4f, 3.8f, -19.5f);
                mainCam.transform.LookAt(playerObj.transform.position + Vector3.up * 1.0f);
                mainCam.backgroundColor = new Color(0.12f, 0.22f, 0.36f);

                var dynamicCam = mainCam.gameObject.GetComponent<DynamicCameraController>();
                if (dynamicCam == null) dynamicCam = mainCam.gameObject.AddComponent<DynamicCameraController>();
                dynamicCam.SetTargets(playerObj.transform, boatObj.transform);
            }

            // 11. 平行光调整（加勒比黄昏与暖金色霞光）
            GameObject dirLight = GameObject.Find("Directional Light");
            if (dirLight != null)
            {
                Light light = dirLight.GetComponent<Light>();
                if (light != null)
                {
                    light.color = new Color(1.0f, 0.88f, 0.72f);
                    light.intensity = 1.35f;
                }
                dirLight.transform.rotation = Quaternion.Euler(32, -40, 0);
            }

            // 保存场景
            string scenePath = "Assets/_Game/Scenes/MainSeaScene.unity";
            EditorSceneManager.SaveScene(newScene, scenePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"<color=green>[老人与海] 完整开放探索与剧情场景构建成功！陆地露台饭店、陋室小木屋、栈桥小艇与开阔海域已就位：{scenePath}</color>");
        }

        private static Material GetOrCreateMaterial(string path, Shader shader, Color color, float smoothness, float metallic)
        {
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);
            if (mat == null)
            {
                mat = new Material(shader);
                mat.name = System.IO.Path.GetFileNameWithoutExtension(path);
                mat.color = color;
                mat.SetFloat("_Smoothness", smoothness);
                mat.SetFloat("_Metallic", metallic);
                AssetDatabase.CreateAsset(mat, path);
            }
            return mat;
        }

        public static void SetupURPPipeline()
        {
            if (!AssetDatabase.IsValidFolder("Assets/_Game/Settings"))
            {
                AssetDatabase.CreateFolder("Assets/_Game", "Settings");
            }

            string rendererPath = "Assets/_Game/Settings/URP_ForwardRenderer.asset";
            string pipelineAssetPath = "Assets/_Game/Settings/URP_PipelineAsset.asset";

            var rendererData = AssetDatabase.LoadAssetAtPath<UniversalRendererData>(rendererPath);
            if (rendererData == null)
            {
                rendererData = ScriptableObject.CreateInstance<UniversalRendererData>();
                AssetDatabase.CreateAsset(rendererData, rendererPath);
            }

            var urpAsset = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(pipelineAssetPath);
            if (urpAsset == null)
            {
                urpAsset = UniversalRenderPipelineAsset.Create(rendererData);
                AssetDatabase.CreateAsset(urpAsset, pipelineAssetPath);
            }

            GraphicsSettings.defaultRenderPipeline = urpAsset;

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        private static void SetField(object target, string fieldName, object value)
        {
            var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
            if (field != null)
            {
                field.SetValue(target, value);
            }
        }
    }
}
