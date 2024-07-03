using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkydiverView : ViewEntity
{
    [SerializeField] private Rigidbody2D _rigidbody;

    public void SetActive(bool value)
        => gameObject.SetActive(value);

    private void OnBecameVisible()
    {
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.gravityScale = 0.1f;
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();
    }
}
