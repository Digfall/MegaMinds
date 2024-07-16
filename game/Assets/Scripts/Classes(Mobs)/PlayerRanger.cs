using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeRangers
{
    public int levelran = 1;
    public int hpran;
    public int damageran;
    public float speed;
    public int costran;
    public int damageUpTextran;
    public int hpUpTextran;
    public Sprite bodySprite;
    public Sprite wepSprite;
}

public class PlayerRanger : PlayerBase
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform projectileSpawnPoint;
    [SerializeField] private TextMeshProUGUI rangerLvlText;
    [SerializeField] private int CurrentLevelRng = 1;
    private const string RangerHPPrefKey = "RangerHP";
    private const string RangerDamagePrefKey = "RangerDamage";
    public Animator anim;
    [SerializeField] private UnitSounds unitSound;

    public List<UpgradeRangers> upgradeLevels = new List<UpgradeRangers>();
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer wepRenderer;

    protected override void Start()
    {
        CurrentLevelRng = PlayerPrefs.GetInt("CurrentLevelRng", CurrentLevelRng);
        UpdateText();
        UpdatePlayerStats();
        UpdateSprites();
        base.Start();
    }

    private void UpdateText()
    {
        if (rangerLvlText != null)
        {
            rangerLvlText.text = CurrentLevelRng.ToString();
        }
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

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(RangerHPPrefKey, HP);
        PlayerPrefs.SetInt(RangerDamagePrefKey, damage);
        PlayerPrefs.Save();
    }
    private void UpdateSprites()
    {
        UpgradeRangers currentLevel = upgradeLevels.Find(level => level.levelran == CurrentLevelRng);
        if (currentLevel != null)
        {
            bodyRenderer.sprite = currentLevel.bodySprite;
            wepRenderer.sprite = currentLevel.wepSprite;
        }
    }
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(RangerHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(RangerDamagePrefKey, damage);
    }

    public void FireProjectile()
    {
        if (attackTarget != null)
        {
            LaunchProjectile(attackTarget);
        }
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            unitSound.PlayAttackSound();
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
                if (target.CompareTag("Enemy") || target.CompareTag("EnemyCastle"))
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
            if (collider.CompareTag("Enemy") || collider.CompareTag("EnemyCastle"))
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

    public override void TakeDamage(int damageran)
    {
        if (isDead) return;

        base.TakeDamage(damageran);
        if (HP <= 0 && isDead)
        {
            anim.SetTrigger("Death");
            StartCoroutine(DestroyAfterDeath());
            unitSound.PlayDeathSound();
        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(1f); // Задержка в секундах перед уничтожением объекта
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        // Оставляем пустым, чтобы не вызывать Destroy в базовом классе
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raycastDistanceToMove);
    }
}