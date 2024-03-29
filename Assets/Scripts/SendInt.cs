using Mono.Cecil;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SendInt : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_InputField _inputText;

    private void Awake()
    {
        if (!_inputText)
        {
            Debug.LogError($"{name}: InputText is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private int GetInt()
    {
        if (int.TryParse(_inputText.text, out int result))
        {
            return result;
        }
        else
        {
            _inputText.text = "Invalid input";
            return -400;
        }
    }

    public void SuscriptionFunc(ref Func<int> getString)
    {
        getString += GetInt;
    }

    public void DesuscriptionFunc(ref Func<int> getString)
    {
        getString -= GetInt;
    }
}
