using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnViewPort : MonoBehaviour
{
    public Action<TurnViewPort> upEvent;
    public Action<TurnViewPort> downEvent;

    [SerializeField] private TMPro.TMP_Text _textName;
    [SerializeField] private TMPro.TMP_Text _textIniciative;
    [SerializeField] private TMPro.TMP_Text _textDex;

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
}
