using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendMonsterFighter : MonoBehaviour
{
    public Func<int> getIniciativeEvent;
    public Action<MonstersTemp, int> sendMonster;

    [SerializeField] private SearchableDropDown _searchDrop;
    [SerializeField] private ReadMonsters _monsters;
    [SerializeField] private SendInt _iniciativeText;

    private int _iniciative;

    private void OnEnable()
    {
        _iniciativeText.SuscriptionFunc(ref getIniciativeEvent);
    }

    private void OnDisable()
    {
        _iniciativeText.DesuscriptionFunc(ref getIniciativeEvent);
    }

    public void SendMonster()
    {
        MonstersTemp monsterTmp;
        monsterTmp = _monsters.SearchMonster(_searchDrop.GetValue());
        _iniciative = (int)getIniciativeEvent?.Invoke();
        if (monsterTmp == null || _iniciative < -400)
        {
            Debug.Log("Invalid input.");
            return;
        }
        Debug.Log($"{monsterTmp.name} send");
        sendMonster?.Invoke(monsterTmp,_iniciative);
    }
}