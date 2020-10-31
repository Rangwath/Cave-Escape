using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject loseMenu = null;

    public void StartFirstLevel()
    {
        SceneManager.LoadScene("Level 1 Underground");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ReloadCurrentScene()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void HandleLoseCondition()
    {
        loseMenu.SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}