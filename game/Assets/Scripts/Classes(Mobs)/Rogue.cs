using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rogue : EnemyBase
{
    [SerializeField] private TextMeshProUGUI rogueLvlText;
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
        if (rogueLvlText != null)
        {
            rogueLvlText.text = level.ToString();
        }
    }

    public override void ApplyLevelAdjustments()
    {
        switch (level)
        {
            case 1:
                HP = 50;
                damage = 50;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 2:
                HP = 120;
                damage = 100;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 3:
                HP = 200;
                damage = 200;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            default:
                HP = 50;
                damage = 50;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
        }
        speed = 2.5f;
        attackRate = 0.5f;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void OnAttack()
    {
        if (Time.time >= nextAttackTime && attackTarget != null)
        {
            isAttacking = true;
            isFighting = true;

            if (attackTarget.CompareTag("Player") || attackTarget.CompareTag("Castle"))
            {
                unitSounds.PlayAttackSound();
                attackTarget.GetComponent<PlayerBase>()?.TakeDamage(damage);
                attackTarget.GetComponent<Castle>()?.TakeDamage(damage);
                nextDamageTime = Time.time + damageRate;
            }

            nextAttackTime = Time.time + 1f / attackRate;
        }
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
        // Оставляем пустым, чтобы не вызывать Destroy в базовом классе
    }

    protected override Transform FindNearestTarget()
    {
        return base.FindNearestTarget();
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
                if (target.CompareTag("Player") || target.CompareTag("Castle"))
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
