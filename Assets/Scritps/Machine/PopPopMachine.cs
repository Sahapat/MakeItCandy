using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopPopMachine : Machine
{
    [SerializeField]
    private Vector3 spawnPoint;
    [SerializeField]
    private GameObject objState;

    private sugarFlavor _flavorState;
    public sugarFlavor flavorState
    {
        get
        {
            return _flavorState;
        }
        set
        {
            _flavorState = value;
            stateSprite.sprite = spriteRefSweetUnit.getSpriteByType(_flavorState);
        }
    }

    private GameObject spawnObject;
    private SpriteRenderer stateSprite;
    private SpriteRefSweetUnit spriteRefSweetUnit;
    public GameObject popPop;
    private void Start()
    {
        maxSlot = 1;
        workingTime = 3;
        stateSprite = objState.GetComponent<SpriteRenderer>();
        spriteRefSweetUnit = FindObjectOfType<SpriteRefSweetUnit>();
        flavorState = sugarFlavor.Orange;
    }
    protected override bool OnFinishWorking()
    {
        if (spawnObject == null)
        {
            spawnObject = Instantiate(popPop, spawnPoint, Quaternion.identity);
            PopPop sweetUnits = spawnObject.GetComponent<PopPop>();
            sweetUnits.LastPosition = spawnPoint;
            sweetUnits.setUnitProperty(GameUnits.PopPop, flavorState);
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
