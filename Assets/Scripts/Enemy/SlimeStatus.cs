using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStatus : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private int maxHealth = 3;

    [SerializeField] private float takeDamageCooldown = 2;
    [SerializeField] private float damageTimer = 2;

    [SerializeField] private bool canTakeDamage = true;
    [SerializeField] private bool isCoroutineRunning = false;

    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float knockbackForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();   
    }
    private void Start()
    {
        player = PlayerGeneralInput.Instance.transform;
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("PlayerWeapon"))
        {
            //damage cooldown
            if (!isCoroutineRunning)
            {
                TakeDamage();
            }
        }

    }

    private IEnumerator DamageTimer()
    {
        isCoroutineRunning = true;

        while (damageTimer > 0) 
        {
            if (damageTimer > 0)
            {
                damageTimer -= Time.deltaTime;
            }
            yield return null;
        } 

        damageTimer = takeDamageCooldown;
        isCoroutineRunning = false;
        yield break;
    }

    private void TakeDamage()
    {
        //take damage
        health -= 1;

        //take knockback
        TakeKnockBack();

        //set damage cooldown
        StartCoroutine(DamageTimer());

        //is dead? destroy GO
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void TakeKnockBack() 
    {
        Vector3 direction = (transform.position - player.position).normalized;

        Debug.Log("direction:" + direction);
        Debug.Log("Force: " + direction * knockbackForce);
        rb.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
    }
}
