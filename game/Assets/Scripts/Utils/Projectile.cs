using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    private int damage;
    private Transform target;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.isKinematic = true;
    }

    public void Initialize(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    void Update()
    {
        if (target != null)
        {
            Vector2 direction = ((Vector2)target.position - rb.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, target.position) < 0.1f)
            {
                OnHitTarget();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform == target)
        {
            OnHitTarget();
        }
    }

    private void OnHitTarget()
    {
        if (target != null)
        {
            EnemyBase enemyBase = target.GetComponent<EnemyBase>();
            if (enemyBase != null)
            {
                enemyBase.TakeDamage(damage);
            }

            EnemyCastle enemyCastle = target.GetComponent<EnemyCastle>();
            if (enemyCastle != null)
            {
                enemyCastle.TakeDamage(damage);
            }
            Castle Castle = target.GetComponent<Castle>();
            if (Castle != null)
            {
                Castle.TakeDamage(damage);
            }
            PlayerBase Player = target.GetComponent<PlayerBase>();
            if (Player != null)
            {
                Player.TakeDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
