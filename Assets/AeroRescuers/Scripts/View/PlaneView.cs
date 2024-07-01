using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneView : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _skin;

    public ReactiveCommand SaveSkydriverCommand = new();
    public ReactiveCommand GetMoneyCommand = new();
    public ReactiveCommand<float> GetDamageCommand = new();

    public void SetSkin(Sprite skin)
        => _skin.sprite = skin;

    public void UpdateForce(Vector2 force)
        => _rigidbody.velocity = force;

    public void SetBodyType(RigidbodyType2D bodyType)
        => _rigidbody.bodyType = bodyType;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var skydriver = collision.gameObject.GetComponent<SkydiverView>();
        if (skydriver != null)
        {
            skydriver.SetActive(false);
            SaveSkydriverCommand.Execute();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var money = collision.gameObject.GetComponent<MoneyView>();
        if (money != null)
        {
            money.SetActive(false);
            GetMoneyCommand.Execute();
        }

        var enemy = collision.gameObject.GetComponent<EnemyView>();
        if (enemy != null)
            GetDamageCommand.Execute(enemy.Damage);
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_skin == null)
            _skin = GetComponent<SpriteRenderer>();
    }
}
