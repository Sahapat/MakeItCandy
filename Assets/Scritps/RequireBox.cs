using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RequireBox : MonoBehaviour
{
    public bool isSuccessOrder;
    private GameUnits requireUnit;
    public sugarFlavor sugar_flavor;
    [SerializeField]
    private AudioClip addToRequirebox;
    [SerializeField]
    private AudioClip successRequire;
    [SerializeField]
    private TextMesh txtAmount = null;
    [SerializeField]
    private GameObject require = null;
    private SpriteRenderer requireSprite = null;
    private AudioSource audioSource = null;
    private GameController gameController = null;
    private int startAmount;
    private int _amount = 0;
    public int amount
    {
        get
        {
            return _amount;
        }
        set
        {
            _amount = (value > 0) ? value : 0;
            txtAmount.text = "X" + _amount;
            if(_amount == 0)
            {
                isSuccessOrder = true;
                SuccessProduct();
            }
        }
    }
    private void Awake()
    {
        requireSprite = require.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if ((requireUnit == unit.gameUnit && sugar_flavor == unit.sugar_flavor) || unit.gameUnit == GameUnits.CandyFloss)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.RequireBox;
                unit.InRequireBox = gameObject;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if ((requireUnit == unit.gameUnit&& sugar_flavor == unit.sugar_flavor)||unit.gameUnit == GameUnits.CandyFloss)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.RequireBox;
                unit.InRequireBox = gameObject;
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
            unit.InRequireBox = null;
        }
    }
    public void SetttingRequirement(GameUnits requireUnit,sugarFlavor flavor,int amount)
    {
        this.requireUnit = requireUnit;
        this.amount = amount;
        this.sugar_flavor = (requireUnit == GameUnits.Sugar)?sugarFlavor.None:flavor;
        this.startAmount = amount;
        requireSprite.sprite = FindObjectOfType<SpriteRefSweetUnit>().getSpriteByType(requireUnit, flavor);
    }
    public void AddProductToRequireBox()
    {
        amount-=1;
        audioSource.PlayOneShot(addToRequirebox);
    }
    public void FinishRequireBox()
    {
        amount = 0;
        audioSource.PlayOneShot(successRequire);
    }
    public void SuccessProduct()
    {
        audioSource.PlayOneShot(successRequire);
    }
    public void BeforeDestory()
    {
        int addScale = 0;
        int moneyScale = 0;
        switch (requireUnit)
        {
            case GameUnits.Sugar:
                addScale = 1;
                moneyScale = 10;
                break;
            case GameUnits.Candy:
                addScale = 1;
                moneyScale = 25;
                break;
            case GameUnits.Topie:
                addScale = 2;
                moneyScale = 40;
                break;
            case GameUnits.PopPop:
                addScale = 2;
                moneyScale = 40;
                break;
        }
        gameController.AddTime(addScale * startAmount);
        gameController.AddMoney(moneyScale * startAmount);
    }
}
