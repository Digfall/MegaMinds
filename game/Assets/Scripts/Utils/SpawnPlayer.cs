using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayer : MonoBehaviour
{
    [Header("Настройки Роги")]
    [SerializeField] private GameObject roguePrefab;
    [SerializeField] private int rogueCost = 2;
    [SerializeField] private TextMeshProUGUI rogueCostText;
    [SerializeField] private TextMeshProUGUI rogueLvlText;
    [SerializeField] private Image rogueImage;
    [SerializeField] private Sprite[] rogueSprites;
    [SerializeField] private int CurrentLevelRog = 1;
    [Header("Настройки Танка")]
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private int tankCost = 5;
    [SerializeField] private TextMeshProUGUI tankCostText;
    [SerializeField] private TextMeshProUGUI tankLvlText;
    [SerializeField] private Image tankImage;
    [SerializeField] private Sprite[] tankSprites;
    [SerializeField] private int CurrentLevelTank = 1;
    [Header("Настройки Воина")]
    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] private int warriorCost = 4;
    [SerializeField] private TextMeshProUGUI warriorCostText;
    [SerializeField] private TextMeshProUGUI warriorLvlText;
    [SerializeField] private Image warriorImage;
    [SerializeField] private Sprite[] warriorSprites;
    [SerializeField] private int CurrentLevelWar = 1;
    [Header("Настройки Ренжа")]
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private int rangerCost = 4;
    [SerializeField] private TextMeshProUGUI rangerCostText;
    [SerializeField] private TextMeshProUGUI rangerLvlText;
    [SerializeField] private Image rangerImage;
    [SerializeField] private Sprite[] rangerSprites;
    [SerializeField] private int CurrentLevelRng = 1;


    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private CoinManager coinManager;
    [SerializeField] private TextMeshProUGUI coinCountText;

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
