using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int HP;
    public int damage;
    public HealthBar healthBar;

    public Transform[] players;
    public Transform attackPos;
    public LayerMask playerMask;
    public LayerMask TowerMask;
    public float radius;

    //private Animator anim;
    private bool isAttacking = false;
    private bool isDamaged = false;
    private float damageStartTime = 0f;
    public float attackRate = 1.0f; // Время между атаками
    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего 

    void Start()
    {
        //anim = GetComponent<Animator>();
        FindPlayers();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
    }

    void Update()
    {
        if (!isAttacking && !isDamaged)
        {
            transform.position = transform.position + new Vector3(-1.2f, 0, 0) * speed * Time.deltaTime;
            //anim.SetBool("Run", true);
        }
        else
        {
            //anim.SetBool("Run", false);
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }

        if (Time.time >= damageStartTime + 5f && isDamaged)
        {
            isDamaged = false;
        }

        if (Time.time >= nextAttackTime && !isAttacking && !isDamaged)
        {
            OnAttack();
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    void OnAttack()
    {
        if (Time.time >= nextDamageTime)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(attackPos.position, radius, playerMask | TowerMask);
            //anim.SetBool("Attack", true);
            isAttacking = true;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].CompareTag("Player") || targets[i].CompareTag("Castle"))
                {
                    targets[i].GetComponent<Player>()?.TakeDamage(damage);
                    targets[i].GetComponent<Castle>()?.TakeDamage(damage);
                    nextDamageTime = Time.time + damageRate;
                }
            }
            //anim.SetBool("Attack", false);
            isAttacking = false;
        }
    }

    void FindPlayers()
    {
        List<Transform> playerList = new List<Transform>();

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerList.Add(playerObject.transform);
        }

        GameObject[] castleObjects = GameObject.FindGameObjectsWithTag("Castle");
        foreach (GameObject castleObject in castleObjects)
        {
            playerList.Add(castleObject.transform);
        }

        players = playerList.ToArray();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            OnAttack();
        }
        Castle castle = collision.gameObject.GetComponent<Castle>();
        if (castle != null)
        {
            OnAttack();
        }
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
        isDamaged = true;
        damageStartTime = Time.time;
    }
}
