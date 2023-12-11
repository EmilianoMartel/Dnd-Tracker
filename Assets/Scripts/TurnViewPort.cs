using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnViewPort : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _textName;
    [SerializeField] private TMPro.TMP_Text _textIniciative;
    [SerializeField] private TMPro.TMP_Text _textDex;

    public void ChangeName(string name)
    {
        _textName.text = name;
    }

    public void ChangeIniciative(int iniciative)
    {
        _textIniciative.text = iniciative.ToString();
    }

    public void ChangeDex(int dex)
    {
        _textDex.text = dex.ToString();
    }
}
