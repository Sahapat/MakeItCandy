using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMachine_Making : SweetUnits
{
    public Flavor IceCreamFlavor = Flavor.None;
    public Flavor ConeFlavor = Flavor.None;
    public bool isSet = false;

    [SerializeField]
    private Sprite[] IceCream = null;
    [SerializeField]
    private Sprite[] Cone = null;
    [SerializeField]
    private GameObject obj_IceCream = null;
    [SerializeField]
    private GameObject obj_Cone = null;

    private SpriteRenderer spIceCream = null;
    private SpriteRenderer spCone = null;

    private void Start()
    {
        spIceCream = obj_IceCream.GetComponent<SpriteRenderer>();
        spCone = obj_Cone.GetComponent<SpriteRenderer>();
        gameUnit = GameUnits.ConeAndIceCream;
    }
    private void Update()
    {
        if(isSet)
        {
            switch (IceCreamFlavor)
            {
                case Flavor.Chocolate:
                    spIceCream.sprite = IceCream[0];
                    break;
                case Flavor.Orange:
                    spIceCream.sprite = IceCream[2];
                    break;
                case Flavor.Vanila:
                    spIceCream.sprite = IceCream[1];
                    break;
                default:
                    spIceCream.sprite = null;
                    break;
            }

            switch (ConeFlavor)
            {
                case Flavor.Chocolate:
                    spCone.sprite = Cone[0];
                    break;
                case Flavor.Orange:
                    spCone.sprite = Cone[2];
                    break;
                case Flavor.Vanila:
                    spCone.sprite = Cone[1];
                    break;
                default:
                    spCone.sprite = null;
                    break;
            }
            isSet = false;
        }
    }
}
