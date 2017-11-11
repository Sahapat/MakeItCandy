using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CandyMachine : Machine
{
    [SerializeField]
    private Vector3[] spawnPoint;
    public bool[] isEmpty;
    private int spawnIndex;
    public GameObject Candy;
    private void Start()
    {
        maxSlot = 3;
        workingTime = 2;
        isEmpty = new bool[3]
            { true,true,true};
    }
    protected override void OnStartWorking()
    {
        for (int i = 0; i < spawnPoint.Length; i++)
        {
            if (isEmpty[i])
            {
                spawnIndex = i;
                break;
            }
        }
    }
    protected override void OnFinishWorking()
    {
        GameObject temp = Instantiate(this.Candy, spawnPoint[spawnIndex], Quaternion.identity);
        Candy candy = temp.GetComponent<Candy>();
        candy.onIndex = spawnIndex;
        candy.LastPosition = spawnPoint[spawnIndex];
        isEmpty[spawnIndex] = false;

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
