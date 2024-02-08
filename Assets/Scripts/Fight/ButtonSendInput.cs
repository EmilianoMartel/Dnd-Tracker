using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSendInput : MonoBehaviour
{
    public Func<string> getNameEvent;
    public Func<int> getIniciativeEvent;
    public Func<int> getDexEvent;
    public Func<int> getMaxLifeEvent;
    public Func<int> getACEvent;
    public Action<Fighter> sendFighters;

    [SerializeField] private SendString _nameText;
    [SerializeField] private SendInt _iniciativeText;
    [SerializeField] private SendInt _dexText;
    [SerializeField] private SendInt _maxLifeText;
    [SerializeField] private SendInt _acText;
    
    private string _name;
    private int _iniciative;
    private int _dex;
    private int _maxLife;
    private int _ac;

    private void OnEnable()
    {
        _nameText.SuscriptionFunc(ref getNameEvent);
        _iniciativeText.SuscriptionFunc(ref getIniciativeEvent);
        _dexText.SuscriptionFunc(ref getDexEvent);
        _maxLifeText.SuscriptionFunc(ref getMaxLifeEvent);
        _acText.SuscriptionFunc(ref getACEvent);
    }

    private void OnDisable()
    {
        _nameText.DesuscriptionFunc(ref getNameEvent);
        _iniciativeText.DesuscriptionFunc(ref getIniciativeEvent);
        _dexText.DesuscriptionFunc(ref getDexEvent);
        _maxLifeText.DesuscriptionFunc(ref getMaxLifeEvent);
        _acText.DesuscriptionFunc(ref getACEvent);
    }

    [ContextMenu("Send Text")]
    public void SendText()
    {
        _name = getNameEvent?.Invoke();
        _iniciative = (int)getIniciativeEvent?.Invoke();
        _dex = (int)getDexEvent?.Invoke();
        _maxLife = (int)getMaxLifeEvent?.Invoke();
        _ac = (int)getACEvent?.Invoke();
        if (_dex < -400 || _iniciative < -400 || _maxLife < -400 || _ac < -400)
        {
            Debug.Log("Invalid input.");
            return;
        }
        Debug.Log($"Send text {_name} {_iniciative} {_dex}");


        Fighter tempFighter = new Fighter();
        tempFighter = NewFighter();

        sendFighters?.Invoke(tempFighter);
    }

    private Fighter NewFighter()
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = _name;
        tempFighter.realName = _name;
        tempFighter.CustomFighter(_maxLife,_ac,_dex);
        return tempFighter;
    }
}
