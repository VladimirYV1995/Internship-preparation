using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Transform _transformPlayer;
    [SerializeField] private Transform _transformCamera;
    [SerializeField] private Terrain _terrain;

    private float _distanceTirrainEnd;
    private float _earlyPlayerPositionX;
    private float _speedPlayer;

    private void Start()
    {
        _earlyPlayerPositionX = _transformPlayer.position.x;
        _distanceTirrainEnd = _terrain.terrainData.size.x - _transformPlayer.position.x;
    }

    private void Update()
    {
        if (_earlyPlayerPositionX != _transformPlayer.position.x)
        {
            _speedPlayer = Mathf.Abs(_transformPlayer.position.x - _earlyPlayerPositionX) / Time.deltaTime;

            _transformCamera.position = MovingEnvironment( _transformCamera.position, _transformPlayer.position.x);            
            _terrain.terrainData.size = MovingEnvironment(_terrain.terrainData.size, _transformPlayer.position.x + _distanceTirrainEnd);

            _earlyPlayerPositionX = _transformPlayer.position.x;
        }
    }

    public Vector3 MovingEnvironment(Vector3 startVector, float x)
    {
        Vector3 endPosition = new Vector3(x, startVector.y, startVector.z);       
        return Vector3.Lerp(startVector, endPosition, _speedPlayer);
    }
}
