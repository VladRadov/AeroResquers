using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlaneEntity", menuName = "ScriptableObject/PlaneEntity")]
public class PlaneEntity : Entity
{
    private Plane _plane;
    private PlaneView _planeView;
    private PlaneController _planeController;

    [SerializeField] private PlaneView _planeViewPrefab;

    public override void Initialize(Transform parent)
    {
        _plane = new Plane();
        _planeView = Instantiate(_planeViewPrefab, parent);
        _planeController = new PlaneController(_plane, _planeView);
    }

    public override void FixedUpdate()
    {

    }

    public override void AddObjectDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_plane.Velocity);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(_plane.Velocity);
    }
}
