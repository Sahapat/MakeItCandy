using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMachine : Machine
{
    [SerializeField]
    private Vector3 spawnPoint;
    public int ConeType;
    public GameObject ConeObj;
    public GameObject spawnObject;
    public bool canSuccess;
    private ConeMachineSelect coneMachineSelect;

    private void Start()
    {
        workingTime = 1.4f;
        maxSlot = 1;
        canSuccess = true;
        coneMachineSelect = FindObjectOfType<ConeMachineSelect>();
    }
    protected override bool OnFinishWorking()
    {
        if(canSuccess)
        {
            spawnObject = Instantiate(ConeObj, spawnPoint, Quaternion.identity);
            Cone cone = spawnObject.GetComponent<Cone>();
            cone.setFlavor(ConeType);
            cone.LastPosition = spawnPoint;
            canSuccess = false;
            return true;
        }
        else
        {
            return false;
        }
    }
    public override void OnAddProduct()
    {
        anim.SetInteger("ConeType", ConeType);
        coneMachineSelect.boxCollider.enabled = false;
    }
}
