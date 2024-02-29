using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class Fighter
{
    public enum Tag
    {
        custom,
        character,
        monster
    }

    private string _name;
    private string _nameFighter;
    private int _initciative;
    private int _dex;
    private Tag _tag;
    private int _maxLife;
    private int _actualLife;
    private int _ac;
    private MonstersTemp _monster;
    private Character _character;

    public string realName { get { return _nameFighter; } set { _nameFighter = value; } }
    public string nameFighter { get { return _nameFighter; } set { _nameFighter = value; } }
    public int iniciative { get { return _initciative; } set { _initciative = value; } }
    public int dex { get { return _dex; } set { _dex = value; } }
    public Tag tagFighter { get { return _tag; } set { _tag = value; } }
    public int maxLife { get { return _maxLife; } }
    public int actualLife { get { return _actualLife; } set { _actualLife = value; } }
    public int aC { get { return _ac; } }
    public MonstersTemp monsterData { get { return _monster; } set { _monster = value; } }
    public Character characterData { get { return _character; } set { _character = value; } }

    public void Damage(int damage)
    {
        _actualLife -= damage;
    }

    public void Healing(int health)
    {
        _actualLife += health;
        if (_actualLife > _maxLife)
        {
            _actualLife = _maxLife;
        }
    }

    public void FullHealth()
    {
        _actualLife = _maxLife;
    }

    public void ChangeMaxLife(int maxLife)
    {
        _maxLife = maxLife;
        _actualLife = maxLife;
    }

    public void CustomFighter(int maxLifeTemp, int aCTemp, int dexTemp)
    {
        _maxLife = maxLifeTemp;
        _ac = aCTemp;
        _dex = dexTemp;
        _actualLife = _maxLife;
    }

    public void SetParameters()
    {
        switch (_tag)
        {
            case Tag.custom:
                break;
            case Tag.character:
                _dex = characterData.dexMod;
                _ac = characterData.armorClass;
                _maxLife = characterData.maxLife;
                _actualLife = _maxLife;
                break;
            case Tag.monster:
                _ac = CheckNumber(monsterData.ArmorClass);
                _maxLife = SetMonsterLife(monsterData.HitPoints);
                _actualLife = _maxLife;
                break;
            default:
                break;
        }
    }

    private int CheckNumber(string input)
    {
        string pattern = @"\d+";

        MatchCollection coincidens = Regex.Matches(input, pattern);

        string numbers = "";
        foreach (Match coinciden in coincidens)
        {
            numbers += coinciden.Value;
        }

        int finalNumber;
        if (int.TryParse(numbers, out finalNumber))
        {
            return finalNumber;
        }
        else
        {
            Console.WriteLine("Cant convert the string to int.");
            return 0;
        }
    }

    public void MaxLifeModifier(int newMaxLife)
    {
        _maxLife = newMaxLife;
    }

    static int SetMonsterLife(string input)
    {
        string pattern = @"^(\d+)";

        Match coincidences = Regex.Match(input, pattern);

        if (coincidences.Success && int.TryParse(coincidences.Groups[1].Value, out int number))
        {
            return number;
        }
        else
        {
            Console.WriteLine("Error.");
            return 0;
        }
    }
}
