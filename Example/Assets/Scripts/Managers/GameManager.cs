using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Linq;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject gameplayPanel;
    [SerializeField] GameObject finishPanel;
    [SerializeField] TextMeshProUGUI clearLevelText;
    [SerializeField] TextMeshProUGUI incomeLevelText;
    [SerializeField] TextMeshProUGUI damageLevelText;
    [SerializeField] TextMeshProUGUI firerateLevelText;
    [SerializeField] TextMeshProUGUI clearPriceText;
    [SerializeField] TextMeshProUGUI incomePriceText;
    [SerializeField] TextMeshProUGUI damagePriceText;
    [SerializeField] TextMeshProUGUI fireratePriceText;
    [SerializeField] TextMeshProUGUI moneyText;
    [SerializeField] TextMeshProUGUI stageGameplayText;
    [SerializeField] TextMeshProUGUI stageFinishText;
    [SerializeField] TMP_Text clearLevelGameplayText;
    [SerializeField] GameObject clearLevelUpgradeButton, incomeUpgradeButton,damageUpgradeButton,firerateUpgradeButton,nextLevelButton;

    [Header("Gameplay")]
    [SerializeField] GameData gameData;
    [SerializeField] ParticleSystem waterParticle;
    GameObject cam;
    [HideInInspector] public bool isGameStart = false;
    [HideInInspector] public bool isGameOver = false;
    [SerializeField] Animator handAnim;
    public Transform targetPosition;
    public GameObject tool1, tool2,tool3;
    public ParticleSystem particles;
    [HideInInspector] public int currentWeaponIndex;
    [HideInInspector] public float currentDamage;
    [HideInInspector] public float currentFireRate;
    [SerializeField] GameObject plateController;
    public Transform[] weaponPositions;
    
    List<WeaponPlate> weaponPlates;

    int clearLevelPrice;
    int incomePrice;
    int damagePrice;
    int fireRatePrice;
    private void Start()
    {
        stageGameplayText.SetText("Stage " + SaveManager.Instance.Level);
        stageFinishText.SetText("STAGE " + SaveManager.Instance.Level);
        ToolCheck();
        clearLevelPrice = 75 * SaveManager.Instance.ClearLevel;
        incomePrice = 250 + (50 * SaveManager.Instance.Income);
        damagePrice = 250 + (50 * SaveManager.Instance.Damage);
        fireRatePrice = 250 + (50 * SaveManager.Instance.FireRate);
        cam = Camera.main.gameObject;
        weaponPlates = FindObjectsOfType<WeaponPlate>().ToList();
        UpdateTexts();
        currentDamage = SaveManager.Instance.Damage;
        ControlButtons();
    }

    public void Play()
    {
        isGameStart = true;
        handAnim.enabled = false;
        waterParticle.transform.GetChild(0).gameObject.SetActive(true);
        cam.GetComponent<CameraFollower>().enabled = true;
        startPanel.SetActive(false);
    }

    public void UpgradeClearLevel()
    {
        if (SaveManager.Instance.Money >= clearLevelPrice)
        {
            handAnim.SetTrigger("levelup");
            SaveManager.Instance.Money -= clearLevelPrice;
            SaveManager.Instance.ClearLevel += 1;
            SaveManager.Instance.SaveToJson();
            clearLevelPrice = 75 * SaveManager.Instance.ClearLevel;
            ToolCheck();
            CheckPlateMaterials();
            UpdateTexts();
            ControlButtons();
        }
    }
    public void UpgradeIncome()
    {
        if (SaveManager.Instance.Money >= incomePrice)
        {
            SaveManager.Instance.Money -= incomePrice;
            SaveManager.Instance.Income += 1;
            SaveManager.Instance.SaveToJson();
            incomePrice = 200 + (50 * SaveManager.Instance.Income);

            UpdateTexts();
            ControlButtons();
        }
    }
    public void UpgradeDamage()
    {
        if (SaveManager.Instance.Money >= damagePrice)
        {
            SaveManager.Instance.Money -= damagePrice;
            SaveManager.Instance.Damage += 1;
            SaveManager.Instance.SaveToJson();
            damagePrice = 200 + (50 * SaveManager.Instance.Damage);

            UpdateTexts();
            ControlButtons();
        }
    }
    public void UpgradeFirerate()
    {
        if (SaveManager.Instance.Money >= fireRatePrice)
        {
            SaveManager.Instance.Money -= fireRatePrice;
            SaveManager.Instance.FireRate += 1;
            SaveManager.Instance.SaveToJson();
            fireRatePrice = 200 + (50 * SaveManager.Instance.FireRate);

            UpdateTexts();
            ControlButtons();
        }
    }
    public void NextLevel()
    {
        if (SaveManager.Instance.Level == 3)
        {
            SaveManager.Instance.Level = 0;
        }
        nextLevelButton.SetActive(false);
        SceneManager.LoadScene(SaveManager.Instance.Level);
        SaveManager.Instance.Level++;
        SaveManager.Instance.SaveToJson();
    }

    void PlaceTool2()
    {
        var sh = particles.shape;
        var main = particles.main;
        var emission = particles.emission;
        main.maxParticles = 300;
        emission.rateOverTime = 180;
        sh.scale = new Vector3(1.40f, 0, 0.35f);
        tool1.SetActive(false);
        tool2.SetActive(true);
    }
    void PlaceTool3()
    {
        var sh = particles.shape;
        var main = particles.main;
        var emission = particles.emission;
        sh.shapeType = ParticleSystemShapeType.Box;
        sh.scale = new Vector3(1.66f, 0, 0.35f);
        sh.position = new Vector3(0.06f, 0, 0);
        main.maxParticles = 450;
        emission.rateOverTime = 200;
        plateController.transform.localScale = new Vector3(2.2f, 0.92944f, 1.196528f);
        tool1.SetActive(false);
        tool2.SetActive(false);
        tool3.SetActive(true);
    }
    void ToolCheck()
    {
        if (SaveManager.Instance.ClearLevel >= 5 && SaveManager.Instance.ClearLevel < 10)
        {
            PlaceTool2();
        }
        if (SaveManager.Instance.ClearLevel >= 10)
        {
            PlaceTool3();
        }
    }
    void UpdateTexts()
    {
        if (SaveManager.Instance.ClearLevel < gameData.MaxClearLevel)
        {
            clearLevelText.SetText("Level " + SaveManager.Instance.ClearLevel);
            clearPriceText.SetText(clearLevelPrice.ToString());
        }
        else
        {
            clearLevelText.SetText("MAX");
            clearPriceText.SetText("MAX");
        }
        if (SaveManager.Instance.Income < gameData.MaxIncomeLevel)
        {
            incomeLevelText.SetText("Level " + SaveManager.Instance.Income);
            incomePriceText.SetText(incomePrice.ToString());
        }
        else
        {
            incomeLevelText.SetText("MAX");
            incomePriceText.SetText("MAX");
        }
        if (SaveManager.Instance.Damage < gameData.MaxDamageLevel)
        {
            damageLevelText.SetText("Level " + SaveManager.Instance.Damage);
            damagePriceText.SetText(damagePrice.ToString());
        }
        else
        {
            damageLevelText.SetText("MAX");
            damagePriceText.SetText("MAX");
        }
        if (SaveManager.Instance.FireRate < gameData.MaxFireRateLevel)
        {
            firerateLevelText.SetText("Level " + SaveManager.Instance.FireRate);
            fireratePriceText.SetText(fireRatePrice.ToString());
        }
        else
        {
            firerateLevelText.SetText("MAX");
            fireratePriceText.SetText("MAX");
        }
        clearLevelGameplayText.SetText("Lv. " + SaveManager.Instance.ClearLevel);
    }
    void CheckPlateMaterials()
    {
        foreach (WeaponPlate weaponPlate in weaponPlates)
        {
            weaponPlate.PlateMaterialCheck();
        }
    }
    private void Update()
    {
        moneyText.SetText(SaveManager.Instance.Money.ToString());

        if (PlateLevelController.isRunnerStarted == true)
        {
            if (currentWeaponIndex == 0 && isGameOver == false)
            {
                ControlButtons();
                gameplayPanel.SetActive(false);
                finishPanel.SetActive(true);
                isGameOver = true;
            }
        }
    }
    void ControlButtons()
    {
        if (SaveManager.Instance.ClearLevel < gameData.MaxClearLevel)
        {
            if (SaveManager.Instance.Money >= clearLevelPrice)
            {
                clearLevelUpgradeButton.GetComponent<Button>().interactable = true;
                clearLevelUpgradeButton.GetComponent<Image>().color = new Color(46 / 255f, 127 / 255f, 1, 1);
                clearLevelUpgradeButton.GetComponent<Shadow>().effectColor = new Color(34 / 255f, 79 / 255f, 156 / 255f, 1);
            }
            else
            {
                clearLevelUpgradeButton.GetComponent<Button>().interactable = false;
                clearLevelUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
                clearLevelUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
            }
        }
        else
        {
            clearLevelUpgradeButton.GetComponent<Button>().interactable = false;
            clearLevelUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
            clearLevelUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
        }

        if (SaveManager.Instance.Income < gameData.MaxIncomeLevel)
        {
            if (SaveManager.Instance.Money >= incomePrice)
            {
                incomeUpgradeButton.GetComponent<Button>().interactable = true;
                incomeUpgradeButton.GetComponent<Image>().color = new Color(46 / 255f, 127 / 255f, 1, 1);
                incomeUpgradeButton.GetComponent<Shadow>().effectColor = new Color(34 / 255f, 79 / 255f, 156 / 255f, 1);
            }
            else
            {
                incomeUpgradeButton.GetComponent<Button>().interactable = false;
                incomeUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
                incomeUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
            }
        }
        else
        {
            incomeUpgradeButton.GetComponent<Button>().interactable = false;
            incomeUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
            incomeUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
        }

        if (SaveManager.Instance.Damage < gameData.MaxDamageLevel)
        {
            if (SaveManager.Instance.Money >= damagePrice)
            {
                damageUpgradeButton.GetComponent<Button>().interactable = true;
                damageUpgradeButton.GetComponent<Image>().color = new Color(46 / 255f, 127 / 255f, 1, 1);
                damageUpgradeButton.GetComponent<Shadow>().effectColor = new Color(34 / 255f, 79 / 255f, 156 / 255f, 1);
            }
            else
            {
                damageUpgradeButton.GetComponent<Button>().interactable = false;
                damageUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
                damageUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
            }
        }
        else
        {
            damageUpgradeButton.GetComponent<Button>().interactable = false;
            damageUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
            damageUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
        }
        if (SaveManager.Instance.FireRate < gameData.MaxFireRateLevel)
        {
            if (SaveManager.Instance.Money >= fireRatePrice)
            {
                firerateUpgradeButton.GetComponent<Button>().interactable = true;
                firerateUpgradeButton.GetComponent<Image>().color = new Color(46 / 255f, 127 / 255f, 1, 1);
                firerateUpgradeButton.GetComponent<Shadow>().effectColor = new Color(34 / 255f, 79 / 255f, 156 / 255f, 1);
            }
            else
            {
                firerateUpgradeButton.GetComponent<Button>().interactable = false;
                firerateUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
                firerateUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
            }
        }
        else
        {
            firerateUpgradeButton.GetComponent<Button>().interactable = false;
            firerateUpgradeButton.GetComponent<Image>().color = new Color(166 / 255f, 166 / 255f, 166 / 255f, 1);
            firerateUpgradeButton.GetComponent<Shadow>().effectColor = new Color(114 / 255f, 114 / 255f, 114 / 255f, 1);
        }
    }

}

