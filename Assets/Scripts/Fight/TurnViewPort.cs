using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class TurnViewPort : MonoBehaviour
{
    public Action<TurnViewPort> isClicked;
    public Action<TurnViewPort> upEvent;
    public Action<TurnViewPort> downEvent;

    [SerializeField] private TMPro.TMP_Text _textName;
    [SerializeField] private TMPro.TMP_Text _textAC;
    [SerializeField] private TMPro.TMP_Text _textLife;
    [SerializeField] private FightManagerDataSO _managerDataSourceSO;

    [SerializeField] private Image _image;
    [SerializeField] private Color _colorTurn;
    [SerializeField] private Color _colorNone;

    public void ChangeName(string name)
    {
        _textName.text = name;
    }

    public void MoveUp()
    {
        upEvent?.Invoke(this);
    }

    public void MoveDown()
    {
        downEvent?.Invoke(this);
    }

    [ContextMenu("Is My Turn")]
    public void IsMyTurn()
    {
        _image.color = _colorTurn;
    }

    [ContextMenu("Isn´t My Turn")]
    public void PassTurn()
    {
        _image.color = _colorNone;
    }

    public void ChangeTextView(Fighter fighter)
    {
        ChangeName(fighter.nameFighter);
        _textAC.text = fighter.aC.ToString();
        _textLife.text = fighter.actualLife.ToString();
    }

    public void OnClick()
    {
        isClicked?.Invoke(this);
    }
}
