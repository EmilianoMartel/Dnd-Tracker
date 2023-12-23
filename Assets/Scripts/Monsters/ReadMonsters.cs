using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEditor;

public class ReadMonsters : MonoBehaviour
{
    private List<MonstersTemp> _monstersList;
    private static string FILENAME = "Monsters.json";
    private static string relativeFolder = "Data";
    private string filePath;

    public List<MonstersTemp> monsterList { get { return _monstersList; } }

    private void Awake()
    {
        filePath = Path.Combine(Application.dataPath, relativeFolder, FILENAME);

        try
        {
            if (File.Exists(filePath))
            {
                _monstersList = JsonConvert.DeserializeObject<List<MonstersTemp>>(File.ReadAllText(filePath));

                //foreach (Monsters monster in _monstersList)
                //{
                //    Debug.Log($"{monster.name}");
                //}
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

    [ContextMenu("Show Monster")]
    private void ShowMonster()
    {
        Debug.Log($"{_monstersList[0].name}");
    }

    public MonstersTemp SearchMonster(string monster)
    {
        MonstersTemp monsterFound = _monstersList.FirstOrDefault(monsterTemp => monsterTemp.name == monster);
        if (monsterFound != null)
        {
            return monsterFound;
        }
        else
        {
            return null;
        }
    }
}