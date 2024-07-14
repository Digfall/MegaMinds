using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warrior : EnemyBase
{
    [SerializeField] private TextMeshProUGUI warriorLvlText;
    [SerializeField] private SpriteRenderer bodyRenderer;
    [SerializeField] private SpriteRenderer weaponRenderer;

    public Animator anim;

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
                HP = 45;
                damage = 8;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 2:
                HP = 50;
                damage = 8;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 3:
                HP = 55;
                damage = 9;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 4:
                HP = 62;
                damage = 10;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 5:
                HP = 70;
                damage = 12;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 6:
                HP = 78;
                damage = 13;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 7:
                HP = 86;
                damage = 14;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 8:
                HP = 94;
                damage = 16;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            case 9:
                HP = 102;
                damage = 17;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            case 10:
                HP = 110;
                damage = 18;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            default:
                HP = 45;
                damage = 8;
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
