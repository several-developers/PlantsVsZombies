using UnityEditor;
using static UnityEditor.BuildPipeline;

namespace Core.Editor
{
    public static class Builder
    {
        [MenuItem("📦 Build/Android")]
        public static void BuildAndroid()
        {
            BuildPlayer(new BuildPlayerOptions
            {
                target = BuildTarget.Android,
                locationPathName = "../artifacts/Game.apk",
                scenes = new[] { "Assets/Core/Scenes/Bootstrap.unity" }
            });
        }
    }
}