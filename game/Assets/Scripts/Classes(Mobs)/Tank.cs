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
                HP = 400;
                damage = 20;
                break;
            case 2:
                HP = 1300;
                damage = 100;
                break;
            case 3:
                HP = 2000;
                damage = 150;
                break;
            default:
                HP = 400;
                damage = 20;
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
