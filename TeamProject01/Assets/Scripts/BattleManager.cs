using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    [SerializeField] private CharacterHealth player;
    [SerializeField] private List<CharacterHealth> monsters = new();
    [SerializeField] private ResultUIController resultUI;

    private int mAliveMonsters;
    public bool Finished { get; private set; }

    private void Awake()
    {
        if (player == null)
        {
            GameObject p = GameObject.FindGameObjectWithTag("Player");
            if (p != null)
            {
                player = p.GetComponent<CharacterHealth>();
            }
        }
    }

    private void OnEnable()
    {
        Finished = false;
        mAliveMonsters = 0;

        if (player != null)
        {
            player.OnDied += OnPlayerDead;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnDied -= OnPlayerDead;
        }

        foreach (CharacterHealth m in monsters)
        {
            if (m != null)
            {
                m.OnDied -= OnMonsterDead;
            }
        }
        monsters.Clear();
    }

    public void RegisterMonster(CharacterHealth monster)
    {
        if (monster == null || monsters.Contains(monster))
        {
            return;
        }

        if (Finished)
        {
            return;
        }

        monsters.Add(monster);
        monster.OnDied += OnMonsterDead;

        if (monster.IsAlive)
        {
            mAliveMonsters++;
        }
    }

    private void OnPlayerDead()
    {
        if (Finished)
        {
            return;
        }

        Finished = true;
        Debug.Log("�й�");
        if (resultUI != null)
        {
            resultUI.ShowAfterDelay(false);
        }
    }

    private void OnMonsterDead()
    {
        if (Finished)
        {
            return;
        }

        mAliveMonsters--;
        if (mAliveMonsters <= 0)
        {
            Finished = true;
            Debug.Log("�¸�");
            if (resultUI != null)
            {
                resultUI.ShowAfterDelay(true);
            }
        }
    }
}
