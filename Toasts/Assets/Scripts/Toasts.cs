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
        public const string DEFAULT_FONT_NAME = "Arial.ttf";

        public static void CreateToast(string displayText)
        {
            GameObject textGO = CreateText(CreateCanvas().transform, displayText);
        }

        private static GameObject CreateText(Transform parent, string displayText)
        {
            GameObject textGO = new GameObject();
            textGO.name = TOASTS_NAME;
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