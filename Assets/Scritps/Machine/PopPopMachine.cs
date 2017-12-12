using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPopMachine : Machine
{
    [SerializeField]
    private Vector3 spawnPoint;
    private GameObject spawnObject;
    public GameObject PopPop;
    private void Start()
    {
        maxSlot = 1;
        workingTime = 3;
    }
    protected override bool OnFinishWorking()
    {
        if (spawnObject == null)
        {
            spawnObject = Instantiate(this.PopPop, spawnPoint, Quaternion.identity);
            SweetUnits sweetUnits = spawnObject.GetComponent<SweetUnits>();
            sweetUnits.LastPosition = spawnPoint;
            return true;
        }
        else
        {
            return false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == GameUnits.Candy)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.PopPop;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == GameUnits.Candy)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.PopPop;
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
