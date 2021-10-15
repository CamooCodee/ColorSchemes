using UnityEngine;
using static lukassacher.ColorSchemes.ColorSchemes;


namespace lukassacher.ColorSchemes
{
    public abstract class Scheme : ScriptableObject
    {
        public abstract string Type { get; }
        
        public void SetAsCurrentScheme()
        {
            SetScheme(this);
        }
    }
}