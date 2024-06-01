using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    [Header("Настройки Роги")]
    public GameObject roguePrefab;
    public int rogueCost = 2;
    public TextMeshProUGUI rogueCostText;
    public TextMeshProUGUI rogueLvlText;
    public Image rogueImage;
    public Sprite[] rogueSprites;
    public int CurrentLevelRog = 1;
    [Header("Настройки Танка")]
    public GameObject tankPrefab;
    public int tankCost = 5;
    public TextMeshProUGUI tankCostText;
    public TextMeshProUGUI tankLvlText;
    public Image tankImage;
    public Sprite[] tankSprites;
    public int CurrentLevelTank = 1;
    [Header("Настройки Воина")]
    public GameObject warriorPrefab;
    public int warriorCost = 4;
    public TextMeshProUGUI warriorCostText;
    public TextMeshProUGUI warriorLvlText;
    public Image warriorImage;
    public Sprite[] warriorSprites;
    public int CurrentLevelWar = 1;
    [Header("Настройки Ренжа")]
    public GameObject rangerPrefab;
    public int rangerCost = 4;
    public TextMeshProUGUI rangerCostText;
    public TextMeshProUGUI rangerLvlText;
    public Image rangerImage;
    public Sprite[] rangerSprites;
    public int CurrentLevelRng = 1;


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
        if (rogueLvlText != null && rogueCostText != null)
        {
            rogueLvlText.text = CurrentLevelRog.ToString();
            rogueCostText.text = rogueCost.ToString();
        }
        if (tankLvlText != null && tankCostText != null)
        {
            tankLvlText.text = CurrentLevelTank.ToString();
            tankCostText.text = tankCost.ToString();
        }
        if (warriorLvlText != null && warriorCostText != null)
        {
            warriorLvlText.text = CurrentLevelWar.ToString();
            warriorCostText.text = warriorCost.ToString();
        }
        if (rangerLvlText != null && rangerCostText != null)
        {
            rangerLvlText.text = CurrentLevelRng.ToString();
            rangerCostText.text = rangerCost.ToString();
        }
    }

    private void UpdateUnitImages()
    {
        if (rogueImage != null && rogueSprites.Length > CurrentLevelRog)
        {
            rogueImage.sprite = rogueSprites[CurrentLevelRog];
        }
        if (tankImage != null && tankSprites.Length > CurrentLevelTank)
        {
            tankImage.sprite = tankSprites[CurrentLevelTank];
        }
        if (warriorImage != null && warriorSprites.Length > CurrentLevelWar)
        {
            warriorImage.sprite = warriorSprites[CurrentLevelWar];
        }
        if (rangerImage != null && rangerSprites.Length > CurrentLevelRng)
        {
            rangerImage.sprite = rangerSprites[CurrentLevelRng];
        }
    }

    private void Start()
    {
        LoadUnitLvlInfo();
        UpdateUnitImages();
        UpdateText();
    }

    void Update()
    {
        UpdateUnitImages();
        UpdateText();
    }

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
