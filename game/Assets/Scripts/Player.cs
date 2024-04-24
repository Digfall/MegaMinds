using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour
{
    [Header("Статы Персонажа")]

    public int HP = 200;
    public int damage = 50;
    public float speed = 2;

    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    public float radius;
    public float attackRate = 1.5f; // Время между атаками в секундах
    private float nextAttackTime = 0f; // Время до следующей атаки
    private float nextDamageTime = 0f; // Время до следующего нанесения урона


    [Header("Обращения к объектам и трансформы")]
    public Transform attackPos;
    public HealthBar healthBar;
    public Transform movePos;
    [SerializeField] private Transform moveTarget; // Цель для передвижения
    [SerializeField] private Transform attackTarget; // Цель для атаки

    [Header("Настройки луча")]
    [SerializeField] private float raycastDistance = 0.5f; // Длина луча для поиска цели
    //[SerializeField] private float raycastDistanceToMove = 15f;

    [Header("Настройки вейпоинтов")]

    [SerializeField] private int currentWayPoint;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private Vector2 targer;
    private bool isAttacking = false;
    private bool isFighting = false;
    private Rigidbody2D rb;

    void Start()
    {

        wayPoints = GameObject.FindGameObjectsWithTag("WayPointPlayer");
        Array.Sort(wayPoints, new GameObjectComparerByName());
        targer = wayPoints[currentWayPoint].transform.position;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;

    }
    void Update()
    {
        FindTargetToAttack();
        if (!isFighting && !isAttacking)
        {
            MoveOnWayPoint();
        }
        else
        {
            rb.velocity = Vector2.zero;
        }

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    void MoveOnWayPoint()
    {
        if (currentWayPoint < wayPoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, targer, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WayPointPlayer")
        {
            currentWayPoint++;
            if (currentWayPoint < wayPoints.Length)
            {
                targer = wayPoints[currentWayPoint].transform.position;
            }
        }
    }
    public void OnAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPos.position, radius);
            isAttacking = true;
            isFighting = true; // Устанавливаем флаг isFighting в true при использовании OnAttack
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i].CompareTag("Enemy") || enemies[i].CompareTag("EnemyCastle"))
                {
                    enemies[i].GetComponent<Enemy>()?.TakeDamage(damage);
                    enemies[i].GetComponent<EnemyCastle>()?.TakeDamage(damage);
                    nextDamageTime = Time.time + damageRate;
                }
            }
            StartCoroutine(EndAttackAnimation());

            nextAttackTime = Time.time + 1f / attackRate; // Устанавливаем время следующей атаки
        }
    }
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        StartCoroutine(ResetIsFightingAfterDelay()); // Запускаем корутину для сброса isFighting обратно в false через 3 секунды после завершения атаки
    }
    IEnumerator ResetIsFightingAfterDelay()
    {
        yield return new WaitForSeconds(2.5f);
        isFighting = false; // Сбрасываем флаг isFighting обратно в false через 3 секунды после завершения атаки
    }

    // void MoveToTarget()
    // {
    //     RaycastHit2D MoveHit = Physics2D.Raycast(attackPos.position, Vector2.right, raycastDistanceToMove);
    //     Debug.DrawRay(attackPos.position, Vector2.right * raycastDistanceToMove, Color.green);
    //     if (MoveHit.collider != null)
    //     {
    //         Transform moveTargetCandidate = MoveHit.collider.transform;

    //         if (moveTargetCandidate.CompareTag("Enemy") || moveTargetCandidate.CompareTag("EnemyCastle"))
    //         {
    //             moveTarget = moveTargetCandidate;
    //         }
    //         else
    //         {
    //             moveTarget = null;
    //         }
    //     }
    //     else
    //     {
    //         moveTarget = null;
    //     }

    //     if (moveTarget != null)
    //     {
    //         transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
    //     }
    // }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }
    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
        isFighting = true; // Устанавливаем isFighting в true при получении урона
        StartCoroutine(ResetIsFightingAfterDelay());
    }
    void FindTargetToAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPos.position, Vector2.right, raycastDistance);
        Debug.DrawRay(attackPos.position, Vector2.right * raycastDistance, Color.red);
        if (hit.collider != null)
        {
            attackTarget = hit.collider.transform;
            if (attackTarget.CompareTag("Enemy") || attackTarget.CompareTag("EnemyCastle"))
            {
                OnAttack();
            }
        }
    }
}