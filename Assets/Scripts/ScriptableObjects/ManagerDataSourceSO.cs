using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ManagerDataSource", menuName = "ManagerDataSource")]
public class ManagerDataSourceSO : ScriptableObject
{
    private TurnViewPort _turnViewPort;

    public TurnViewPort turnViewPort { get => _turnViewPort; set => _turnViewPort = value; }
}
