using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    public static CurrencyController instance;

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

    private void Start()
    {
        UIController.instance.UpdateMoneyText(currentMoney);
    }

    public float currentMoney;

    public void SpendMoney(float amountToSpend)
    {
        currentMoney -= amountToSpend;
        UIController.instance.UpdateMoneyText(currentMoney);
    }

    public void AddMoney(float amountToAdd)
    {
        currentMoney += amountToAdd;
        UIController.instance.UpdateMoneyText(currentMoney);
    }

    public bool checkMoney(float amount)
    {
        if(currentMoney >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
