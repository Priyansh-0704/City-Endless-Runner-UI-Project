using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToPlayGame : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
