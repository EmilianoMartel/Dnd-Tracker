using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTextLogic : MonoBehaviour
{
    /// <summary>
    /// If the bool is true move the fighter one position to 0, else move the fighter one position to the last index.
    /// </summary>
    public Action<int, bool> indexChange;

    [SerializeField] private ButtonSendInput _button;
    [SerializeField] private TurnLogic _turnLogic;
    [SerializeField] private TurnViewPort _turnPrefab;

    private TurnViewPort _textTemp;
    private List<TurnViewPort> _turnViewList = new List<TurnViewPort>();

    private void OnEnable()
    {
        _button.sendCharacters += NewText;
        _turnLogic.fightOrder += UpdateList;
    }

    private void OnDisable()
    {
        _button.sendCharacters -= NewText;
        _turnLogic.fightOrder -= UpdateList;
        for (int i = 0; i < _turnViewList.Count; i++)
        {
            _turnViewList[i].upEvent -= MoveToUp;
            _turnViewList[i].downEvent -= MoveDown;
        }
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
        _textTemp = Instantiate(_turnPrefab, transform.position, Quaternion.identity);
        _textTemp.ChangeName(name);
        _textTemp.ChangeIniciative(iniciative);
        _textTemp.ChangeDex(dex);
        _textTemp.transform.parent = transform;
        _turnViewList.Add(_textTemp);
        _textTemp.upEvent += MoveToUp;
        _textTemp.downEvent += MoveDown;
    }

    private void UpdateList(List<Fighter> names)
    {
        if (names.Count != _turnViewList.Count)
        {
            Debug.LogError($"{name}: List have different lenght. Check this");
            return;
        }
        for (int i = 0; i < _turnViewList.Count; i++)
        {
            _turnViewList[i].ChangeName(names[i].nameFighter);
            _turnViewList[i].ChangeIniciative(names[i].iniciative);
            _turnViewList[i].ChangeDex(names[i].dex);
        }
    }

    private void MoveToUp(TurnViewPort moveUp)
    {
        if (_turnViewList.Contains(moveUp))
        {
            int index = _turnViewList.IndexOf(moveUp);
            if (index > 0)
            {
                indexChange?.Invoke(index, true);
            }
        }
    }

    private void MoveDown(TurnViewPort moveDown)
    {
        if (_turnViewList.Contains(moveDown))
        {
            int index = _turnViewList.IndexOf(moveDown);
            if (index < _turnViewList.Count - 1)
            {
                indexChange?.Invoke(index, false);
            }
        }
    }
}
