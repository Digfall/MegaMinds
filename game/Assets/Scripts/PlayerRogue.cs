using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UpgradeRogues
{
    public int levelrog;
    public int hprog;
    public int damagerog;
    public float speed;
    public int costrog;
    public int damageUpTextrog;
    public int hpUpTextrog;
}

public class PlayerRogue : PlayerBase
{
    private const string RogueHPPrefKey = "RogueHP";
    private const string RogueDamagePrefKey = "RogueDamage";

    public List<UpgradeRogues> upgradeLevels = new List<UpgradeRogues>(); // Инициализация списка

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
        PlayerPrefs.SetInt(RogueHPPrefKey, HP);
        PlayerPrefs.SetInt(RogueDamagePrefKey, damage);
        PlayerPrefs.Save(); // Сохраняем изменения в PlayerPrefs
    }

    // Метод для обновления характеристик персонажа в соответствии с текущими значениями из PlayerPrefs
    public void UpdatePlayerStats()
    {
        HP = PlayerPrefs.GetInt(RogueHPPrefKey, HP);
        damage = PlayerPrefs.GetInt(RogueDamagePrefKey, damage);
    }

    protected override void OnAttack()
    {
        base.OnAttack();
    }

    public override void TakeDamage(int damagerog)
    {
        base.TakeDamage(damagerog);
    }

    protected override void FindTargetToAttack()
    {
        base.FindTargetToAttack();
    }
}
