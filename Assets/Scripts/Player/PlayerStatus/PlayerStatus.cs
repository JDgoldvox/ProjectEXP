using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus Instance;

    [field: Header("Default")]
    [field: SerializeField] public float maxHealth;
    [field: SerializeField] public float currentHealth;
    [field: SerializeField] public float maxMana;
    [field: SerializeField] public float currentMana;
    [field: SerializeField] public float maxEnergy;
    [field: SerializeField] public float currentEnergy;
    [field: SerializeField] public int currentCredits;

    List<STATUS> currentStatus;

    [field: Header("Default")]
    [SerializeField] private int test;

    private void Awake()
    {
        Instance = this;
    }

}
