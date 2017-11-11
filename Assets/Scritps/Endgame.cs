using UnityEngine.UI;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    [SerializeField]
    private Text resultMoney;
    private GameController gameController = null;
    private int currentMoney;

    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
    }
    private void Update()
    {
        if(currentMoney < gameController.money)
        {
            currentMoney += (int)(gameController.money * 0.05);
        }
        else if(currentMoney > gameController.money)
        {
            currentMoney = gameController.money;
        }
        resultMoney.text = currentMoney.ToString();
    }
}
