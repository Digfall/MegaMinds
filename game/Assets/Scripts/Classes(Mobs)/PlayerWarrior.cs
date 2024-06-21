using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeLevel
{
    public int levelwar = 1;
    public int hpwar;
    public int damagewar;
    public float speed;
    public int costwar;
    public int damageUpTextwar;
    public int hpUpTextwar;
}

public class PlayerWarrior : PlayerBase
{
    [SerializeField] private TextMeshProUGUI warriorLvlText;
    [SerializeField] private int CurrentLevelWar = 1;
    private const string WarriorHPPrefKey = "WarriorHP";
    private const string WarriorDamagePrefKey = "WarriorDamage";
    public Animator anim;

    public List<UpgradeLevel> upgradeLevels = new List<UpgradeLevel>();

    protected override void Start()
    {
        CurrentLevelWar = PlayerPrefs.GetInt("CurrentLevelWar", CurrentLevelWar);
        UpdateText();
        UpdatePlayerStats();
        base.Start();
    }

    private void UpdateText()
    {
        if (warriorLvlText != null)
        {
            warriorLvlText.text = CurrentLevelWar.ToString();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(WarriorHPPrefKey, HP);
        PlayerPrefs.SetInt(WarriorDamagePrefKey, damage);
        PlayerPrefs.Save();
    }

    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(WarriorHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(WarriorDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            isAttacking = true;
            isFighting = true;

            if (attackTarget.CompareTag("Enemy") || attackTarget.CompareTag("EnemyCastle"))
            {
                attackTarget.GetComponent<EnemyBase>()?.TakeDamage(damage);
                attackTarget.GetComponent<EnemyCastle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public override void TakeDamage(int damagewar)
    {
        if (isDead) return;

        base.TakeDamage(damagewar);
        if (HP <= 0 && isDead)
        {
            anim.SetBool("Death", true);
            StartCoroutine(DestroyAfterDeath());
        }
    }

    private IEnumerator DestroyAfterDeath()
    {
        yield return new WaitForSeconds(0.8f); // Задержка в секундах перед уничтожением объекта
        Destroy(gameObject);
    }

    protected override void OnDeath()
    {
        // Оставляем пустым, чтобы не вызывать Destroy в базовом классе
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

    protected override void FindTargetToAttack()
    {
        Collider2D[] targetsInRange = Physics2D.OverlapCircleAll(attackPos.position, radius);
        if (targetsInRange.Length > 0)
        {
            Transform closestTarget = null;
            float minDistance = Mathf.Infinity;

            foreach (Collider2D target in targetsInRange)
            {
                if (target.CompareTag("Enemy") || target.CompareTag("EnemyCastle"))
                {
                    float distance = Vector2.Distance(transform.position, target.transform.position);
                    if (distance < minDistance)
                    {
                        closestTarget = target.transform;
                        minDistance = distance;
                    }
                }
            }

            attackTarget = closestTarget;

            if (attackTarget != null)
            {
                anim.SetBool("Attack", true);
                isAttacking = true;
                isFighting = true;
                OnAttack();
            }
            else
            {
                anim.SetBool("Attack", false);
                isAttacking = false;
                isFighting = false;
            }
        }
        else
        {
            attackTarget = null;
            isAttacking = false;
            isFighting = false;
        }
    }
}
