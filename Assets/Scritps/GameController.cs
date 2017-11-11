using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private Text txtTime;
    [SerializeField]
    private Text txtMoney;
    
    public bool isGameStart = false;
    public bool isTimeout = false;
    private TimeCounter timeCounter = null;
    private AudioSource audiosource;
    public AudioClip endgame;
    private Main main;
    public int minute
    {
        get;
        private set;
    }
    private int _second;
    public int second
    {
        get
        {
            return _second;
        }
        set
        {
            _second = value;
            if(_second >= 60)
            {
                minute += 1;
                _second = 0;
            }
            else if(_second < 0)
            {
                if(minute > 0)
                {
                    minute -= 1;
                    second = 59;
                }
                else if(minute == 0)
                {
                    second = 0;
                    minute = 0;
                    isTimeout = true;
                }
                else
                {
                    minute = 0;
                    second = 59;
                }
            }
            string Txtsecond = (_second.ToString().Length == 1) ? "0" + _second : _second.ToString();
            txtTime.text = minute.ToString() + ":" + Txtsecond;
        }
    }
    private int _money = 0;
    public int money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            txtMoney.text = _money.ToString();
        }
    }
    public int gameTime = 0;
    private GamePause pause;
    public GameObject EndGame;
    private void Start()
    {
        timeCounter = FindObjectOfType<TimeCounter>();
        pause = FindObjectOfType<GamePause>();
        main = FindObjectOfType<Main>();
        audiosource = GetComponent<AudioSource>();
        minute = 3;
        second = 0;
        money = 0;
        StartCoroutine(gameStart());
    }
    private void Update()
    {
        if(isGameStart)
        {
            if (pause.isPause)
            {
                timeCounter.isCounter = false;
            }
            else
            {
                timeCounter.isCounter = true;
            }
            if (minute == 0 && second == 0)
            {
                isGameStart = false;
                main.EndGame();
                EndGame.SetActive(true);
                audiosource.PlayOneShot(endgame);
            }
        }
    }
    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(8f);
        isGameStart = true;
    }
    public void AddTime(int add)
    {
        second += add;
    }
    public void AddMoney(int add)
    {
        money += add;
    }
}
