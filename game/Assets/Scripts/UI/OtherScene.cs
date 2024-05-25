using UnityEngine;
using TMPro;

public class OtherScene : MonoBehaviour
{
    public TextMeshProUGUI scienceTotalCount; // Ссылка на компонент TextMeshPro для отображения количества науки всего

    private void Start()
    {
        // Показываем значение ScienceTotal из GameManager
        scienceTotalCount.text = GameManager.TotalScience.ToString();
    }
}
