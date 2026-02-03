using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIController : MonoBehaviour
{
    [SerializeField] private CharacterHealth player;
    [SerializeField] private Image HpGauge;

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
        if (player != null)
        {
            player.OnHpChanged += SetHpGauge;
        }
    }

    private void OnDisable()
    {
        if (player != null)
        {
            player.OnHpChanged -= SetHpGauge;
        }
    }

    public void SetHpGauge(float ratio)
    {
        HpGauge.fillAmount = ratio;
    }
}
