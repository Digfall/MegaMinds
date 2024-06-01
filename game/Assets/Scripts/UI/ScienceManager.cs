using UnityEngine;
using TMPro;

public class ScienceManager : MonoBehaviour
{
    public TextMeshProUGUI scienceCountEnemy;
    public TextMeshProUGUI scienceCountEnemyEnd;
    public TextMeshProUGUI scienceCountCastle;
    public TextMeshProUGUI scienceTotalCount;
    public TextMeshProUGUI scienceCountEnemyEndLose;
    public TextMeshProUGUI scienceTotalCountLose;

    private int scienceCount = 0;
    private int scienceCastleCount = 0;

    private void Update()
    {
    }

    public void UpdateScienceCountEnemy()
    {
        scienceCount += 25;
        UpdateUI();
    }

    public void UpdateScienceCountCastle(int level)
    {
        int reward = GetRewardForLevel(level);
        scienceCastleCount += reward;
        UpdateUI();
    }

    private int GetRewardForLevel(int level)
    {
        switch (level)
        {
            case 1: return 350;
            case 2: return 500;
            case 3: return 1000;
            default: return 350;
        }
    }

    public void UpdateUI()
    {
        scienceCountEnemy.text = scienceCount.ToString();
        scienceCountEnemyEnd.text = scienceCount.ToString();
        scienceCountEnemyEndLose.text = scienceCount.ToString();
        scienceCountCastle.text = scienceCastleCount.ToString();

        int totalScience = GameManager.TotalScience + scienceCount + scienceCastleCount;
        scienceTotalCount.text = totalScience.ToString();
        GameManager.TotalScience = totalScience;

        int totalScienceLose = GameManager.TotalScience + scienceCount;
        scienceTotalCountLose.text = totalScienceLose.ToString();
    }
}
