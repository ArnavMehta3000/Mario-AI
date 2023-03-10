using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            DestroyImmediate(gameObject);
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == null)
            Instance = null;
    }

    private void Start()
    {
        NewGame();
    }

    private void NewGame()
    {
        // Set lives here?
    }

    private void Resetlevel()
    {
        // Reset Ai here
    }

    private void GameOver()
    {

    }
}
