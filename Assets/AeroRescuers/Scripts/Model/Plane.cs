using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Plane
{
    private Vector2 _forceUp;
    private Vector2 _forceDown;
    private bool _isFall;

    public Plane(Vector2 startForce)
    {
        _forceUp = startForce;
        _forceDown = startForce;
        _isFall = false;
    }

    public Vector2 ForceUp => _forceUp;
    public Vector2 ForceDown => _forceDown;
    public Vector3 ForceFly => _forceUp + _forceDown;
    public bool IsFall { get { return _isFall; } set { _isFall = value; } }

    public void LossHeight(float speedIncreaseGravity)
        => _forceDown -= new Vector2(0, speedIncreaseGravity);

    public void RecoveryHeight()
        => _forceDown = _forceUp;
}
