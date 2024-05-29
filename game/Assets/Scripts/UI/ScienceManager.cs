using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScienceManager : MonoBehaviour
{

    public TextMeshProUGUI scienceCountEnemy; // Ссылка на компонент TextMeshPro для отображения количества науки
    public TextMeshProUGUI scienceCountEnemyEnd; // Ссылка на компонент TextMeshPro для отображения количества науки
    public TextMeshProUGUI scienceCountCastle; // Ссылка на компонент TextMeshPro для отображения количества науки за крепость
    public TextMeshProUGUI scienceTotalCount; // Ссылка на компонент TextMeshPro для отображения количества науки всего

    public TextMeshProUGUI scienceCountEnemyEndLose; // Ссылка на компонент TextMeshPro для отображения количества науки
    public TextMeshProUGUI scienceTotalCountLose; // Ссылка на компонент TextMeshPro для отображения количества науки всего

    public int scienceCount = 0;
    public int ScienceCastleCount = 0;

    public int ScienceTotal = 0;
    public int ScienceTotalLose = 0;



    private void Update()
    {

    }

    public void UpdateScienceCountEnemy()
    {
        // Обновляем текстовый компонент с количеством монет
        scienceCount += 25;
        scienceCountEnemy.text = scienceCount.ToString();
        scienceCountEnemyEnd.text = scienceCount.ToString();
        scienceCountEnemyEndLose.text = scienceCount.ToString();
    }
    public void UpdateScienceCountCastle()
    {

        ScienceCastleCount += 350;
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
