using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeRangers
{
    public int levelran;
    public int hpran;
    public int damageran;
    public float speed;
    public int costran;
    public int damageUpTextran;
    public int hpUpTextran;
}

public class PlayerRanger : PlayerBase
{
    private const string RangerHPPrefKey = "RangerHP";
    private const string RangerDamagePrefKey = "RangerDamage";

    public List<UpgradeRangers> upgradeLevels = new List<UpgradeRangers>(); // Инициализация списка

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
        PlayerPrefs.SetInt(RangerHPPrefKey, HP);
        PlayerPrefs.SetInt(RangerDamagePrefKey, damage);
        PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
    }

    // Метод для обновления характеристик персонажа в соответствии с текущими значениями из PlayerPrefs
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(RangerHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(RangerDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    public override void TakeDamage(int damageran)
    {
        base.TakeDamage(damageran);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
