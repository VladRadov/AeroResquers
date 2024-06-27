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
}
