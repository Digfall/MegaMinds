using TMPro;
using UnityEngine;

public class SpawnPlayer : MonoBehaviour
{
    // Префабы юнитов и их стоимости
    public GameObject roguePrefab;
    public int rogueCost = 3;
    public TextMeshProUGUI rogueCostText;
    public TextMeshProUGUI rogueLvlText;
    public int CurrentLevelRog;

    public GameObject tankPrefab;
    public int tankCost = 5;
    public TextMeshProUGUI tankCostText;
    public TextMeshProUGUI tankLvlText;
    public int CurrentLevelTank;

    public GameObject warriorPrefab;
    public int warriorCost = 4;
    public TextMeshProUGUI warriorCostText;
    public TextMeshProUGUI warriorLvlText;
    public int CurrentLevelWar;

    public GameObject rangerPrefab;
    public int rangerCost = 6;
    public TextMeshProUGUI rangerCostText;
    public TextMeshProUGUI rangerLvlText;
    public int CurrentLevelRng;

    public Transform[] spawnPositions;

    public CoinManager coinManager;
    public TextMeshProUGUI coinCountText;

    private void LoadUnitCosts()
    {
        // Загружаем стоимость юнитов из PlayerPrefs
        CurrentLevelRog = PlayerPrefs.GetInt("CurrentLevelRog", CurrentLevelRog);
        CurrentLevelTank = PlayerPrefs.GetInt("CurrentLevelTank", CurrentLevelTank);
        CurrentLevelWar = PlayerPrefs.GetInt("CurrentLevelWar", CurrentLevelWar);
        CurrentLevelRng = PlayerPrefs.GetInt("CurrentLevelRng", CurrentLevelRng);
    }

    private void UpdateLvlText()
    {
        // Обновляем только те текстовые элементы, которые существуют
        if (rogueLvlText != null)
        {
            rogueLvlText.text = CurrentLevelRog.ToString();
        }
        if (tankLvlText != null)
        {
            tankLvlText.text = CurrentLevelTank.ToString();
        }
        if (warriorLvlText != null)
        {
            warriorLvlText.text = CurrentLevelWar.ToString();
        }
        if (rangerLvlText != null)
        {
            rangerLvlText.text = CurrentLevelRng.ToString();
        }
    }

    private void Start()
    {
        LoadUnitCosts();
        // Обновляем текстовые элементы для отображения стоимости юнитов
        UpdateCostTexts();
        UpdateLvlText();
    }

    private void UpdateCostTexts()
    {
        // Обновляем только те текстовые элементы, которые существуют
        if (rogueCostText != null)
        {
            rogueCostText.text = rogueCost.ToString();
        }
        if (tankCostText != null)
        {
            tankCostText.text = tankCost.ToString();
        }
        if (warriorCostText != null)
        {
            warriorCostText.text = warriorCost.ToString();
        }
        if (rangerCostText != null)
        {
            rangerCostText.text = rangerCost.ToString();
        }
    }

    // Метод спавна юнита
    public void SpawnUnit(GameObject unitPrefab, int cost)
    {
        if (coinManager.coinCount >= cost)
        {
            coinManager.coinCount -= cost;
            coinManager.UpdateCoinCountText();

            int randomIndex = Random.Range(0, spawnPositions.Length);
            Transform selectedSpawnPoint = spawnPositions[randomIndex];
            GameObject unitInstance = Instantiate(unitPrefab, selectedSpawnPoint.position, Quaternion.identity);

            HealthBar healthBar = unitInstance.GetComponentInChildren<HealthBar>();
            if (healthBar != null)
            {
                healthBar.gameObject.SetActive(true);
            }
        }
    }

    // Методы спавна конкретных юнитов
    public void SpawnRogue()
    {
        SpawnUnit(roguePrefab, rogueCost);
    }

    public void SpawnTank()
    {
        SpawnUnit(tankPrefab, tankCost);
    }

    public void SpawnWarrior()
    {
        SpawnUnit(warriorPrefab, warriorCost);
    }

    public void SpawnRanger()
    {
        SpawnUnit(rangerPrefab, rangerCost);
    }
}
