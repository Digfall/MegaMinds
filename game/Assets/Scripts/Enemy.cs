using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Статы Персонажа")]
    public int HP;
    public int damage;
    public float speed;
    //public float attackRate = 1.0f; // Время между атаками
    public float damageRate = 1.0f; // Задержка между каждым нанесением урона
    public float radius;


    [Header("Обращения к объектам и трансформы")]
    public Transform attackPos;
    public HealthBar healthBar;
    public Transform movePos;
    [SerializeField] private Transform moveTarget; // Цель для передвижения
    [SerializeField] private Transform attackTarget; // Цель для атаки


    [Header("Настройки луча")]
    [SerializeField] private float raycastDistance = 0.5f; // Длина луча для поиска цели
    [SerializeField] private float raycastDistanceToMove = 15f;

    [Header("Настройки вейпоинтов")]

    [SerializeField] private int currentWayPoint;
    [SerializeField] private GameObject[] wayPoints;
    [SerializeField] private Vector2 targer;

    private bool isFighting = false;
    private bool isAttacking = false;
    private float nextDamageTime = 0f; // Время до следующего удара
    private Rigidbody2D rb;

    void Start()
    {
        wayPoints = GameObject.FindGameObjectsWithTag("WayPoint");
        targer = wayPoints[currentWayPoint].transform.position;
        rb = GetComponent<Rigidbody2D>();
        healthBar.SetHealth(HP);
        healthBar.maxHealth = HP;
    }

    void Update()
    {

        FindTargetToAttack();
        if (!isFighting && !isAttacking)
        {
            // MoveToTarget();
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
        transform.position = Vector2.MoveTowards(transform.position, targer, speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "WayPoint")
        {
            if (currentWayPoint >= wayPoints.Length - 1)
            {
                Destroy(gameObject);
            }
            else
            {
                currentWayPoint++;
                targer = wayPoints[currentWayPoint].transform.position;
            }
        }
    }
    //Следование за целью
    void MoveToTarget()
    {
        RaycastHit2D moveRaycastHit = Physics2D.Raycast(movePos.position, Vector2.left, raycastDistanceToMove);
        Debug.DrawRay(movePos.position, Vector2.left * raycastDistanceToMove, Color.blue);

        if (moveRaycastHit.collider != null)
        {
            Transform moveTargetCandidate = moveRaycastHit.collider.transform;

            if (moveTargetCandidate.CompareTag("Player") || moveTargetCandidate.CompareTag("Castle"))
            {
                moveTarget = moveTargetCandidate;
            }
            else
            {
                moveTarget = null;
            }
        }
        else
        {
            moveTarget = null;
        }

        if (moveTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
        }
    }
    //Метод на атаку
    void OnAttack()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(attackPos.position, radius);
        isAttacking = true;
        isFighting = true;
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i].CompareTag("Player") || targets[i].CompareTag("Castle"))
            {
                targets[i].GetComponent<Player>()?.TakeDamage(damage);
                targets[i].GetComponent<Castle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }
        }
        StartCoroutine(EndAttackAnimation());
    }
    //Время после атаки для анимации
    IEnumerator EndAttackAnimation()
    {
        yield return new WaitForSeconds(0.5f);
        isAttacking = false;
        StartCoroutine(ResetIsFightingAfterDelay());
    }

    //Таймер файта
    IEnumerator ResetIsFightingAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        isFighting = false;
    }

    //Рендж атаки
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);
    }

    public void TakeDamage(int damage)
    {
        healthBar.SetHealth(HP);
        HP -= damage;
        isFighting = true;
        StartCoroutine(ResetIsFightingAfterDelay());
    }

    void FindTargetToAttack()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPos.position, Vector2.left, raycastDistance);
        if (hit.collider != null)
        {
            attackTarget = hit.collider.transform;
            if (attackTarget.CompareTag("Player") || attackTarget.CompareTag("Castle"))
            {
                OnAttack();
                Debug.Log("Я НАШЕЛ ВРАГА");
            }
        }

        Debug.DrawRay(attackPos.position, Vector2.left * raycastDistance, Color.red);
    }

}
