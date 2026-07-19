using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static bool IsGameOver = false;

    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TMP_Text pointsText;

    private int currentPoints;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        currentPoints = 0;

        gameOverPanel.SetActive(false);

        pointsText.text = "Puntos: " + currentPoints;
    }

    public void GameOver()
    {
        IsGameOver = true;

        gameOverPanel.SetActive(true);

        Time.timeScale = 0f;

        Debug.Log("GAME OVER");
    }

    public void RetryGame()
    {
        IsGameOver = false;

        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void AddPoints(int points)
    {
        currentPoints += points;

        pointsText.text = "Puntos: " + currentPoints;

        Debug.Log("Puntos actuales: " + currentPoints);
    }
}