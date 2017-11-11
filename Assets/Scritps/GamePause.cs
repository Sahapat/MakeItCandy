using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePause : MonoBehaviour
{
    public GameObject Pause;
    public bool isPause { get; private set; }
    private GameController gameController;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        Pause.SetActive(isPause);
    }
    private void Update()
    {
        if (gameController.isGameStart)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                isPause = !isPause;
                Pause.SetActive(isPause);
            }
        }
    }
    public void Resume()
    {
        isPause = !isPause;
        Pause.SetActive(isPause);
    }
    public void Exit()
    {
        Application.Quit();
    }
}
