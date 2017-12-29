using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprinkleMachine : Machine
{
    public GameObject coneInput;
    public Vector3 OnMachinePoint;

    protected override bool OnFinishWorking()
    {
        IceCreamMachine_Making icecream = coneInput.GetComponent<IceCreamMachine_Making>();
        icecream.setUnitProperty(icecream.ConeFlavor, icecream.IceCreamFlavor, true);
        icecream.isSprinkle = true;
        return true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if(unit.gameUnit == GameUnits.ConeAndIceCream&&coneInput == null)
            {
                unit.onMachine = OnMachine.Sprinkle;
                unit.canPlace = true;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == GameUnits.ConeAndIceCream && coneInput == null)
            {
                unit.onMachine = OnMachine.Sprinkle;
                unit.canPlace = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            unit.canPlace = false;
            unit.onMachine = OnMachine.None;
        }
    }
}
