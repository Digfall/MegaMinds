using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Warrior : EnemyBase
{
    [SerializeField] private TextMeshProUGUI warriorLvlText;

    protected override void Start()
    {
        UpdateText();
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
                break;
            case 2:
                HP = 50;
                damage = 8;
                break;
            case 3:
                HP = 55;
                damage = 9;
                break;
            case 4:
                HP = 62;
                damage = 10;
                break;
            case 5:
                HP = 70;
                damage = 12;
                break;
            case 6:
                HP = 78;
                damage = 13;
                break;
            case 7:
                HP = 86;
                damage = 14;
                break;
            case 8:
                HP = 94;
                damage = 16;
                break;
            case 9:
                HP = 102;
                damage = 17;
                break;
            case 10:
                HP = 110;
                damage = 18;
                break;
            default:
                HP = 45;
                damage = 8;
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
