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

    [SerializeField] private TurnLogic _turnLogic;
    [SerializeField] private TurnViewPort _turnPrefab;
    [SerializeField] private InputRead _inputRead;

    private TurnViewPort _textTemp;
    private List<TurnViewPort> _turnViewList = new List<TurnViewPort>();

    private void OnEnable()
    {
        _turnLogic.sendFighter += NewText;
        _turnLogic.fightOrderEvent += UpdateList;
        _turnLogic.startFightEvent += StartFight;
        _turnLogic.indexTurnEvent += NextTurn;
        _inputRead.mousePositionEvent += CheckClick;
    }

    private void OnDisable()
    {
        _turnLogic.sendFighter -= NewText;
        _turnLogic.fightOrderEvent -= UpdateList;
        _inputRead.mousePositionEvent -= CheckClick;

        for (int i = 0; i < _turnViewList.Count; i++)
        {
            _turnViewList[i].upEvent -= MoveToUp;
            _turnViewList[i].downEvent -= MoveDown;
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

    private void CheckClick(Vector3 position)
    {
        Debug.Log("try");

        Ray ray = Camera.main.ScreenPointToRay(position);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Click in: " + hit.collider.gameObject.name);
        }
    }
}