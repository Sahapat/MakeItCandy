using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCounter : MonoBehaviour
{
    public bool isCounter = false;
    private GameController gameController = null;
    private bool isStart = true;
    private WaitForSeconds seconds;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        seconds = new WaitForSeconds(1);
    }
    
    IEnumerator timeCount(int n)
    {
        while(true)
        {
            gameController.second -= n;
            gameController.gameTime += n;
            yield return seconds;
        }
    }

    private void Update()
    {
        if(isCounter)
        {
            if(isStart)
            {
                StartCoroutine(timeCount(1));
                isStart = false;
            }
        }
        else
        {
            StopAllCoroutines();
            isStart = true;
        }
    }
}
