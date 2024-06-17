using UnityEngine;
using TMPro;

public class OtherScene : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scienceTotalCount;
    private const string CurrentTotalSciencePrefKey = "TotalScienceKey";

    void Start()
    {
        UpdateTotalScienceText();
    }

    public void UpdateTotalScienceText()
    {

        GameManager.TotalScience = PlayerPrefs.GetInt(CurrentTotalSciencePrefKey, 0);

        scienceTotalCount.text = GameManager.TotalScience.ToString();

        Time.timeScale = 1f;
    }
}
