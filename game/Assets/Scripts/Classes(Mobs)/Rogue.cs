using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Rogue : EnemyBase
{
    [SerializeField] private TextMeshProUGUI rogueLvlText;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer weaponRenderer;

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
        base.OnAttack();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override Transform FindNearestTarget()
    {
        return base.FindNearestTarget();
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
