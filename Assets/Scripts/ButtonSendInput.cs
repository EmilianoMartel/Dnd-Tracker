using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSendInput : MonoBehaviour
{
    public Func<string> getNameEvent;
    public Func<int> getIniciativeEvent;
    public Func<int> getDexEvent;
    public Action<string, int, int> sendCharacters;

    [SerializeField] private SendString _nameText;
    [SerializeField] private SendInt _iniciativeText;
    [SerializeField] private SendInt _dexText;

    private string _name;
    private int _iniciative;
    private int _dex;

    private void OnEnable()
    {
        _nameText.SuscriptionFunc(ref getNameEvent);
        _iniciativeText.SuscriptionFunc(ref getIniciativeEvent);
        _dexText.SuscriptionFunc(ref getDexEvent);
    }

    private void OnDisable()
    {
        _nameText.DesuscriptionFunc(ref getNameEvent);
        _iniciativeText.DesuscriptionFunc(ref getIniciativeEvent);
        _dexText.DesuscriptionFunc(ref getDexEvent);
    }

    [ContextMenu("Send Text")]
    public void SendText()
    {
        _name = getNameEvent?.Invoke();
        _iniciative = (int)getIniciativeEvent?.Invoke();
        _dex = (int)getDexEvent?.Invoke();
        if (_dex < -400 || _iniciative < -400)
        {
            Debug.Log("Invalid input.");
            return;
        }
        
        Debug.Log($"Send text {_name} {_iniciative} {_dex}");
        sendCharacters?.Invoke(_name,_iniciative,_dex);
    }
}
