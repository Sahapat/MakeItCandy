using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoryScene : MonoBehaviour
{
    public Text showingText;
    public GameObject EndGame;
    private Main main;
    private GameController controller;

    public AudioClip winnerClip;
    public AudioClip loserClip;
    private AudioSource audiosource;
    private TimeCounter timeCounter = null;

    private void Awake()
    {
        audiosource = GetComponent<AudioSource>();
        timeCounter = FindObjectOfType<TimeCounter>();
        controller = FindObjectOfType<GameController>();
        main = FindObjectOfType<Main>();
    }
    public void Win()
    {
        controller.isGameStart = false;
        showingText.text = "Pass";
        main.EndGame();
        EndGame.SetActive(true);
        timeCounter.isCounter = false;
        audiosource.PlayOneShot(winnerClip);
        PlayerStats.passLevel += 1;
    }
    public void Lose()
    {
        timeCounter.isCounter = false;
        controller.isGameStart = false;
        showingText.text = "Lose";
        main.EndGame();
        EndGame.SetActive(true);
        audiosource.PlayOneShot(loserClip);
    }
}
