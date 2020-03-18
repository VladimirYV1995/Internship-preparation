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
    struct Borders
    {
        public float Near { get; }
        public float Far { get; }
        public float Left { get; }


        public Borders(float near, float far, float left)
        {
            Near = near;
            Far = far;
            Left = left;
        }
    }

    private Borders _bordersMovement;

    private void Start()
    {
        _bordersMovement = new Borders(_terrain.transform.position.z,
                                              _terrain.transform.position.z + _terrain.terrainData.size.z,
                                              _transform.position.x
                                              );
    }

    private void Update()
    {
        Move("Horizontal", Vector3.right);
        Move("Vertical", Vector3.forward);

        if (_transform.position.z >= _bordersMovement.Far || _transform.position.z <= _bordersMovement.Near || _transform.position.x <= _bordersMovement.Left)
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