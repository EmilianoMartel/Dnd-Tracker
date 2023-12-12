using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTextLogic : MonoBehaviour
{
    public Action<Fighter> newFighter;

    [SerializeField] private ButtonSendInput _button;
    [SerializeField] private TurnLogic _turnLogic;
    [SerializeField] private Fighter _fighterPrefab;

    private Fighter _textTemp;
    private List<Fighter> _fighterList = new List<Fighter>();

    private void OnEnable()
    {
        _button.sendCharacters += NewText;
        _turnLogic.fightOrder += UpdateList;
    }

    private void OnDisable()
    {
        _button.sendCharacters -= NewText;
        _turnLogic.fightOrder -= UpdateList;
    }

    private void Awake()
    {
        if (!_button)
        {
            Debug.LogError($"{name}: button is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_turnLogic)
        {
            Debug.LogError($"{name}: TurnLogic is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void NewText(string name, int iniciative, int dex)
    {
        _textTemp = Instantiate(_fighterPrefab, transform.position, Quaternion.identity);
        _textTemp.nameFighter = name;
        _textTemp.iniciative = iniciative;
        _textTemp.dex = dex;
        _textTemp.ShowFighter();
        _textTemp.transform.parent = transform;
        _fighterList.Add(_textTemp);
        newFighter?.Invoke(_textTemp);
        //_textTemp.view.upEvent += MoveToUp;
    }

    private void UpdateList(List<Fighter> names)
    {
        if (names.Count != _fighterList.Count)
        {
            Debug.LogError($"{name}: List have different lenght. Check this");
            return;
        }
        for (int i = 0; i < _fighterList.Count; i++)
        {
            //_fighterList[i].ChangeName(names[i].name);
            //_fighterList[i].ChangeIniciative(names[i].iniciative);
            //_fighterList[i].ChangeDex(names[i].dex);
        }
    }

    private void UpdateList(List<TurnViewPort> names)
    {
        for (int i = 0; i < _fighterList.Count; i++)
        {
        }
    }

    //private void MoveToUp(TurnViewPort moveUp)
    //{
    //    Fighter tempFighter;
    //    if (_fighterList.Contains(moveUp))
    //    {
    //        int index = _fighterList.IndexOf(moveUp);
    //        if (index > 0)
    //        {
    //
    //        }
    //    }
    //}
}
