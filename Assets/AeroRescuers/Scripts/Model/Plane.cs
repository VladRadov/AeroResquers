using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Plane
{
    private Vector2 _forceGravity;
    private float _maxHeight;
    private float _currentHeight;
    private bool _isFall;
    private bool _isFly;

    public Plane(Vector2 forceGravity, float maxHeight)
    {
        _forceGravity = forceGravity;
        _maxHeight = maxHeight;
        _isFall = false;
        _isFly = false;
    }

    public Vector2 ForceGravity => _forceGravity;
    public float CurrentHeight => _currentHeight;
    public bool IsFall { get { return _isFall; } set { _isFall = value; } }
    public bool IsFly { get { return _isFly; } set { _isFly = value; } }

    public void LossHeight(float speedIncreaseGravity)
        => _currentHeight -= speedIncreaseGravity;

    public void ChangeRoute()
        => _forceGravity *= -1;

    public void RecoveryHeight()
        => _currentHeight = _maxHeight;
}
