using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToasts
{
    public class Toast : MonoBehaviour
    {
        public const float ALPHA_REDUCTION_VALUE_SHORT = 0.01f;
        public const float ALPHA_REDUCTION_VALUE_LONG = 0.03f;

        //private GameObject toastTextGO;
        //private GameObject toastBG;

        private Image bgImage;

        private Text textBox;

        private bool animate;

        private float newAlpha;
        private float alphaReductionValue;

        internal void StartAnimation(GameObject toastTextGO, GameObject toastBG, Toasts.ToastDuration toastDuration)
        {
            this.textBox = toastTextGO.GetComponent<Text>();
            this.bgImage = toastBG.GetComponent<Image>();

            this.alphaReductionValue = toastDuration == Toasts.ToastDuration.Short ? ALPHA_REDUCTION_VALUE_SHORT : ALPHA_REDUCTION_VALUE_LONG;

            animate = true;
        }

        void Update()
        {
            if (animate)
            {
                this.newAlpha = (this.bgImage.color.a - this.alphaReductionValue);

                this.bgImage.color = new Color(this.bgImage.color.r, this.bgImage.color.g, this.bgImage.color.b, this.newAlpha);

                this.textBox.color = new Color(this.textBox.color.r, this.textBox.color.g, this.textBox.color.b, this.newAlpha);

                if (this.newAlpha <= 0)
                {
                    animate = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
