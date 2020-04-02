using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Libreria para controlar la interfaz y sus componentes
using UnityEngine.SceneManagement; // Modulo para manejar escenas
public enum GameState { Idle, Playing, Ended };

public class GameController : MonoBehaviour
{
    public float parallaxSpeed = 0.02f; // iniciamos con la velocidad indicada
    public RawImage background; // importamos el background
    public GameObject uiIdle;
    public GameObject uiScore;
    public GameObject panel;

    public Text pointsText;
    public Text recordText;


    public GameState gameState = GameState.Idle;

    public GameObject player;
    public GameObject enemyGenerator;

    public float scaleTime = 6f;
    public float scaleInc = .25f;

    private AudioSource musicPlayer;

    private int points = 0;

    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        recordText.text = "RECORD: " + GetMaxScore().ToString();
    }

    // Update is called once per frame
    void Update()
    {
        bool userAction = Input.GetKeyDown("up") || Input.GetMouseButtonDown(0);

        //Empieza el Juego 
        if (gameState == GameState.Idle && userAction)
        {
            gameState = GameState.Playing;
            uiIdle.SetActive(false);
            uiScore.SetActive(true);
            panel.SetActive(false);
            player.SendMessage("UpdateState", "PlayerRun");
            enemyGenerator.SendMessage("StartGenerator");
            musicPlayer.Play();
            InvokeRepeating("GameTimeScale", scaleTime, scaleTime);
        }
        //Comenzo el juego 
        else if (gameState == GameState.Playing)
        {
            Parallax();
        }
        else if (gameState == GameState.Ended)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
        void Parallax()
        {
            float finalSpeed = parallaxSpeed * Time.deltaTime;
            background.uvRect = new Rect(background.uvRect.x + finalSpeed * 3, 0f, 1f, 1f);
        }
        
       void GameTimeScale()
    {
        Time.timeScale += scaleInc;
        Debug.Log("Ritomo Incrementado: " + Time.timeScale.ToString());
    }

    public void ResetTimeScale()
    {
        CancelInvoke("GameTimeScale");
        Time.timeScale = 1f;
        Debug.Log("Ritmo restart" + Time.timeScale.ToString());
    }

    public void IncreasePoints()
    {
        points++;
        pointsText.text = points.ToString();
        if(points >= GetMaxScore())
        {
            recordText.text = "RECORD: " + points.ToString();
            SaveScore(points);

        }
        if(points == 30)
        {
            SceneManager.LoadScene("GameWin");
        }
    }

    public int GetMaxScore()
    {
        return PlayerPrefs.GetInt("Max Points", 0);
    }

    public void SaveScore(int currentPoints)
    {
        PlayerPrefs.SetInt("Max Points", currentPoints);
    }
}
