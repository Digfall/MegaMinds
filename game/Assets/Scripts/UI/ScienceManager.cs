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

    public int scienceCount = 0;
    public int ScienceCastleCount = 0;
    public int ScienceTotal = 0;
    public int ScienceTotalLose = 0;

    private void Update()
    {
    }

    public void UpdateScienceCountEnemy()
    {
        scienceCount += 25;
        scienceCountEnemy.text = scienceCount.ToString();
        scienceCountEnemyEnd.text = scienceCount.ToString();
        scienceCountEnemyEndLose.text = scienceCount.ToString();
    }

    public void UpdateScienceCountCastle(int level)
    {
        int reward = 0;
        switch (level)
        {
            case 1:
                reward = 350;
                break;
            case 2:
                reward = 500;
                break;
            case 3:
                reward = 1000;
                break;
            // третий сам добавь 
            default:
                reward = 350;
                break;
        }
        ScienceCastleCount += reward;
        scienceCountCastle.text = ScienceCastleCount.ToString();
        UpdateScienceCountTotal();
    }

    public void UpdateScienceCountTotal()
    {
        ScienceTotal = GameManager.TotalScience + scienceCount + ScienceCastleCount;
        scienceTotalCount.text = ScienceTotal.ToString();
        GameManager.TotalScience = ScienceTotal;
    }

    public void UpdateScienceCountTotalLose()
    {
        ScienceTotalLose = GameManager.TotalScience + scienceCount;
        scienceTotalCountLose.text = ScienceTotalLose.ToString();
        GameManager.TotalScience = ScienceTotalLose;
    }
}