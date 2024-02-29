using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMaxLife : MonoBehaviour
{    /// <summary>
     /// This func send the MaxLife and Index in list
     /// </summary>
    public Action<int, int> changeMaxLifeEvent;
    private Func<int> getLife;

    [SerializeField] private FightManagerDataSO _dataSource;
    [SerializeField] private Button _button;
    [SerializeField] private SendInt _maxLifeInput;

    [SerializeField] private float _waitForManager = 2;

    private ViewPortManager _viewPortManager;
    private int _index = 0;

    private void OnEnable()
    {
        _maxLifeInput.SuscriptionFunc(ref getLife);
        _button.onClick.AddListener(SendHealth);
        if (_viewPortManager)
        {
            _viewPortManager.indexClicked += SetIndexHealth;
        }
    }

    private void OnDisable()
    {
        _maxLifeInput.DesuscriptionFunc(ref getLife);
        _button.onClick.RemoveAllListeners();
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
        if (!_button)
        {
            Debug.LogError(message: $"{name}: Button is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        if (!_maxLifeInput)
        {
            Debug.LogError(message: $"{name}: DamageInput is null\n Check and assigned one\nDisabling component");
            enabled = false;
            return;
        }
        _dataSource.changeMaxLife = this;
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
        health = (int)getLife?.Invoke();
        changeMaxLifeEvent?.Invoke(health, _index);
    }

    private void SetIndexHealth(int index)
    {
        _index = index;
    }
}
