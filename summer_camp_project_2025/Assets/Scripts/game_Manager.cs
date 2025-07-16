using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using System.Threading;
using TMPro;

public class game_Manager : MonoBehaviour
{
    [Header("Game Variables")]
    public player_Controller player;
    public float time;
    public bool timeActive;

    [Header("Game UI")]
    public TMP_Text gameUI_score;
    public TMP_Text gameUI_health;
    public TMP_Text gameUI_time;

    [Header("Countdown UI")]
    public int countdown;
    public TMP_Text countdownText;

    [Header("EndScreenUI")]
    public TMP_Text endUI_score;
    public TMP_Text endUI_time;

    [Header("Screens")]
    public GameObject countdownUI;
    public GameObject gameUI;
    public GameObject endUI;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player_Capsule").GetComponent<player_Controller>();

        //make sure the timer is set to 0
        time = 0;

        //initially disables player movement
        player.enabled = false;

        //set screen to the countdown
        SetScreen(countdownUI);

        //start CoRoutine
        StartCoroutine(CountDownRoutine());
    }

    IEnumerator CountDownRoutine()
    {
        countdownText.gameObject.SetActive(true);

        countdown = 3;
        while (countdown > 0)
        {
            countdownText.text = countdown.ToString();
            yield return new WaitForSeconds(1f);
            countdown--;
        }
        countdownText.text = "go!!!";
        yield return new WaitForSeconds(1f);

        player.enabled = true;

        //start the game
        startGame();

    }
    public void startGame()

    {
        //set screen to see stats
        SetScreen(gameUI);
        timeActive = true;
    }
    public void endGame()
    {
        timeActive = false;
        player.enabled = false;

        //set the endscreen to show stats
        endUI_score.text = "Coins: " + player.coinCount;
        endUI_time.text = "Time: " + (time *1).ToString("F2");

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        SetScreen(endUI);

    }

    public void OnRestartButton()
    {
        //restart scene to play again
        SceneManager.LoadScene(0);
    }

    void Update()
    {
        if (timeActive == true)
        {
            time += Time.deltaTime;
        }

        gameUI_score.text = "Coins: " + player.coinCount;
        gameUI_health.text = "Health: " + player.health;
        gameUI_time.text = "Time: " + (time * 1).ToString("F2");

    }

    public void SetScreen(GameObject Screen)
    {
        //disable all other screens
        gameUI.SetActive(false);
        countdownUI.SetActive(false);
        endUI.SetActive(false);

        //activate the requested screen
        Screen.SetActive(true);
    }

}