using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMachineSelect : MonoBehaviour
{
    private ConeMachine coneMachine = null;
    private GamePause pause;
    private void Awake()
    {
        coneMachine = FindObjectOfType<ConeMachine>();
        pause = FindObjectOfType<GamePause>();
    }

    private void OnMouseDown()
    {
        if (!pause.isPause)
        {
            switch (gameObject.name)
            {
                case "Chocolate":
                    if (!coneMachine.isWorking)
                    {
                        coneMachine.ConeType = 0;
                        coneMachine.Add();
                    }
                    break;
                case "Orange":
                    if (!coneMachine.isWorking)
                    {
                        coneMachine.ConeType = 2;
                        coneMachine.Add();
                    }
                    break;
                case "Vanila":
                    if (!coneMachine.isWorking)
                    {
                        coneMachine.ConeType = 1;
                        coneMachine.Add();
                    }
                    break;
            }
        }
    }
}
