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

        public static void CreateToast(string displayText, ToastDuration toastDuration)
        {
            Transform canvas = CreateCanvas().transform;

            GameObject parentGO = CreateToastGameObject(canvas);

            //GameObject backgroundGO = CreateBG(parentGO.transform);
            //GameObject textGO = CreateText(parentGO.transform, displayText);

            //UpdateDimensions(backgroundGO);

            Toast toast = parentGO.AddComponent<Toast>();
            toast.StartAnimation(displayText, toastDuration);
        }

        private static GameObject CreateToastGameObject(Transform parent)
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
            image.color = Color.yellow;

            return toastGO;
        }

        private static GameObject CreateBG(Transform parent)
        {
            GameObject backgroundGO = new GameObject();
            backgroundGO.name = TOASTS_BG;
            backgroundGO.transform.parent = parent;

            RectTransform rectTransform = backgroundGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);
            rectTransform.sizeDelta = new Vector2(322, 64);

            Image image = backgroundGO.AddComponent<Image>();
            image.color = Color.yellow;

            ////Content size fitter
            //ContentSizeFitter contentSizeFitter = backgroundGO.AddComponent<ContentSizeFitter>();
            //contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
            //contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.Unconstrained;

            //////Horizontal layout
            //VerticalLayoutGroup verticalLayoutGroup = backgroundGO.AddComponent<VerticalLayoutGroup>();
            //verticalLayoutGroup.childControlWidth = true;
            //verticalLayoutGroup.childControlHeight = true;
            //verticalLayoutGroup.childForceExpandWidth = false;
            //verticalLayoutGroup.childForceExpandHeight = false;

            return backgroundGO;
        }

        //private static void UseTextGenerator(string displayText, TextGenerationSettings settings, RectTransform textBox)
        //{
        //    //TextGenerationSettings settings = new TextGenerationSettings();
        //    //settings.color = Color.black;
        //    //settings.font = Resources.GetBuiltinResource(typeof(Font), DEFAULT_FONT_NAME) as Font;
        //    //settings.fontSize = 18;
        //    ////settings.
        //    //settings.generateOutOfBounds = true;
        //    //settings.horizontalOverflow = HorizontalWrapMode.Overflow;
        //    ////settings.resizeTextForBestFit = true;
        //    //settings.verticalOverflow = VerticalWrapMode.Overflow;
        //    TextGenerator textGenerator = new TextGenerator();
        //    Debug.Log("TG : " + textGenerator.GetPreferredWidth(displayText, settings) + " : " + textGenerator.GetPreferredHeight(displayText, settings));
        //}

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