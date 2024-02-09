using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Bullet _bullet;

    [SerializeField] private float _speedAttack;

    [SerializeField] private float _distanceAttack;

    [SerializeField] private KeyCode _keyAttack;

    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Transform _directionPoint;

    private bool _isAttackTimer = false;

    private void Update()
    {
        if (Input.GetKeyDown(_keyAttack))
        {
            if (_isAttackTimer == false)
            {
                Shot();

                _isAttackTimer = true;

                StartCoroutine(DetainAttack());
            }
        }
    }

    private void Shot()
    {
        Bullet bullet = Instantiate(_bullet, _shootPoint.position, Quaternion.identity);

        //bullet.transform.Translate(Vector3.right * _distanceAttack);

        bullet.SetDirection(_directionPoint.position);
    }

    private IEnumerator DetainAttack()
    {
        yield return new WaitForSeconds(_speedAttack);

        _isAttackTimer = false;
    }
}
