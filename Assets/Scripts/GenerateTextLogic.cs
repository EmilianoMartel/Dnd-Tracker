using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTextLogic : MonoBehaviour
{
    [SerializeField] private ButtonSendInput _button;
    [SerializeField] private TurnViewPort _textPrefab;
    [SerializeField] private TurnLogic _turnLogic;

    private GameObject _temp;
    private TurnViewPort _textTemp;
    private List<TurnViewPort> _textList = new List<TurnViewPort>();

    private void OnEnable()
    {
        _button.sendCharacters += NewText;
        _turnLogic.fightOrder += UpdateList;
    }

    private void OnDisable()
    {
        _button.sendCharacters -= NewText;
        _turnLogic.fightOrder -= UpdateList;
    }

    private void Awake()
    {
        if (!_button)
        {
            Debug.LogError($"{name}: button is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_turnLogic)
        {
            Debug.LogError($"{name}: TurnLogic is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    private void NewText(string name, int iniciative, int dex)
    {
        _textTemp = Instantiate(_textPrefab, transform.position, Quaternion.identity);
        _textTemp.ChangeName(name);
        _textTemp.ChangeIniciative(iniciative);
        _textTemp.ChangeDex(dex);
        _textTemp.transform.parent = transform;
        _textList.Add(_textTemp);
    }

    private void UpdateList(List<Fighter> names)
    {
        if (names.Count != _textList.Count)
        {
            Debug.LogError($"{name}: List have different lenght. Check this");
            return;
        }
        for (int i = 0; i < _textList.Count; i++)
        {
            _textList[i].ChangeName(names[i].name);
            _textList[i].ChangeIniciative(names[i].initciative);
            _textList[i].ChangeDex(names[i].dex);
        }
    }
}
