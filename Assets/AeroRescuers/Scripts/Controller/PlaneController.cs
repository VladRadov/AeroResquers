using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController
{
    private Plane _plane;
    private PlaneView _planeView;
    private const int _speedRotation = 5;
    private Vector3 _tragetPosition;

    public PlaneController(Plane plane, PlaneView planeView)
    {
        _plane = plane;
        _planeView = planeView;
    }

    public void FlyingUp()
        => _planeView.UpdateForce(_plane.ForceFly);

    public void RotationPlane(int speedRotation, float sensitivityRotation)
    {
        var angleRotation = Quaternion.Lerp(_planeView.transform.rotation, Quaternion.Euler(0, 0, _plane.ForceFly.y * speedRotation), sensitivityRotation);
        _planeView.Rotation(angleRotation);
    }

    public void SetPlaneStatic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Static);
        AudioManager.Instance.StopSoundEnginePlane();
    }

    public void SetPlaneDynamic()
    {
        _planeView.SetBodyType(RigidbodyType2D.Dynamic);
        AudioManager.Instance.PlayEnginePlane();
    }

    public void LossHeight(float speedIncreaseGravity)
    {
        if (_plane.ForceDown.y > -1.5 && _plane.IsFall)
            _plane.IsFall = false;
        else if (_plane.ForceDown.y <= -1.5 && _plane.IsFall == false)
        {
            _plane.IsFall = true;
            AudioManager.Instance.PlayAirplaneFall();
        }

        _plane.LossHeight(speedIncreaseGravity);
    }

    public void StartFly()
    {
        _tragetPosition = new Vector3(-193, 28, 0);
        _planeView.PlayAnimationUp();
    }

    public void Flying()
    {
        if (Vector3.Distance(_planeView.transform.localPosition, _tragetPosition) <= 0.1)
        {
            _plane.IsFly = true;
            SetPlaneDynamic();
        }
        else
        {
            var target = Vector3.MoveTowards(_planeView.transform.localPosition, _tragetPosition, 0.7f);
            _planeView.UpdateLocalPosition(target);
        }
    }

    public void RecoveryHeight()
        => _plane.RecoveryHeight();
}
