using System;
using UnityEngine;
using static lukassacher.ColorSchemes.ColorSchemes;

namespace lukassacher.ColorSchemes
{
    [CreateAssetMenu(fileName = "New Color Scheme", menuName = "Scriptable Objects/Schemes/Color Scheme")]
    public class ColorScheme : Scheme
    {
        public override string Type => DefaultSchemeType;

        public SchemeColor[] colors = new SchemeColor[0];

        public void OnValidate()
        {
            if (!SchemeOfTypeExists(Type) || IsACurrentScheme(this))
            {
                SetScheme(this);
            }
        }
    }

    [Serializable]
    public struct SchemeColor
    {
        public string name;
        public Color color;
    }
}