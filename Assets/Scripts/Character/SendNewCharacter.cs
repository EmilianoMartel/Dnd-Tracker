using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendNewCharacter : MonoBehaviour
{
    public Action<Character> sendCharacterEvent;

    public Func<string> getNameEvent;
    public Func<int> getstrengthEvent;
    public Func<int> getDexEvent;
    public Func<int> getConstitutionEvent;
    public Func<int> getIntelligenceEvent;
    public Func<int> getWisdomEvent;
    public Func<int> getCharismaEvent;
    public Func<int> getACEvent;
    public Func<int> getMaxLiveEvent;

    [SerializeField] private SendString _nameInput;
    [SerializeField] private SendInt _strengthInput;
    [SerializeField] private SendInt _dexInput;
    [SerializeField] private SendInt _constitutionInput;
    [SerializeField] private SendInt _intelligenceInput;
    [SerializeField] private SendInt _wisdomInput;
    [SerializeField] private SendInt _charismaInput;
    [SerializeField] private SendInt _armorClassInput;
    [SerializeField] private SendInt _maxLifeInput;

    private Character newCharacter = new Character();

    private void OnEnable()
    {
        _nameInput.SuscriptionFunc(ref getNameEvent);
        _strengthInput.SuscriptionFunc(ref getstrengthEvent);
        _dexInput.SuscriptionFunc(ref getDexEvent);
        _constitutionInput.SuscriptionFunc(ref getConstitutionEvent);
        _intelligenceInput.SuscriptionFunc(ref getIntelligenceEvent);
        _wisdomInput.SuscriptionFunc(ref getWisdomEvent);
        _charismaInput.SuscriptionFunc(ref getCharismaEvent);
        _armorClassInput.SuscriptionFunc(ref getACEvent);
        _maxLifeInput.SuscriptionFunc(ref getMaxLiveEvent);
    }

    private void OnDisable()
    {
        _nameInput.DesuscriptionFunc(ref getNameEvent);
        _strengthInput.DesuscriptionFunc(ref getstrengthEvent);
        _dexInput.DesuscriptionFunc(ref getDexEvent);
        _constitutionInput.DesuscriptionFunc(ref getConstitutionEvent);
        _intelligenceInput.DesuscriptionFunc(ref getIntelligenceEvent);
        _wisdomInput.DesuscriptionFunc(ref getWisdomEvent);
        _charismaInput.DesuscriptionFunc(ref getCharismaEvent);
        _armorClassInput.DesuscriptionFunc(ref getACEvent);
        _maxLifeInput.DesuscriptionFunc(ref getMaxLiveEvent);
    }

    private void Awake()
    {
        NullReferenceController();
    }

    private void NullReferenceController()
    {
        if (!_nameInput)
        {
            Debug.LogError($"{name}: Name input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_strengthInput)
        {
            Debug.LogError($"{name}: Strength input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_dexInput)
        {
            Debug.LogError($"{name}: Dex input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_constitutionInput)
        {
            Debug.LogError($"{name}: Constitution input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_intelligenceInput)
        {
            Debug.LogError($"{name}: Intelligence input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_wisdomInput)
        {
            Debug.LogError($"{name}: Wisdom input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_charismaInput)
        {
            Debug.LogError($"{name}: Charisma input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_armorClassInput)
        {
            Debug.LogError($"{name}: ArmorClass input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_maxLifeInput)
        {
            Debug.LogError($"{name}: MaxLife input is null\nCheck this and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    public void SendCharacter()
    {
        newCharacter.nameCharacter = getNameEvent?.Invoke();
        newCharacter.strength = (int)getstrengthEvent?.Invoke();
        newCharacter.dexterity = (int)getDexEvent?.Invoke();
        newCharacter.constitution = (int)getConstitutionEvent?.Invoke();
        newCharacter.intelligence = (int)getIntelligenceEvent?.Invoke();
        newCharacter.wisdom = (int)getWisdomEvent?.Invoke();
        newCharacter.charisma = (int)getCharismaEvent?.Invoke();
        newCharacter.armorClass = (int)getACEvent?.Invoke();
        newCharacter.maxLife  = (int)getMaxLiveEvent?.Invoke();
    }
}