using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMachine_Create : MonoBehaviour
{
    public int CreateRequireIndex;
    public bool isSpecial;
    public GameObject requireBox;
    public GameObject specialRequireBox;
    public bool isSet;
    public int amount;
    private GameUnits unit;
    private Flavor coneFlavor;
    private Flavor iceCreamFlavor;

    private StockPath stockPath = null;

    private void Awake()
    {
        stockPath = FindObjectOfType<StockPath>();
    }

    private void Update()
    {
        if(isSet)
        {
            if (stockPath.requireBox[CreateRequireIndex] == null)
            {
                if(isSpecial)
                {
                    GameObject temp = Instantiate(specialRequireBox, stockPath.requireBoxPos[CreateRequireIndex], Quaternion.identity);
                    SpecialRequireBox setting = temp.GetComponent<SpecialRequireBox>();
                    setting.SettingRequirement(unit, coneFlavor, iceCreamFlavor,amount);
                    stockPath.requireBox[CreateRequireIndex] = temp;
                }
                else
                {
                    GameObject temp = Instantiate(requireBox, stockPath.requireBoxPos[CreateRequireIndex], Quaternion.identity);
                    RequireBox setting = temp.GetComponent<RequireBox>();
                    setting.SetttingRequirement(unit, amount);
                    stockPath.requireBox[CreateRequireIndex] = temp;
                }
            }
            isSet = false;
        }
    }

    public void SetRequireBox(GameUnits unit,int amount)
    {
        this.unit = unit;
        coneFlavor = Flavor.None;
        iceCreamFlavor = Flavor.None;
        this.amount = amount;
        isSpecial = false;
        isSet = true;
    }
    public void SetRequireBox(GameUnits unit,Flavor coneFlavor,Flavor iceCreamFlavor,int amount)
    {
        this.unit = unit;
        this.coneFlavor = coneFlavor;
        this.iceCreamFlavor = iceCreamFlavor;
        this.amount = amount;
        isSpecial = true;
        isSet = true;
    }
}
