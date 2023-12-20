using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class Classes
{
    public enum HitDice
    {
        d12,
        d10,
        d8,
        d6
    }
    public string name;
    public Dictionary<string, List<string>> basicTable;
    public List<Feactures> feactures;
    public HitDice hitDice;
    public List<Proficiencies> proficiencies;

}
[System.Serializable]
public class Proficiencies
{
    public string name;
    public string content;
}
[System.Serializable]
public class SavingTrows
{
    public bool STR;
    public bool DEX;
    public bool CON;
    public bool INT;
    public bool WIS;
    public bool CHA;
}