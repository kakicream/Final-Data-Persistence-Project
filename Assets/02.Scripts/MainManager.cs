using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    //public GameObject GameOverText;
    public GameObject GameOverTextHighScore;
    private TextMeshProUGUI GameOverTextHigh;

    private bool m_Started = false;
    private int m_Points;

    private bool m_GameOver = false;

    public Text BestScoreText;
    private int bestScoreSoFar;
    private string bestScoreHolder;




    // Start is called before the first frame update
    void Start()
    {
        bestScoreHolder = MenuUIController.bestScoreHolder;
        
        bestScoreSoFar = PlayerInfoController.Instance.highScore;
        BestScoreText.text = $"Best Score : {bestScoreHolder} : {bestScoreSoFar}";
        


        GameOverTextHigh = GameOverTextHighScore.GetComponent<TextMeshProUGUI>();

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);

        int[] pointCountArray = new[] { 1, 1, 2, 2, 5, 5 };
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
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
                       
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // If game over, go back to menu scene, set the high score informations
                PlayerInfoController.Instance.SaveHighScore();
                SceneManager.LoadScene(0);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points} Player : {PlayerInfoController.Instance.playerName}";
    }

    public void GameOver()
    {
        m_GameOver = true;
        if (m_Points > bestScoreSoFar)
        {
            
            PlayerInfoController.Instance.highScore = m_Points;
            GameOverTextHigh.SetText($"New Record!!!\nScore : {m_Points}\nPlayer : {PlayerInfoController.Instance.playerName}\nPress SPACEBAR to Restart");
        }
        else // if no New Record
        {
            GameOverTextHigh.SetText($"Game Over\nScore : {m_Points}\nPlayer : {PlayerInfoController.Instance.playerName}\nPress SPACEBAR to Restart");
            PlayerInfoController.Instance.highScore = bestScoreSoFar;
            PlayerInfoController.Instance.playerName = bestScoreHolder;
        }

        GameOverTextHighScore.SetActive(true);
    }
}
