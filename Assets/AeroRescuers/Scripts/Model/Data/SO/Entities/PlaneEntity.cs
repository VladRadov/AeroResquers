using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "PlaneEntity", menuName = "ScriptableObject/PlaneEntity")]
public class PlaneEntity : Entity
{
    private Plane _plane;
    private PlaneView _planeView;
    private PlaneController _planeController;

    [SerializeField] private PlaneView _planeViewPrefab;
    [SerializeField] private List<SkinPlane> _skinsPlane;
    [SerializeField] private float _forceUp;

    public PlaneView View => _planeView;
    public PlaneController Controller => _planeController;

    public override void Initialize(Transform parent)
    {
        var findedSkin = _skinsPlane.Find(skin => skin.Name == ContainerSaveerPlayerPrefs.Instance.SaveerData.CurrentSkin);
        _plane = new Plane(Vector2.up * _forceUp);
        _planeView = Instantiate(_planeViewPrefab, parent);

        if(findedSkin != null)
            _planeView.SetSkin(findedSkin.SpriteSkin);

        _planeController = new PlaneController(_plane, _planeView);
    }

    public override void FixedUpdate()
    {

    }

    public override void AddObjectDisposable()
    {
        ManagerUniRx.AddObjectDisposable(_planeView.SaveSkydriverCommand);
        ManagerUniRx.AddObjectDisposable(_planeView.GetMoneyCommand);
        ManagerUniRx.AddObjectDisposable(_planeView.GetDamageCommand);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(_planeView.SaveSkydriverCommand);
        ManagerUniRx.Dispose(_planeView.GetMoneyCommand);
        ManagerUniRx.Dispose(_planeView.GetDamageCommand);
    }
}
