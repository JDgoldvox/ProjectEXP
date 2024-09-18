using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    [SerializeField] private Collider2D playerCollider;

    [SerializeField] private float takeDamageCooldown = 2;
    [SerializeField] private float damageTimer = 2;

    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private bool isTakeDamageCoroutineRunning = false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackForce;

    private PlayerStatus S_PlayerStatus;
    private Transform lastEnemyInContactPosition;

    private void Awake()
    {
        playerCollider = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        S_PlayerStatus = PlayerStatus.Instance;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Enemy"))
        {
            //damage cooldown
            if (!isTakeDamageCoroutineRunning)
            {
                Debug.Log("got hit");

                //enemy position
                lastEnemyInContactPosition = collider.transform;
                TakeDamage();
            }
        }

    }

    private IEnumerator DamageTimer()
    {
        isTakeDamageCoroutineRunning = true;

        while (damageTimer > 0)
        {
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            yield return null;
        }

        damageTimer = takeDamageCooldown;
        isTakeDamageCoroutineRunning = false;
        yield break;
    }

    private void TakeDamage()
    {
        //take damage
        S_PlayerStatus.currentHealth -= 1;

        //take knockback
        TakeKnockBack();

        //set damage cooldown
        StartCoroutine(DamageTimer());

        //is dead? destroy GO
        if (S_PlayerStatus.currentHealth <= 0)
        {
            Destroy(gameObject);
            //player died
        }
    }

    private void TakeKnockBack()
    {
        Debug.Log("knocking back");
        Vector3 direction = (transform.position - lastEnemyInContactPosition.position).normalized;

        //Debug.Log("direction:" + direction);
        //Debug.Log("Force: " + direction * knockbackForce);
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}
