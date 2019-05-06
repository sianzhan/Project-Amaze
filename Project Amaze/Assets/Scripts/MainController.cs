using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainController : MonoBehaviour {

    [SerializeField] private GameObject panelPause;
    [SerializeField] private GameObject panelGameover;
    [SerializeField] private GameObject panelEndgame;

    public AudioClip musicGameEnd;
    AudioSource[] srcAudio;

    static MainController gameCurrent;

    public enum GameState
    {
        PLAYING,
        PAUSE,
        GAMEOVER,
        ENDGAME
    }

    public static GameState stateGame;

    public static GameState CurrentGameState()
    {
        return stateGame;
    }

    // Use this for initialization
    void Start () {
        srcAudio = GetComponents<AudioSource>();
        gameCurrent = this;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        stateGame = GameState.PLAYING;
        panelPause.SetActive(false);
        panelGameover.SetActive(false);
        panelEndgame.SetActive(false);

    }


    static int movHeightEnding = 0;
    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.R))
        {
            stateGame = GameState.PLAYING;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else if(Input.GetKeyDown(KeyCode.P))
        {
            ToggleGamePause();
        }
        else if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }else if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            CurrentGame().EndGame();
        }

        if (stateGame == GameState.ENDGAME)
        {
            Image img = panelEndgame.GetComponent<Image>();


            //Background fade
            Color col = img.color;
            if (col.a < 0.5)
            {
                col.a = col.a + Time.deltaTime / 10;
                img.color = col;
            }


            if(!srcAudio[1].isPlaying)
            {
                srcAudio[0].Stop();
                srcAudio[1].Play();
            }

        }
    }

    public static MainController CurrentGame()
    {
        return gameCurrent;
    }

    public void ToggleGamePause()
    {
        if (stateGame == GameState.PLAYING)
        {
            stateGame = GameState.PAUSE;
            panelPause.SetActive(true);

        }
        else
        {
            stateGame = GameState.PLAYING;
            panelPause.SetActive(false);
        }
    }

    public void GameOver()
    {
        stateGame = GameState.GAMEOVER;
        panelGameover.SetActive(true);
    }

    public void EndGame()
    {
        stateGame = GameState.ENDGAME;
        panelEndgame.SetActive(true);

        GameObject credit = panelEndgame.transform.GetChild(0).gameObject;
        credit.GetComponent<Animator>().Play("credit");
        }

}
