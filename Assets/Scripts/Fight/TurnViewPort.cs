using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurnViewPort : MonoBehaviour
{
    public Action<TurnViewPort> upEvent;
    public Action<TurnViewPort> downEvent;

    [SerializeField] private TMPro.TMP_Text _textName;
    [SerializeField] private TMPro.TMP_Text _textIniciative;
    [SerializeField] private TMPro.TMP_Text _textDex;

    [SerializeField] private Image _image;
    [SerializeField] private Color _colorTurn;
    [SerializeField] private Color _colorNone;

    public void ChangeName(string name)
    {
        _textName.text = name;
    }

    public void ChangeIniciative(int iniciativeTemp)
    {
        _textIniciative.text = iniciativeTemp.ToString();
    }

    public void ChangeDex(int dexTemp)
    {
        _textDex.text = dexTemp.ToString();
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
}
