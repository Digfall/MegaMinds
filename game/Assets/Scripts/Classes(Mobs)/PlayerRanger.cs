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
}

public class PlayerRanger : PlayerBase
{
    [SerializeField] private TextMeshProUGUI rangerLvlText;
    [SerializeField] private int CurrentLevelRng = 1;
    private const string RangerHPPrefKey = "RangerHP";
    private const string RangerDamagePrefKey = "RangerDamage";

    public List<UpgradeRangers> upgradeLevels = new List<UpgradeRangers>();

    protected override void Start()
    {
        CurrentLevelRng = PlayerPrefs.GetInt("CurrentLevelRng", CurrentLevelRng);
        UpdateText();
        UpdatePlayerStats();
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

    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(RangerHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(RangerDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            EnemyBase enemyBase = attackTarget.GetComponent<EnemyBase>();
            EnemyCastle enemyCastle = attackTarget.GetComponent<EnemyCastle>();

            if (enemyBase != null)
            {
                enemyBase.TakeDamage(damage);
            }
            else if (enemyCastle != null)
            {
                enemyCastle.TakeDamage(damage);
            }

            nextDamageTime = Time.time + damageRate;
            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    protected override void FindTargetToAttack()
    {
        if (attackTarget != null && (Vector2.Distance(transform.position, attackTarget.position) > radius || !attackTarget.CompareTag("Enemy") && !attackTarget.CompareTag("EnemyCastle")))
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
                isAttacking = true;
                isFighting = true;
                rb.velocity = Vector2.zero;
            }
            else
            {
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
        base.TakeDamage(damageran);
    }

    protected override void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(attackPos.position, radius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, raycastDistanceToMove);
    }
}
