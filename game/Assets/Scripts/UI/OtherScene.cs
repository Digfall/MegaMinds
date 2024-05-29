using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OtherScene : MonoBehaviour
{
    public TextMeshProUGUI scienceTotalCount; // Ссылка на компонент TextMeshPro для отображения количества науки всего

    void Start()
    {
        UpdateTotalScienceText();
    }
    public void UpdateTotalScienceText()
    {
        scienceTotalCount.text = GameManager.TotalScience.ToString();
    }
}
