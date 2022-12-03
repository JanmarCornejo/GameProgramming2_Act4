using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{

    /*NOTE: 
     * Wave system is not entirely polished and can only work at spawn points
     * it needs a major change so that the spawning area is always outside player viewpoint
    */

    public enum SpawnState { spawning, waiting, counting};

    [System.Serializable]
    public class Wave
    {
        public string name;
        public Transform enemy;
        public int count;
        public float rate;
    }

    public GameManager gm;
    public Wave[] waves;
    private int nextWave = 0;

    public Transform[] spawnPoints;

    public float timeBetweenWaves = 5f;
    [SerializeField] float waveCountdown;

    [SerializeField] float searchCount = 1f;

    private SpawnState state = SpawnState.counting;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
    }
    private void Update()
    {
        if(state == SpawnState.waiting)
        {
            if (!EnemyAlive())
            {
                NewRound();
            }
            else
            {
                return;
            }
        }

        if(waveCountdown <= 0)
        {
            if (state != SpawnState.spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
            
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }

    void NewRound()
    {
        Debug.Log("Wave Complete");

        state = SpawnState.counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)
        {
            //nextWave = 0;
            Debug.Log("All waves completed");
            gm.GameComplete();
        }

        nextWave++;
    }

    bool EnemyAlive()
    {
        searchCount -= Time.deltaTime;
        if (searchCount <= 0f)
        {
            searchCount = 1f;
            if (GameObject.FindGameObjectWithTag("Enemy") == null)
            {
                return false;
            }
        }
        return true;
    }

    IEnumerator SpawnWave(Wave _wave)
    {
        Debug.Log("Spawning Wave : " + _wave.name);

        state = SpawnState.spawning;

        for (int i = 0; i < _wave.count; i++)
        {
            SpawnEnemy(_wave.enemy);
            yield return new WaitForSeconds(1f / _wave.rate);
        }

        state = SpawnState.waiting;

        yield break;
    }

    void SpawnEnemy(Transform _enemy)
    {
        Debug.Log("Spawning Enemy : " + _enemy.name);        

        Transform _sp = spawnPoints[Random.Range(0,spawnPoints.Length)];
        Instantiate(_enemy, _sp.position, _sp.rotation);       
    }
}
