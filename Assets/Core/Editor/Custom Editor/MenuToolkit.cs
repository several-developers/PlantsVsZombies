#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Core.Editor.CustomEditor
{
    public class MenuToolkit : MonoBehaviour
    {
        // FIELDS: --------------------------------------------------------------------------------

        // ✨ ⏳

        private const string ScenesMenuItem = "🕹 Plants vs Zombies/💾 Scenes/";
        private const string ScenesPath = "Assets/Core/Scenes/";

        private const string BootstrapSceneMenuItem = ScenesMenuItem + "🚀 Bootstrap";
        private const string LoginSceneMenuItem = ScenesMenuItem + "🗝 Login";
        private const string MainMenuSceneMenuItem = ScenesMenuItem + "🌐 Main Menu";
        private const string GameSceneMenuItem = ScenesMenuItem + "⚔ Game";

        private const string BootstrapScenePath = ScenesPath + "Bootstrap.unity";
        private const string LoginScenePath = ScenesPath + "Login.unity";
        private const string MainMenuScenePath = ScenesPath + "MainMenu.unity";
        private const string GameScenePath = ScenesPath + "Game.unity";

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [MenuItem(BootstrapSceneMenuItem)]
        private static void LoadBootstrapScene() =>
            OpenScene(BootstrapScenePath);

        [MenuItem(LoginSceneMenuItem)]
        private static void LoadLoginScene() =>
            OpenScene(LoginScenePath);

        [MenuItem(MainMenuSceneMenuItem)]
        private static void LoadMainMenuScene() =>
            OpenScene(MainMenuScenePath);

        [MenuItem(GameSceneMenuItem)]
        private static void LoadGameScene() =>
            OpenScene(GameScenePath);

        private static void OpenScene(string path)
        {
            bool canOpenScene = !Application.isPlaying &&
                                EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo();
            
            if (!canOpenScene)
                return;

            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }
    }
}
#endif