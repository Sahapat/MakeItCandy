using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : SweetUnits
{
    public int onIndex = -1;
    private void Start()
    {
        gameUnit = GameUnits.Candy;
    }
    protected override void BeforeDestroy()
    {
        FindObjectOfType<CandyMachine>().isEmpty[onIndex] = true;
    }
    private void OnMouseEnter()
    {
    }
    private void OnMouseExit()
    {
    }
}
