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
    [SerializeField] private List<CloudEntity> _cloudEntityPrefab;
    [SerializeField] private List<Entity> _enemyPrefabs;
    [Header("Other settings")]
    [SerializeField] private int _number;
    [SerializeField] private int _startFramesMap;
    [Header("Кол-во парашютистов на уровне")]
    [SerializeField] private int _countMaxSkydrivers;

    public override ViewEntity View { get; }
    public int CountMaxSkydrivers => _countMaxSkydrivers;
    public int LevelNumber => _number;
    public ReactiveCommand OnWinLevelCommand;

    public override void Initialize(Transform parent)
    {
        OnWinLevelCommand = new();
        _levelController = new LevelController();

        for (int i = 0; i < _startFramesMap; i++)
        {
            var frameMap = Instantiate(_frameMapEntityPrefab, parent);
            frameMap.Initialize(parent);
            FrameMapView frameMapView = (FrameMapView)frameMap.View;

            if (i % 2 == 0)
                frameMap.Controller.RotateBack();

            if (i != 0)
                frameMap.Controller.UpdatePosition(i * new Vector3(frameMapView.Width, 0, 0));

            frameMapView.OffsetFrameBack.Subscribe((frameMap) =>
            {
                _levelController.ChangeLastFrameMap(frameMap);
                InitializeSkydriver(frameMap);
                InitializeMoney(frameMap);
                InitializeAirTunnel(frameMap);
                InitializeCloud(frameMap);
                InitializeEnemy(frameMap);
            });

            if (i != 0)
            {
                InitializeSkydriver(frameMapView);
                InitializeMoney(frameMapView);
                InitializeAirTunnel(frameMapView);
                InitializeCloud(frameMapView);
                InitializeEnemy(frameMapView);
            }

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
        ManagerUniRx.AddObjectDisposable(OnWinLevelCommand);
    }

    public override void Dispose()
    {
        ManagerUniRx.Dispose(OnWinLevelCommand);
    }

    private bool TryWinLevel()
        => _countMaxSkydrivers != 0 && _countMaxSkydrivers == ContainerSaveerPlayerPrefs.Instance.SaveerData.CountSkydriver;

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
        var y = Random.Range(-frameMap.Height / 4, frameMap.Height / 2);
        var airTunnelEntity = Instantiate(_airTunnelEntityPrefab);
        airTunnelEntity.Initialize(frameMap.transform);
        airTunnelEntity.View.transform.localPosition = new Vector3(x, y, 0);
    }

    private void InitializeCloud(FrameMapView frameMap)
    {
        var countClouds = ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame == 0 ? Random.Range(0, 2) : Random.Range(1, 3);

        for (int i = 0; i < countClouds; i++)
        {
            var indexCloud = Random.Range(0, _cloudEntityPrefab.Count);
            var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);

            var cloud = _cloudEntityPrefab[indexCloud];
            cloud.Initialize(frameMap.transform);
            cloud.View.transform.localPosition = new Vector3(x, cloud.View.transform.localPosition.y, 0);
        }
    }

    private void InitializeEnemy(FrameMapView frameMap)
    {
        var count = ContainerSaveerPlayerPrefs.Instance.SaveerData.TypeGame == 0 ? Random.Range(0, _enemyPrefabs.Count) : _enemyPrefabs.Count;

        for (int i = 0; i < count; i++)
        {
            var x = Random.Range(-frameMap.Width / 4, frameMap.Width / 4);
            float y = (frameMap.Height / 2) - 10;

            var indexEnemy = Random.Range(0, _enemyPrefabs.Count);

            if (_enemyPrefabs[indexEnemy] is WaveEntity)
                y = Random.Range(-frameMap.Height / 4, - frameMap.Height / 2);

            var wave = Instantiate(_enemyPrefabs[indexEnemy]);
            wave.Initialize(frameMap.transform);
            wave.View.transform.localPosition = new Vector3(x, y, 0);
        }
    }
}
