using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadSpells : MonoBehaviour
{
    private List<Spells> _monstersList;
    private static string FILENAME = "spells.json";
    private static string relativeFolder = "Data";
    private string filePath;

    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, relativeFolder, FILENAME);

        try
        {
            if (File.Exists(filePath))
            {
                _monstersList = JsonConvert.DeserializeObject<List<Spells>>(File.ReadAllText(filePath));
            }
            else
            {
                Debug.LogError($"File dont exist: {filePath}");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"Error to charge JSON: {e.Message}");
        }
    }

    [ContextMenu("Show Spells")]
    private void ShowSpells()
    {
        Debug.Log($"{_monstersList[0].name}");
    }
}
