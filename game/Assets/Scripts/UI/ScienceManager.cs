using UnityEngine;
using TMPro;

public class ScienceManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scienceCountEnemy;
    [SerializeField] private TextMeshProUGUI scienceCountEnemyEnd;
    [SerializeField] private TextMeshProUGUI scienceCountCastle;
    [SerializeField] private TextMeshProUGUI scienceTotalCount;
    [SerializeField] private TextMeshProUGUI scienceCountEnemyEndLose;
    [SerializeField] private TextMeshProUGUI scienceTotalCountLose;

    [SerializeField] private int scienceCount = 0;
    [SerializeField] private int ScienceCastleCount = 0;
    [SerializeField] private int ScienceTotal = 0;
    [SerializeField] private int ScienceTotalLose = 0;

    public void UpdateScienceCountEnemy()
    {
        scienceCount += 1;
        scienceCountEnemy.text = scienceCount.ToString();
        scienceCountEnemyEnd.text = scienceCount.ToString();
        scienceCountEnemyEndLose.text = scienceCount.ToString();
    }

    public void UpdateScienceCountCastle(int levelREWARD)
    {
        int reward = 0;
        switch (levelREWARD)
        {
            case 1:
                reward = 40;
                break;
            default:
                reward = 40;
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
