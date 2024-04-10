using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public int HP;
    public int damage;

    public Transform player;
    public Transform attackPos;
    public LayerMask playerMask;
    public float radius;

    private Animator anim;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        SearchForPlayer();
        transform.position = transform.position + new Vector3(-1.2f, 0, 0) * speed * Time.deltaTime;

        // Включаем анимацию бега всегда, когда враг движется
        anim.SetBool("Run", true);

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void TakeDamage(int damage)
    {
        HP -= damage;
    }

    void Attack()
    {
        // Атакуем игрока
        anim.SetBool("Attack", true);
    }

    // Метод для обработки атаки в анимации
    public void OnAttack()
    {
        Collider2D[] players = Physics2D.OverlapCircleAll(attackPos.position, radius, playerMask);
        anim.SetBool("Attack", false);
        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].CompareTag("Player"))
            {
                players[i].GetComponent<Player>().TakeDamage(damage);
            }
        }
    }

    void SearchForPlayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(attackPos.position, Vector2.left, radius, playerMask);
        if (hit.collider != null)
        {
            Player player = hit.collider.GetComponent<Player>();
            if (player != null)
            {
                print("НАШЕЛ!!!");
                // Найден игрок, вызываем метод атаки
                player.TakeDamage(damage);

            }
        }
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
            // Вызываем метод обработки атаки при столкновении с игроком
            OnAttack();
        }
    }

}
