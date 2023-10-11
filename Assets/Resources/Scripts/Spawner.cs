using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform SpawnerLoc;
    public GameObject[] EnemyPrefabs;
    public int RandomEnemyPrefab;
    public void ButtonPressedCavalry()
    {
        
        
        
        RandomEnemyPrefab = Random.Range(0, EnemyPrefabs.Length);
        GameObject enemyPrefab = Instantiate(EnemyPrefabs[RandomEnemyPrefab], SpawnerLoc.transform.position, SpawnerLoc.transform.rotation);
    }
}
