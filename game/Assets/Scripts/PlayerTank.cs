using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeTanks
{
    public int levelTank;
    public int hpTank;
    public int damageTank;
    public float speed;
    public int costTank;
    public int damageUpTextTank;
    public int hpUpTextTank;
}

public class PlayerTank : PlayerBase
{
    private const string TankHPPrefKey = "TankHP";
    private const string TankDamagePrefKey = "TankDamage";

    public List<UpgradeTanks> upgradeLevels = new List<UpgradeTanks>(); // Инициализация списка

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
        PlayerPrefs.SetInt(TankHPPrefKey, HP);
        PlayerPrefs.SetInt(TankDamagePrefKey, damage);
        PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
    }

    // Метод для обновления характеристик персонажа в соответствии с текущими значениями из PlayerPrefs
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(TankHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(TankDamagePrefKey, damage);
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
