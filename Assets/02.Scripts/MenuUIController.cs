using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuUIController : MonoBehaviour
{
    public TextMeshProUGUI highScore;
    public TMP_InputField playerNameInput;

    public static string bestScoreHolder;

    void Start()
    {
        bestScoreHolder = PlayerInfoController.Instance.playerName;

        playerNameInput.text = PlayerInfoController.Instance.playerName;

        highScore.text = $"High Score : {PlayerInfoController.Instance.highScore} by {bestScoreHolder}";
    }

    public void StartClicked()
    {
        // save the playername
        PlayerInfoController.Instance.playerName = playerNameInput.text;
        PlayerInfoController.Instance.SaveName();
        Debug.Log($"Saved Player's name! : {PlayerInfoController.Instance.playerName}");
        SceneManager.LoadScene(1);
    }

    public void ExitClicked()
    {
        // save the playername and high score
        PlayerInfoController.Instance.SaveName();
        PlayerInfoController.Instance.SaveHighScore();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
