using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownCustom : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown _dropdown;

    private List<GameObject> _namesList = new List<GameObject>();

    private void Awake()
    {
        if (!_dropdown)
        {
            Debug.Log($"{name}: DropDown is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        _dropdown.onValueChanged.AddListener(delegate
        {
            DropdownValueChanged(_dropdown);
        });
    }

    private void DropdownValueChanged(TMP_Dropdown dropdown)
    {
        Debug.Log(dropdown.value);
        for (int i = 0; i < _namesList.Count; i++)
        {
            if (dropdown.value == i)
            {
                _namesList[i].SetActive(true);
            }
            else
            {
                _namesList[i].SetActive(false);
            }
        }
    }

    public void AddNames(GameObject namesObject, string name)
    {
        _namesList.Add(namesObject);
        _dropdown.options.Add(new TMP_Dropdown.OptionData(name));
        namesObject.SetActive(false);
    }
}
