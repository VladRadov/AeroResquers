using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController
{
    private Plane _plane;
    private PlaneView _planeView;
    private float _currentGravity;

    public PlaneController(Plane plane, PlaneView planeView)
    {
        _plane = plane;
        _planeView = planeView;
        _currentGravity = _planeView.CurrentGravity;
    }

    public void FlyingUp()
    {
        _planeView.UpdateForce(_plane.ForceUp);
    }

    public void SetPlaneStatic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Static);
    }

    public void SetPlaneDynamic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Dynamic);
        _planeView.SetGravity(_currentGravity);
    }

    public void LossHeight(float speedIncreaseGravity)
    {
        _currentGravity += speedIncreaseGravity;
        _planeView.SetGravity(_currentGravity);
    }

    public void RecoveryHeight()
        => _currentGravity = 1;
}
