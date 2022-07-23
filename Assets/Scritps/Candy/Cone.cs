using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : SweetUnits
{
    public Flavor flavor = Flavor.None;
    private ConeMachine coneMachine;
    private SpriteRefSweetUnit spriteRef;
    private SpriteRenderer spriteRenderer;
    private void Start()
    {
        gameUnit = GameUnits.Cone;
        coneMachine = FindObjectOfType<ConeMachine>();
    }
    public void PlaceOnMachine(Vector3 pos)
    {
        transform.position = pos;
        LastPosition = pos;
        coneMachine.canSuccess = true;
    }
    protected override void BeforeDestroy()
    {
        coneMachine.canSuccess = true;
    }
    public void setFlavor(int flavor)
    {
        spriteRef = FindObjectOfType<SpriteRefSweetUnit>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        switch(flavor)
        {
            case 0:
                this.flavor = Flavor.Chocolate;
                break;
            case 1:
                this.flavor = Flavor.Orange;
                break;
            case 2:
                this.flavor = Flavor.Vanila;
                break;
            default:
                this.flavor = Flavor.None;
                break;
        }
        spriteRenderer.sprite = spriteRef.getSpriteByType(GameUnits.Cone, this.flavor, false);
    }
}
