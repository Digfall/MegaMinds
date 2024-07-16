using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeTanks
{
    public int levelTank = 1;
    public int hpTank;
    public int damageTank;
    public float speed;
    public int costTank;
    public int damageUpTextTank;
    public int hpUpTextTank;
    public Sprite bodySprite;
    public Sprite wepSprite;
}

public class PlayerTank : PlayerBase
{
    [SerializeField] private TextMeshProUGUI tankLvlText;
    [SerializeField] private int CurrentLevelTank = 1;
    private const string TankHPPrefKey = "TankHP";
    private const string TankDamagePrefKey = "TankDamage";
    public Animator anim;
    [SerializeField] private UnitSounds unitSound;

    public List<UpgradeTanks> upgradeLevels = new List<UpgradeTanks>();
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer wepRenderer;

    protected override void Start()
    {
        CurrentLevelTank = PlayerPrefs.GetInt("CurrentLevelTank", CurrentLevelTank);
        UpdateText();
        UpdatePlayerStats();
        UpdateSprites();
        base.Start();
    }

    private void UpdateText()
    {
        if (tankLvlText != null)
        {
            tankLvlText.text = CurrentLevelTank.ToString();
        }
    }
    protected override void Update()
    {
        base.Update();
    }
    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(TankHPPrefKey, HP);
        PlayerPrefs.SetInt(TankDamagePrefKey, damage);
        PlayerPrefs.Save();
    }
    private void UpdateSprites()
    {
        UpgradeTanks currentLevel = upgradeLevels.Find(level => level.levelTank == CurrentLevelTank);
        if (currentLevel != null)
        {
            bodyRenderer.sprite = currentLevel.bodySprite;
            wepRenderer.sprite = currentLevel.wepSprite;
        }
    }
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(TankHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(TankDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            isAttacking = true;
            isFighting = true;

            if (attackTarget.CompareTag("Enemy") || attackTarget.CompareTag("EnemyCastle"))
            {
                unitSound.PlayAttackSound();
                attackTarget.GetComponent<EnemyBase>()?.TakeDamage(damage);
                attackTarget.GetComponent<EnemyCastle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
    }

    public override void TakeDamage(int damageTank)
    {
        if (isDead) return;

        base.TakeDamage(damageTank);
        if (HP <= 0 && isDead)
        {
            anim.SetBool("Death", true);
            StartCoroutine(DestroyAfterDeath());
            unitSound.PlayDeathSound();
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
