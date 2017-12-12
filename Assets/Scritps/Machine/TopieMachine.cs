using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopieMachine : Machine
{
    [SerializeField]
    private Vector3 spawnPoint;
    private GameObject spawnObject;

    public GameObject Topie = null;

    private void Start()
    {
        workingTime = 3;
        maxSlot = 1;
    }
    protected override bool OnFinishWorking()
    {
        if (spawnObject == null)
        {
            spawnObject = Instantiate(this.Topie, spawnPoint, Quaternion.identity);
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
                unit.onMachine = OnMachine.Topie;
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
                unit.onMachine = OnMachine.Topie;
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
