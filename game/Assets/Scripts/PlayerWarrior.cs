using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarrior : PlayerBase
{
    private const string WarriorHPPrefKey = "WarriorHP";
    private const string WarriorDamagePrefKey = "WarriorDamage";
    private const string WarriorSpeedPrefKey = "WarriorSpeed";

    protected override void Start()
    {
        // При запуске игры загружаем сохраненные значения или устанавливаем начальные значения
        HP = PlayerPrefs.GetInt(WarriorHPPrefKey, 200);
        damage = PlayerPrefs.GetInt(WarriorDamagePrefKey, 50);
        speed = PlayerPrefs.GetFloat(WarriorSpeedPrefKey, 2f);
        radius = 0.8f;
        attackRate = 0.8f;

        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    // Метод для сохранения характеристик при улучшении
    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(WarriorHPPrefKey, HP);
        PlayerPrefs.SetInt(WarriorDamagePrefKey, damage);

    }
    protected override void OnAttack()
    {
        base.OnAttack();
    }
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
