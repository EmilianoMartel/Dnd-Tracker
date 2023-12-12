using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    private string _name;
    private int _initciative;
    private int _dex;
    [SerializeField] private TurnViewPort _view;

    public string nameFighter { get { return _name; } set { _name = value; } }
    public int iniciative { get { return _initciative; } set { _initciative = value; } }
    public int dex { get { return _dex; } set { _dex = value; } }
    public TurnViewPort view { get { return _view; } }

    public void ShowFighter()
    {
        _view.ChangeName(_name);
        _view.ChangeIniciative(_initciative);
        _view.ChangeDex(_dex);
    }
}
