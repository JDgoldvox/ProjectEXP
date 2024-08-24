using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePathfinding : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = PlayerGeneralInput.Instance.transform;
    }

    private void FixedUpdate()
    {
        GoTowardsPlayer();
    }

    private void GoTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        rb.velocity = direction * movementSpeed;   
    }
}
