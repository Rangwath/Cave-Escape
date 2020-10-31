using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX = null;
    [SerializeField] private int coinScore = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
        
        GameSession gameSession = FindObjectOfType<GameSession>();
        if (gameSession == null)
        {
            Debug.LogError("GameSession is null, add GameSession to the scene.");
            return;
        }
        gameSession.AddToScore(coinScore);

        Destroy(gameObject);
        //gameObject.SetActive(false);
    }
}
