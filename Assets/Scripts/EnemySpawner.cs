using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private float spawnRadius = 7, time = 1.5f;

    public GameObject[] enemies;

    IEnumerator SpawnEnemy()
    {
        //Vector2 spawnpos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector2 spawnPos = EntityManager.Instance.GetEntityPlayer().transform.position;
        spawnPos += Random.insideUnitCircle.normalized * spawnRadius;

        Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
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
