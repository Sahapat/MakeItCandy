using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRefSweetUnit : MonoBehaviour
{
    [SerializeField]
    private Sprite[] poppopUnit;
    [SerializeField]
    private Sprite[] topieUnit;
    [SerializeField]
    private Sprite[] coneUnit;
    [SerializeField]
    private Sprite[] icecreamUnit;
    [SerializeField]
    private Sprite[] icecreamAndSprinkleUnit;
    [SerializeField]
    private Sprite[] commonUnit;
    [SerializeField]
    private Sprite[] flavorUnit;

    public Sprite getSpriteByType(sugarFlavor flavor)
    {
        if (flavorUnit.Length == 0) return null;
        Sprite temp = null; 
        switch(flavor)
        {
            case sugarFlavor.Orange:
                temp = flavorUnit[0];
                break;
            case sugarFlavor.PineApple:
                temp = flavorUnit[1];
                break;
            case sugarFlavor.Grape:
                temp = flavorUnit[2];
                break;
            case sugarFlavor.Stawberry:
                temp = flavorUnit[3];
                break;
            default:
                temp = null;
                break;
        }
        return temp;
    }
    public Sprite getSpriteByType(GameUnits unit,Flavor flavor,bool isSprinkle)
    {
        if (coneUnit.Length == 0 && icecreamUnit.Length == 0 && icecreamAndSprinkleUnit.Length == 0) return null;

        Sprite temp = null;

        switch(unit)
        {
            case GameUnits.Cone:
                switch(flavor)
                {
                    case Flavor.Chocolate:
                        temp = coneUnit[0];
                        break;
                    case Flavor.Orange:
                        temp = coneUnit[1];
                        break;
                    case Flavor.Vanila:
                        temp = coneUnit[2];
                        break;
                    default:
                        temp = null;
                        break;
                }
                break;
            case GameUnits.IceCream:
                switch (flavor)
                {
                    case Flavor.Chocolate:
                        temp = (isSprinkle) ? icecreamAndSprinkleUnit[0] : icecreamUnit[0];
                        break;
                    case Flavor.Orange:
                        temp = (isSprinkle) ? icecreamAndSprinkleUnit[1] : icecreamUnit[1];
                        break;
                    case Flavor.Vanila:
                        temp = (isSprinkle) ? icecreamAndSprinkleUnit[2] : icecreamUnit[2];
                        break;
                    default:
                        temp = null;
                        break;
                }
                break;
            default:
                temp = null;
                break;
        }
        return temp;
    }
    public Sprite getSpriteByType(GameUnits unit, sugarFlavor flavor)
    {
        if (poppopUnit.Length == 0 && topieUnit.Length == 0) return null;
        Sprite temp = null;

        switch(unit)
        {
            case GameUnits.PopPop:
                switch(flavor)
                {
                    case sugarFlavor.Orange:
                        temp = poppopUnit[0];
                        break;
                    case sugarFlavor.Stawberry:
                        temp = poppopUnit[1];
                        break;
                    case sugarFlavor.Grape:
                        temp = poppopUnit[2];
                        break;
                    case sugarFlavor.PineApple:
                        temp = poppopUnit[3];
                        break;
                    default:
                        temp = null;
                        break;
                }
                break;
            case GameUnits.Topie:
                switch (flavor)
                {
                    case sugarFlavor.Orange:
                        temp = topieUnit[0];
                        break;
                    case sugarFlavor.Stawberry:
                        temp = topieUnit[1];
                        break;
                    case sugarFlavor.Grape:
                        temp = topieUnit[2];
                        break;
                    case sugarFlavor.PineApple:
                        temp = topieUnit[3];
                        break;
                    default:
                        temp = null;
                        break;
                }
                break;
            case GameUnits.Sugar:
                temp = commonUnit[0];
                break;
            case GameUnits.Candy:
                temp = commonUnit[1];
                break;
            case GameUnits.CandyFloss:
                temp = commonUnit[2];
                break;
            default:
                temp = null;
                break;
        }
        return temp;
    }
}
