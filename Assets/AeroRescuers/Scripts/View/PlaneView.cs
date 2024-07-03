using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneView : ViewEntity
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _skin;

    public float CurrentGravity => _rigidbody.gravityScale;

    public ReactiveCommand SaveSkydriverCommand = new();
    public ReactiveCommand GetMoneyCommand = new();
    public ReactiveCommand<float> GetDamageCommand = new();
    public ReactiveCommand OnDisappearedWithMapCommand = new();
    public ReactiveCommand OnCollisionAirTunnelCommand = new();

    public void SetSkin(Sprite skin)
        => _skin.sprite = skin;

    public void UpdateForce(Vector2 force)
        => _rigidbody.velocity = force;

    public void SetBodyType(RigidbodyType2D bodyType)
        => _rigidbody.bodyType = bodyType;

    public void SetGravity(float value)
        => _rigidbody.gravityScale = value;

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

        var airTunnel = collision.gameObject.GetComponent<AirTunnelView>();
        if (airTunnel != null)
        {
            airTunnel.SetActive(false);
            OnCollisionAirTunnelCommand.Execute();
        }

        var cloud = collision.gameObject.GetComponent<CloudView>();
        if (cloud != null && cloud.IsSetDamage == false)
        {
            cloud.SetActive(false);
            cloud.SetDamaged();
            GetDamageCommand.Execute(cloud.Damage);
        }
    }

    private void OnBecameInvisible()
    {
        if(OnDisappearedWithMapCommand.IsDisposed == false)
            OnDisappearedWithMapCommand.Execute();
    }

    private void OnValidate()
    {
        if (_rigidbody == null)
            _rigidbody = GetComponent<Rigidbody2D>();

        if (_skin == null)
            _skin = GetComponent<SpriteRenderer>();
    }
}
