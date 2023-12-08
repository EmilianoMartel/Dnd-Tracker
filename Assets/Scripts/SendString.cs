using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendString : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputText;

    private string GetName()
    {
        return _inputText.text;
    }

    public void SuscriptionFunc(ref Func<string> getString)
    {
        getString += GetName;
    }

    public void DesuscriptionFunc(ref Func<string> getString)
    {
        getString -= GetName;
    }
}
