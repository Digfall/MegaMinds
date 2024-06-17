using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class UpgradeRogues
{
    public int levelrog = 1;
    public int hprog;
    public int damagerog;
    public float speed;
    public int costrog;
    public int damageUpTextrog;
    public int hpUpTextrog;
}

public class PlayerRogue : PlayerBase
{
    [SerializeField] private TextMeshProUGUI rogueLvlText;
    [SerializeField] private int CurrentLevelRog = 1;
    private const string RogueHPPrefKey = "RogueHP";
    private const string RogueDamagePrefKey = "RogueDamage";

    public List<UpgradeRogues> upgradeLevels = new List<UpgradeRogues>();

    protected override void Start()
    {
        CurrentLevelRog = PlayerPrefs.GetInt("CurrentLevelRog", CurrentLevelRog);
        UpdateText();
        UpdatePlayerStats();
        base.Start();
    }
    private void UpdateText()
    {
        if (rogueLvlText != null)
        {
            rogueLvlText.text = CurrentLevelRog.ToString();
        }
    }
    protected override void Update()
    {
        base.Update();
    }

    public void SavePlayerStats()
    {
        PlayerPrefs.SetInt(RogueHPPrefKey, HP);
        PlayerPrefs.SetInt(RogueDamagePrefKey, damage);
        PlayerPrefs.Save();
    }

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
