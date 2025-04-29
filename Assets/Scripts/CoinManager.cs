using UnityEngine;
using TMPro;
using UnityEditor;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance;

    [SerializeField] private TextMeshProUGUI coinsText;
    private float currentCoins;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        PlayerPrefs.DeleteKey("PlayerCoins");
        PlayerPrefs.Save();
    }

    private void Start()
    {
        LoadCoins();
        UpdateCoinDisplay();
    }

    public void AddCoins(float amount)
    {
        currentCoins += amount;
        SaveCoins();
        UpdateCoinDisplay();
    }

    public void SubtractCoins(float amount)
    {
        currentCoins -= amount;
        SaveCoins();
        UpdateCoinDisplay();
    }

    public float GetCurrentCoins()
    {
        return currentCoins;
    }

    private void UpdateCoinDisplay()
    {
        if (coinsText != null)
        {
            coinsText.text = currentCoins >= 1000
                ? (currentCoins / 1000f).ToString("0.#") + "k"
                : currentCoins.ToString("0");
        }
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetFloat("PlayerCoins", currentCoins);
        PlayerPrefs.Save();
    }

    private void LoadCoins()
    {
        currentCoins = PlayerPrefs.GetFloat("PlayerCoins", 750f);
    }
}