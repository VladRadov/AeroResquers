using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController
{
    private Plane _plane;
    private PlaneView _planeView;

    public PlaneController(Plane plane, PlaneView planeView)
    {
        _plane = plane;
        _planeView = planeView;
    }

    public void FlyingUp()
    {
        _planeView.UpdateForce(_plane.ForceUp);
    }

    public void OnWinLevel()
    {
        _planeView.SetBodyType(RigidbodyType2D.Static);
    }
}
