using System.IO;
using UnityEditor;
using UnityEngine;

namespace EditorExtensions
{
    public class CashCleaner : MonoBehaviour
    {
        [MenuItem("Window/Clean Cash")]
        public static void CleanCash()
        {
            PlayerPrefs.DeleteAll();
            File.Delete(Path.Combine(Application.persistentDataPath, "game_stats.json"));
        }
    }
}