using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace lukassacher.ColorSchemes
{
    [CreateAssetMenu(fileName = "SchemeSerializationHelper", menuName = "Scriptable Objects/Schemes/Serialization Helper")]
    public class SchemeSerializationHelper : ScriptableObject
    {
        private static SchemeSerializationHelper instance;
        public static SchemeSerializationHelper Instance
        {
            get
            {
                if (instance != null) return instance;
                
                var guids = AssetDatabase.FindAssets("t:"+ nameof(SchemeSerializationHelper));
                if (guids.Length == 0)
                {
                    Debug.LogWarning("It's recommended to have a scheme serialization helper in your project!");
                    return null;
                }
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                instance = AssetDatabase.LoadAssetAtPath<SchemeSerializationHelper>(path);
                return instance;
            }
        }

        [Tooltip("If this is disabled, a manual selection of any image in the scene is required in order to actually update the visual color.")]
        [SerializeField] private bool selectImageOnSet = true;
        public bool SelectImageOnSet => selectImageOnSet;

        [SerializeField, HideInInspector] private List<Scheme> schemes = new List<Scheme>();

        public void Save(Dictionary<string, Scheme> obj)
        {
            schemes.Clear();
            schemes = obj.Values.ToList();
        }

        public Dictionary<string, Scheme> Load()
        {
            return schemes.ToDictionary(scheme => scheme.Type);
        }
    }
}