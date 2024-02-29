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

    private FighterManager _fighterManager;
    [SerializeField] private TurnViewPort _turnPrefab;
    [SerializeField] private InputRead _inputRead;

    //DataSource
    [SerializeField] private FightManagerDataSO _dataSO;
    [SerializeField] private float _waitForManager;

    private TurnViewPort _textTemp;
    private List<TurnViewPort> _turnViewList = new List<TurnViewPort>();

    private void OnEnable()
    {
        _dataSO.viewPortManager = this;
        if (_fighterManager)
        {
            _fighterManager.sendFighter += NewText;
            _fighterManager.fightOrderEvent += UpdateList;
            _fighterManager.startFightEvent += StartFight;
            _fighterManager.indexTurnEvent += NextTurn;
        }
    }

    private void OnDisable()
    {
        if (_fighterManager)
        {
            _fighterManager.sendFighter -= NewText;
            _fighterManager.fightOrderEvent -= UpdateList;
            _fighterManager.startFightEvent -= StartFight;
            _fighterManager.indexTurnEvent -= NextTurn;
            _fighterManager.changeLife -= ChangeLife;
        }

        for (int i = 0; i < _turnViewList.Count; i++)
        {
            _turnViewList[i].upEvent -= MoveToUp;
            _turnViewList[i].downEvent -= MoveDown;
            _turnViewList[i].isClicked -= CheckClick;
        }
    }

    private void Awake()
    {
        if (!_dataSO)
        {
            Debug.LogError($"{name}: DataSource is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_turnPrefab)
        {
            Debug.LogError($"{name}: TurnViewPortPrefab is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_inputRead)
        {
            Debug.LogError($"{name}: InputRead is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }

        _dataSO.viewPortManager = this;
        StartCoroutine(SetManager());
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSO.fighterManager && !_fighterManager)
        {
            _fighterManager = _dataSO.fighterManager;
            _fighterManager.sendFighter += NewText;
            _fighterManager.fightOrderEvent += UpdateList;
            _fighterManager.startFightEvent += StartFight;
            _fighterManager.indexTurnEvent += NextTurn;
            _fighterManager.changeLife += ChangeLife;
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
            if (index >= 0)
            {
                Debug.Log($"The index is {index}");
                indexClicked?.Invoke(index);
            }
        }
    }

    private void ChangeLife(int index, int life)
    {
        if (index >= _turnViewList.Count || index < 0)
        {
            Debug.LogError($"{name}: The index number is out of range");
            return;
        }

        _turnViewList[index].ChangeLife(life);
    }
}