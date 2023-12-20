using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CreateNewCharacter : MonoBehaviour
{
    [System.Serializable]
    private class CharacterListContainer
    {
        public List<Character> CharacterList;
    }

    const string FILENAME = "/Characters.json";

    private List<Character> _characterList = new List<Character>();
    private Character _character = new Character();
    string filePath = Application.dataPath + FILENAME;

    private void Awake()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            CharacterListContainer container = JsonUtility.FromJson<CharacterListContainer>(json);

            _characterList = container.CharacterList;
        }
    }

    private void NewCharacter()
    {
        _character.nameCharacter = "";
    }
}
