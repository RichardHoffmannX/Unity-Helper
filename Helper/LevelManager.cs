using Sirenix.OdinInspector; // Options for Button Test
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Scenes must be added to Build Settings")]
    [SerializeField] private string[] scenes;

    private int currentSceneIndex = -1;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        CacheCurrentSceneIndex();
    }

    private void CacheCurrentSceneIndex()
    {
        string activeSceneName = SceneManager.GetActiveScene().name;

        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i] == activeSceneName)
            {
                currentSceneIndex = i;
                return;
            }
        }

        currentSceneIndex = -1;
    }

    [Button]
    public void LoadSceneByIndex(int index)
    {
        if (!IsValidIndex(index))
        {
            Debug.LogError($"LevelLoader: Invalid scene index {index}");
            return;
        }

        currentSceneIndex = index;
        SceneManager.LoadScene(scenes[index]);
    }

    [Button]
    public void LoadSceneByName(string sceneName)
    {
        for (int i = 0; i < scenes.Length; i++)
        {
            if (scenes[i] == sceneName)
            {
                currentSceneIndex = i;
                SceneManager.LoadScene(sceneName);
                return;
            }
        }

        Debug.LogError($"LevelLoader: Scene '{sceneName}' not found in array");
    }

    public void LoadNextScene()
    {
        if (scenes.Length == 0)
            return;

        int nextIndex = (currentSceneIndex + 1) % scenes.Length;
        LoadSceneByIndex(nextIndex);
    }

    public void LoadPreviousScene()
    {
        if (scenes.Length == 0)
            return;

        int prevIndex = currentSceneIndex - 1;
        if (prevIndex < 0)
            prevIndex = scenes.Length - 1;

        LoadSceneByIndex(prevIndex);
    }

    public void ReloadCurrentScene()
    {
        if (currentSceneIndex < 0)
        {
            Debug.LogWarning("LevelLoader: Current scene not found in array, reloading active scene.");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            return;
        }

        LoadSceneByIndex(currentSceneIndex);
    }

    public void LoadRandomScene()
    {
        if (scenes.Length == 0)
            return;

        int randomIndex = Random.Range(0, scenes.Length);
        LoadSceneByIndex(randomIndex);
    }

    private bool IsValidIndex(int index)
    {
        return index >= 0 && index < scenes.Length;
    }
}
