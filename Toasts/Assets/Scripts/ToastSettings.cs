using UnityEngine;

namespace UnityToasts
{
    public class ToastSettings : ScriptableObject
    {
        #region Load DebugData from resources folder and if not create asset file .
        static ToastSettings _Instance;

        public static ToastSettings Instance
        {
            get
            {
                if (_Instance == null)
                {
                    string className = typeof(ToastSettings).Name;

                    _Instance = Resources.Load(className) as ToastSettings;

                    if (_Instance == null)
                    {
                        _Instance = CreateInstance<ToastSettings>();

                        ScriptableObjectUtils.CreateAsset(_Instance, className);
                    }
                }
                return _Instance;
            }
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Toast/Toast Settings")]
        public static void Edit()
        {
            UnityEditor.Selection.activeObject = Instance;

        }
#endif
        #endregion

        #region
        public Color toastBackgroundColor = Color.yellow;
        public Color toastTextColor = Color.black;

        public int fontSize = 25;

        public Font toastFont;
        #endregion
    }
}