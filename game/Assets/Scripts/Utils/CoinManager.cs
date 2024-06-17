using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private Image coinImage;
    [SerializeField] private TextMeshProUGUI coinCountText;
    [SerializeField] private float coinGenerationTime = 2f;
    private float elapsedTime = 0f;
    public int coinCount = 0;
    public int maxCoinCount = 10;


    private void Update()
    {
        elapsedTime += Time.deltaTime;

        float progress = Mathf.Clamp01(elapsedTime / coinGenerationTime);

        coinImage.fillAmount = progress;

        if (elapsedTime >= coinGenerationTime)
        {
            if (coinCount < maxCoinCount)
            {
                elapsedTime = 0f;
                coinCount++;
                UpdateCoinCountText();
            }
        }
    }

    public void UpdateCoinCountText()
    {
        coinCountText.text = coinCount.ToString();
    }
}
