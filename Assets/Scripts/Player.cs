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

        if (Input.GetAxis("Horizontal") > 0 || _transform.position.x < _leftDistance)
        {
            _rigidbody.AddForce(Vector3.right * _moveForce);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            _rigidbody.AddForce(Vector3.left * _moveForce);
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            _rigidbody.AddForce(Vector3.forward * _moveForce);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            _rigidbody.AddForce(Vector3.back * _moveForce);
        }

        if (_transform.position.z >= _longDistance || _transform.position.z <= _nearDistance )
        {
            _rigidbody.velocity = -_rigidbody.velocity;
        }

        if (Input.GetKey(KeyCode.Space) && _onGround)
        {
            _rigidbody.AddForce(Vector3.up * _jumpForce);
            _onGround = false;
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


