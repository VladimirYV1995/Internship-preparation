using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Transform _transform;
    [SerializeField] private Terrain _terrain;

    [SerializeField] private float _moveForce;
    [SerializeField] private float _jumpForce;
    [SerializeField] private bool _onGround;

    private float _nearDistance, _longDistance, _leftDistance;

    private void Start()
    {
        _nearDistance = _terrain.transform.position.z;
        _longDistance = _terrain.transform.position.z + _terrain.terrainData.size.z;
        _leftDistance = _transform.position.x;
    }

    private void Update()
    {
        Move("Horizontal", Vector3.right);
        Move("Vertical", Vector3.forward);

        if (_transform.position.z >= _longDistance || _transform.position.z <= _nearDistance || _transform.position.x <= _leftDistance)
        {
           _rigidbody.velocity = -_rigidbody.velocity;
            _rigidbody.AddForce(_rigidbody.velocity.normalized * _moveForce);
        }

        if (Input.GetKey(KeyCode.Space) && _onGround)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
            _onGround = false;
        }
    }

    private void Move(string axis, Vector3 positive)
    {
        if (Input.GetAxis(axis) != 0)
        {
             int realDirectrion = (int)(Input.GetAxis(axis) / Mathf.Abs(Input.GetAxis(axis)));
            _rigidbody.AddForce(realDirectrion * positive * _moveForce);
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider)
        {
            _onGround = true;
        }
    }

}


