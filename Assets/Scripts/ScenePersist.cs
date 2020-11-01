using UnityEngine;

public class ScenePersist : MonoBehaviour
{
    private void Awake()
    {
        // Singleton pattern
        int numberScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if (numberScenePersist > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
