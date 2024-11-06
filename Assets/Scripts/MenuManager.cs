using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.IO;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField playerNameInput;
    [SerializeField] private TextMeshProUGUI bestScoreText;
    private void Start()
    {
        LoadBestScore();
    }
    public void StartGame()
    {
        // Save the player's name to PlayerPrefs for use in the game scene
        if (playerNameInput.text != "")
        {
            Debug.Log(playerNameInput.text);
            PlayerPrefs.SetString("CurrentPlayerName", playerNameInput.text);
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Please enter a name.");
        }
        
    }
    public void LoadBestScore()
    {
        // Display the highest score and corresponding player name from MainGameManager
        bestScoreText.text = "Best Score: " + GameManager.Instance.bestScore + " Name: " + GameManager.Instance.playerName;
    }
    public void DeleteSavedData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path)) // Check if the file exists
        {
            File.Delete(path); // Delete the file
            Debug.Log("Saved data deleted."); // Log message to console
            GameManager.Instance.SetDefaultValues();
            bestScoreText.text = "Best Score: " + GameManager.Instance.bestScore + " Name: " + GameManager.Instance.playerName;
        }
        else
        {
            Debug.Log("No saved data to delete."); // Log message if no file exists
        }
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
