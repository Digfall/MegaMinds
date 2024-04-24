using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScienceManager : MonoBehaviour
{

    public TextMeshProUGUI coinCountText; // Ссылка на компонент TextMeshPro для отображения количества монет

    public int coinCount = 0;


    private void Update()
    {

    }

    public void UpdateScienceCountText()
    {
        // Обновляем текстовый компонент с количеством монет
        coinCount += 50;
        coinCountText.text = coinCount.ToString();
    }
}
