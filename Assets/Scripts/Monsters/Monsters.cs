using Newtonsoft.Json;

[System.Serializable]
public class Monsters
{
    [Newtonsoft.Json.JsonProperty("name")]
    public string name;
    [Newtonsoft.Json.JsonProperty("meta")]
    public string meta;
    [Newtonsoft.Json.JsonProperty("Armor Class")]
    public string ArmorClass;
    [Newtonsoft.Json.JsonProperty("Hit Points")]
    public string HitPoints;
    public string Speed;
    public string STR;
    public string STR_mod;
    public string DEX;
    public string DEX_mod;
    public string CON;
    public string CON_mod;
    public string INT;
    public string INT_mod;
    public string WIS;
    public string WIS_mod;
    public string CHA;
    public string CHA_mod;
    [Newtonsoft.Json.JsonProperty("Saving Throws")]
    public string SavingThrows;
    public string Skills;
    public string Senses;
    public string Languages;
    public string Challenge;
    public string Traits;
    public string Actions;
    [Newtonsoft.Json.JsonProperty("Legendary Actions")]
    public string LegendaryActions;
    public string img_url;
}
