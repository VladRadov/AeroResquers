using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Entity> _entities;
    [SerializeField] private Transform _canvasMain;

    private void Start()
    {
        foreach (var entity in _entities)
        {
            entity.Initialize(_canvasMain);
            entity.AddObjectDisposable();
        }
    }

    private void FixedUpdate()
    {
        foreach (var entity in _entities)
            entity.FixedUpdate();
    }

    private void OnDisable()
    {
        foreach (var entity in _entities)
            entity.Dispose();
    }
}
