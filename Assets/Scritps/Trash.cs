using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    public AudioClip trashClip;
    private AudioSource audioSource;
    private GameController gameController = null;
    private Animator anim = null;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter(Collider other)
    {
         if (other.CompareTag("GameUnits"))
         {
             SweetUnits unit = other.GetComponent<SweetUnits>();
            unit.canPlace = true;
            unit.onMachine = OnMachine.Trash;
         }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            unit.canPlace = true;
            unit.onMachine = OnMachine.Trash;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            unit.canPlace = false;
            unit.onMachine = OnMachine.None;
        }
    }
    public void MinusMoney(GameUnits units)
    {
        audioSource.PlayOneShot(trashClip);
        anim.SetTrigger("Working");
        int moneyMinus = 0;
        int timeMinus = 0;
        switch(units)
        {
            case GameUnits.Candy:
                moneyMinus = -20;
                timeMinus = -3;
                break;
            case GameUnits.ConeAndIceCream:
                moneyMinus = -70;
                timeMinus = -10;
                break;
            case GameUnits.Cone:
                moneyMinus = -20;
                timeMinus = -3;
                break;
            case GameUnits.PopPop:
                moneyMinus = -35;
                timeMinus = -5;
                break;
            case GameUnits.Sugar:
                moneyMinus = -10;
                timeMinus = -2;
                break;
            case GameUnits.Topie:
                moneyMinus = -35;
                timeMinus = -5;
                break;
        }
        gameController.AddTime(timeMinus);
        gameController.AddMoney(moneyMinus);
    }
}
