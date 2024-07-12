using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tank : EnemyBase
{
    [SerializeField] private TextMeshProUGUI tankLvlText;
    protected override void Start()
    {
        UpdateText();
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
                break;
            case 2:
                HP = 90;
                damage = 9;
                break;
            case 3:
                HP = 96;
                damage = 10;
                break;
            case 4:
                HP = 104;
                damage = 10;
                break;
            case 5:
                HP = 70;
                damage = 11;
                break;
            case 6:
                HP = 112;
                damage = 12;
                break;
            case 7:
                HP = 132;
                damage = 13;
                break;
            case 8:
                HP = 142;
                damage = 14;
                break;
            case 9:
                HP = 152;
                damage = 15;
                break;
            case 10:
                HP = 162;
                damage = 16;
                break;
            default:
                HP = 84;
                damage = 8;
                break;
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
