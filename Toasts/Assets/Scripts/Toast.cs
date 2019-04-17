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

        private enum AnimationState
        {
            Stop,
            Appearing,
            Disappearing
        }

        private float tempAlpha;
        private float totalDuration;

        internal void DisplayToast(string displayText, Toasts.ToastDuration toastDuration)
        {
            this.animationState = AnimationState.Stop;

            this.totalDuration = toastDuration == Toasts.ToastDuration.Short ? DURATION_SHORT : DURATION_LONG;

            this.image = this.gameObject.GetComponent<Image>();

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

            this.animationState = AnimationState.Appearing;
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
            Color color = Color.black;
            color.a = 0;
            text.color = color;
            text.fontSize = 25;
            text.horizontalOverflow = HorizontalWrapMode.Wrap;
            text.verticalOverflow = VerticalWrapMode.Truncate;

            text.font = Resources.GetBuiltinResource(typeof(Font), DEFAULT_FONT_NAME) as Font;

            this.textBox = text;
        }

        private void Update()
        {
            if (this.animationState == AnimationState.Appearing)
            {
                if (this.image.color.a < 1)
                {
                    this.tempAlpha += Time.deltaTime / this.totalDuration;

                    Color tempColor = this.gameObject.GetComponent<Image>().color;
                    tempColor.a = this.tempAlpha;

                    this.textBox.color = new Color(this.textBox.color.r, this.textBox.color.g, this.textBox.color.b, tempColor.a);

                    this.image.color = tempColor;
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

                    Color tempColor = this.gameObject.GetComponent<Image>().color;
                    tempColor.a = this.tempAlpha;

                    this.textBox.color = new Color(this.textBox.color.r, this.textBox.color.g, this.textBox.color.b, tempColor.a);

                    this.image.color = tempColor;
                }
                else
                {
                    this.animationState = AnimationState.Stop;
                }
            }
        }
    }
}
