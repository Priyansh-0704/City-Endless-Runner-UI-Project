using UnityEngine;
using TMPro;

public class ShopDiamondManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI quantityText;
    [SerializeField] private TextMeshProUGUI buyAmountText;

    private int quantity = 1;
    private int diamondCost = 500;

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
        int totalCost = quantity * diamondCost;

        if (CoinManager.Instance.GetCurrentCoins() >= totalCost)
        {
            CoinManager.Instance.SubtractCoins(totalCost);
            DiamondsManager.Instance.AddDiamonds(quantity);
        } else
        {
            Debug.Log("Not enough coins for diamonds!");
        }
    }

    private void UpdateUI()
    {
        int totalCost = quantity * diamondCost;
        quantityText.text = quantity.ToString();

        if (buyAmountText != null)
        {
            buyAmountText.text = totalCost >= 1000
                ? (totalCost / 1000f).ToString("0.#") + "k"
                : totalCost.ToString("0");
        }
    }
}