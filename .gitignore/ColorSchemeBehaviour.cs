using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lukassacher.ColorSchemes
{
    public class ColorSchemeBehaviour : SchemeBehaviour
    {
        [Serializable]
        public class SupportedColorScheme
        {
            public ColorScheme scheme;
            public int schemeColorIndex;
        }

        public List<SupportedColorScheme> supported = new List<SupportedColorScheme>();
        private Graphic _graphic;


        private void OnValidate()
        {
            if(supported.Count == 0) return;
            
            for (var i = 0; i < supported.Count; i++)
            {
                for (var j = 0; j < i; j++)
                {
                    if (supported[i].scheme == supported[j].scheme)
                    {
                        supported[i].scheme = null;
                    }
                }
            }

            ColorSchemes.UpdateSchemesOn(this);
        }

        private void Awake()
        {
            _graphic = GetComponent<Graphic>();
            if (_graphic == null) 
                Debug.LogError($"The {nameof(ColorSchemeBehaviour)} requires a Graphic component.");
        }

        public override void SetScheme(Scheme scheme)
        {
            if(_graphic == null) return;
            if(scheme == null) return;
            
            foreach (var s in supported)
            {
                if(s.scheme == null) continue;
                if (s.scheme == scheme)
                {
                    _graphic.color = s.scheme.colors[s.schemeColorIndex].color;
                    return;
                }
            }
        }
    }
}