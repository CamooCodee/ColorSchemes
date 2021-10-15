using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace lukassacher.ColorSchemes
{
    [CustomEditor(typeof(Scheme), true)]
    public class SchemeEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set As Current Scheme"))
            {
                var inst = SchemeSerializationHelper.Instance;
                if(inst != null && inst.SelectImageOnSet)
                    Selection.objects = new Object[] { FindObjectOfType<Image>().gameObject };
                
                ((Scheme)target).SetAsCurrentScheme();
            }
        }
    }
}