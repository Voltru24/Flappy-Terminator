using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] Bullet _bullet;
    [SerializeField] private float _speedAttack = 3f;
    [SerializeField] private Transform _shootPoint;

    private bool _isAttackTimer = true;
    private Transform _target;

    public event Action Dead;

    private void OnValidate()
    {
        GetComponent<Collider2D>().isTrigger = true;
    }

    private void Start()
    {
        StartCoroutine(DetainAttack());
    }

    private void FixedUpdate()
    {
        if(_isAttackTimer == false)
        {
            Shot();

            _isAttackTimer = true;

            StartCoroutine(DetainAttack());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bullet>())
        {
            Dead?.Invoke();

            Destroy(gameObject);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    private void Shot()
    {
        Bullet bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);

        bullet.SetDirection(_target.position);
    }

    private IEnumerator DetainAttack()
    {
        yield return new WaitForSeconds(_speedAttack);

        _isAttackTimer = false;
    }
}
