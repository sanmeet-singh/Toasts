using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToasts
{
    [RequireComponent(typeof(Image))]
    public class Toast : MonoBehaviour
    {
        public const float DURATION_SHORT = 2f;
        public const float DURATION_LONG = 4f;

        public const string TOASTS_TEXT = "ToastText";
        public const string DEFAULT_FONT_NAME = "Arial.ttf";

        private Text textBox;

        private Image image;

        private AnimationState animationState;

        private Color tempColor;

        private enum AnimationState
        {
            Stop,
            Appearing,
            Disappearing
        }

        private float tempAlpha;
        private float totalDuration;

        internal void DisplayToast(string displayText, Toasts.ToastDuration toastDuration, Toasts.ToastAlignment toastAlignment)
        {
            this.animationState = AnimationState.Stop;

            this.totalDuration = toastDuration == Toasts.ToastDuration.Short ? DURATION_SHORT : DURATION_LONG;

            this.image = this.gameObject.GetComponent<Image>();

            CreateText(displayText);
            StartCoroutine(CheckForDimensions(toastAlignment));
        }

        private IEnumerator CheckForDimensions(Toasts.ToastAlignment toastAlignment)
        {
            yield return new WaitForEndOfFrame();

            //width
            float maxWidth = Screen.width * 0.7f;
            if (this.gameObject.GetComponent<RectTransform>().sizeDelta.x > maxWidth)
            {
                this.gameObject.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, maxWidth);
            }

            yield return new WaitForEndOfFrame();

            float maxHeight = Screen.height * 0.5f;
            if (this.gameObject.GetComponent<RectTransform>().sizeDelta.y > maxHeight)
            {
                this.gameObject.GetComponent<ContentSizeFitter>().horizontalFit = ContentSizeFitter.FitMode.Unconstrained;
                this.gameObject.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, maxHeight);
            }

            yield return new WaitForEndOfFrame();

            SetToastAlignment(toastAlignment);

            this.animationState = AnimationState.Appearing;
        }

        private void SetToastAlignment(Toasts.ToastAlignment toastAlignment)
        {
            RectTransform rectTransform = this.gameObject.GetComponent<RectTransform>();
            //set anchors
            switch (toastAlignment)
            {
                case Toasts.ToastAlignment.Top:
                    rectTransform.anchorMin = new Vector2(0.5f, 1);
                    rectTransform.anchorMax = new Vector2(0.5f, 1);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.anchoredPosition3D = new Vector3(0, -rectTransform.sizeDelta.y);
                    break;

                case Toasts.ToastAlignment.Bottom:
                    rectTransform.anchorMin = new Vector2(0.5f, 0);
                    rectTransform.anchorMax = new Vector2(0.5f, 0);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.anchoredPosition3D = new Vector3(0, rectTransform.sizeDelta.y);
                    break;

                default:
                    rectTransform.anchorMin = new Vector2(0.5f, 0.5f);
                    rectTransform.anchorMax = new Vector2(0.5f, 0.5f);
                    rectTransform.pivot = new Vector2(0.5f, 0.5f);
                    rectTransform.anchoredPosition3D = new Vector3(0, 0, 0);
                    break;
            }
        }

        private void CreateText(string displayText)
        {
            GameObject textGO = new GameObject();
            textGO.name = TOASTS_TEXT;
            textGO.transform.parent = this.transform;

            RectTransform rectTransform = textGO.AddComponent<RectTransform>();
            rectTransform.localPosition = new Vector3(0, 0, 0);

            Text text = textGO.AddComponent<Text>();
            text.text = displayText;
            this.tempColor = ToastSettings.Instance.toastTextColor;
            this.tempColor.a = 0;
            text.color = this.tempColor;
            text.fontSize = ToastSettings.Instance.fontSize;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;

            text.font = ToastSettings.Instance.toastFont == null ? Resources.GetBuiltinResource(typeof(Font), DEFAULT_FONT_NAME) as Font : ToastSettings.Instance.toastFont;

            this.textBox = text;
        }

        private void Update()
        {
            if (this.animationState == AnimationState.Appearing)
            {
                if (this.image.color.a < 1)
                {
                    this.tempAlpha += Time.deltaTime / this.totalDuration;

                    this.tempColor = this.image.color;
                    this.tempColor.a = this.tempAlpha;

                    this.image.color = tempColor;

                    this.tempColor = this.textBox.color;
                    this.tempColor.a = this.tempAlpha * 2;

                    this.textBox.color = this.tempColor;
                }
                else
                {
                    this.animationState = AnimationState.Disappearing;
                }
            }
            else if (this.animationState == AnimationState.Disappearing)
            {
                if (this.image.color.a > 0)
                {
                    this.tempAlpha -= Time.deltaTime / this.totalDuration;

                    this.tempColor = this.image.color;
                    this.tempColor.a = this.tempAlpha;

                    this.image.color = tempColor;

                    this.tempColor = this.textBox.color;
                    this.tempColor.a = this.tempAlpha;

                    this.textBox.color = this.tempColor;
                }
                else
                {
                    this.animationState = AnimationState.Stop;
                    Destroy(this.transform.parent.gameObject);
                }
            }
        }
    }
}
