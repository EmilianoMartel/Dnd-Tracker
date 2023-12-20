using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class Races
{
    public enum Size
    {
        Small,
        Medium,
        Large,
    }
    public string name;
    public AbilityScoreIncrease AbilityScoreIncrease;
    public string age;
    public Size size;
    public int speed;
    public List<string> languages;
    public List<Feactures> skills;
}

public class AbilityScoreIncrease
{
    public int STR;
    public int DEX;
    public int CON;
    public int INT;
    public int WIS;
    public int CHA;
}

public class SubRaces
{
    public string name;
    public AbilityScoreIncrease AbilityScoreIncrease;
}

public class Feactures
{
    public string name;
    public string content;
}
