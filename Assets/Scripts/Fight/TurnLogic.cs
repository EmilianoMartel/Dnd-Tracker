using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct Fighter
{
    private string _name;
    private int _initciative;
    private int _dex;

    public string nameFighter { get { return _name; } set { _name = value; } }
    public int iniciative { get { return _initciative; } set { _initciative = value; } }
    public int dex { get { return _dex; } set { _dex = value; } }
}

public class TurnLogic : MonoBehaviour
{
    public Action<List<Fighter>> fightOrderEvent;
    public Action<int> indexTurnEvent;
    public Action startFightEvent;

    [SerializeField] private ButtonSendInput _button;
    [SerializeField] private GenerateTextLogic _generateText;

    private List<Fighter> _fightersList = new List<Fighter>();
    private int index = 0;

    private void OnEnable()
    {
        _button.sendCharacters += NewFighter;
        _generateText.indexChange += MoveFighters;
    }

    private void OnDisable()
    {
        _button.sendCharacters -= NewFighter;
        _generateText.indexChange -= MoveFighters;
    }

    private void Awake()
    {
        if (!_button)
        {
            Debug.LogError($"{name}: Button is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_generateText)
        {
            Debug.LogError($"{name}: Generate Text is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void NewFighter(string name, int iniciative, int dex)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = name;
        tempFighter.iniciative = iniciative;
        tempFighter.dex = dex;
        _fightersList.Add(tempFighter);
    }

    private void NewFighter(Character character, int iniciative)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = character.nameCharacter;
        tempFighter.iniciative = iniciative;
        tempFighter.dex = character.dexterity;
        _fightersList.Add(tempFighter);
    }

    private void NewFighter(Monsters monster, int iniciative)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = monster.name;
        tempFighter.iniciative = iniciative;
        string dex = monster.DEX_mod;
        dex = dex.Replace("(", "").Replace(")", "").Replace(" ", "");

        int sing = (dex.Contains("+")) ? 1 : -1;

        if (int.TryParse(dex, out int num))
        {
            int result = sing * num;
            tempFighter.dex = result;
        }

        _fightersList.Add(tempFighter);
    }

    [ContextMenu("Order Text")]
    public void OrderText()
    {
        _fightersList = _fightersList.OrderByDescending(chara => chara.iniciative)
                                     .ThenByDescending(chara => chara.dex)
                                     .ToList();

        fightOrderEvent?.Invoke(_fightersList);
    }

    private void MoveFighters(int index, bool isMoveUp)
    {
        Fighter tempFighter = new Fighter();
        if (isMoveUp)
        {
            if (index == 0)
            {
                Debug.LogError($"{name}: Index is 0\nYou can´t move to negative position.");
                return;
            }
            tempFighter = _fightersList[index];
            _fightersList[index] = _fightersList[index - 1];
            _fightersList[index - 1] = tempFighter;
        }
        else
        {
            if (index >= _fightersList.Count)
            {
                Debug.LogError($"{name}: Index is similar or greater to list length\nYou can´t move to overflow position.");
                return;
            }
            tempFighter = _fightersList[index];
            _fightersList[index] = _fightersList[index + 1];
            _fightersList[index + 1] = tempFighter;
        }
        fightOrderEvent?.Invoke(_fightersList);
    }

    [ContextMenu("Next Turn")]
    public void NextTurn()
    {
        index++;
        if (index >= _fightersList.Count)
        {
            index = 0;
        }
        indexTurnEvent?.Invoke(index);
    }

    [ContextMenu("Start Fight")]
    public void StartFight()
    {
        startFightEvent?.Invoke();
    }
}