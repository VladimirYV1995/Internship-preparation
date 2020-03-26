using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _transform;

    [SerializeField] private float _moveForce;
    [SerializeField] private float _jumpForce ;

    private void Update()
    {
        TryMove(Input.GetAxis("Vertical"), Vector3.forward);
        TryMove(Input.GetAxis("Horizontal"), Vector3.right);
    }

    public void HitBall()
    {
        _rigidbody.velocity = -_rigidbody.velocity;
        _rigidbody.AddForce( -_rigidbody.velocity * _moveForce);
    }

    private void TryMove(float valueAxis, Vector3 positiveDirection)
    {
        if (valueAxis != 0 )
        {
            int realDirectrion = (int)(valueAxis / Mathf.Abs(valueAxis));
            _rigidbody.AddForce(realDirectrion * positiveDirection * _moveForce);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            TryJump();            
        }
    }

    private void TryJump()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
        }
    }
}