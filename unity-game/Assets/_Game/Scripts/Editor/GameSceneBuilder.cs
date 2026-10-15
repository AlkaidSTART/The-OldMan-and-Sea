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

            // 1. 确保材质目录与材质
            if (!AssetDatabase.IsValidFolder("Assets/_Game/Materials"))
            {
                AssetDatabase.CreateFolder("Assets/_Game", "Materials");
            }

            Shader litShader = Shader.Find("Universal Render Pipeline/Lit") ?? Shader.Find("Standard");

            // 海水材质
            Material oceanMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/_Game/Materials/M_OceanSurface.mat");
            if (oceanMat == null)
            {
                oceanMat = new Material(litShader);
                oceanMat.name = "M_OceanSurface";
                oceanMat.color = new Color(0.06f, 0.22f, 0.38f, 0.9f);
                oceanMat.SetFloat("_Smoothness", 0.85f);
                oceanMat.SetFloat("_Metallic", 0.1f);
                AssetDatabase.CreateAsset(oceanMat, "Assets/_Game/Materials/M_OceanSurface.mat");
            }

            // 木船材质
            Material woodMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/_Game/Materials/M_WoodSkiff.mat");
            if (woodMat == null)
            {
                woodMat = new Material(litShader);
                woodMat.name = "M_WoodSkiff";
                woodMat.color = new Color(0.42f, 0.26f, 0.15f);
                woodMat.SetFloat("_Smoothness", 0.2f);
                AssetDatabase.CreateAsset(woodMat, "Assets/_Game/Materials/M_WoodSkiff.mat");
            }

            // 马林鱼材质
            Material marlinMat = AssetDatabase.LoadAssetAtPath<Material>("Assets/_Game/Materials/M_BlueMarlin.mat");
            if (marlinMat == null)
            {
                marlinMat = new Material(litShader);
                marlinMat.name = "M_BlueMarlin";
                marlinMat.color = new Color(0.15f, 0.32f, 0.72f);
                marlinMat.SetFloat("_Smoothness", 0.7f);
                AssetDatabase.CreateAsset(marlinMat, "Assets/_Game/Materials/M_BlueMarlin.mat");
            }

            // 2. 实体化构建【海洋水面】
            GameObject oceanObj = GameObject.CreatePrimitive(PrimitiveType.Plane);
            oceanObj.name = "OceanSurface";
            oceanObj.transform.position = Vector3.zero;
            oceanObj.transform.localScale = new Vector3(80f, 1f, 80f);
            var oceanRenderer = oceanObj.GetComponent<Renderer>();
            if (oceanRenderer != null) oceanRenderer.sharedMaterial = oceanMat;

            // 3. 实体化构建【16英尺旧木小舟】
            GameObject boatObj = GameObject.CreatePrimitive(PrimitiveType.Cube);
            boatObj.name = "OldMan_Skiff_16ft";
            boatObj.transform.position = new Vector3(0, 0.4f, 0);
            boatObj.transform.localScale = new Vector3(1.6f, 0.7f, 4.8f);
            var boatRenderer = boatObj.GetComponent<Renderer>();
            if (boatRenderer != null) boatRenderer.sharedMaterial = woodMat;

            var boatCtrl = boatObj.AddComponent<BoatController>();
            var boatRb = boatObj.GetComponent<Rigidbody>();
            if (boatRb != null)
            {
                boatRb.mass = 350f;
            }

            // 船尾受力桩点
            GameObject sternPoint = new GameObject("SternAnchorPoint");
            sternPoint.transform.parent = boatObj.transform;
            sternPoint.transform.localPosition = new Vector3(0, 0.3f, 2.2f);

            // 4. 实体化构建【18英尺大西洋蓝马林鱼】
            GameObject marlinObj = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            marlinObj.name = "GiantMarlin_18ft";
            marlinObj.transform.position = new Vector3(0, -1.8f, 28f);
            marlinObj.transform.localScale = new Vector3(1.1f, 1.1f, 4.5f);
            marlinObj.transform.rotation = Quaternion.Euler(0, 0, 90f);
            var marlinRenderer = marlinObj.GetComponent<Renderer>();
            if (marlinRenderer != null) marlinRenderer.sharedMaterial = marlinMat;

            var marlinAI = marlinObj.AddComponent<MarlinAI>();

            // 5. 实体化构建【深海钓线】
            GameObject lineObj = new GameObject("FishingLine_380Fathoms");
            var lr = lineObj.AddComponent<LineRenderer>();
            lr.startWidth = 0.04f;
            lr.endWidth = 0.04f;
            lr.sharedMaterial = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = new Color(1f, 0.95f, 0.8f, 0.95f);
            lr.endColor = new Color(0.4f, 0.75f, 0.95f, 0.6f);

            var linePhysics = lineObj.AddComponent<FishingLinePhysics>();
            linePhysics.Initialize(sternPoint.transform, marlinObj.transform);

            // 6. 全局游戏管理器与 HUD
            GameObject rootObj = new GameObject("GameRoot_Manager");
            rootObj.AddComponent<DialogueManager>();
            var stats = rootObj.AddComponent<OldManSurvivalStats>();
            var weaponSystem = rootObj.AddComponent<WeaponDegradationSystem>();
            var bootstrap = rootObj.AddComponent<GameBootstrap>();
            var hud = rootObj.AddComponent<GameHUD>();

            // 注入引用给 Bootstrap
            SetField(bootstrap, "boat", boatCtrl);
            SetField(bootstrap, "marlin", marlinAI);
            SetField(bootstrap, "linePhysics", linePhysics);
            SetField(bootstrap, "stats", stats);
            SetField(bootstrap, "weaponSystem", weaponSystem);

            // 注入引用给 HUD
            SetField(hud, "linePhysics", linePhysics);
            SetField(hud, "stats", stats);
            SetField(hud, "marlin", marlinAI);
            SetField(hud, "weaponSystem", weaponSystem);

            // 7. 调整主摄像机（第三人称绝美越肩视角）
            Camera mainCam = Camera.main;
            if (mainCam != null)
            {
                mainCam.transform.position = new Vector3(0, 6.5f, -10f);
                mainCam.transform.rotation = Quaternion.Euler(22f, 0, 0);
                mainCam.backgroundColor = new Color(0.08f, 0.20f, 0.35f);
            }

            // 8. 调整平行光（加勒比海烈日照耀）
            GameObject dirLight = GameObject.Find("Directional Light");
            if (dirLight != null)
            {
                Light light = dirLight.GetComponent<Light>();
                if (light != null)
                {
                    light.color = new Color(1.0f, 0.96f, 0.88f);
                    light.intensity = 1.4f;
                }
                dirLight.transform.rotation = Quaternion.Euler(45, -35, 0);
            }

            // 保存场景
            string scenePath = "Assets/_Game/Scenes/MainSeaScene.unity";
            EditorSceneManager.SaveScene(newScene, scenePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();

            Debug.Log($"<color=cyan>[老人与海] 实体化场景构建完成！小船、海水、大马林鱼已全部就位：{scenePath}</color>");
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
