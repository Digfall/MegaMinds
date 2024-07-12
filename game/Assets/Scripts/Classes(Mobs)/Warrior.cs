using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warrior : EnemyBase
{
    [SerializeField] private TextMeshProUGUI warriorLvlText;
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
        if (warriorLvlText != null)
        {
            warriorLvlText.text = level.ToString();
        }
    }

    public override void ApplyLevelAdjustments()
    {
        switch (level)
        {
            case 1:
                HP = 200;
                damage = 50;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 2:
                HP = 400;
                damage = 90;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 3:
                HP = 600;
                damage = 149;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            default:
                HP = 200;
                damage = 50;
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
