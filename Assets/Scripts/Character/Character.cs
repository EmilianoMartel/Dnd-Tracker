using Newtonsoft.Json;
using Unity.VisualScripting;

[System.Serializable]
public class Character
{
    private string _name;
    private Races _race;
    private Classes _class;

    //Abilities
    private int _strength;
    private int _strengMOD;
    private int _dexterity;
    private int _dexterityMOD;
    private int _constitution;
    private int _constitutionMod;
    private int _intelligence;
    private int _intelligenceMod;
    private int _wisdom;
    private int _wisdomMod;
    private int _charisma;
    private int _charismaMod;
    //SavingTrows

    //Skills

    //Armor Initiative and speed
    private int _armorClass;
    private int _initiativeMod;
    private int _speed;

    private int _maxLife;
    private int _actualLife;

    public string nameCharacter { get { return _name; } set { _name = value; } }
    public int strength { get { return _strength; } set { _strength = value; } }
    public int strMod { get { return _strengMOD; } set { _strengMOD = value; } }
    public int dexterity { get { return _dexterity; } set { _dexterity = value; } }
    public int dexMod { get { return _dexterityMOD; } set { _dexterityMOD = value; } }
    public int constitution { get { return _constitution; } set { _constitution = value; } }
    public int constMOD { get { return _constitutionMod; } set { _constitutionMod = value; } }
    public int intelligence { get { return _intelligence; } set { _intelligence = value; } }
    public int intMOD { get { return _intelligenceMod; } set { _intelligenceMod = value; } }
    public int wisdom { get { return _wisdom; } set { _wisdom = value; } }
    public int wisdomMOD { get { return _wisdomMod; } set { _wisdomMod = value; } }
    public int charisma { get { return _charisma; } set { _charisma = value; } }
    public int charMOD { get { return _charismaMod; } set { _charismaMod = value; } }
    public int armorClass { get { return _armorClass; } set { _armorClass = value; } }
    public int maxLife { get { return _maxLife; } set { _maxLife = value; } }
    public int actualLife { get { return _actualLife; } set { _actualLife = value; } }

    public void BaseModifiers()
    {
        _strengMOD = ModCalculator(_strength) + _race.AbilityScoreIncrease.STR;
        _dexterityMOD = ModCalculator(_dexterity) + _race.AbilityScoreIncrease.DEX;
        _constitutionMod = ModCalculator(_constitution) + _race.AbilityScoreIncrease.CON;
        _intelligenceMod = ModCalculator(_intelligence) + _race.AbilityScoreIncrease.INT;
        _wisdomMod = ModCalculator(_wisdom) + _race.AbilityScoreIncrease.WIS;
        _charismaMod = ModCalculator(_charisma) + _race.AbilityScoreIncrease.CHA;
    }

    static int ModCalculator(int x)
    {
        return (int)(10.0 / 9.0) * (x - 10);
    }
}
