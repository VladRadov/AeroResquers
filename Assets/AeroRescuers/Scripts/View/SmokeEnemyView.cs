using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeEnemyView : EnemyView
{
    [SerializeField] private float _step;

    private void Start()
    {
        transform.localScale = new Vector3(1, 0.1f, 0);
        transform.localPosition = new Vector3(0, -150, 0);
    }

    private void FixedUpdate()
    {
        if ((transform.localScale.y > 1.7 && _step > 0) || (transform.localScale.y <= 0 && _step < 0))
            _step *= (-1);

        transform.localScale = new Vector3(1, transform.localScale.y + _step, 0);
    }
}
