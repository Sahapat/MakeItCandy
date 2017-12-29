using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameUnits
{
    Sugar,
    Topie,
    Candy,
    PopPop,
    Cone,
    IceCream,
    ConeAndIceCream,
    CandyFloss,
    ConeAndUceCreamAndSprinkle,
    None
};
public enum sugarFlavor
{
    Orange,
    Stawberry,
    Grape,
    PineApple,
    None
};
public enum Flavor
{
    Chocolate,
    Vanila,
    Orange,
    None
};
public enum OnMachine
{
    Candy,
    Topie,
    PopPop,
    ConeChocolate,
    ConeVanila,
    ConeOrange,
    IceCreamChocolate,
    IceCreamVanila,
    IceCreamOrange,
    Trash,
    RequireBox,
    SpecialRequireBox,
    Sprinkle,
    None
};

public class SweetUnits : MonoBehaviour
{
    public GameUnits gameUnit;
    public sugarFlavor sugar_flavor
    {
        get;
        protected set;
    }
    public OnMachine onMachine = OnMachine.None;
    public Vector3 LastPosition;
    public GameObject InRequireBox;
    public bool canPlace;
    private WaitForSeconds secound;

    private void Awake()
    {
        secound = new WaitForSeconds(0.05f);
    }
    public void ToLastPostion()
    {
        transform.position = LastPosition;
    }
    public void PlaceObject()
    {
        StartCoroutine(makeItSmall());
    }
    IEnumerator makeItSmall()
    {
        for(int i =0;i<10;i++)
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x / 2, this.transform.localScale.y / 2, 1);
            yield return secound;
        }
        BeforeDestroy();
        Destroy(gameObject);
    }
    protected virtual void BeforeDestroy()
    {
        print("not implement");
    }
}
