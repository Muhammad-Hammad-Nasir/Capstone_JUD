using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    // Screen UI GameObjects
    public GameObject background;
    public GameObject homeScreen;
    public GameObject pauseScreen;
    public GameObject optionScreen;
    public GameObject gameScreen;
    public GameObject exitScreen;
    public GameObject soundOnButton;
    public GameObject soundOffButton;
    public GameObject round1EndScreen;
    public GameObject round2EndScreen;
    public GameObject round3EndScreen;
    public GameObject round4EndScreen;
    public GameObject gameoverScreen;

    // GameObjects
    public GameObject myCamera;
    public GameObject cameraPos;
    public GameObject player;
    public GameObject round1Transform, round1CameraPos, round1Objects;
    public GameObject round2Transform, round2CameraPos, round2Objects;
    public GameObject round3Transform, round3CameraPos, round3Objects;
    public GameObject round4Transform, round4CameraPos, round4Objects;

    // UI Texts
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI totalTime1;
    public TextMeshProUGUI totalTime2;

    // Public Variables
    public float score;
    public int health;
    public bool isGameover;
    public bool round1Complete;
    public bool round2Complete;
    public bool round3Complete;
    public bool round4Complete;
    public bool isPaused;
    public bool isMusic = true;
    public bool round1;
    public bool round2;
    public bool round3;
    public bool round4;

    private Rigidbody playerRb;
    private AudioSource music;
    private int minutes;
    private int seconds;
    private float timeCount;

    void Start()
    {
        Time.timeScale = 0;
        music = myCamera.GetComponent<AudioSource>();
        playerRb = player.GetComponent<Rigidbody>();
        health = 3;
        homeScreen.SetActive(true);
        background.SetActive(true);
    }

    void Update()
    {
        PauseGame();
        RoundComplete();
        Timer();
        CheckGameover();
        DisplayUI();
    }

    void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.P) && !isGameover && !isPaused && (round1 == true || round2 == true || round3 == true || round4 == true) )
        {
            Time.timeScale = 0;
            background.SetActive(true);
            pauseScreen.SetActive(true);
            music.Stop();
            isPaused = true;
        }
        else if (Input.GetKeyDown(KeyCode.P) && !isGameover && isPaused)
        {
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
            background.SetActive(false);
            if(isMusic)
            {
                music.Play();
            }
            isPaused = false;
        }
    }

    void RoundComplete()
    {
        if(round1Complete == true)
        {
            music.Stop();
            background.SetActive(true);

            player.transform.position = round2Transform.transform.position;
            player.transform.rotation = round2Transform.transform.rotation;

            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            round1EndScreen.SetActive(true);
            round1Complete = false;
            Time.timeScale = 0;
        }
        else if (round2Complete == true)
        {
            music.Stop();
            background.SetActive(true);

            player.transform.position = round3Transform.transform.position;
            player.transform.rotation = round3Transform.transform.rotation;

            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            round2EndScreen.SetActive(true);
            round2Complete = false;
            Time.timeScale = 0;
        }
        else if (round3Complete == true)
        {
            music.Stop();
            background.SetActive(true);

            player.transform.position = round4Transform.transform.position;
            player.transform.rotation = round4Transform.transform.rotation;

            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            round3EndScreen.SetActive(true);
            round3Complete = false;
            Time.timeScale = 0;
        }
        else if (round4Complete == true)
        {
            music.Stop();
            background.SetActive(true);

            player.transform.position = round1Transform.transform.position;
            player.transform.rotation = round1Transform.transform.rotation;

            playerRb.velocity = Vector3.zero;
            playerRb.angularVelocity = Vector3.zero;

            round4EndScreen.SetActive(true);
            round4Complete = false;
            Time.timeScale = 0;
        }
    }

    void Timer()
    {
        timeCount += Time.deltaTime;
        minutes = Mathf.FloorToInt(timeCount / 60);
        seconds = Mathf.FloorToInt(timeCount % 60);
    }

    void CheckGameover()
    {
        if (isGameover)
        {
            music.Stop();
            Time.timeScale = 0;
            background.SetActive(true);
            gameoverScreen.SetActive(true);
        }
    }

    void DisplayUI()
    {
        scoreText.text = "Score: " + score;
        lifeText.text = "Life: " + health;
        timeText.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        totalTime1.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
        totalTime2.text = "Time: " + string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void UpdateScore(int addScore)
    {
        score += addScore;
    }

    public void Round1()
    {
        Time.timeScale = 1;
        if (isMusic)
        {
            music.Play();
        }

        round1 = true;
        round2 = false;
        round3 = false;
        round4 = false;

        homeScreen.SetActive(false);
        background.SetActive(false);

        gameScreen.SetActive(true);

        round1Objects.SetActive(true);
        round2Objects.SetActive(false);
        round3Objects.SetActive(false);
        round4Objects.SetActive(false);

        cameraPos.transform.position = round1CameraPos.transform.position;
        cameraPos.transform.rotation = round1CameraPos.transform.rotation;
        player.transform.position = round1Transform.transform.position;
        player.transform.rotation = round1Transform.transform.rotation;
    }

    public void Round2()
    {
        Time.timeScale = 1;
        if (isMusic)
        {
            music.Play();
        }
        round1 = false;
        round2 = true;
        round3 = false;
        round4 = false;

        background.SetActive(false);
        round1EndScreen.SetActive(false);

        gameScreen.SetActive(true);

        round1Objects.SetActive(false);
        round2Objects.SetActive(true);
        round3Objects.SetActive(false);
        round4Objects.SetActive(false);

        cameraPos.transform.position = round2CameraPos.transform.position;
        cameraPos.transform.rotation = round2CameraPos.transform.rotation;
        player.transform.position = round2Transform.transform.position;
        player.transform.rotation = round2Transform.transform.rotation;
    }

    public void Round3()
    {
        Time.timeScale = 1;
        if (isMusic)
        {
            music.Play();
        }
        round1 = false;
        round2 = false;
        round3 = true;
        round4 = false;

        background.SetActive(false);
        round2EndScreen.SetActive(false);

        gameScreen.SetActive(true);

        round1Objects.SetActive(false);
        round2Objects.SetActive(false);
        round3Objects.SetActive(true);
        round4Objects.SetActive(false);

        cameraPos.transform.position = round3CameraPos.transform.position;
        cameraPos.transform.rotation = round3CameraPos.transform.rotation;
        player.transform.position = round3Transform.transform.position;
        player.transform.rotation = round3Transform.transform.rotation;
    }

    public void Round4()
    {
        Time.timeScale = 1;
        if (isMusic)
        {
            music.Play();
        }
        round1 = false;
        round2 = false;
        round3 = false;
        round4 = true;

        background.SetActive(false);
        round3EndScreen.SetActive(false);

        gameScreen.SetActive(true);

        round1Objects.SetActive(false);
        round2Objects.SetActive(false);
        round3Objects.SetActive(false);
        round4Objects.SetActive(true);

        cameraPos.transform.position = round4CameraPos.transform.position;
        cameraPos.transform.rotation = round4CameraPos.transform.rotation;
        player.transform.position = round4Transform.transform.position;
        player.transform.rotation = round4Transform.transform.rotation;
    }

    public void GameOptions()
    {
        homeScreen.SetActive(false);
        optionScreen.SetActive(true);
    }

    public void SoundOn()
    {
        isMusic = true;
        soundOffButton.SetActive(false);
        soundOnButton.SetActive(true);
    }

    public void SoundOff()
    {
        isMusic = false;
        soundOnButton.SetActive(false);
        soundOffButton.SetActive(true);
    }

    public void BackToHome()
    {
        music.Stop();
        gameoverScreen.SetActive(false);
        optionScreen.SetActive(false);
        round1EndScreen.SetActive(false);
        round2EndScreen.SetActive(false);
        round3EndScreen.SetActive(false);
        round4EndScreen.SetActive(false);
        homeScreen.SetActive(true);
    }

    public void Resume()
    {
        Time.timeScale = 1;
        pauseScreen.SetActive(false);
        background.SetActive(false);
        if (isMusic)
        {
            music.Play();
        }
        isPaused = false;
    }

    public void ExitGame()
    {
        background.SetActive(false);
        homeScreen.SetActive(false);
        exitScreen.SetActive(true);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
}
