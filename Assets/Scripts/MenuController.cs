using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public string levelToStart;

    public void Start()
    {
        AudioManager.instance.PlayTitle();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(levelToStart);

        AudioManager.instance.PlayNextBGM();

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quiting the game");

        AudioManager.instance.PlaySFXPitchAdjusted(5);
    }

}
