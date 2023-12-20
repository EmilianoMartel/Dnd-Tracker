using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownAdd : MonoBehaviour
{
    [SerializeField] private DropDownCustom _dropCustom;
    [SerializeField] private string _optionName;

    private void Awake()
    {
        if (!_dropCustom)
        {
            Debug.LogError($"{name}: DropCustom is null\nCheck and assigned one.\nDisabling component");
            enabled = false;
            return;
        }
        _dropCustom.AddNames(this.gameObject, _optionName);
    }
}