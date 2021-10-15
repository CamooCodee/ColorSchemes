using UnityEditor;
using UnityEngine;

namespace lukassacher.ColorSchemes
{
    [CustomPropertyDrawer(typeof(ColorSchemeBehaviour.SupportedColorScheme))]
    public class SupportedColorSchemePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            position.height = 20f;

            #region Setting Up Properties And Values

            var schemeProp = property.FindPropertyRelative("scheme");
            var scheme = schemeProp.objectReferenceValue as ColorScheme;
            var colorIndexProp = property.FindPropertyRelative("schemeColorIndex");
            var oldSelected = colorIndexProp.intValue;
            
            #endregion
            
            EditorGUI.PropertyField(position, schemeProp);
            position.center += Vector2.up * 25f;
            
            #region Handle Scheme Being Null

            if (scheme == null)
            {
                EditorGUI.LabelField(position, "Select a scheme!");
                return;
            }
            

            #endregion
            #region Setting Up Properties And Values When Scheme Isn't Null
            
            var colorNames = new string[scheme.colors.Length];
            for (var i = 0; i < colorNames.Length; i++)
            { 
                colorNames[i] = scheme.colors[i].name;
            }
            
            #endregion
            #region Drawing Popup

            var prev = GUI.contentColor;
            GUI.contentColor = scheme.colors[oldSelected].color;
            var selected = EditorGUI.Popup(position, oldSelected, colorNames);
            colorIndexProp.intValue = selected;
            GUI.contentColor = prev;

            #endregion
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 45f;
        }
    }
}