using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeRogues
{
    public int levelrog = 1;
    public int hprog;
    public int damagerog;
    public float speed;
    public int costrog;
    public int damageUpTextrog;
    public int hpUpTextrog;
}

public class PlayerRogue : PlayerBase
{
    [SerializeField] private TextMeshProUGUI rogueLvlText;
    [SerializeField] private int CurrentLevelRog = 1;
    private const string RogueHPPrefKey = "RogueHP";
    private const string RogueDamagePrefKey = "RogueDamage";
    public Animator anim;

    public List<UpgradeRogues> upgradeLevels = new List<UpgradeRogues>();

    protected override void Start()
    {
        CurrentLevelRog = PlayerPrefs.GetInt("CurrentLevelRog", CurrentLevelRog);
        UpdateText();
        UpdatePlayerStats();
        base.Start();
    }

    private void UpdateText()
    {
        if (rogueLvlText != null)
        {
            rogueLvlText.text = CurrentLevelRog.ToString();
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(RogueHPPrefKey, HP);
        PlayerPrefs.SetInt(RogueDamagePrefKey, damage);
        PlayerPrefs.Save();
    }

    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(RogueHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(RogueDamagePrefKey, damage);
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
