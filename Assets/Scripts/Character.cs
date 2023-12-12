using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    private string _name;
    private int _strength;
    private int _dexterity;
    private int _constitution;
    private int _intelligence;
    private int _wisdom;
    private int _charisma;
    private int _armorClass;
    private int _maxLife;
    private int _actualLife;

    public string nameCharacter { get { return _name; } set { _name = value; } }
    public int strength { get { return _strength; } set { _strength = value; } }
    public int dexterity { get {  return _dexterity; } set { _dexterity = value; } }
    public int constitution { get { return _constitution; } set { _constitution = value; } }
    public int intelligence { get { return _intelligence; } set { _intelligence = value; } }
    public int wisdom { get { return _wisdom; } set { _wisdom = value; } }
    public int charisma { get { return _charisma; } set { _charisma = value; } }
    public int armorClass { get { return _armorClass; } set { _armorClass = value;} }
    public int maxLife { get { return _maxLife; } set { _maxLife = value; } }
    public int actualLife { get { return _actualLife; } set { _actualLife = value; } }
}
