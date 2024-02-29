using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewButtonsLogic : MonoBehaviour
{
    [SerializeField] private FighterManager _turnLogic;

    //Buttons
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _nextTurnButton;
    [SerializeField] private Button _orderButton;

    //InputSets
    [SerializeField] private GameObject _fightersInput;
    [SerializeField] private GameObject _modifierInput;

    [SerializeField] private float _waitForDesactivate = 3;

    private void OnEnable()
    {
        _turnLogic.startFightEvent += StartFight;
    }

    private void OnDisable()
    {
        _turnLogic.startFightEvent -= StartFight;
    }

    private void Awake()
    {
        if(!_turnLogic)
        {
            Debug.LogError($"{name}: TurnLogic is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_startButton)
        {
            Debug.LogError($"{name}: StarButton is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_nextTurnButton)
        {
            Debug.LogError($"{name}: NextTurn is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_orderButton)
        {
            Debug.LogError($"{name}: OrderButton is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_fightersInput)
        {
            Debug.LogError($"{name}: FighterInput is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_modifierInput)
        {
            Debug.LogError($"{name}: ModifierInput is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        StartCoroutine(DesactivateInputs());
    }

    private IEnumerator DesactivateInputs()
    {
        yield return new WaitForSeconds(_waitForDesactivate);
        _nextTurnButton.gameObject.SetActive(false);
        _modifierInput.gameObject.SetActive(false);
    }

    private void StartFight()
    {
        _nextTurnButton.gameObject.SetActive(true);
        _modifierInput.gameObject.SetActive(true);
        _startButton.gameObject.SetActive(false);
        _orderButton.gameObject.SetActive(false);
        _fightersInput.gameObject.SetActive(false);
    }
}
