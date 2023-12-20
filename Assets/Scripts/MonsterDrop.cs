using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MonsterDrop : MonoBehaviour
{
    [SerializeField] private SearchableDropDown _dropDown;
    [SerializeField] private ReadMonsters _monsters;

    private List<string> _nameList = new List<string>();

    private void Awake()
    {
        if (!_monsters)
        {
            Debug.LogError($"{name}: ReadMonster is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }

    }

    private void Start()
    {
        AddMonstersDrop();
    }

    private void AddMonstersDrop()
    {
        for (int i = 0; i < _monsters.monsterList.Count; i++)
        {
            _nameList.Add(_monsters.monsterList[i].name);
            _dropDown.AddOptionAvl(_monsters.monsterList[i].name);
        }
    }


}
