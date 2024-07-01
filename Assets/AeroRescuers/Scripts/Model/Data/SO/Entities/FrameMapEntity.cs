using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameMapEntity", menuName = "ScriptableObject/FrameMapEntity")]
public class FrameMapEntity : Entity
{
    private FrameMapView _frameMapView;
    private FrameMapController _frameMapController;

    [SerializeField] private FrameMapView _frameMapPreafb;
    [SerializeField] private float _distanceOffset;

    public FrameMapController Controller => _frameMapController;
    public FrameMapView View => _frameMapView;

    public override void Initialize(Transform parent)
    {
        _frameMapView = Instantiate(_frameMapPreafb, parent);

        _frameMapController = new FrameMapController(_frameMapView);
    }

    public override void FixedUpdate()
    {
        if(_frameMapView.Position.x < _distanceOffset)
            _frameMapView.OffsetFrameBack.Execute(_frameMapView);
    }

    public override void AddObjectDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_frameMapView.OffsetFrameBack);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(_frameMapView.OffsetFrameBack);
    }
}
