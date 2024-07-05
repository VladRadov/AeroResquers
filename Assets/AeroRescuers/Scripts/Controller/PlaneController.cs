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
        AudioManager.Instance.StopSoundEnginePlane();
    }

    public void SetPlaneDynamic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Dynamic);
        _planeView.SetGravity(_currentGravity);
        AudioManager.Instance.PlayEnginePlane();
    }

    public void LossHeight(float speedIncreaseGravity)
    {
        if (_currentGravity < 7 && _plane.IsFall)
            _plane.IsFall = false;
        else if (_currentGravity >= 7 && _plane.IsFall == false)
        {
            _plane.IsFall = true;
            AudioManager.Instance.PlayAirplaneFall();
        }

        _currentGravity += speedIncreaseGravity;
        _planeView.SetGravity(_currentGravity);
    }

    public void RecoveryHeight()
        => _currentGravity = 1;
}
