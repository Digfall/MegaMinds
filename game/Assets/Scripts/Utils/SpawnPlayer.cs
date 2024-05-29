using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    // Префабы юнитов и их стоимости
    public GameObject roguePrefab;
    public int rogueCost = 2;
    public TextMeshProUGUI rogueCostText;
    public TextMeshProUGUI rogueLvlText;
    public Image rogueImage; // Добавьте изображение для Rogue
    public Sprite[] rogueSprites; // Массив спрайтов для разных уровней Rogue
    public int CurrentLevelRog;

    public GameObject tankPrefab;
    public int tankCost = 5;
    public TextMeshProUGUI tankCostText;
    public TextMeshProUGUI tankLvlText;
    public Image tankImage; // Добавьте изображение для Tank
    public Sprite[] tankSprites; // Массив спрайтов для разных уровней Tank
    public int CurrentLevelTank;

    public GameObject warriorPrefab;
    public int warriorCost = 4;
    public TextMeshProUGUI warriorCostText;
    public TextMeshProUGUI warriorLvlText;
    public Image warriorImage; // Добавьте изображение для Warrior
    public Sprite[] warriorSprites; // Массив спрайтов для разных уровней Warrior
    public int CurrentLevelWar;

    public GameObject rangerPrefab;
    public int rangerCost = 4;
    public TextMeshProUGUI rangerCostText;
    public TextMeshProUGUI rangerLvlText;
    public Image rangerImage; // Добавьте изображение для Ranger
    public Sprite[] rangerSprites; // Массив спрайтов для разных уровней Ranger
    public int CurrentLevelRng;


    public Transform[] spawnPositions;

    public CoinManager coinManager;
    public TextMeshProUGUI coinCountText;

    private void LoadUnitLvlInfo()
    {
        CurrentLevelRog = PlayerPrefs.GetInt("CurrentLevelRog", CurrentLevelRog);
        CurrentLevelTank = PlayerPrefs.GetInt("CurrentLevelTank", CurrentLevelTank);
        CurrentLevelWar = PlayerPrefs.GetInt("CurrentLevelWar", CurrentLevelWar);
        CurrentLevelRng = PlayerPrefs.GetInt("CurrentLevelRng", CurrentLevelRng);
    }

    private void UpdateText()
    {
        // Обновляем только те текстовые элементы, которые существуют
        if (rogueLvlText != null && rogueCostText != null && rogueImage != null && rogueSprites.Length > CurrentLevelRog)
        {
            rogueLvlText.text = CurrentLevelRog.ToString();
            rogueCostText.text = rogueCost.ToString();
            rogueImage.sprite = rogueSprites[CurrentLevelRog];
        }
        if (tankLvlText != null && tankCostText != null && tankImage != null && tankSprites.Length > CurrentLevelTank)
        {
            tankLvlText.text = CurrentLevelTank.ToString();
            tankCostText.text = tankCost.ToString();
            tankImage.sprite = tankSprites[CurrentLevelTank];
        }
        if (warriorLvlText != null && warriorCostText != null && warriorImage != null && warriorSprites.Length > CurrentLevelWar)
        {
            warriorLvlText.text = CurrentLevelWar.ToString();
            warriorCostText.text = warriorCost.ToString();
            warriorImage.sprite = warriorSprites[CurrentLevelWar];
        }
        if (rangerLvlText != null && rangerCostText != null && rangerImage != null && rangerSprites.Length > CurrentLevelRng)
        {
            rangerLvlText.text = CurrentLevelRng.ToString();
            rangerCostText.text = rangerCost.ToString();
            rangerImage.sprite = rangerSprites[CurrentLevelRng];
        }
    }

    private void Start()
    {
        LoadUnitLvlInfo();
        UpdateText();
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
