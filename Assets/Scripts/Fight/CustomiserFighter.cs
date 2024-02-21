using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomiserFighter : MonoBehaviour
{
    public Func<int> getDamageEvent;

    [SerializeField] private SendInt _damageInput;
    public Fighter actualFighter;

    private void OnEnable()
    {
        _damageInput.SuscriptionFunc(ref getDamageEvent);
    }

    private void OnDisable()
    {
        _damageInput.DesuscriptionFunc(ref getDamageEvent);
    }

    public void SendDamage()
    {

    }
}
