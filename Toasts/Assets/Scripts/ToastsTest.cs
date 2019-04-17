using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastsTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        UnityToasts.Toasts.CreateToast("This is first toast. I hope it will look good on screen.", UnityToasts.Toasts.ToastDuration.Long);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
