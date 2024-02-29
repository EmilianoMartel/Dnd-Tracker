using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Damage : MonoBehaviour
{
    /// <summary>
    /// This func send the Damage and Index in list
    /// </summary>
    public Action<int, int> damageEvent;
    private Func<int> getDamage;

    [SerializeField] private FightManagerDataSO _dataSource;
    [SerializeField] private Button _button;
    [SerializeField] private SendInt _damageInput;

    [SerializeField] private float _waitForManager;

    private ViewPortManager _viewPortManager;
    private int _indexDamage = 0;

    private void OnEnable()
    {
        _damageInput.SuscriptionFunc(ref getDamage);
        _button.onClick.AddListener(SendDamage);
        if (_viewPortManager)
        {
            _viewPortManager.indexClicked += SetIndexDamage;
        }
    }

    private void OnDisable()
    {
        _damageInput.DesuscriptionFunc(ref getDamage);
        _button.onClick.RemoveAllListeners();
        if (_viewPortManager)
        {
            _viewPortManager.indexClicked -= SetIndexDamage;
        }
    }

    private void Awake()
    {
        if (!_dataSource)
        {
            Debug.LogError(message: $"{name}: DataSource is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_button)
        {
            Debug.LogError(message: $"{name}: Button is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_damageInput)
        {
            Debug.LogError(message: $"{name}: DamageInput is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _dataSource.damage = this;
        StartCoroutine(SetManager());
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSource.viewPortManager && !_viewPortManager)
        {
            _viewPortManager = _dataSource.viewPortManager;
            _viewPortManager.indexClicked += SetIndexDamage;
        }
    }

    private void SendDamage()
    {
        int damage;
        damage = (int)getDamage?.Invoke();
        damageEvent?.Invoke(damage, _indexDamage);
    }

    private void SetIndexDamage(int index)
    {
        Debug.Log($"Recibio index {index}");
        _indexDamage = index;
    }
}
