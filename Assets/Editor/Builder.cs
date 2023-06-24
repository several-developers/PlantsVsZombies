using System;
using UnityEditor;
using UnityEditor.Build.Reporting;
using static UnityEditor.BuildPipeline;

namespace Core.Editor
{
    public static class Builder
    {
        [MenuItem("📦 Build/Android")]
        public static void BuildAndroid()
        {
            BuildReport report = BuildPlayer(new BuildPlayerOptions
            {
                target = BuildTarget.Android,
                locationPathName = "artifacts/Game.apk",
                scenes = new[] { "Assets/Core/Scenes/Bootstrap.unity" }
            });

            if (report.summary.result != BuildResult.Succeeded)
                throw new Exception("Failed to build Android package. See log for details.");
        }
    }
}