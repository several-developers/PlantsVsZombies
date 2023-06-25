#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Core.App;
using Core.Infrastructure.Data;
using Core.Utilities;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Core.Editor.CustomEditor
{
    // 
    // Be sure to check out OdinMenuStyleExample.cs as well. It shows you various ways to customize the look and behaviour of OdinMenuTrees.
    // 

    public class CustomEditor : OdinMenuEditorWindow
    {
        private enum MenuType
        {
            Default = 0,
            GameData = 1,
        }

        // FIELDS: --------------------------------------------------------------------------------

        private const string CustomEditorMenuItem = "🕹 Plants vs Zombies/⚙ PvZ Editor";

        [SerializeField]
        private GameDataViewer _gameDataViewer = new();

        private OdinMenuTree _customTree;
        private MenuType _menuType = MenuType.Default;

        // PROTECTED METHODS: ---------------------------------------------------------------------

        protected override OdinMenuTree BuildMenuTree()
        {
            OdinMenuTree tree = new OdinMenuTree(true);
            
            Debug.Log("Drawing tree...");
            
            SetupMenuStyle(tree);

            switch (_menuType)
            {
                case MenuType.Default:
                    tree.AddAllAssetsAtPath(menuPath: "",
                        assetFolderPath: "Assets/Resources/Game Data/",
                        typeof(EditorMeta),
                        includeSubDirectories: true);
                    break;
                
                case MenuType.GameData:
                    DrawDataButtons(tree);
                    break;
            }
            
            tree.EnumerateTree().SortMenuItemsByName();
            
            return tree;
        }

        protected override void OnGUI()
        {
            DrawDataToolbar();
            base.OnGUI();
        }

        protected override void OnBeginDrawEditors()
        {
            bool skip = MenuTree == null || MenuTree.Selection == null;
            
            if (skip)
                return;

            DrawMetaBar();
        }

        // protected override IEnumerable<object> GetTargets()
        // {
        //     switch (_menuType)
        //     {
        //         case MenuType.GameData:
        //             yield return _gameDataViewer;
        //             break;
        //     }
        // }

        // PRIVATE METHODS: -----------------------------------------------------------------------

        [MenuItem(CustomEditorMenuItem)]
        private static void OpenWindow()
        {
            var window = GetWindow<CustomEditor>();
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
        }

        private void SetupMenuStyle(OdinMenuTree tree)
        {
            tree.Config.DrawSearchToolbar = true;
            tree.DefaultMenuStyle = OdinMenuStyle.TreeViewStyle;
            tree.DefaultMenuStyle.Height = 28;
            tree.DefaultMenuStyle.IndentAmount = 12;
            tree.DefaultMenuStyle.IconSize = 26;
            tree.DefaultMenuStyle.NotSelectedIconAlpha = 1;
            tree.DefaultMenuStyle.IconPadding = 4;
            tree.DefaultMenuStyle.SelectedColorDarkSkin = EditorDatabase.SelectedColor;
            tree.DefaultMenuStyle.SelectedInactiveColorDarkSkin = EditorDatabase.SelectedInactiveColor;

            //tree.Add("Menu Style", tree.DefaultMenuStyle);
        }

        private void AddDragHandles(OdinMenuItem menuItem)
        {
            menuItem.OnDrawItem += x =>
                DragAndDropUtilities.DragZone(menuItem.Rect, menuItem.Value, false, false);
        }

        private void DrawDataToolbar()
        {
            bool redrawTree = false;
            int heightMargin = 2;
            int widthMargin = 6;
            int iconsSize = 21;

            GUIStyle guiStyle = new GUIStyle();
            guiStyle.alignment = TextAnchor.MiddleRight;
            guiStyle.margin = new RectOffset(left: widthMargin, right: widthMargin, top: heightMargin,
                bottom: heightMargin);

            Texture metaMenuIcon = _menuType == MenuType.Default
                ? EditorIcons.List.Highlighted
                : EditorIcons.List.Inactive;
            
            Texture dataMenuIcon = _menuType == MenuType.GameData
                ? EditorIcons.Clouds.Highlighted
                : EditorIcons.Clouds.Inactive;

            SirenixEditorGUI.BeginHorizontalToolbar(guiStyle.lineHeight);

            if (SirenixEditorGUI.IconButton(metaMenuIcon, guiStyle, width: iconsSize, height: iconsSize))
            {
                _menuType = MenuType.Default;
                redrawTree = true;
            }

            if (SirenixEditorGUI.IconButton(dataMenuIcon, guiStyle, width: iconsSize, height: iconsSize))
            {
                _menuType = MenuType.GameData;
                redrawTree = true;
            }

            SirenixEditorGUI.EndHorizontalToolbar();

            if (redrawTree)
                BuildMenuTree();
        }

        private void DrawMetaBar()
        {
            OdinMenuTreeSelection selection = MenuTree.Selection;
            OdinMenuItem selected = MenuTree.Selection.FirstOrDefault();
            int toolbarHeight = MenuTree.Config.SearchToolbarHeight;
            bool isSelectedNull = selected == null;
            EditorMeta selectedMeta = selection.SelectedValue as EditorMeta;
            string metaPath = "Assets/Resources/Game Data/";
            string labelName = "";

            switch (_menuType)
            {
                case MenuType.GameData:
                    labelName = "Game Data";
                    break;

                case MenuType.Default:
                    labelName = isSelectedNull ? "Meta" : $"Meta - {selected.Name}";
                    break;
            }

            // Draws a toolbar with the name of the currently selected menu item.
            SirenixEditorGUI.BeginHorizontalToolbar(toolbarHeight);

            // Draw label
            GUILayout.Label(labelName);

            // Create and Selects the newly created item in the editor
            if (SirenixEditorGUI.ToolbarButton("Create"))
            {
                if (selectedMeta != null)
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);
                    string fileName = Path.GetFileName(metaPath);
                    metaPath = metaPath.Replace(fileName, "");
                }

                ScriptableObjectCreator.ShowDialog<EditorMeta>(metaPath, TrySelectMenuItemWithObject);
            }

            if (!isSelectedNull && selectedMeta)
            {
                if (SirenixEditorGUI.ToolbarButton("Duplicate"))
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);
                    ScriptableObjectCreator.Duplicate(selectedMeta, metaPath, ScriptableObjectUtility.SaveAsset);
                }

                if (SirenixEditorGUI.ToolbarButton("Save"))
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();

                    TrySelectMenuItemWithObject(selectedMeta);

                    // if (!string.IsNullOrWhiteSpace(newName))
                    // {
                    //     AssetDatabase.RenameAsset(metaPath, newName);
                    //     AssetDatabase.SaveAssets();
                    //     AssetDatabase.Refresh();
                    //
                    //     TrySelectMenuItemWithObject(selectedMeta);
                    // }
                }

                if (SirenixEditorGUI.ToolbarButton("Delete"))
                {
                    metaPath = AssetDatabase.GetAssetPath(selectedMeta);

                    AssetDatabase.DeleteAsset(metaPath);
                    AssetDatabase.SaveAssets();
                }
            }

            SirenixEditorGUI.EndHorizontalToolbar();
        }

        private void DrawDataButtons(OdinMenuTree tree)
        {
            IEnumerable<Type> subclassTypes = Assembly
                .GetAssembly(typeof(DataBase))
                .GetTypes()
                .Where(t => t.IsSubclassOf(typeof(DataBase)));

            foreach (Type type in subclassTypes)
            {
                tree.Add(type.Name.GetNiceName(), Activator.CreateInstance(type));
            }
        }
    }
}
#endif