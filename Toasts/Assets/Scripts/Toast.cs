using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityToasts
{
    public class Toast : MonoBehaviour
    {
        private GameObject toastName;
        private GameObject toastBG;

        private bool animate;

        internal void StartAnimation(GameObject toastName, GameObject toastBG)
        {
            this.toastName = toastName;
            this.toastBG = toastBG;

            animate = true;
        }

        void Update()
        {
            if (animate)
            {
                Image image = this.toastBG.GetComponent<Image>();
                image.color = new Color(image.color.r, image.color.g, image.color.b, (image.color.a - 0.01f));

                Text text = this.toastName.GetComponent<Text>();
                text.color = new Color(text.color.r, text.color.g, text.color.b, (text.color.a - 0.01f));

                if (image.color.a <= 0)
                {
                    animate = false;
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
