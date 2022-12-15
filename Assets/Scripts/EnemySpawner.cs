using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 7, time = 1.5f;

    public GameObject[] enemies;
    [SerializeField] private EntityType[] _enemies;

    IEnumerator SpawnEnemy()
    {
        //Vector2 spawnpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 spawnPos = EntityManager.Instance.GetPlayerEntity().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        var randIndex = Random.Range(0, _enemies.Length);
        var entity = EntityManager.Instance.CreateEntity(_enemies[randIndex]);
        var enemy = (Enemy)entity;
        enemy.InitializeEnemy();
        enemy.transform.position = spawnPos;
        //Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
        //TODO difficulty
        yield return new WaitForSeconds(time);
        StartCoroutine(SpawnEnemy());
    }

    private void OnEnable()
    {
        CharacterSelectUI.playerSpawnEvent += Spawn;
    }
    private void OnDisable()
    {
        CharacterSelectUI.playerSpawnEvent -= Spawn;
    }
    void Spawn()
    {
        StartCoroutine(SpawnEnemy());
    }
}
