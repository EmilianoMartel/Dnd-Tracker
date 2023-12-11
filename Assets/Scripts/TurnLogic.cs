using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Fighter
{
    public string name;
    public int initciative;
    public int dex;
    public int diceValue;
}

public class TurnLogic : MonoBehaviour
{
    public Action<List<Fighter>> fightOrder;

    [SerializeField] private ButtonSendInput _button;

    private List<Fighter> _fightersList = new List<Fighter>();
    private Fighter _tempFighter;

    private void OnEnable()
    {
        _button.sendCharacters += NewFighter;
    }

    private void OnDisable()
    {
        _button.sendCharacters += NewFighter;
    }

    private void Awake()
    {
        if (!_button)
        {
            Debug.LogError($"{name}: button is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void NewFighter(string name, int initiative, int dex)
    {
        _tempFighter.name = name;
        _tempFighter.dex = dex;
        _tempFighter.initciative = initiative;
        _tempFighter.diceValue = initiative - dex;
        _fightersList.Add(_tempFighter);
    }

    [ContextMenu("Order Text")]
    public void OrderText()
    {
        List<string> _namesList = new List<string>();
        _fightersList = _fightersList.OrderByDescending(chara => chara.initciative)
                                     .ThenByDescending(chara => chara.dex)
                                     .ToList();

        fightOrder?.Invoke(_fightersList);
    }
}