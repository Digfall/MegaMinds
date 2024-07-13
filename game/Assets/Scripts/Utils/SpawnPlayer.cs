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
    [SerializeField] private Image rogueBGImage;
    [SerializeField] private Sprite[] rogueBGSprites;
    [SerializeField] private Image rogueWEPImage;
    [SerializeField] private Sprite[] rogueWEPSprites;
    [SerializeField] private Image rogueBODYImage;
    [SerializeField] private Sprite[] rogueBODYSprites;
    [SerializeField] private int CurrentLevelRog = 1;
    [Header("Настройки Танка")]
    [SerializeField] private GameObject tankPrefab;
    [SerializeField] private int tankCost = 5;
    [SerializeField] private TextMeshProUGUI tankCostText;
    [SerializeField] private TextMeshProUGUI tankLvlText;
    [SerializeField] private Image tankBGImage;
    [SerializeField] private Sprite[] tankBGSprites;
    [SerializeField] private Image tankWEPImage;
    [SerializeField] private Sprite[] tankWEPSprites;
    [SerializeField] private Image tankBODYImage;
    [SerializeField] private Sprite[] tankBODYSprites;
    [SerializeField] private int CurrentLevelTank = 1;
    [Header("Настройки Воина")]
    [SerializeField] private GameObject warriorPrefab;
    [SerializeField] private int warriorCost = 4;
    [SerializeField] private TextMeshProUGUI warriorCostText;
    [SerializeField] private TextMeshProUGUI warriorLvlText;
    [SerializeField] private Image warriorBGImage;
    [SerializeField] private Sprite[] warriorBGSprites;
    [SerializeField] private Image warriorWEPImage;
    [SerializeField] private Sprite[] warriorWEPSprites;
    [SerializeField] private Image warriorBODYImage;
    [SerializeField] private Sprite[] warriorBODYSprites;
    [SerializeField] private int CurrentLevelWar = 1;
    [Header("Настройки Ренжа")]
    [SerializeField] private GameObject rangerPrefab;
    [SerializeField] private int rangerCost = 4;
    [SerializeField] private TextMeshProUGUI rangerCostText;
    [SerializeField] private TextMeshProUGUI rangerLvlText;
    [SerializeField] private Image rangerBGImage;
    [SerializeField] private Sprite[] rangerBGSprites;
    [SerializeField] private Image rangerWEPImage;
    [SerializeField] private Sprite[] rangerWEPSprites;
    [SerializeField] private Image rangerBODYImage;
    [SerializeField] private Sprite[] rangerBODYSprites;
    [SerializeField] private int CurrentLevelRng = 1;


    [SerializeField] private Transform[] spawnPositions;

    [SerializeField] private CoinManager coinManager;
    [SerializeField] private TextMeshProUGUI coinCountText;

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
        if (rogueBGImage != null && rogueBGSprites.Length > CurrentLevelRog)
        {
            rogueBGImage.sprite = rogueBGSprites[CurrentLevelRog];
            rogueWEPImage.sprite = rogueWEPSprites[CurrentLevelRog];
            rogueBODYImage.sprite = rogueBODYSprites[CurrentLevelRog];
        }
        if (tankBGImage != null && tankBGSprites.Length > CurrentLevelTank)
        {
            tankBGImage.sprite = tankBGSprites[CurrentLevelTank];
            tankWEPImage.sprite = tankWEPSprites[CurrentLevelTank];
            tankBODYImage.sprite = tankBODYSprites[CurrentLevelTank];
        }
        if (warriorBGImage != null && warriorBGSprites.Length > CurrentLevelWar)
        {
            warriorBGImage.sprite = warriorBGSprites[CurrentLevelWar];
            warriorWEPImage.sprite = warriorWEPSprites[CurrentLevelWar];
            warriorBODYImage.sprite = warriorBODYSprites[CurrentLevelWar];
        }
        if (rangerBGImage != null && rangerBGSprites.Length > CurrentLevelRng)
        {
            rangerBGImage.sprite = rangerBGSprites[CurrentLevelRng];
            rangerWEPImage.sprite = rangerWEPSprites[CurrentLevelRng];
            rangerBODYImage.sprite = rangerBODYSprites[CurrentLevelRng];
        }
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
