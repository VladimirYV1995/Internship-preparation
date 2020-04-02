using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _transform;

    [SerializeField] private float _moveForce;
    [SerializeField] private float _jumpForce;

    private void Update()
    {
        TryMove();
    }

    private void TryMove()
    {
        Vector3 direction = DetermineDirection();

        if (direction != Vector3.zero)
        {
            _rigidbody.AddForce(direction * _moveForce);
        }
    }

    private Vector3 DetermineDirection()
    {
        int x = DetermineAxis(Input.GetAxis("Horizontal"));
        int z = DetermineAxis(Input.GetAxis("Vertical"));
        return new Vector3(x, 0, z);
    }

    private int DetermineAxis(float valueAxis)
    {
        if (valueAxis != 0)
        {
            return (int)(valueAxis / Mathf.Abs(valueAxis));
        }
        else
        {
            return 0;
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

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<LevelGenerator>(out LevelGenerator levelGenerator))
        {
            _rigidbody.velocity = -_rigidbody.velocity;
            _rigidbody.AddForce(_rigidbody.velocity * _moveForce);
        }
    }
}