using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX = null;
    [SerializeField] private int coinScore = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int playerLayer = LayerMask.NameToLayer("Player");

        if (collision.gameObject.layer == playerLayer)
        {
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);

            GameSession gameSession = FindObjectOfType<GameSession>();
            if (gameSession == null)
            {
                Debug.LogError("GameSession is null, add GameSession to the scene.");
                return;
            }
            gameSession.AddToScore(coinScore);

            gameObject.SetActive(false);
        }
    }
}
