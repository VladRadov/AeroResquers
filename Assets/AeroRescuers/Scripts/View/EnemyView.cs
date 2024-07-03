using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyView : ViewEntity
{
    [SerializeField] private float _damage;

    public float Damage => _damage;
}
