using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyMachine : Machine
{
    [SerializeField]
    private GameObject[] spawnObject;
    [SerializeField]
    private Vector3[] spawnPoint;
    private int spawnIndex;
    public GameObject Candy;
    private void Start()
    {
        maxSlot = 3;
        workingTime = 2;
        spawnObject = new GameObject[spawnPoint.Length];
    }
    protected override bool OnFinishWorking()
    {
        for(int i =0;i<spawnObject.Length;i++)
        {
            if(spawnObject[i] == null)
            {
                spawnIndex = i;
                break;
            }
            else
            {
                spawnIndex = -1;
            }
        }

        if(spawnIndex == -1)
        {
            return false;
        }
        else
        {
            spawnObject[spawnIndex] = Instantiate(this.Candy, spawnPoint[spawnIndex], Quaternion.identity);
            Candy candy = spawnObject[spawnIndex].GetComponent<Candy>();
            candy.LastPosition = spawnPoint[spawnIndex];
            return true;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if(unit.gameUnit == GameUnits.Sugar)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.Candy;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == GameUnits.Sugar)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.Candy;
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
