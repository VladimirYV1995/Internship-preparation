using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Transform _camera;
    [SerializeField] private BoxCollider _border;
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

            ChengeBorder();

            _camera.position = ChangePosition(_camera.position, _player.position.x, speedPlayer);
            _terrain.terrainData.size = ChangePosition(_terrain.terrainData.size, _player.position.x + _distanceEndTerrain, speedPlayer);

            _lastPlayerPositionX = _player.position.x;
        }
    }

    private void ChengeBorder()
    {
        _border.center = _terrain.terrainData.size / 2;
        _border.size = _terrain.terrainData.size;
    }

    private Vector3 ChangePosition(Vector3 startPostion, float newPositionX, float speedPlayer)
    {
        Vector3 endPosition = new Vector3(newPositionX, startPostion.y, startPostion.z);       
        return Vector3.Lerp(startPostion, endPosition, speedPlayer);
    }
}
