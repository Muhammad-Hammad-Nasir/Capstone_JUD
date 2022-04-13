using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
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

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI lifeText;

    public GameObject myCamera;
    public GameObject player;
    public GameObject round1Transform, round1Objects;
    public GameObject round2Transform, round2Objects;
    public GameObject round3Transform, round3Objects;
    public GameObject round4Transform, round4Objects;
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

    private AudioSource music;

    void Start()
    {
        health = 3;
        Time.timeScale = 0;
        homeScreen.SetActive(true);
        background.SetActive(true);
        music = myCamera.GetComponent<AudioSource>();
    }

    void Update()
    {
        PauseGame();
        RoundComplete();
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
            background.SetActive(true);

            player.transform.position = round2Transform.transform.position;
            player.transform.rotation = round2Transform.transform.rotation;

            round1EndScreen.SetActive(true);
            round1Complete = false;
        }
        else if (round2Complete == true)
        {
            background.SetActive(true);

            player.transform.position = round3Transform.transform.position;
            player.transform.rotation = round3Transform.transform.rotation;

            round2EndScreen.SetActive(true);
            round2Complete = false;
        }
        else if (round3Complete == true)
        {
            background.SetActive(true);

            player.transform.position = round4Transform.transform.position;
            player.transform.rotation = round4Transform.transform.rotation;

            round3EndScreen.SetActive(true);
            round3Complete = false;
        }
        else if (round4Complete == true)
        {
            background.SetActive(true);

            player.transform.position = round1Transform.transform.position;
            player.transform.rotation = round1Transform.transform.rotation;

            round4EndScreen.SetActive(true);
            round4Complete = false;
        }
    }

    void CheckGameover()
    {
        if (isGameover)
        {

        }
    }

    void DisplayUI()
    {
        scoreText.text = "Score: " + score;
        lifeText.text = "Life: " + health;
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

        player.transform.position = round1Transform.transform.position;
        player.transform.rotation = round1Transform.transform.rotation;
    }

    public void Round2()
    {
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

        player.transform.position = round2Transform.transform.position;
        player.transform.rotation = round2Transform.transform.rotation;
    }

    public void Round3()
    {
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

        player.transform.position = round3Transform.transform.position;
        player.transform.rotation = round3Transform.transform.rotation;
    }

    public void Round4()
    {
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

        player.transform.position = round4Transform.transform.position;
        player.transform.rotation = round4Transform.transform.rotation;
    }

    public void StartGame()
    {
        Round1();
    }

    public void SecondRound()
    {
        Round2();
    }

    public void ThirdRound()
    {
        Round3();
    }

    public void FourthRound()
    {
        Round4();
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
}
