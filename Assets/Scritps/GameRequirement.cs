using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRequirement : MonoBehaviour
{
    [Header("Requirement")]
    public GameUnits unit;
    public Flavor icecream;
    public Flavor cone;
    public bool isSpecial;
    public int amount;

    public int RequireBoxEmptyPos;
    public int SuccessPos;
    public GameObject SugarObj = null;
    private SugarLineController sugarline;
    private HandleMachine_Create createRequireBox;
    private HandleMachine_Destory destoryRequireBox;
    private StockPath stockpath = null;
    private HandleOrder handleOrder = null;
    private void Awake()
    {
        sugarline = FindObjectOfType<SugarLineController>();
        createRequireBox = FindObjectOfType<HandleMachine_Create>();
        destoryRequireBox = FindObjectOfType<HandleMachine_Destory>();
        stockpath = FindObjectOfType<StockPath>();
        handleOrder = FindObjectOfType<HandleOrder>();
        amount = 3;
    }
    private void Update()
    {
        if(sugarline.SugarOnLine[sugarline.SugarOnLine.Length-1] == null)
        {
            AddSugar();
            sugarline.MoveSugar();
        }
        if (handleOrder.avilable)
        {
            if (stockpath.CheckEmpty(ref RequireBoxEmptyPos))
            {
                RandomRequirement();
                handleOrder.MoveToPos(RequireBoxEmptyPos);
                if (isSpecial)
                {
                    createRequireBox.CreateRequireIndex = RequireBoxEmptyPos;
                    createRequireBox.SetRequireBox(unit, cone, icecream, amount);
                }
                else
                {
                    createRequireBox.CreateRequireIndex = RequireBoxEmptyPos;
                    createRequireBox.SetRequireBox(unit, amount);
                }
                handleOrder.Add();
            }
            else if (stockpath.CheckSuccess(ref SuccessPos))
            {
                handleOrder.MoveToPos(SuccessPos);
                destoryRequireBox.obj_Destory = stockpath.requireBox[SuccessPos];
                handleOrder.Remove();
            }
        }
    }
    private void RandomRequirement()
    {
        switch(Random.Range(0,4))
        {
            case 0:
                unit = GameUnits.Sugar;
                break;
            case 1:
                unit = GameUnits.PopPop;
                break;
            case 2:
                unit = GameUnits.Topie;
                break;
            case 3:
                unit = GameUnits.Candy;
                break;
        }

        switch(Random.Range(0,2))
        {
            case 0:
                isSpecial = false;
                break;
            case 1:
                isSpecial = true;
                break;
        }

        if(isSpecial)
        {
            unit = GameUnits.ConeAndIceCream;
            switch(Random.Range(0,3))
            {
                case 0:
                    cone = Flavor.Chocolate;
                    break;
                case 1:
                    cone = Flavor.Orange;
                    break;
                case 2:
                    cone = Flavor.Vanila;
                    break;
            }
            switch(Random.Range(0,3))
            {
                case 0:
                    icecream = Flavor.Chocolate;
                    break;
                case 1:
                    icecream = Flavor.Orange;
                    break;
                case 2:
                    icecream = Flavor.Vanila;
                    break;
            }
        }
        amount = Random.Range(2, 7);
    }
    private void AddSugar()
    {
        GameObject temp = Instantiate(SugarObj, sugarline.AddPosition, Quaternion.identity);
        sugarline.SugarOnLine[3] = temp;
    }
}
