using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastsTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //UnityToasts.Toasts.CreateToast("This is first toast. I hope it will look good on screen.", UnityToasts.Toasts.ToastDuration.Long, UnityToasts.Toasts.ToastAlignment.Top);
        //UnityToasts.Toasts.CreateToast("This is first toast. I hope it will look good on screen.", UnityToasts.Toasts.ToastDuration.Short, UnityToasts.Toasts.ToastAlignment.Bottom);
        UnityToasts.Toasts.CreateToast("This is first toast. I hope it will look good on screen.", UnityToasts.Toasts.ToastDuration.Long, UnityToasts.Toasts.ToastAlignment.Centre);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
