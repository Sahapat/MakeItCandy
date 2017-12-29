using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public Text playerName;
    public Text money;

    private void Start()
    {
        playerName.text = (PlayerStats.playerName != null) ? "2. "+PlayerStats.playerName :"2. "+ "Unknown";
        money.text = PlayerStats.highScore.ToString();
    }
}
