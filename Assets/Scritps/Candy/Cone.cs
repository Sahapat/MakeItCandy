using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cone : SweetUnits
{
    public Flavor flavor = Flavor.None;
    private ConeMachine coneMachine;
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
    public void setFlavor(int flavor)
    {
        switch(flavor)
        {
            case 0:
                this.flavor = Flavor.Chocolate;
                break;
            case 1:
                this.flavor = Flavor.Vanila;
                break;
            case 2:
                this.flavor = Flavor.Orange;
                break;
            default:
                this.flavor = Flavor.None;
                break;
        }
    }
    private void OnMouseEnter()
    {
    }
    private void OnMouseExit()
    {
    }
}
