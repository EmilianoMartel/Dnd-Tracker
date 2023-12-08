using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TurnLogic : MonoBehaviour
{
    struct Fighter
    {
        public string name;
        public int initiative;
        public int dex;
        public int diceValue;
        public TMPro.TMP_Text text;
    }

    [SerializeField] private ButtonSendInput _button;
    [SerializeField] private TMPro.TMP_Text _textPrefab;

    private List<TMPro.TMP_Text> _characterList = new List<TMPro.TMP_Text>();
    private TMPro.TMP_Text _tempText;
    private List<Fighter> _fightersList = new List<Fighter>();
    private Fighter _tempFighter;

    private void OnEnable()
    {
        _button.sendCharacters += NewText;
    }

    private void OnDisable()
    {
        _button.sendCharacters += NewText;
    }

    private void Awake()
    {

    }

    private void NewText(string name, int initiative, int dex)
    {
        _tempFighter.text = Instantiate(_textPrefab, transform.position, Quaternion.identity);
        _tempFighter.text.text = name;
        _tempFighter.name = name;
        _tempFighter.dex = dex;
        _tempFighter.initiative = initiative;
        _tempFighter.diceValue = initiative - dex;
        _tempFighter.text.transform.parent = transform;
        _fightersList.Add(_tempFighter);
    }

    [ContextMenu("Order Text")]
    private void OrderText()
    {
        int previusInitiative = 0;
        Fighter changeCharacter;
        _fightersList = _fightersList.OrderBy(chara => chara.initiative).ToList();

        for (int i = 0; i < _fightersList.Count; i++)
        {
            if (i == 0)
            {
                previusInitiative = _fightersList[i].initiative;
            }
            else
            {
                if (previusInitiative == _fightersList[i].initiative)
                {
                    if (_fightersList[i].dex == _fightersList[i - 1].dex)
                    {
                        if (_fightersList[i].diceValue > _fightersList[i - 1].diceValue)
                        {
                            changeCharacter = _fightersList[i];
                            _fightersList[i] = _fightersList[i - 1];
                            _fightersList[i - 1] = changeCharacter;
                        }
                    }
                    else if (_fightersList[i].dex > _fightersList[i - 1].dex)
                    {
                        changeCharacter = _fightersList[i];
                        _fightersList[i] = _fightersList[i - 1];
                        _fightersList[i - 1] = changeCharacter;
                    }
                }
            }
        }
    }
}