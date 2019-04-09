using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToasts
{
    public static class Toasts
    {
        public const string CANVAS_NAME = "ToastsCanvas";
        public const string TOASTS_NAME = "Toast";
        public const string TOASTS_TEXT = "ToastText";
        public const string TOASTS_BG = "ToastBG";
        public const string DEFAULT_FONT_NAME = "Arial.ttf";

        public static void CreateToast(string displayText)
        {
            Transform canvas = CreateCanvas().transform;

            GameObject parentGO = CreateToastGameObject(canvas);

            GameObject backgroundGO = CreateBG(parentGO.transform);
            GameObject textGO = CreateText(parentGO.transform, displayText);

            Toast toast = parentGO.AddComponent<Toast>();
            toast.StartAnimation(textGO, backgroundGO);
        }

        private static GameObject CreateToastGameObject(Transform parent)
        {
            GameObject toastGO = new GameObject();
            toastGO.name = TOASTS_NAME;
            toastGO.transform.parent = parent;

            RectTransform rectTransform = toastGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);

            return toastGO;
        }

        private static GameObject CreateBG(Transform parent)
        {
            GameObject backgroundGO = new GameObject();
            backgroundGO.name = TOASTS_BG;
            backgroundGO.transform.parent = parent;

            RectTransform rectTransform = backgroundGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);

            Image image = backgroundGO.AddComponent<Image>();
            image.color = Color.red;

            return backgroundGO;
        }

        private static GameObject CreateText(Transform parent, string displayText)
        {
            GameObject textGO = new GameObject();
            textGO.name = TOASTS_TEXT;
            textGO.transform.parent = parent;

            RectTransform rectTransform = textGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);

            Text text = textGO.AddComponent<Text>();
            text.text = displayText;
            text.font = Resources.GetBuiltinResource(typeof(Font), DEFAULT_FONT_NAME) as Font;

            return textGO;
        }

        private static GameObject CreateCanvas()
        {
            GameObject canvasGO = new GameObject();
            canvasGO.name = CANVAS_NAME;
            Canvas canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            return canvasGO;
        }
    }
}