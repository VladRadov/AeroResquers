using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class GameManager : MonoBehaviour
{
    private bool _isPause;

    [Header("Components")]
    [SerializeField] private List<Entity> _entities;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private CounterManager _counterManager;
    [SerializeField] private PanelWinView _panelWinView;
    [SerializeField] private PanelGameOverView _panelGameOverView;
    [SerializeField] private HealthManager _healthManager;
    [Header("Other settings")]
    [SerializeField] private Transform _mainCanvas;

    private void Start()
    {
        _isPause = false;

        foreach (var entity in _entities)
        {
            if (entity is PlaneEntity || entity is FramesMapEntity)
            {
                entity.Initialize(_mainCanvas);
                entity.AddObjectDisposable();
            }
        }

        FramesMapEntity framesMap = GetEntity<FramesMapEntity>();

        foreach (var entity in _entities)
        {
            if (entity is PlaneEntity == false && entity is FramesMapEntity == false)
            {
                entity.Initialize(framesMap.View.Transform);
                entity.AddObjectDisposable();
            }
        }

        PlaneEntity plane = GetEntity<PlaneEntity>();
        LevelEntity level = GetEntity<LevelEntity>();

        _counterManager.SetMaxSaveSkydrivers(level.CountMaxSkydrivers);

        _healthManager.GameOverCommand.Subscribe(_ => { OnGameOver(); });
        level.OnWinLevelCommand.Subscribe(_ => { OnWinLevel(); plane.Controller.OnWinLevel(); });
        plane.View.SaveSkydriverCommand.Subscribe(_ => { _counterManager.IncreaseCountSkydrivers(); });
        plane.View.GetMoneyCommand.Subscribe(_ => { _counterManager.IncreaseCountMoney(); });
        plane.View.GetDamageCommand.Subscribe(damage => { _healthManager.Damage(damage); });
        plane.View.OnDisappearedWithMapCommand.Subscribe(_ => { _healthManager.GameOverCommand.Execute(); });
        _inputManager.OnMoveCommand.Subscribe(_ => { plane.Controller.FlyingUp(); });
    }

    private void OnWinLevel()
    {
        _panelWinView.SetActive(true);
        _isPause = true;
    }

    private void OnGameOver()
    {
        _panelGameOverView.SetActive(true);
        _isPause = true;
    }

    private T GetEntity<T>() where T : Entity
    {
        return (T)_entities.Find(entity => entity is T);
    }

    private void FixedUpdate()
    {
        if (_isPause)
            return;

        foreach (var entity in _entities)
            entity.FixedUpdate();
    }

    private void OnDisable()
    {
        foreach (var entity in _entities)
            entity.Dispose();
    }
}
