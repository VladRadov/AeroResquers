using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Plane
{
    private Vector2 _forceUp;
    private bool _isFall;

    public Plane(Vector2 forceUp)
    {
        _forceUp = forceUp;
        _isFall = false;
    }

    public Vector2 ForceUp => _forceUp;
    public bool IsFall { get { return _isFall; } set { _isFall = value; } }
}
