using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter
{
    private string _name;
    private int _initciative;
    private int _dex;
    private string _tag;
    private int _maxLife;
    private int _actualLife;

    public string nameFighter { get { return _name; } set { _name = value; } }
    public int iniciative { get { return _initciative; } set { _initciative = value; } }
    public int dex { get { return _dex; } set { _dex = value; } }
    public string tagFighter { get { return _tag; } set { _tag = value; } }
    public int maxLife { get { return _maxLife; } set { _maxLife = value; } }
    public int actualLife { get { return _actualLife; } set { _actualLife = value; } }

    public void Damage(int damage)
    {
        _actualLife -= damage;
    }

    public void Healing(int health)
    {
        _actualLife += health;
    }


}
