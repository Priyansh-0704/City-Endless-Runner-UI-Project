using UnityEngine;
using TMPro;

public class ShopKeyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI buyAmountText;

    private int quantity = 1;
    private int keyCost = 100;

    private void Start()
    {
        UpdateUI();
    }

    public void Increase()
    {
        quantity++;
        UpdateUI();
    }

    public void Decrease()
    {
        if (quantity > 1)
        {
            quantity--;
            UpdateUI();
        }
    }

    public void Buy()
    {
        int totalCost = quantity * keyCost;

        if (CoinManager.Instance.GetCurrentCoins() >= totalCost)
        {
            CoinManager.Instance.SubtractCoins(totalCost);
            KeysManager.Instance.AddKeys(quantity);
        } else
        {
            Debug.Log("Not enough coins for keys!");
        }
    }

    private void UpdateUI()
    {
        int totalCost = quantity * keyCost;
        quantityText.text = quantity.ToString();

        if (buyAmountText != null)
        {
            buyAmountText.text = totalCost >= 1000
                ? (totalCost / 1000f).ToString("0.#") + "k"
                : totalCost.ToString("0");
        }
    }
}