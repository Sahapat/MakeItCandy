using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPop : SweetUnits
{
    private SpriteRenderer spriteRenderer = null;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void setUnitProperty(GameUnits unit,sugarFlavor flavor)
    {
        gameUnit = unit;
        sugar_flavor = flavor;
        spriteRenderer.sprite = FindObjectOfType<SpriteRefSweetUnit>().getSpriteByType(unit, flavor);
    }
}
