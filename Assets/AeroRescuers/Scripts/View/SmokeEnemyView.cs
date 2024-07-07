using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnemyView : EnemyView
{
    private Transform _transform;

    [SerializeField] private float _step;
    [SerializeField] private float _maxScaleSmoke;

    private void Start()
    {
        _transform = transform;
        _transform.localScale = new Vector3(1, 0.1f, 0);
        _transform.localPosition = new Vector3(0, -150, 0);
    }

    private void FixedUpdate()
    {
        if ((_transform.localScale.y > _maxScaleSmoke && _step > 0) || (_transform.localScale.y <= 0.3 && _step < 0))
            _step *= (-1);

        _transform.localScale = new Vector3(1, _transform.localScale.y + _step, 0);
    }
}
