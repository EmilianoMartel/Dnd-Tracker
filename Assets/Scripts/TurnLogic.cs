using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    public Action<List<Fighter>> fightOrder;

    [SerializeField] private GenerateTextLogic _fighterText;

    private List<Fighter> _fightersList = new List<Fighter>();

    private void OnEnable()
    {
        _fighterText.newFighter += NewFighter;
    }

    private void OnDisable()
    {
        _fighterText.newFighter -= NewFighter;
    }

    private void Awake()
    {
        if (!_fighterText)
        {
            Debug.LogError($"{name}: TextLogic is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void NewFighter(Fighter fighter)
    {
        _fightersList.Add(fighter);
    }

    [ContextMenu("Order Text")]
    public void OrderText()
    {
        _fightersList = _fightersList.OrderByDescending(chara => chara.iniciative)
                                     .ThenByDescending(chara => chara.dex)
                                     .ToList();

        fightOrder?.Invoke(_fightersList);
    }
}