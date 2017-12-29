using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialRequireBox : MonoBehaviour
{
    public bool isSuccessOrder = false;
    public GameUnits requireUnit;
    public Flavor ConeFlavor;
    public Flavor IceCreamFlavor;
    public bool isSprinkle;

    [SerializeField]
    private AudioClip addToRequireBox;
    [SerializeField]
    private AudioClip successRequire;
    [SerializeField]
    private TextMesh txtAmount = null;
    [SerializeField]
    private GameObject Top = null;
    [SerializeField]
    private GameObject Bot = null;

    private SpriteRenderer requireTop = null;
    private SpriteRenderer requireBot = null;
    private AudioSource audioSource;
    private GameController gameController;
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
        requireTop = Top.GetComponent<SpriteRenderer>();
        requireBot = Bot.GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        gameController = FindObjectOfType<GameController>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == requireUnit)
            {
                IceCreamMachine_Making specialUnit = other.GetComponent<IceCreamMachine_Making>();
                if ((specialUnit.ConeFlavor == ConeFlavor && specialUnit.IceCreamFlavor == IceCreamFlavor))
                {
                    unit.canPlace = true;
                    unit.onMachine = OnMachine.SpecialRequireBox;
                    unit.InRequireBox = gameObject;
                }
            }
            if(unit.gameUnit == GameUnits.CandyFloss)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.SpecialRequireBox;
                unit.InRequireBox = gameObject;
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("GameUnits"))
        {
            SweetUnits unit = other.GetComponent<SweetUnits>();
            if (unit.gameUnit == requireUnit)
            {
                IceCreamMachine_Making specialUnit = other.GetComponent<IceCreamMachine_Making>();
                if (specialUnit.ConeFlavor == ConeFlavor && specialUnit.IceCreamFlavor == IceCreamFlavor)
                {
                    unit.canPlace = true;
                    unit.onMachine = OnMachine.SpecialRequireBox;
                    unit.InRequireBox = gameObject;
                }
            }
            if (unit.gameUnit == GameUnits.CandyFloss)
            {
                unit.canPlace = true;
                unit.onMachine = OnMachine.SpecialRequireBox;
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
        }
    }
    public void AddProductToRequireBox()
    {
        amount-=1;
        audioSource.PlayOneShot(addToRequireBox);
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
    public void BeforeDestroy()
    {
        gameController.AddTime(3 * startAmount);
        int addMoney = 0;
        for (int i = 0; i < startAmount; i++)
        {
            addMoney += Random.Range(50, 70);
        }
        gameController.AddMoney(addMoney);
    }
    public void SettingRequirement(GameUnits units,Flavor coneFlavor,Flavor iceCreamFlavor,bool isSprinkle,int amount)
    {
        SpriteRefSweetUnit spriteRef = FindObjectOfType<SpriteRefSweetUnit>();
        ConeFlavor = coneFlavor;
        IceCreamFlavor = iceCreamFlavor;
        this.amount = amount;
        this.isSprinkle = isSprinkle;
        if(this.isSprinkle)
        {
            this.requireUnit = GameUnits.ConeAndUceCreamAndSprinkle;
        }
        else
        {
            this.requireUnit = units;
        }
        startAmount = amount;
        requireTop.sprite = spriteRef.getSpriteByType(GameUnits.IceCream, IceCreamFlavor, this.isSprinkle);
        requireBot.sprite = spriteRef.getSpriteByType(GameUnits.Cone, ConeFlavor, false);
    }
}
