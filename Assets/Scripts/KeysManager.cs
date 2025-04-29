using TMPro;
using UnityEngine;

public class KeysManager : MonoBehaviour
{
    public static KeysManager Instance;

    [SerializeField] private TextMeshProUGUI keysText;
    private float currentKeys;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        PlayerPrefs.DeleteKey("PlayerKeys");
        PlayerPrefs.Save();
    }

    private void Start()
    {
        LoadKeys();
        UpdateKeysDisplay();
    }

    public void AddKeys(float amount)
    {
        currentKeys += amount;
        SaveKeys();
        UpdateKeysDisplay();
    }

    public void SubtractKeys(float amount)
    {
        currentKeys -= amount;
        SaveKeys();
        UpdateKeysDisplay();
    }

    public float GetCurrentKeys()
    {
        return currentKeys;
    }

    private void UpdateKeysDisplay()
    {
        if (keysText != null)
        {
            keysText.text = currentKeys >= 1000
                ? (currentKeys / 1000f).ToString("0.#") + "k"
                : currentKeys.ToString("0");
        }
    }

    private void SaveKeys()
    {
        PlayerPrefs.SetFloat("PlayerKeys", currentKeys);
        PlayerPrefs.Save();
    }

    private void LoadKeys()
    {
        currentKeys = PlayerPrefs.GetFloat("PlayerKeys", 2f);
    }
}