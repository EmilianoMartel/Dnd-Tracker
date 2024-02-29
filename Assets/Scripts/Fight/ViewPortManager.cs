using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPortManager : MonoBehaviour
{
    /// <summary>
    /// If the bool is true move the fighter one position to 0, else move the fighter one position to the last index.
    /// </summary>
    public Action<int, bool> indexChange;
    public Action<int> indexClicked;

    [SerializeField] private FighterManager _turnLogic;
    [SerializeField] private TurnViewPort _turnPrefab;
    [SerializeField] private InputRead _inputRead;

    //DataSource
    [SerializeField] private FightManagerDataSO _dataSO;

    private TurnViewPort _textTemp;
    private List<TurnViewPort> _turnViewList = new List<TurnViewPort>();

    private void OnEnable()
    {
        _dataSO.viewPortManager = this;
        _turnLogic.sendFighter += NewText;
        _turnLogic.fightOrderEvent += UpdateList;
        _turnLogic.startFightEvent += StartFight;
        _turnLogic.indexTurnEvent += NextTurn;
    }

    private void OnDisable()
    {
        _turnLogic.sendFighter -= NewText;
        _turnLogic.fightOrderEvent -= UpdateList;

        for (int i = 0; i < _turnViewList.Count; i++)
        {
            _turnViewList[i].upEvent -= MoveToUp;
            _turnViewList[i].downEvent -= MoveDown;
            _turnViewList[i].isClicked -= CheckClick;
        }
        _turnLogic.startFightEvent -= StartFight;
        _turnLogic.indexTurnEvent -= NextTurn;
    }

    private void Awake()
    {
        if (!_turnLogic)
        {
            Debug.LogError($"{name}: TurnLogic is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void NewText(Fighter fighter)
    {
        _textTemp = Instantiate(_turnPrefab, transform.position, Quaternion.identity);
        _textTemp.ChangeTextView(fighter);
        _textTemp.transform.parent = transform;
        _turnViewList.Add(_textTemp);
        _textTemp.upEvent += MoveToUp;
        _textTemp.downEvent += MoveDown;
        _textTemp.isClicked += CheckClick;
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
            _turnViewList[i].ChangeTextView(names[i]);
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

    private void StartFight()
    {
        for (int i = 0; i < _turnViewList.Count; i++)
        {
            if (i == 0)
            {
                _turnViewList[i].IsMyTurn();
            }
            else
            {
                _turnViewList[i].PassTurn();
            }
        }
    }

    private void NextTurn(int index)
    {
        if (index < 0 || index >= _turnViewList.Count)
        {
            Debug.LogError($"{name}: index overflow to list.");
            return;
        }
        if (index == 0)
        {
            _turnViewList[index].IsMyTurn();
            _turnViewList[_turnViewList.Count - 1].PassTurn();
        }
        else
        {
            _turnViewList[index].IsMyTurn();
            _turnViewList[index - 1].PassTurn();
        }
    }

    private void CheckClick(TurnViewPort onClickTurn)
    {
        if (_turnViewList.Contains(onClickTurn))
        {
            int index = _turnViewList.IndexOf(onClickTurn);
            if (index > 0)
            {
                indexClicked?.Invoke(index);
            }
        }
    }
}