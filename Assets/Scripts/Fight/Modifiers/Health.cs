using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    /// <summary>
    /// This func send the Health and Index in list
    /// </summary>
    public Action<int, int> healthEvent;
    public Action<int> fullHealthEvent;
    private Func<int> getHealth;

    [SerializeField] private FightManagerDataSO _dataSource;
    [SerializeField] private Button _buttonHealth;
    [SerializeField] private Button _fullHealthButton;
    [SerializeField] private SendInt _healthInput;

    [SerializeField] private float _waitForManager = 2;

    private ViewPortManager _viewPortManager;
    private int _index = 0;

    private void OnEnable()
    {
        _healthInput.SuscriptionFunc(ref getHealth);
        _buttonHealth.onClick.AddListener(SendHealth);
        _fullHealthButton.onClick.AddListener(SendHealth);
        if (_viewPortManager)
        {
            _viewPortManager.indexClicked += SetIndexHealth;
        }
    }

    private void OnDisable()
    {
        _healthInput.DesuscriptionFunc(ref getHealth);
        _buttonHealth.onClick.RemoveAllListeners();
        _fullHealthButton.onClick.RemoveAllListeners();
        if (_viewPortManager)
        {
            _viewPortManager.indexClicked -= SetIndexHealth;
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
        if (!_buttonHealth)
        {
            Debug.LogError(message: $"{name}: Button is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_healthInput)
        {
            Debug.LogError(message: $"{name}: DamageInput is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _dataSource.health = this;
        StartCoroutine(SetManager());
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSource.viewPortManager && !_viewPortManager)
        {
            _viewPortManager = _dataSource.viewPortManager;
            _viewPortManager.indexClicked += SetIndexHealth;
        }
    }

    private void SendHealth()
    {
        int health;
        health = (int)getHealth?.Invoke();
        healthEvent?.Invoke(health, _index);
    }

    private void SendFullHealth()
    {
        fullHealthEvent?.Invoke(_index);
    }

    private void SetIndexHealth(int index)
    {
        _index = index;
    }
}
