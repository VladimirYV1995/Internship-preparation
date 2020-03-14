using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private Terrain _terrain;
    [SerializeField] private GameObject _barrier;
    [SerializeField] private GameObject _coin;

    [SerializeField] private float _initialTerrainSizeX;

    [SerializeField] private float _distanceBetweenBarriers;

    private Vector3 _cellSize;
    private float _lastBarrierPositionX;
    private float _lastCoinPositionX;
    private float _cellCountOnWidth;
    private HashSet<Vector3> _filledCells;

    private void Awake()
    {
        //!!! после изменения в игре террейна изменяет и в редакторе!!!! Функцию держать именно в Awake, иначе _terrain.terrainData.size.x устанавливается рандомным значением
        _terrain.terrainData.size = new Vector3(_initialTerrainSizeX, _terrain.terrainData.size.y, _terrain.terrainData.size.z);

        _cellSize = _barrier.GetComponent<Transform>().localScale;
        if (_distanceBetweenBarriers < _cellSize.x)
        {
            _distanceBetweenBarriers = _cellSize.x;
        }

        _lastBarrierPositionX = _player.position.x - _distanceBetweenBarriers;
        _lastCoinPositionX = _player.position.x - _cellSize.z;

        _cellCountOnWidth = (int)(_terrain.terrainData.size.z / _cellSize.z);
        _filledCells = new HashSet<Vector3>();
    }

    private void Update()
    {        
        TryCreateTemplate(ref _lastBarrierPositionX, _distanceBetweenBarriers, _barrier);

        if (Random.Range(0, 100) == 0)
        {
            TryCreateTemplate(ref _lastCoinPositionX, _cellSize.x, _coin);
        }
    }

    private void TryCreateTemplate(ref float lastPosition, float distance,  GameObject template)
    {
        if (_player.position.x - lastPosition >= distance)
        {
            Vector3Int gridPosition = GridFragmentationAxis();

            while (_filledCells.Contains(gridPosition))
            {
                gridPosition.y += 1;
            }
            _filledCells.Add(gridPosition);

            Vector3 tempatePosition = GridToWorldPosition(gridPosition);
            Instantiate(template, tempatePosition, Quaternion.identity);

            lastPosition = _player.position.x;
        }
    }
    private Vector3Int GridFragmentationAxis()
    {
        var gridStartingPosition = WorldToGridPosition(_player.position);
        var cellCountOnDistance = (int)((_terrain.terrainData.size.x - _player.position.x) / _cellSize.x);
        int x = gridStartingPosition.x + cellCountOnDistance;
        int z = (int)(Random.Range(0, _cellCountOnWidth));
        var gridPosition = new Vector3Int(x, 0, z);
        return gridPosition;
    }

    private Vector3Int WorldToGridPosition(Vector3 worldPosition)
    {
        int x = (int)(worldPosition.x / _cellSize.x);
        int y = (int)(worldPosition.y / _cellSize.y);
        int z = (int)(worldPosition.z / _cellSize.z);

        return new Vector3Int(x, y, z);
    }
    private Vector3 GridToWorldPosition(Vector3Int gridPosition)
    {
        float x = gridPosition.x * _cellSize.x + _cellSize.x / 2;
        float y = gridPosition.y * _cellSize.y + _cellSize.y / 2;
        float z = gridPosition.z * _cellSize.z + _cellSize.z / 2;

        return new Vector3(x, y, z);
    }
}

