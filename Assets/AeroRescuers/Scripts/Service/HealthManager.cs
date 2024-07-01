using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    private float _currentHealth;

    [SerializeField] private Slider _indicatorHealth;

    public void Damage(float countDamage)
    {
        _currentHealth -= countDamage;
        ViewCurrentHealth();
    }

    private void Start()
    {
        _currentHealth = 100;
        _indicatorHealth.maxValue = 100;
        ViewCurrentHealth();
    }

    private void ViewCurrentHealth()
        => _indicatorHealth.value = _currentHealth;
}
