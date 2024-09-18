using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimePathfinding : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
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

        // Move towards player by modifying the velocity, but add to the current velocity instead of overriding it
        Vector2 targetVelocity = direction * movementSpeed;

        // Blend between current velocity and target velocity to maintain smooth movement and still allow knockback
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, Time.fixedDeltaTime * 5f); // Adjust interpolation factor as needed
    }
}
