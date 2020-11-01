using UnityEngine;

public class WinHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession == null)
        {
            Debug.LogError("GameSession is null, add GameSession to the scene.");
            return;
        }

        gameSession.ResetGameSession();
    }
}