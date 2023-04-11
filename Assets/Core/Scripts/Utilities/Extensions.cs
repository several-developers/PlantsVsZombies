using System.Text.RegularExpressions;
using UnityEngine;

namespace Core.Utilities
{
    public static class Extensions
    {
        // PUBLIC METHODS: ------------------------------------------------------------------------
        
        public static void CopyToClipboard(this string str) =>
            GUIUtility.systemCopyBuffer = str;
        
        public static string GetNiceName(this string text) =>
            Regex.Replace(text, "([a-z0-9])([A-Z0-9])", "$1 $2");
        
        public static void ConvertToMinutes(this float time, out string result)
        {
            time = Mathf.Max(time, 0);
            
            int minutes = Mathf.FloorToInt(time % 3600 / 60f);
            int seconds = Mathf.FloorToInt(time % 60);
            
            result = $"{minutes:D2}:{seconds:D2}";
        }

        public static void ConvertToHours(this float time, out string result)
        {
            time = Mathf.Max(time, 0);
            
            int hours = Mathf.FloorToInt(time / 3600f);
            int minutes = Mathf.FloorToInt(time % 3600 / 60f);
            int seconds = Mathf.FloorToInt(time % 60);
            
            result = $"{hours:D2}:{minutes:D2}:{seconds:D2}";
        }
    }
}