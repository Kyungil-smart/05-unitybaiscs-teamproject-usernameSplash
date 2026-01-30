using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float mSpawnDelay = 2f;

    private bool mIsTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (mIsTriggered) return;
        mIsTriggered = true;

        StartCoroutine(SpawnDelay());
    }

    IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(mSpawnDelay);

        for (int i = 0; i < monsterPrefabs.Length; i++)
        {
            Transform pos = spawnPositions[i % spawnPositions.Length];
            Instantiate(monsterPrefabs[i], pos.position, pos.rotation);
        }
        Destroy(gameObject);
    }
}
