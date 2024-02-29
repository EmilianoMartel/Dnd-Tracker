using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerDataSource", menuName = "FightManagerDataSource")]
public class FightManagerDataSO : ScriptableObject
{
    private FighterManager _fighterManager;
    private ViewPortManager _viewPortManager;
    private Damage _damage;

    public FighterManager fighterManager { get { return _fighterManager; } set { _fighterManager = value; } }
    public ViewPortManager viewPortManager { get { return _viewPortManager; } set { _viewPortManager = value;} }
    public Damage damage { get { return _damage; } set { _damage = value; } }
}
