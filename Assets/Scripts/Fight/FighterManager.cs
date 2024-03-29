using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class FighterManager : MonoBehaviour
{
    public Action<List<Fighter>> fightOrderEvent;
    /// <summary>
    /// This event send the index list and the new life
    /// </summary>
    public Action<int, int> changeLife;
    public Action<int> indexTurnEvent;
    public Action startFightEvent;
    public Action<Fighter> sendFighter;

    [SerializeField] private ButtonSendInput _buttonCustom;
    [SerializeField] private SendMonsterFighter _buttonSendFighter;

    //Managers
    private ViewPortManager _viewPortManager;

    //Modifiers
    private Damage _damage;
    private Health _health;
    private ChangeMaxLife _changeMaxLife;

    //DataSource
    [SerializeField] private FightManagerDataSO _dataSO;
    [SerializeField] private float _waitForManager;

    [SerializeField] private string _monsterTag;
    [SerializeField] private string _customTag;
    [SerializeField] private string _playerTag;

    private List<Fighter> _fightersList = new List<Fighter>();
    private int index = 0;

    private void OnEnable()
    {
        _dataSO.fighterManager = this;
        _buttonCustom.sendFighters += NewFighter;
        _buttonSendFighter.sendMonster += NewFighter;
        if (_viewPortManager)
        {
            _viewPortManager.indexChange += MoveFighters;
        }
        if (_damage)
        {
            _damage.damageEvent += DamageFighter;
        }
        if (_health)
        {
            _health.healthEvent += HealthFighter;
            _health.fullHealthEvent += FullHealth;
        }
        if (_changeMaxLife)
        {
            _changeMaxLife.changeMaxLifeEvent += MaxLifeChange;
        }
    }

    private void OnDisable()
    {
        _buttonCustom.sendFighters -= NewFighter;
        _buttonSendFighter.sendMonster -= NewFighter;
        if (_viewPortManager)
        {
            _viewPortManager.indexChange -= MoveFighters;
        }
        if (_damage)
        {
            _damage.damageEvent -= DamageFighter;
        }
        if (_health)
        {
            _health.healthEvent -= HealthFighter;
            _health.fullHealthEvent -= FullHealth;
        }
        if (_changeMaxLife)
        {
            _changeMaxLife.changeMaxLifeEvent -= MaxLifeChange;
        }
    }

    private void Awake()
    {
        if (!_buttonCustom)
        {
            Debug.LogError($"{name}: ButtonCustom is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_buttonSendFighter)
        {
            Debug.LogError($"{name}: ButtonSendFighter is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_dataSO)
        {
            Debug.LogError($"{name}: DataSource is null\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }

        _dataSO.fighterManager = this;
        StartCoroutine(SetManager());
    }

    private IEnumerator SetManager()
    {
        yield return new WaitForSeconds(_waitForManager);
        if (_dataSO.viewPortManager && !_viewPortManager)
        {
            _viewPortManager = _dataSO.viewPortManager;
            _viewPortManager.indexChange += MoveFighters;
        }
        if (_dataSO.damage && !_damage)
        {
            _damage = _dataSO.damage;
            _damage.damageEvent += DamageFighter;
        }
        if (_dataSO.health && !_health)
        {
            _health = _dataSO.health;
            _health.healthEvent += HealthFighter;
            _health.fullHealthEvent += FullHealth;
        }
        if (_dataSO.changeMaxLife && !_changeMaxLife)
        {
            _changeMaxLife = _dataSO.changeMaxLife;
            _changeMaxLife.changeMaxLifeEvent += MaxLifeChange;
        }
    }

    private void NewFighter(Fighter newFighter)
    {
        newFighter.tagFighter = Fighter.Tag.custom;
        _fightersList.Add(newFighter);
        sendFighter?.Invoke(newFighter);
    }

    private void NewFighter(Character character, int iniciative)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = character.nameCharacter;
        tempFighter.iniciative = iniciative;
        tempFighter.dex = character.dexterity;
        tempFighter.tagFighter = Fighter.Tag.character;

        tempFighter.characterData = character;

        _fightersList.Add(tempFighter);
        sendFighter?.Invoke(tempFighter);
    }

    private void NewFighter(MonstersTemp monster, int iniciative)
    {
        Fighter tempFighter = new Fighter();
        tempFighter.nameFighter = monster.name;
        tempFighter.iniciative = iniciative;
        string dex = monster.DEX_mod;
        tempFighter.tagFighter = Fighter.Tag.monster;
        dex = dex.Replace("(", "").Replace(")", "").Replace(" ", "");

        int sing = (dex.Contains("+")) ? 1 : -1;

        if (int.TryParse(dex, out int num))
        {
            int result = sing * num;
            tempFighter.dex = result;
        }

        tempFighter.monsterData = monster;

        tempFighter.SetParameters();
        _fightersList.Add(tempFighter);
        sendFighter?.Invoke(tempFighter);
    }

    [ContextMenu("Order Text")]
    public void OrderText()
    {
        _fightersList = _fightersList.OrderByDescending(chara => chara.iniciative)
                                     .ThenByDescending(chara => chara.dex)
                                     .ToList();

        fightOrderEvent?.Invoke(_fightersList);
    }

    private void MoveFighters(int index, bool isMoveUp)
    {
        Fighter tempFighter = new Fighter();
        if (isMoveUp)
        {
            if (index == 0)
            {
                Debug.LogError($"{name}: Index is 0\nYou can�t move to negative position.");
                return;
            }
            tempFighter = _fightersList[index];
            _fightersList[index] = _fightersList[index - 1];
            _fightersList[index - 1] = tempFighter;
        }
        else
        {
            if (index >= _fightersList.Count)
            {
                Debug.LogError($"{name}: Index is similar or greater to list length\nYou can�t move to overflow position.");
                return;
            }
            tempFighter = _fightersList[index];
            _fightersList[index] = _fightersList[index + 1];
            _fightersList[index + 1] = tempFighter;
        }
        fightOrderEvent?.Invoke(_fightersList);
    }

    [ContextMenu("Next Turn")]
    public void NextTurn()
    {
        index++;
        if (index >= _fightersList.Count)
        {
            index = 0;
        }
        indexTurnEvent?.Invoke(index);
    }

    [ContextMenu("Start Fight")]
    public void StartFight()
    {
        startFightEvent?.Invoke();
    }

    private void DamageFighter(int damage, int index)
    {
        if (index >= _fightersList.Count || index < 0)
        {
            Debug.LogError($"{name}: The index number is out of range");
            return;
        }
        _fightersList[index].Damage(damage);

        changeLife?.Invoke(index, _fightersList[index].actualLife);
    }

    private void HealthFighter(int health, int index)
    {
        if (index >= _fightersList.Count || index < 0)
        {
            Debug.LogError($"{name}: The index number is out of range");
            return;
        }
        _fightersList[index].Healing(health);

        changeLife?.Invoke(index, _fightersList[index].actualLife);
    }

    private void FullHealth(int index)
    {
        if (index >= _fightersList.Count || index < 0)
        {
            Debug.LogError($"{name}: The index number is out of range");
            return;
        }
        _fightersList[index].FullHealth();

        changeLife?.Invoke(index, _fightersList[index].actualLife);
    }

    private void MaxLifeChange(int life, int index)
    {
        if (index >= _fightersList.Count || index < 0)
        {
            Debug.LogError($"{name}: The index number is out of range");
            return;
        }

        _fightersList[index].ChangeMaxLife(life);

        changeLife?.Invoke(index, _fightersList[index].actualLife);
    }
}