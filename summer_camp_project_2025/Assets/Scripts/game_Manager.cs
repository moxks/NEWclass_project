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

    [Header("Screens")]
    public GameObject countdownUI;
    public GameObject gameUI;   

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player_Capsule").GetComponent<player_Controller>();

        //make sure the timer is set to 0
        time = 0;

        //initially disables player movement
        player.enabled = false;
        StartCoroutine(CountDownRoutine());

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
    }

    void Update()
    {
        if (timeActive == true)
        {
            time += Time.deltaTime;
        }

        gameUI_score.text = "Coins: " + player.coinCount;
        gameUI_health.text = "Health: " + player.health;
        gameUI_time.text = "Time: " + (time * 10).ToString("F2");

    }

    public void SetScreen(GameObject Screen)
    {
        //disable all other screens
        gameUI.SetActive(false);
        countdownUI.SetActive(false);

        //activate the requested screen
        Screen.SetActive(true);
    }

}