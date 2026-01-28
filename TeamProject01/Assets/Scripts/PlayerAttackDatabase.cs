using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Combat/AttackDatabase")]
public class PlayerAttackDatabase : ScriptableObject
{
    public List<PlayerAttackData> Attacks = new List<PlayerAttackData>();

    private Dictionary<string, PlayerAttackData> mAttackDataMap;


    public PlayerAttackData Get(string id)
    {
        if (mAttackDataMap == null)
        {
            mAttackDataMap = new Dictionary<string, PlayerAttackData>();

            foreach (PlayerAttackData elem in Attacks)
            {
                Debug.Log($"[PlayerAttackDatabase] Add Elem, ID : {elem.ID}");
                if (elem == null || string.IsNullOrWhiteSpace(elem.ID))
                {
                    continue;
                }

                if (!mAttackDataMap.ContainsKey(elem.ID))
                {
                    mAttackDataMap.Add(elem.ID, elem);
                }
                else
                {
                    Debug.LogError($"[PlayerAttackDatabase] Duplicated ID : {elem.ID} : {elem.name}");
                }
            }
        }

        if (string.IsNullOrWhiteSpace(id))
        {
            return null;
        }

        mAttackDataMap.TryGetValue(id, out PlayerAttackData data);
        return data;
    }

}