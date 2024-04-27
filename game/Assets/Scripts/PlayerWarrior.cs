using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWarrior : PlayerBase
{
    private const string HPPrefKey = "PlayerHP";
    private const string DamagePrefKey = "PlayerDamage";
    private const string SpeedPrefKey = "PlayerSpeed";

    protected override void Start()
    {
        // При запуске игры загружаем сохраненные значения или устанавливаем начальные значения
        HP = PlayerPrefs.GetInt(HPPrefKey, 200);
        damage = PlayerPrefs.GetInt(DamagePrefKey, 50);
        speed = PlayerPrefs.GetFloat(SpeedPrefKey, 2f);
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
        PlayerPrefs.SetInt(HPPrefKey, HP);
        PlayerPrefs.SetInt(DamagePrefKey, damage);
        PlayerPrefs.SetFloat(SpeedPrefKey, speed);
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
