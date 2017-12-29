using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceCreamMachine_Making : SweetUnits
{
    public Flavor IceCreamFlavor = Flavor.None;
    public Flavor ConeFlavor = Flavor.None;
    public bool isSprinkle = false;

    [SerializeField]
    private GameObject obj_IceCream = null;
    [SerializeField]
    private GameObject obj_Cone = null;

    private SpriteRenderer spIceCream = null;
    private SpriteRenderer spCone = null;

    private IceCreamMachine[] iceCreamMachine = new IceCreamMachine[3];
    private SpriteRefSweetUnit spriteRef = null;

    private void Start()
    {
        gameUnit = GameUnits.ConeAndIceCream;
        sugar_flavor = sugarFlavor.None;
        iceCreamMachine[0] = GameObject.Find("Chocolate").GetComponent<IceCreamMachine>();
        iceCreamMachine[1] = GameObject.Find("Orange").GetComponent<IceCreamMachine>();
        iceCreamMachine[2] = GameObject.Find("Vanila").GetComponent<IceCreamMachine>();
    }
    protected override void BeforeDestroy()
    {
        switch(IceCreamFlavor)
        {
            case Flavor.Chocolate:
                iceCreamMachine[0].canPlace = true;
                break;
            case Flavor.Orange:
                iceCreamMachine[1].canPlace = true;
                break;
            case Flavor.Vanila:
                iceCreamMachine[2].canPlace = true;
                break;
        }
    }
    public void setUnitProperty(Flavor coneFlavor,Flavor icecreamFlavor,bool isSprinkle)
    {
        spriteRef = FindObjectOfType<SpriteRefSweetUnit>();
        spIceCream = obj_IceCream.GetComponent<SpriteRenderer>();
        spCone = obj_Cone.GetComponent<SpriteRenderer>();
        this.IceCreamFlavor = icecreamFlavor;
        this.ConeFlavor = coneFlavor;
        spIceCream.sprite = spriteRef.getSpriteByType(GameUnits.IceCream, icecreamFlavor, isSprinkle);
        spCone.sprite = spriteRef.getSpriteByType(GameUnits.Cone, coneFlavor, false);
        if (isSprinkle) gameUnit = GameUnits.ConeAndUceCreamAndSprinkle;
    }
    public void PlaceOnMachine(Vector3 pos)
    {
        transform.position = pos;
        LastPosition = pos;
        switch(IceCreamFlavor)
        {
            case Flavor.Chocolate:
                iceCreamMachine[0].canPlace = true;
                break;
            case Flavor.Orange:
                iceCreamMachine[1].canPlace = true;
                break;
            case Flavor.Vanila:
                iceCreamMachine[2].canPlace = true;
                break;
        }
    }
}
