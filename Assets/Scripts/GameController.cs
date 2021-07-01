using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MazeConstructor))]
public class GameController : MonoBehaviour
{
    [SerializeField] private FpsMovement player;
    [SerializeField] private Text timeLabel;
    [SerializeField] private Text ScoreLabel;

    private MazeConstructor generate;

    private DateTime startTime;
    private int timeLimit;
    private int reduceLimitBy;

    private int score;
    private bool goalReached;

    void Start()
    {
        generate = GetComponent<MazeConstructor>();
        StartNewGame();
    }

    private void StartNewGame()
    {
        timeLimit = 80;
        reduceLimitBy = 5;
        startTime = DateTime.Now;
        score = 0;
        ScoreLabel.text = score.ToString();

        StartNewMaze();
    }

    private void StartNewMaze()
    {
        generate.GenerateNewMaze(13, 15, OnStartTrigger, OnGoalTrigger);

        float x = generate.startCol * generate.hallWidth;
        float y = 1;
        float z = generate.startRow * generate.hallWidth; ;
        player.transform.position = new Vector3(x, y, z);

        goalReached = false;
        player.enabled = true;

        timeLimit -= reduceLimitBy;
        startTime = DateTime.Now;

    }

    private void Update()
    {
        if(!player.enabled)
        {
            return;
        }

        int timeUsed = (int)(DateTime.Now - startTime).TotalSeconds;
        int timeLeft = timeLimit - timeUsed;

        if(timeLeft>0)
        {
            timeLabel.text = timeLeft.ToString();
        }
        else
        {
            timeLabel.text = "Time UP";
            player.enabled = false;

            Invoke("StartNewGame", 4);
        }
    }

    private void OnGoalTrigger(GameObject trigger, GameObject other)
    {
        Debug.Log("Goal");
        goalReached = true;

        score += 1;
        ScoreLabel.text = score.ToString();

        Destroy(trigger);
    }

    private void OnStartTrigger(GameObject trigger, GameObject other)
    {
        if(goalReached)
        {
            Debug.Log("Finish");
            player.enabled = false;

            Invoke("StartNewMaze", 4);
        }
    }

}
