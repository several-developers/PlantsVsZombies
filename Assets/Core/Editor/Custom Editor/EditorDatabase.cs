#if UNITY_EDITOR
using UnityEngine;

namespace Core.Editor.CustomEditor
{
    public static class EditorDatabase
    {
        // FIELDS: --------------------------------------------------------------------------------
        
        public static readonly Color SelectedColor = new(0.745f, 0.256f, 0.302f);
        public static readonly Color SelectedInactiveColor = new(0.205f, 0.205f, 0.205f);
    }
}
#endif