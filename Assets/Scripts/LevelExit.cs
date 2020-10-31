using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        DestroyCurrentScenePersist();
        LoadNextLevel();
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    private void DestroyCurrentScenePersist()
    {
        ScenePersist scenePersist = FindObjectOfType<ScenePersist>();
        if (scenePersist == null)
        {
            Debug.LogWarning("ScenePersist cannot be found and cannot be destroyed.");
            return;
        }

        scenePersist.gameObject.SetActive(false);
        Destroy(scenePersist.gameObject);
    }
}