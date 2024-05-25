using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeLevel
{
    public int levelwar;
    public int hpwar;
    public int damagewar;
    public float speed;
    public int costwar;
    public int damageUpTextwar;
    public int hpUpTextwar;
}

public class PlayerWarrior : PlayerBase
{
    private const string WarriorHPPrefKey = "WarriorHP";
    private const string WarriorDamagePrefKey = "WarriorDamage";

    public List<UpgradeLevel> upgradeLevels = new List<UpgradeLevel>(); // Инициализация списка

    protected override void Start()
    {
        // При запуске игры загружаем сохраненные значения или устанавливаем начальные значения
        UpdatePlayerStats(); // Обновляем характеристики при старте
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
        PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
    }

    // Метод для обновления характеристик персонажа в соответствии с текущими значениями из PlayerPrefs
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(WarriorHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(WarriorDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    public override void TakeDamage(int damagewar)
    {
        base.TakeDamage(damagewar);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
