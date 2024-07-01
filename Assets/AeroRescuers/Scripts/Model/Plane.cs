using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class Plane
{
    private Vector2 _forceUp;

    public Plane(Vector2 forceUp)
    {
        _forceUp = forceUp;
    }

    public Vector2 ForceUp => _forceUp;
}
