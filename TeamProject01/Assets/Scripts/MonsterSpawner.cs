using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] monsterPrefabs;
    [SerializeField] private Transform[] spawnPositions;
    [SerializeField] private float mSpawnDelay = 2f;

    [SerializeField] private BattleManager battleManager;

    private bool mIsTriggered = false;

    private void Reset()
    {
        battleManager = FindObjectOfType<BattleManager>();
    }

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
            GameObject m = Instantiate(monsterPrefabs[i], pos.position, pos.rotation);

            CharacterHealth monster = m.GetComponent<CharacterHealth>();
            if (battleManager != null && monster != null)
            {
                battleManager.RegisterMonster(monster);
            }
        }
        Destroy(gameObject);
    }
}
