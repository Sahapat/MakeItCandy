using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRequirement : MonoBehaviour
{
    [Header("Requirement")]
    public GameUnits unit;
    public Flavor icecream;
    public sugarFlavor sugar_flavor;
    public Flavor cone;
    public bool isSpecial;
    public bool isSprinkle;
    public int amount;

    public int RequireBoxEmptyPos;
    public int SuccessPos;
    public GameObject SugarObj = null;
    private SugarLineController sugarline;
    private HandleMachine_Create createRequireBox;
    private HandleMachine_Destory destoryRequireBox;
    private StockPath stockpath = null;
    private HandleOrder handleOrder = null;
    private StoryScene storyScene;
    private GameController controller;
    private SpricalCandyFloss candyFloss = null;

    public bool isEndless;
    public int numOfBox;
    private void Awake()
    {
        sugarline = FindObjectOfType<SugarLineController>();
        createRequireBox = FindObjectOfType<HandleMachine_Create>();
        destoryRequireBox = FindObjectOfType<HandleMachine_Destory>();
        stockpath = FindObjectOfType<StockPath>();
        handleOrder = FindObjectOfType<HandleOrder>();
        storyScene = FindObjectOfType<StoryScene>();
        controller = FindObjectOfType<GameController>();
        candyFloss = FindObjectOfType<SpricalCandyFloss>();
        numOfBox = LevelManager.amountBox;
    }
    private void Update()
    {
        if(sugarline.SugarOnLine[sugarline.SugarOnLine.Length-1] == null)
        {
            AddSugar();
            sugarline.MoveSugar();
        }
        if (isEndless)
        {
            if (handleOrder.avilable)
            {
                if (stockpath.CheckEmpty(ref RequireBoxEmptyPos))
                {
                    RandomRequirementEndless();
                    handleOrder.MoveToPos(RequireBoxEmptyPos);
                    if (isSpecial)
                    {
                        createRequireBox.CreateRequireIndex = RequireBoxEmptyPos;
                        createRequireBox.SetRequireBox(unit, cone, icecream, isSprinkle, amount);
                    }
                    else
                    {
                        createRequireBox.CreateRequireIndex = RequireBoxEmptyPos;
                        createRequireBox.SetRequireBox(unit, sugar_flavor, amount);
                    }
                    handleOrder.Add();
                }
                else if (stockpath.CheckSuccess(ref SuccessPos))
                {
                    handleOrder.MoveToPos(SuccessPos);
                    destoryRequireBox.obj_Destory = stockpath.requireBox[SuccessPos];
                    handleOrder.Remove();
                    candyFloss.flossLevel += 1;
                }
            }
        }
        else
        {
            if (handleOrder.avilable)
            {
                if (stockpath.CheckEmpty(ref RequireBoxEmptyPos)&&numOfBox > 0)
                {
                    RandomRequirementStory();
                    handleOrder.MoveToPos(RequireBoxEmptyPos);
                    if (isSpecial)
                    {
                        createRequireBox.CreateRequireIndex = RequireBoxEmptyPos;
                        createRequireBox.SetRequireBox(unit, cone, icecream, isSprinkle, amount);
                    }
                    else
                    {
                        createRequireBox.CreateRequireIndex = RequireBoxEmptyPos;
                        createRequireBox.SetRequireBox(unit, sugar_flavor, amount);
                    }
                    handleOrder.Add();
                    numOfBox -= 1;
                }
                else if (stockpath.CheckSuccess(ref SuccessPos))
                {
                    handleOrder.MoveToPos(SuccessPos);
                    destoryRequireBox.obj_Destory = stockpath.requireBox[SuccessPos];
                    handleOrder.Remove();
                }
            }
            if(stockpath.requireBox[0] == null&& stockpath.requireBox[1] == null&& stockpath.requireBox[2] == null&&numOfBox == 0)
            {
                if (controller.isGameStart)
                {
                    storyScene.Win();
                }
            }
        }
    }
    private void RandomRequirementEndless()
    {
        if(Random.value <= 0.1)
        {
            switch(Random.Range(0,2))
            {
                case 0:
                    unit = GameUnits.Sugar;
                    sugar_flavor = sugarFlavor.None;
                    break;
                case 1:
                    unit = GameUnits.Candy;
                    sugar_flavor = sugarFlavor.None;
                    break;
            }
        }
        else if(Random.value <= 0.6)
        {
            switch (Random.Range(0, 2))
            {
                case 0:
                    unit = GameUnits.Topie;
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            sugar_flavor = sugarFlavor.Grape;
                            break;
                        case 1:
                            sugar_flavor = sugarFlavor.Orange;
                            break;
                        case 2:
                            sugar_flavor = sugarFlavor.PineApple;
                            break;
                        case 3:
                            sugar_flavor = sugarFlavor.Stawberry;
                            break;
                    }
                    break;
                case 1:
                    unit = GameUnits.PopPop;
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            sugar_flavor = sugarFlavor.Grape;
                            break;
                        case 1:
                            sugar_flavor = sugarFlavor.Orange;
                            break;
                        case 2:
                            sugar_flavor = sugarFlavor.PineApple;
                            break;
                        case 3:
                            sugar_flavor = sugarFlavor.Stawberry;
                            break;
                    }
                    break;
            }
        }
        else if(Random.value <= 40)
        {
            isSpecial = true;
        }
        else
        {
            unit = GameUnits.Candy;
            sugar_flavor = sugarFlavor.None;
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
            if(Random.value <= 0.35f)
            {
                isSprinkle = true;
            }
            else
            {
                isSprinkle = false;
            }
        }
        amount = Random.Range(1, 4);
    }
    private void RandomRequirementStory()
    {
        if(LevelManager.haveConeMachine)
        {
            if(LevelManager.haveIceCreamMachine)
            {
                if (Random.value <= (LevelManager.IceCreamPercent))
                {
                    unit = GameUnits.ConeAndIceCream;
                    switch (Random.Range(0, 3))
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
                    switch (Random.Range(0, 3))
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
                    if (Random.value <= (LevelManager.sprinklePercent))
                    {
                        isSprinkle = true;
                    }
                    else
                    {
                        isSprinkle = false;
                    }
                }
                if (LevelManager.haveCandyMachine)
                {
                    if (Random.value <= LevelManager.candyPercent)
                    {
                        unit = GameUnits.Candy;
                        sugar_flavor = sugarFlavor.None;
                    }
                    else
                    {
                        unit = GameUnits.Sugar;
                        sugar_flavor = sugarFlavor.None;
                    }
                }

                if (LevelManager.haveTopieMachine)
                {
                    if (Random.value <= LevelManager.topiePercent)
                    {
                        unit = GameUnits.Topie;
                        switch (Random.Range(0, 4))
                        {
                            case 0:
                                sugar_flavor = sugarFlavor.Grape;
                                break;
                            case 1:
                                sugar_flavor = sugarFlavor.Orange;
                                break;
                            case 2:
                                sugar_flavor = sugarFlavor.PineApple;
                                break;
                            case 3:
                                sugar_flavor = sugarFlavor.Stawberry;
                                break;
                        }
                    }
                }
                if (LevelManager.havePoppopMachine)
                {
                    if (Random.value <= LevelManager.PoppopPercent)
                    {
                        unit = GameUnits.PopPop;
                        switch (Random.Range(0, 4))
                        {
                            case 0:
                                sugar_flavor = sugarFlavor.Grape;
                                break;
                            case 1:
                                sugar_flavor = sugarFlavor.Orange;
                                break;
                            case 2:
                                sugar_flavor = sugarFlavor.PineApple;
                                break;
                            case 3:
                                sugar_flavor = sugarFlavor.Stawberry;
                                break;
                        }
                    }
                }
                amount = Random.Range(1, LevelManager.amount);
            }
        }
        else
        {
            if (LevelManager.haveCandyMachine)
            {
                if (Random.value <= LevelManager.candyPercent)
                {
                    unit = GameUnits.Candy;
                    sugar_flavor = sugarFlavor.None;
                }
                else if(Random.value <= 0.1)
                {
                    unit = GameUnits.Sugar;
                    sugar_flavor = sugarFlavor.None;
                }
            }

            if (LevelManager.haveTopieMachine)
            {
                if (Random.value <= LevelManager.topiePercent)
                {
                    unit = GameUnits.Topie;
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            sugar_flavor = sugarFlavor.Grape;
                            break;
                        case 1:
                            sugar_flavor = sugarFlavor.Orange;
                            break;
                        case 2:
                            sugar_flavor = sugarFlavor.PineApple;
                            break;
                        case 3:
                            sugar_flavor = sugarFlavor.Stawberry;
                            break;
                    }
                }
            }
            if (LevelManager.havePoppopMachine)
            {
                if (Random.value <= LevelManager.PoppopPercent)
                {
                    unit = GameUnits.PopPop;
                    switch (Random.Range(0, 4))
                    {
                        case 0:
                            sugar_flavor = sugarFlavor.Grape;
                            break;
                        case 1:
                            sugar_flavor = sugarFlavor.Orange;
                            break;
                        case 2:
                            sugar_flavor = sugarFlavor.PineApple;
                            break;
                        case 3:
                            sugar_flavor = sugarFlavor.Stawberry;
                            break;
                    }
                }
            }
            amount = Random.Range(1, LevelManager.amount);
        }
    }
    private void AddSugar()
    {
        GameObject temp = Instantiate(SugarObj, sugarline.AddPosition, Quaternion.identity);
        sugarline.SugarOnLine[sugarline.SugarOnLine.Length-1] = temp;
    }
}
