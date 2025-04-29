using TMPro;
using UnityEngine;

public class DiamondsManager : MonoBehaviour
{
    public static DiamondsManager Instance;

    [SerializeField] private TextMeshProUGUI diamondsText;
    private float currentDiamonds;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        PlayerPrefs.DeleteKey("PlayerDiamonds");
        PlayerPrefs.Save();
    }

    private void Start()
    {
        LoadDiamonds();
        UpdateDiamondsDisplay();
    }

    public void AddDiamonds(float amount)
    {
        currentDiamonds += amount;
        SaveDiamonds();
        UpdateDiamondsDisplay();
    }

    public void SubtractDiamonds(float amount)
    {
        currentDiamonds -= amount;
        SaveDiamonds();
        UpdateDiamondsDisplay();
    }

    public float GetCurrentDiamonds()
    {
        return currentDiamonds;
    }

    private void UpdateDiamondsDisplay()
    {
        if (diamondsText != null)
        {
            diamondsText.text = currentDiamonds >= 1000
                ? (currentDiamonds / 1000f).ToString("0.#") + "k"
                : currentDiamonds.ToString("0");
        }
    }

    private void SaveDiamonds()
    {
        PlayerPrefs.SetFloat("PlayerDiamonds", currentDiamonds);
        PlayerPrefs.Save();
    }

    private void LoadDiamonds()
    {
        currentDiamonds = PlayerPrefs.GetFloat("PlayerDiamonds", 1f);
    }
}