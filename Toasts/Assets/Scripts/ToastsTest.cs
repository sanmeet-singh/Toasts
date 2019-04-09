using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToastsTest : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        UnityToasts.Toasts.CreateToast("First Toast", UnityToasts.Toasts.ToastDuration.Short);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
