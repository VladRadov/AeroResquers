using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class PlaneView : ViewEntity
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _skin;
    [SerializeField] private Animator _animator;

    public float CurrentGravity => _rigidbody.gravityScale;

    public ReactiveCommand SaveSkydriverCommand = new();
    public ReactiveCommand GetMoneyCommand = new();
    public ReactiveCommand<float> GetDamageCommand = new();
    public ReactiveCommand OnDisappearedWithMapCommand = new();
    public ReactiveCommand OnCollisionAirTunnelCommand = new();

    public void SetSkin(Sprite skin)
        => _skin.sprite = skin;

    public void UpdateForce(Vector2 force)
    {
        var target = Vector3.Lerp(_rigidbody.velocity, force, 0.06f);
        _rigidbody.velocity = target;
    }

    public void SetBodyType(RigidbodyType2D rigidbodyType2D)
        => _rigidbody.bodyType = rigidbodyType2D;

    public void UpdatePosition(Vector2 newPosition)
    {
        var target = Vector3.Lerp(transform.position, newPosition, 0.06f);
        transform.position = target;
    }

    public void Rotation(Quaternion target)
        => transform.rotation = target;

    public void PlayAnimationUp()
    {
        if (_animator != null)
        {
            _animator.SetTrigger("IsFly");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var skydriver = collision.gameObject.GetComponent<SkydiverView>();
        if (skydriver != null)
        {
            AudioManager.Instance.PlayCaughtSkydriver();
            skydriver.SetActive(false);
            SaveSkydriverCommand.Execute();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var money = collision.gameObject.GetComponent<MoneyView>();
        if (money != null)
        {
            AudioManager.Instance.PlayCoin();
            money.SetActive(false);
            GetMoneyCommand.Execute();
        }

        var enemy = collision.gameObject.GetComponent<EnemyView>();
        if (enemy != null)
        {
            if (enemy is SmokeEnemyView)
                AudioManager.Instance.PlayAttackFire();
            else if (enemy is StoneView)
            {
                AudioManager.Instance.PlayAttackStone();
                enemy.gameObject.SetActive(false);
            }
            else if (enemy is WaveView)
                AudioManager.Instance.PlayAttackWave();

            GetDamageCommand.Execute(enemy.Damage);
        }

        var airTunnel = collision.gameObject.GetComponent<AirTunnelView>();
        if (airTunnel != null)
        {
            AudioManager.Instance.PlayAirplaneTunnel();
            airTunnel.SetActive(false);
            OnCollisionAirTunnelCommand.Execute();
            //PlayAnimationUp();
        }

        var cloud = collision.gameObject.GetComponent<CloudView>();
        if (cloud != null && cloud.IsSetDamage == false)
        {
            AudioManager.Instance.PlayAttackCloud();
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

        if (_animator == null)
            _animator = GetComponent<Animator>();
    }
}
