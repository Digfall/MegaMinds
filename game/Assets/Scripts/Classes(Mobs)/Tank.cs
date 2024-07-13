using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tank : EnemyBase
{
    [SerializeField] private TextMeshProUGUI tankLvlText;
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
        if (tankLvlText != null)
        {
            tankLvlText.text = level.ToString();
        }
    }
    public override void ApplyLevelAdjustments()
    {
        switch (level)
        {
            case 1:
                HP = 84;
                damage = 8;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 2:
                HP = 90;
                damage = 9;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 3:
                HP = 96;
                damage = 10;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 4:
                HP = 104;
                damage = 10;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;
            case 5:
                HP = 70;
                damage = 11;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 6:
                HP = 112;
                damage = 12;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 7:
                HP = 132;
                damage = 13;
                bodyRenderer.sprite = bodySpriteLvl2;
                weaponRenderer.sprite = weaponSpriteLvl2;
                break;
            case 8:
                HP = 142;
                damage = 14;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            case 9:
                HP = 152;
                damage = 15;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            case 10:
                HP = 162;
                damage = 16;
                bodyRenderer.sprite = bodySpriteLvl3;
                weaponRenderer.sprite = weaponSpriteLvl3;
                break;
            default:
                HP = 84;
                damage = 8;
                bodyRenderer.sprite = bodySpriteLvl1;
                weaponRenderer.sprite = weaponSpriteLvl1;
                break;


            // case 1:
            //     HP = 400;
            //     damage = 20;
            //     bodyRenderer.sprite = bodySpriteLvl1;
            //     weaponRenderer.sprite = weaponSpriteLvl1;
            //     break;
            // case 2:
            //     HP = 1300;
            //     damage = 100;
            //     bodyRenderer.sprite = bodySpriteLvl2;
            //     weaponRenderer.sprite = weaponSpriteLvl2;
            //     break;
            // case 3:
            //     HP = 2000;
            //     damage = 150;
            //     bodyRenderer.sprite = bodySpriteLvl3;
            //     weaponRenderer.sprite = weaponSpriteLvl3;
            //     break;
            // default:
            //     HP = 400;
            //     damage = 20;
            //     bodyRenderer.sprite = bodySpriteLvl1;
            //     weaponRenderer.sprite = weaponSpriteLvl1;
            //     break;
        }
        speed = 1.2f;
        attackRate = 1.5f;
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
