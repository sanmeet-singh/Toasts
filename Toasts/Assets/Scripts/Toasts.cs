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
        public const string TOASTS_BG = "ToastBG";

        public enum ToastDuration
        {
            Short,
            Long
        }

        public enum ToastAlignment
        {
            Top,
            Centre,
            Bottom
        }

        public static void CreateToast(string displayText, ToastDuration toastDuration, ToastAlignment toastAlignment)
        {
            Transform canvas = CreateCanvas().transform;

            GameObject parentGO = CreateToastGameObject(canvas, toastAlignment);

            Toast toast = parentGO.AddComponent<Toast>();
            toast.DisplayToast(displayText, toastDuration, toastAlignment);
        }

        private static GameObject CreateToastGameObject(Transform parent, ToastAlignment toastAlignment)
        {
            GameObject toastGO = new GameObject();
            toastGO.name = TOASTS_NAME;
            toastGO.transform.parent = parent;

            RectTransform rectTransform = toastGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);

            //Content size fitter
            ContentSizeFitter contentSizeFitter = toastGO.AddComponent<ContentSizeFitter>();
            contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
            contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;

            ////Horizontal layout
            VerticalLayoutGroup verticalLayoutGroup = toastGO.AddComponent<VerticalLayoutGroup>();
            verticalLayoutGroup.childControlWidth = true;
            verticalLayoutGroup.childControlHeight = true;
            verticalLayoutGroup.childForceExpandWidth = false;
            verticalLayoutGroup.childForceExpandHeight = false;

            Image image = toastGO.AddComponent<Image>();
            Color bgColor = ToastSettings.Instance.toastBackgroundColor;
            bgColor.a = 0;
            image.color = bgColor;

            return toastGO;
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