using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMachine : Machine
{
    public Vector3 OnMachinePoint;
    [SerializeField]
    private Vector3 spawnPoint;
    [SerializeField]
    private Flavor iceCreamFlavor;
    private Flavor ConeFlavor;
    
    public GameObject IceCreamAndCone = null;
    public GameObject coneInput = null;
    private void Start()
    {
        maxSlot = 1;
        workingTime = 2;
    }
    public void AddConeFlavor(Flavor flavor)
    {
        ConeFlavor = flavor;
    }
    protected override void OnFinishWorking()
    {
        GameObject temp = Instantiate(IceCreamAndCone, spawnPoint, Quaternion.identity);
        IceCreamMachine_Making iceCreamMaking = temp.GetComponent<IceCreamMachine_Making>();
        iceCreamMaking.ConeFlavor = ConeFlavor;
        iceCreamMaking.IceCreamFlavor = iceCreamFlavor;
        iceCreamMaking.LastPosition = spawnPoint;
        iceCreamMaking.isSet = true;
        Destroy(coneInput);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == GameUnits.Cone)
            {
                unit.canPlace = true;
                switch(iceCreamFlavor)
                {
                    case Flavor.Chocolate:
                        unit.onMachine = OnMachine.IceCreamChocolate;
                        break;
                    case Flavor.Orange:
                        unit.onMachine = OnMachine.IceCreamOrange;
                        break;
                    case Flavor.Vanila:
                        unit.onMachine = OnMachine.IceCreamVanila;
                        break;
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == GameUnits.Cone)
            {
                unit.canPlace = true;
                switch (iceCreamFlavor)
                {
                    case Flavor.Chocolate:
                        unit.onMachine = OnMachine.IceCreamChocolate;
                        break;
                    case Flavor.Orange:
                        unit.onMachine = OnMachine.IceCreamOrange;
                        break;
                    case Flavor.Vanila:
                        unit.onMachine = OnMachine.IceCreamVanila;
                        break;
                }
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
