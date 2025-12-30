using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    public static UIController instance;

    public GameObject[] toolBarActivatorIcons;

    public TMP_Text timeText;

    public InventoryController theIC;

    public Image seedImage;

    public ShopController theShop;

    public TMP_Text moneyText;

    public GameObject pauseScreen;
    public string MainMenuScene;

    private void Awake()
    {
        if (instance == null) 
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current.iKey.wasPressedThisFrame)
        {
            theIC.OpenClose();
        }

#if UNITY_EDITOR

        if (Keyboard.current.bKey.wasPressedThisFrame)
        {
            theShop.OpenClose();
        }

#endif

        if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.pKey.wasPressedThisFrame)
        {
            PauseUnpuase();
        }
    }

    public void SwitchTool(int selected)
    {
        foreach (GameObject icon in toolBarActivatorIcons)
        {
            icon.SetActive(false);
        }

        toolBarActivatorIcons[selected].SetActive(true);

    }

    public void UpdateTimeText(float currentTime)
    {
        if (currentTime < 12)
        {
            timeText.text = Mathf.FloorToInt(currentTime) + "AM";
        }
        else if (currentTime < 13)
        {
            timeText.text = "12PM";
        }
        else if (currentTime < 24)
        {
            timeText.text = Mathf.FloorToInt(currentTime - 12) + "PM";
        }
        else if(currentTime < 25)
        {
            timeText.text = "12AM";
        }
        else
        {
            timeText.text = Mathf.FloorToInt(currentTime - 24) + "AM";
        }
    }

    public void SwitchSeed(CropController.CropType crop)
    {
        seedImage.sprite = CropController.instance.GetCropInfo(crop).seedType;

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void UpdateMoneyText(float currentMoney)
    {
        moneyText.text = "$" + currentMoney;
    }

    public void PauseUnpuase()
    {
        if (pauseScreen.activeSelf == false)
        {
            pauseScreen.SetActive(true);

            Time.timeScale = 0f;
        }
        else
        {
            pauseScreen.SetActive(false);

            Time.timeScale = 1f;
        }

        AudioManager.instance.PlaySFXPitchAdjusted(5);

    }

    public void MainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(MainMenuScene);

        Destroy(gameObject);
        Destroy(PlayerController.instance.gameObject);
        Destroy(GridInfo.instance.gameObject);
        Destroy(TimeController.instance.gameObject);
        Destroy(CropController.instance.gameObject);
        Destroy(CurrencyController.instance.gameObject);

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}
