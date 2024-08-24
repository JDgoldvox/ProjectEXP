using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    [field: Header("Default")]
    [field: SerializeField] public float maxHealth { get; private set; }
    [field: SerializeField] public float currentHealth { get; private set; }
    [field: SerializeField] public float maxMana { get; private set; }
    [field: SerializeField] public float currentMana { get; private set; }
    [field: SerializeField] public float maxEnergy { get; private set; }
    [field: SerializeField] public float currentEnergy { get; private set; }
    [field: SerializeField] public int currentCredits { get; private set; }

    List<STATUS> currentStatus;

    [field: Header("Default")]
    [SerializeField] private int test;

}
