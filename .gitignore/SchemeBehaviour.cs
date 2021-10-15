using UnityEngine;

namespace lukassacher.ColorSchemes
{
    [ExecuteAlways]
    public abstract class SchemeBehaviour : MonoBehaviour
    {
        protected virtual void OnEnable()
        {
            ColorSchemes.Add(this);
        }
        
        protected virtual void OnDisable()
        {
            ColorSchemes.Remove(this);
        }

        public virtual bool IsInterestedIn(string schemeType)
        {
            return schemeType == ColorSchemes.DefaultSchemeType;
        }

        public abstract void SetScheme(Scheme scheme);
    }
}