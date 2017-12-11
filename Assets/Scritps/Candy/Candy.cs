using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : SweetUnits
{
    private void Start()
    {
        gameUnit = GameUnits.Candy;
    }
    protected override void BeforeDestroy()
    {
    }
    private void OnMouseEnter()
    {
    }
    private void OnMouseExit()
    {
    }
}
