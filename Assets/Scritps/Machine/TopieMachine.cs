using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopieMachine : Machine
{
    [SerializeField]
    private Vector3 spawnPoint;
    private GameObject spawnObject;
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
    public GameObject topie = null;
    private SpriteRenderer stateSprite = null;
    private SpriteRefSweetUnit spriteRefSweetUnit;

    private void Start()
    {
        workingTime = 3;
        maxSlot = 1;
        stateSprite = objState.GetComponent<SpriteRenderer>();
        spriteRefSweetUnit = FindObjectOfType<SpriteRefSweetUnit>();
        flavorState = sugarFlavor.Orange;
    }
    protected override bool OnFinishWorking()
    {
        if (spawnObject == null)
        {
            spawnObject = Instantiate(this.topie, spawnPoint, Quaternion.identity);
            Topie sweetUnits = spawnObject.GetComponent<Topie>();
            sweetUnits.LastPosition = spawnPoint;
            sweetUnits.setUnitProperty(GameUnits.Topie, flavorState);
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
