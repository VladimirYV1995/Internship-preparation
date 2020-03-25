using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private Terrain _terrain;

    private float _distanceEndTerrain;
    private float _lastPlayerPositionX;

    private void Start()
    {
        _lastPlayerPositionX = _player.position.x;
        _distanceEndTerrain = _terrain.terrainData.size.x - _player.position.x;
    }

    private void Update()
    {
        if (_lastPlayerPositionX != _player.position.x)
        {
            float speedPlayer = Mathf.Abs(_player.position.x - _lastPlayerPositionX) / Time.deltaTime;

            _camera.position = PositioningElement(_camera.position, _player.position.x, speedPlayer);            
            _terrain.terrainData.size = PositioningElement(_terrain.terrainData.size, _player.position.x + _distanceEndTerrain, speedPlayer);

            _lastPlayerPositionX = _player.position.x;
        }
    }

    public Vector3 PositioningElement(Vector3 startPostion, float newPositionAxisX, float speedPlayer)
    {
        Vector3 endPosition = new Vector3(newPositionAxisX, startPostion.y, startPostion.z);       
        return Vector3.Lerp(startPostion, endPosition, speedPlayer);
    }
}
