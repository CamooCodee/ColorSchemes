using System.Collections.Generic;

namespace lukassacher.ColorSchemes
{
    public static class ColorSchemes
    {
        public const string DefaultSchemeType = "DEFAULT_SCHEME_TYPE";
        
        private static readonly List<SchemeBehaviour> allBehaviours = new List<SchemeBehaviour>();
        private static Dictionary<string, Scheme> _currentSchemes = null;
        private static Dictionary<string, Scheme> CurrentSchemes
        {
            get
            {
                if (_currentSchemes != null) return _currentSchemes;
                
                var helper = SchemeSerializationHelper.Instance;
                _currentSchemes = helper != null ? helper.Load() : new Dictionary<string, Scheme>();

                return _currentSchemes;
            }
        }

        /// <summary>
        /// Include an object into scheme events
        /// </summary>
        public static void Add(SchemeBehaviour o)
        {
            if(o == null || allBehaviours.Contains(o)) return;
            allBehaviours.Add(o);
            
            foreach (var scheme in CurrentSchemes)
                if (o.IsInterestedIn(scheme.Key))
                    o.SetScheme(scheme.Value);
        }

        /// <summary>
        /// Remove an object from being included into scheme events.
        /// </summary>
        public static void Remove(SchemeBehaviour o)
        {
            if(o == null || !allBehaviours.Contains(o)) return;
            allBehaviours.Remove(o);
        }
        
        /// <summary>
        /// Sets all current schemes it is interested in on the given object.
        /// </summary>
        public static void UpdateSchemesOn(SchemeBehaviour o)
        {
            foreach (var scheme in CurrentSchemes)
                if (o.IsInterestedIn(scheme.Key))
                    o.SetScheme(scheme.Value);
        }

        /// <summary>
        /// Set the scheme for every behaviour interested in the scheme type.
        /// </summary>
        /// <param name="scheme">The scheme to set.</param>
        public static void SetScheme(Scheme scheme)
        {
            if(scheme == null) return;

            bool shouldSave = !CurrentSchemes.ContainsValue(scheme);
            
            if (CurrentSchemes.ContainsKey(scheme.Type))
                CurrentSchemes[scheme.Type] = scheme;
            else CurrentSchemes.Add(scheme.Type, scheme);

            var helper = SchemeSerializationHelper.Instance;
            if(helper != null && shouldSave)
                helper.Save(CurrentSchemes);
            
            foreach (var behaviour in allBehaviours)
                if (behaviour.IsInterestedIn(scheme.Type))
                    behaviour.SetScheme(scheme);
        }

        /// <summary>
        /// Returns if the given scheme is one of the current ones.
        /// </summary>
        public static bool IsACurrentScheme(Scheme scheme)
        {
            if (!CurrentSchemes.ContainsKey(scheme.Type)) return false;

            return CurrentSchemes[scheme.Type] == scheme;
        }

        /// <summary>
        /// Returns if there is a current scheme set for the passed type.
        /// </summary>
        public static bool SchemeOfTypeExists(string type)
        {
            return CurrentSchemes.ContainsKey(type);
        }
    }
}