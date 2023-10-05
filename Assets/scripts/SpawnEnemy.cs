using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject[] _enemyTanks;
    public bool activeSpawn;

    private Score _score;
    private GameObject _parentTanksEnemy;
    private Transform _spawnPos;
    private float _spawnSecondMin = 30f;
    private float _spawnSecondMax = 40f;

    
    void Start()
    {
        SPT();
        _spawnPos = GameObject.Find("SpawnEnemyPos").transform;
        _parentTanksEnemy = GameObject.Find("EnemyTanks");
        _score = Camera.main.GetComponent<Score>();
       
    }

    
    void Update()
    {
        
    }
    void SpawnTank()
    {
        //Debug.LogError("SpawnTank");
        float rangFloat = Random.Range(_spawnSecondMin, _spawnSecondMax);
        //print(rangFloat);
        Invoke("SpawnTank", rangFloat);
        //if (!activeSpawn) return;
        if (_score.StopTank) return;
        if (_score.getCountChildren() >= 4) return;
        int RND = Random.Range(0, _enemyTanks.Length - 1);
        GameObject enemyTank = Instantiate(_enemyTanks[RND], _spawnPos);
        
        enemyTank.transform.parent = null;
    }
    void SPT()
    {
        //print("spt");
        Invoke("SpawnTank", 40f);
    }

    
}
