using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private int _maxCountEnemy = 1;
    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private ScoreCounter _scoreCounter;

    private int _countEnemy = 0;
            
    private void Awake()
    {
        Spawn();
    }

    private void DeadEnemy()
    {
        _countEnemy--;

        _scoreCounter.Add();

        Spawn();
    }

    private void Spawn()
    {
        List<Transform> tempSpawnPoints = new List<Transform>();

        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            if (_spawnPoints[i].childCount == 0)
            {
                tempSpawnPoints.Add(_spawnPoints[i]);
            }
        }

        if (tempSpawnPoints.Count == 0)
        {
            return;
        }

        int enemiesToSpawn = Mathf.Min(_maxCountEnemy - _countEnemy, tempSpawnPoints.Count);

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int index = Random.Range(0, tempSpawnPoints.Count);

            _countEnemy++;

            Enemy newEnemy = Instantiate(_enemy, tempSpawnPoints[index]);
            newEnemy.SetTarget(_target);
            newEnemy.Dead += DeadEnemy;

            tempSpawnPoints.RemoveAt(index);
        }
    }
}
