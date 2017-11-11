using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMachine : Machine
{
    [SerializeField]
    private Vector3 spawnPoint;

    public int ConeType;
    public GameObject[] ConeObj;

    private void Start()
    {
        workingTime = 3;
        maxSlot = 1;
    }
    protected override void OnFinishWorking()
    {
        GameObject temp = Instantiate(ConeObj[ConeType], spawnPoint, Quaternion.identity);
        Cone cone = temp.GetComponent<Cone>();
        cone.setFlavor(ConeType);
        cone.LastPosition = spawnPoint;
    }
    public override void OnAddProduct()
    {
        anim.SetInteger("ConeType", ConeType);
    }
}
