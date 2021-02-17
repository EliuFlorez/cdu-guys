using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Core;

public class StartController : MonoBehaviour
{
    public int startTime = 3;
    public TMP_Text startDisplay;

    public float timeRemaining = 120;
    public bool timerIsRunning = false;
    public TMP_Text timeText;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("CountDownToStart");
    }

    // Update is called once per frame
    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;

                // Game Over
                Game.state = Game.State.over;
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    IEnumerator CountDownToStart()
    {
        while (startTime > 0)
        {
            startDisplay.text = startTime.ToString();

            yield return new WaitForSeconds(1f);

            startTime--;
        }

        startDisplay.text = "GO!";

        // Begin Play

        yield return new WaitForSeconds(0.5f);

        startDisplay.gameObject.SetActive(false);

        // Starts the timer automatically
        timerIsRunning = true;

        // Game State
        Game.state = Game.State.play;
    }
}
