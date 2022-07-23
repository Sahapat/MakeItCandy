using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingLevel : MonoBehaviour
{
    public GameObject CandyMachine;
    public GameObject PoppopMachine;
    public GameObject TopieMachine;
    public GameObject CandyFloss;
    public GameObject ConeMachine;
    public GameObject IceCreamMachine;
    public GameObject Sprinkle;
    public GameObject Trash;

    private void Start()
    {
        CandyMachine.SetActive(LevelManager.haveCandyMachine);
        PoppopMachine.SetActive(LevelManager.havePoppopMachine);
        TopieMachine.SetActive(LevelManager.haveTopieMachine);
        CandyFloss.SetActive(LevelManager.haveFloss);
        ConeMachine.SetActive(LevelManager.haveConeMachine);
        IceCreamMachine.SetActive(LevelManager.haveIceCreamMachine);
        Sprinkle.SetActive(LevelManager.haveSpinkle);
        Trash.SetActive(LevelManager.haveTrash);
    }
}
