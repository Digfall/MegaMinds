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