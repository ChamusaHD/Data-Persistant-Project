using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI currentScoreText;
    public GameObject GameOverText;

    private bool hasGameStarted = false;
    private bool isGameOver = false;

    public int m_Points;
    void Start()
    {
        LoadHighScore();
        bestScoreText.text = $"Best Score: {GameManager.Instance.bestScore} By: {GameManager.Instance.playerName}";

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!hasGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                hasGameStarted = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (isGameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }
    public void AddPoint(int point)
    {
        m_Points += point;
        currentScoreText.text = $"Score : {m_Points}";

        if (m_Points > GameManager.Instance.bestScore)
        {
            GameManager.Instance.bestScore = m_Points;
            GameManager.Instance.playerName = PlayerPrefs.GetString("CurrentPlayerName", "Player"); // Update high score holder's name
            GameManager.Instance.SaveNameAndScore();
            UpdateHighScoreDisplay();
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        GameOverText.SetActive(true);
        //UpdateHighScoreDisplay();
    }

    void LoadHighScore()
    {
        // Load high score from GameManager
        GameManager.Instance.LoadNameAndScore();
        UpdateHighScoreDisplay();
    }

    void UpdateHighScoreDisplay()
    {
        // Update UI for score and high score
        currentScoreText.text = $"Score : {m_Points}";
        bestScoreText.text = $"Best Score: {GameManager.Instance.bestScore} By: {GameManager.Instance.playerName}";
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
