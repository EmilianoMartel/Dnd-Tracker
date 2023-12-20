using Newtonsoft.Json;
using System.Collections.Generic;

[System.Serializable]
public class Spells
{
    public string casting_time { get; set; }
    public List<string> classes { get; set; }
    public Components components { get; set; }
    public string description { get; set; }
    public string duration { get; set; }
    public string level { get; set; }
    public string name { get; set; }
    public string range { get; set; }
    public bool ritual { get; set; }
    public string school { get; set; }
    public List<string> tags { get; set; }
    public string type { get; set; }
}

public class Components
{
    public bool Material { get; set; }
    public string Raw { get; set; }
    public bool Somatic { get; set; }
    public bool Verbal { get; set; }
}
