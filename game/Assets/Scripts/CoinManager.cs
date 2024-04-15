using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public Image coinImage; // Ссылка на изображение монеты
    public TextMeshProUGUI coinCountText; // Ссылка на компонент TextMeshPro для отображения количества монет
    public float coinGenerationTime = 3f; // Время, через которое генерируется монета (в секундах)

    private float elapsedTime = 0f;
    public int coinCount = 0;
    public int maxCoinCount = 10; // Максимальное количество монет


    private void Update()
    {
        // Увеличиваем прошедшее время
        elapsedTime += Time.deltaTime;

        // Вычисляем прогресс генерации монеты от 0 до 1
        float progress = Mathf.Clamp01(elapsedTime / coinGenerationTime);

        // Обновляем заполнение изображения монеты
        coinImage.fillAmount = progress;

        // Если прошло достаточно времени для генерации монеты
        if (elapsedTime >= coinGenerationTime)
        {
            // Если максимальное количество монет не достигнуто
            if (coinCount < maxCoinCount)
            {
                // Сбрасываем время и генерируем новую монету
                elapsedTime = 0f;
                coinCount++; // Увеличиваем количество монет
                UpdateCoinCountText(); // Обновляем текст с количеством монет
            }
        }
    }

    public void UpdateCoinCountText()
    {
        // Обновляем текстовый компонент с количеством монет
        coinCountText.text = coinCount.ToString();
    }
}
