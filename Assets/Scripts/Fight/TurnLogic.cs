using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TurnLogic : MonoBehaviour
{
    public Action<List<Fighter>> fightOrderEvent;
    public Action<int> indexTurnEvent;
    public Action startFightEvent;
    public Action<string, int, int> sendFighter;

    [SerializeField] private ButtonSendInput _buttonCustom;
    [SerializeField] private SendMonsterFighter _buttonSendFighter;
    [SerializeField] private GenerateTextLogic _generateText;

    [SerializeField] private string _monsterTag;
    [SerializeField] private string _customTag;
    [SerializeField] private string _playerTag;

    private List<Fighter> _fightersList = new List<Fighter>();
    private int index = 0;

    private void OnEnable()
    {
        _buttonCustom.sendCharacters += NewFighter;
        _buttonSendFighter.sendMonster += NewFighter;
        _generateText.indexChange += MoveFighters;
    }

    private void OnDisable()
    {
        _buttonCustom.sendCharacters -= NewFighter;
        _buttonSendFighter.sendMonster -= NewFighter;
        _generateText.indexChange -= MoveFighters;
    }

    private void Awake()
    {
        if (!_buttonCustom)
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
        tempFighter.tagFighter = _customTag;
        _fightersList.Add(tempFighter);
        sendFighter?.Invoke(name, iniciative, dex);
    }

    private void NewFighter(Character character, int iniciative)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = character.nameCharacter;
        tempFighter.iniciative = iniciative;
        tempFighter.dex = character.dexterity;
        tempFighter.tagFighter = _playerTag;
        _fightersList.Add(tempFighter);
        sendFighter?.Invoke(character.nameCharacter, iniciative, character.dexMod);
    }

    private void NewFighter(MonstersTemp monster, int iniciative)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = monster.name;
        tempFighter.iniciative = iniciative;
        string dex = monster.DEX_mod;
        tempFighter.tagFighter = _monsterTag;
        dex = dex.Replace("(", "").Replace(")", "").Replace(" ", "");

        int sing = (dex.Contains("+")) ? 1 : -1;

        if (int.TryParse(dex, out int num))
        {
            int result = sing * num;
            tempFighter.dex = result;
        }
        _fightersList.Add(tempFighter);
        sendFighter?.Invoke(monster.name, iniciative, tempFighter.dex);
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