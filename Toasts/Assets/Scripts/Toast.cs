using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToasts
{
    public class Toast : MonoBehaviour
    {
        public const float ALPHA_REDUCTION_VALUE_SHORT = 0.01f;
        public const float ALPHA_REDUCTION_VALUE_LONG = 0.03f;

        public const string TOASTS_TEXT = "ToastText";
        public const string DEFAULT_FONT_NAME = "Arial.ttf";

        //private GameObject toastTextGO;
        //private GameObject toastBG;

        //private Image bgImage;

        private Text textBox;

        private GameObject textGO;

        private bool animate;

        private float newAlpha;
        private float alphaReductionValue;

        internal void StartAnimation(string displayText, Toasts.ToastDuration toastDuration)
        {
            //this.textBox = toastTextGO.GetComponent<Text>();
            //this.bgImage = toastBG.GetComponent<Image>();

            //this.alphaReductionValue = toastDuration == Toasts.ToastDuration.Short ? ALPHA_REDUCTION_VALUE_SHORT : ALPHA_REDUCTION_VALUE_LONG;

            //animate = true;

            CreateText(displayText);
            StartCoroutine(CheckForDimensions());
        }

        private IEnumerator CheckForDimensions()
        {
            yield return new WaitForEndOfFrame();

            //width
            float maxWidth = Screen.width * 0.7f;
            if (this.gameObject.GetComponent<RectTransform>().sizeDelta.x > maxWidth)
            {
                Debug.Log("Restrict width");
                this.gameObject.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
            }

            yield return new WaitForEndOfFrame();

            float maxHeight = Screen.height * 0.5f;
            Debug.Log(this.gameObject.GetComponent<RectTransform>().sizeDelta.y + " : " + maxHeight);
            if (this.gameObject.GetComponent<RectTransform>().sizeDelta.y > maxHeight)
            {
                Debug.Log("Restrict height");
                this.gameObject.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
            }
        }

        private void CreateText(string displayText)
        {
            textGO = new GameObject();
            textGO.name = TOASTS_TEXT;
            textGO.transform.parent = this.transform;

            RectTransform rectTransform = textGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);

            Text text = textGO.AddComponent<Text>();
            text.text = displayText;
            text.color = Color.black;
            text.fontSize = 25;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;

            text.font = Resources.GetBuiltinResource(typeof(Font), DEFAULT_FONT_NAME) as Font;

            //rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, text.preferredWidth);
            //rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, text.preferredHeight);

            this.textBox = text;
        }

        private void Update()
        {
        }
    }
}
