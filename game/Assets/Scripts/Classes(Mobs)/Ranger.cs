using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ranger : EnemyBase
{
    [SerializeField] private TextMeshProUGUI rangerLvlText;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer weaponRenderer;
    public Animator anim;
    [SerializeField] private UnitSounds unitSounds;

    [SerializeField] private Sprite bodySpriteLvl1;
    [SerializeField] private Sprite bodySpriteLvl2;
    [SerializeField] private Sprite bodySpriteLvl3;

    [SerializeField] private Sprite weaponSpriteLvl1;
    [SerializeField] private Sprite weaponSpriteLvl2;
    [SerializeField] private Sprite weaponSpriteLvl3;
    protected override void Start()
    {
        UpdateText();
        ApplyLevelAdjustments();
        base.Start();
    }
    private void UpdateText()
    {
        if (rangerLvlText != null)
        {
            rangerLvlText.text = level.ToString();
        }
    }

    public override void ApplyLevelAdjustments()
    {
        switch (level)
        {
            case 1:
                HP = 24;
                damage = 12;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 2:
                HP = 27;
                damage = 14;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 3:
                HP = 30;
                damage = 15;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 4:
                HP = 33;
                damage = 17;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 5:
                HP = 36;
                damage = 18;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 6:
                HP = 39;
                damage = 19;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 7:
                HP = 42;
                damage = 20;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 8:
                HP = 45;
                damage = 21;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            case 9:
                HP = 48;
                damage = 23;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            case 10:
                HP = 51;
                damage = 24;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            default:
                HP = 24;
                damage = 12;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
        }
        speed = 2f;
        attackRate = 0.8f;
    }

    protected override void Update()
    {
        base.Update();
        FindTargetToAttack();
        if (isAttacking && attackTarget != null)
        {
            OnAttack();
        }
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            unitSounds.PlayAttackSound();
            nextDamageTime = Time.time + damageRate;
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }
    private void LaunchProjectile(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();
        projectileScript.Initialize(target, damage);
    }
    protected override void FindTargetToAttack()
    {
        if (attackTarget != null && (Vector2.Distance(transform.position, attackTarget.position) > radius || (!attackTarget.CompareTag("Enemy") && !attackTarget.CompareTag("EnemyCastle"))))
        {
            attackTarget = null;
        }

        if (attackTarget == null)
        {
            Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(attackPos.position, radius);
            float minDistance = Mathf.Infinity;

            foreach (Collider2D target in targetsInRange)
            {
                if (target.CompareTag("Player") || target.CompareTag("Castle"))
                {
                    float distance = Vector2.Distance(transform.position, target.transform.position);
                    if (distance < minDistance)
                    {
                        attackTarget = target.transform;
                        minDistance = distance;
                    }
                }
            }

            if (attackTarget != null)
            {
                anim.SetBool("Attack", true);
                isAttacking = true;
                isFighting = true;
                rb.velocity = Vector2.zero;
            }
            else
            {
                anim.SetBool("Attack", false);
                isAttacking = false;
                isFighting = false;
            }
        }
    }

    protected override void MoveToTarget()
    {
        Transform nearestTarget = FindNearestTarget();

        if (nearestTarget != null)
        {
            moveTarget = nearestTarget;
            transform.position = Vector2.MoveTowards(transform.position, moveTarget.position, speed * Time.deltaTime);
        }
        else
        {
            moveTarget = null;
        }
    }

    protected override Transform FindNearestTarget()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, raycastDistanceToMove);
        Transform nearestTarget = null;
        float nearestDistance = Mathf.Infinity;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player") || collider.CompareTag("Castle"))
            {
                float distanceToTarget = Vector2.Distance(transform.position, collider.transform.position);
                if (distanceToTarget < nearestDistance)
                {
                    nearestTarget = collider.transform;
                    nearestDistance = distanceToTarget;
                }
            }
        }

        return nearestTarget;
    }


    public override void TakeDamage(int damage)
    {
        if (isDead) return;

        base.TakeDamage(damage);
        if (HP <= 0 && isDead)
        {
            anim.SetTrigger("Death");
            StartCoroutine(DestroyAfterDeath());
            unitSounds.PlayDeathSound();
        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1f); // Задержка в секундах перед уничтожением объекта
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        FindObjectOfType<ScienceManager>().UpdateScienceCountEnemy();
        // Оставляем пустым, чтобы не вызывать Destroy в базовом классе
    }

    public void FireProjectile()
    {
        if (attackTarget != null)
        {
            LaunchProjectile(attackTarget);
        }
    }

}
