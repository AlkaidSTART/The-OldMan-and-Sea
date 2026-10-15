using UnityEngine;
using UnityEditor;
using UnityEditor.Build.Reporting;
using System.IO;

namespace OldManAndTheSea.Editor
{
    public static class WebGLBuilder
    {
        [MenuItem("Old Man & Sea/Build WebGL (导出到 Web 项目)")]
        public static void BuildWebGL()
        {
            Debug.Log("[OldManAndTheSea] Starting WebGL Build process...");

            // 1. 确保场景生成与配置
            GameSceneBuilder.GenerateScene();

            string scenePath = "Assets/_Game/Scenes/MainSeaScene.unity";
            string[] scenes = new string[] { scenePath };

            // 2. 导出路径：Next.js 项目的 public/unity-build
            string projectRoot = Path.GetFullPath(Path.Combine(Application.dataPath, "../.."));
            string buildPath = Path.Combine(projectRoot, "public/unity-build");

            if (!Directory.Exists(buildPath))
            {
                Directory.CreateDirectory(buildPath);
            }

            // 3. WebGL 配置优化：禁用压缩以确保本地开发服务器 (Next.js / Vite) 零配置直读
            EditorUserBuildSettings.selectedBuildTargetGroup = BuildTargetGroup.WebGL;
            PlayerSettings.WebGL.compressionFormat = WebGLCompressionFormat.Disabled;
            PlayerSettings.WebGL.decompressionFallback = false;
            PlayerSettings.productName = "The Old Man and the Sea";
            PlayerSettings.companyName = "OldManSeaStudio";

            BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions
            {
                scenes = scenes,
                locationPathName = buildPath,
                target = BuildTarget.WebGL,
                options = BuildOptions.None
            };

            Debug.Log($"[OldManAndTheSea] Building player to: {buildPath}");
            BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
            BuildSummary summary = report.summary;

            if (summary.result == BuildResult.Succeeded)
            {
                Debug.Log($"[OldManAndTheSea] WebGL Build SUCCEEDED! Total size: {summary.totalSize / (1024 * 1024)} MB");
            }
            else
            {
                Debug.LogError($"[OldManAndTheSea] WebGL Build FAILED with result: {summary.result}, errors: {summary.totalErrors}");
                EditorApplication.Exit(1);
            }
        }
    }
}
