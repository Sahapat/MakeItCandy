using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Candy : SweetUnits
{
    private void Start()
    {
        gameUnit = GameUnits.Candy;
        sugar_flavor = sugarFlavor.None;
    }
}
