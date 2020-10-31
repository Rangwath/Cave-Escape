using UnityEngine;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] private int playerLives = 3;
    [SerializeField] private int score = 0;

    [SerializeField] private Text livesText = null;
    [SerializeField] private Text scoreText = null;

    private void Awake()
    {
        // Singleton pattern
        int numberGameSessions = FindObjectsOfType<GameSession>().Length;
        if (numberGameSessions > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();
    }

    public void ProcessPlayerDeath()
    {
        MenuManager menuManager = FindObjectOfType<MenuManager>();
        if (menuManager == null)
        {
            Debug.LogError("MenuManager is null, add MenuManager to the scene.");
            return;
        }

        if (playerLives > 1)
        {
            TakeLife(menuManager);
            
        }
        else
        {
            ResetGameSession(menuManager);
        }
    }

    public void AddToScore(int pointsToAdd)
    {
        score += pointsToAdd;
        scoreText.text = score.ToString();
    }

    private void TakeLife(MenuManager menuManager)
    {
        playerLives--;
        livesText.text = playerLives.ToString();
        menuManager.ReloadCurrentScene();
    }

    private void ResetGameSession(MenuManager menuManager)
    {
        menuManager.HandleLoseCondition();

        Destroy(gameObject);
    }
}
