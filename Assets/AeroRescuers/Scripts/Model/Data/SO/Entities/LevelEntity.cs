using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

[CreateAssetMenu(fileName = "LevelEntity", menuName = "ScriptableObject/LevelEntity")]
public class LevelEntity : Entity
{
    private LevelController _levelController;

    [Header("Components")]
    [SerializeField] private FrameMapEntity _frameMapEntityPrefab;
    [SerializeField] private List<SkydiverEntity> _skydiverEntities;
    [SerializeField] private MoneyEnetity _moneyEnetityPrefab;
    [SerializeField] private AirTunnelEntity _airTunnelEntityPrefab;
    [Header("Other settings")]
    [SerializeField] private int _number;
    [SerializeField] private int _startFramesMap;
    [SerializeField] private int _countMaxSkydrivers;

    public int CountMaxSkydrivers => _countMaxSkydrivers;
    public ReactiveCommand OnWinLevelCommand;

    public override void Initialize(Transform parent)
    {
        OnWinLevelCommand = new();
        _levelController = new LevelController();

        for (int i = 0; i < _startFramesMap; i++)
        {
            var frameMap = Instantiate(_frameMapEntityPrefab, parent);
            frameMap.Initialize(parent);

            if (i % 2 == 0)
                frameMap.Controller.RotateBack();

            if (i != 0)
                frameMap.Controller.UpdatePosition(i * new Vector3(frameMap.View.Width, 0, 0));

            frameMap.View.OffsetFrameBack.Subscribe((frameMap) =>
            {
                _levelController.ChangeLastFrameMap(frameMap);
                InitializeSkydriver(frameMap);
                InitializeMoney(frameMap);
                InitializeAirTunnel(frameMap);
            });

            InitializeSkydriver(frameMap.View);
            InitializeMoney(frameMap.View);
            InitializeAirTunnel(frameMap.View);

            if (i == _startFramesMap - 1)
                _levelController.SetLastFrameMapEntity(frameMap.View.transform.localPosition);

            _levelController.AddFrameMapEntity(frameMap);
        }
    }

    public override void FixedUpdate()
    {
        if (TryWinLevel())
            OnWinLevelCommand.Execute();

        _levelController.FixedUpdateFramesMap();
    }

    public override void AddObjectDisposable()
    {

    }

    public override void Dispose()
    {

    }

    private bool TryWinLevel()
        => _countMaxSkydrivers == ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver;

    private void InitializeSkydriver(FrameMapView frameMap)
    {
        var countSkudrivers = Random.Range(1, 3);

        for (int i = 0; i < countSkudrivers; i++)
        {
            var indexSkydrive = Random.Range(0, _skydiverEntities.Count);
            var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);

            var skydriver = _skydiverEntities[indexSkydrive];
            skydriver.Initialize(frameMap.transform);
            skydriver.View.transform.localPosition = new Vector3(x, skydriver.View.transform.localPosition.y, 0);
        }
    }

    private void InitializeMoney(FrameMapView frameMap)
    {
        var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);
        var y = Random.Range(0, frameMap.Height / 4);
        var moneyEntity = Instantiate(_moneyEnetityPrefab);
        moneyEntity.Initialize(frameMap.transform);
        moneyEntity.View.transform.localPosition = new Vector3(x, y, 0);
    }

    private void InitializeAirTunnel(FrameMapView frameMap)
    {
        var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);
        var y = Random.Range(0, frameMap.Height / 2);
        var airTunnelEntity = Instantiate(_airTunnelEntityPrefab);
        airTunnelEntity.Initialize(frameMap.transform);
        airTunnelEntity.View.transform.localPosition = new Vector3(x, y, 0);
    }
}
