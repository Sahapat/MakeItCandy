using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoppopStateSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject selector;
    [SerializeField]
    private GameObject Orange;
    [SerializeField]
    private GameObject Grape;
    [SerializeField]
    private GameObject PineApple;
    [SerializeField]
    private GameObject Stawberry;

    public float unselectScale = 1;
    public float selectScale = 1.3f;

    private PopPopMachine popPopMachine = null;
    private Animator anim = null;
    private BoxCollider boxCollider = null;
    private GamePause pause;

    public Vector2 selectFlavorPos;
    private bool canDrag;
    private bool isShow;
    private sbyte selected;

    private Vector3 selectSize;
    private Vector3 unselecSize;

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        anim = selector.GetComponent<Animator>();
        popPopMachine = FindObjectOfType<PopPopMachine>();
        pause = FindObjectOfType<GamePause>();
        canDrag = true;
    }
    private void OnMouseDown()
    {
        if (!pause.isPause&&!popPopMachine.isWorking)
        {
            isShow = true;
            canDrag = true;
            anim.SetBool("isShow", isShow);
        }
    }
    private void OnMouseDrag()
    {
        if (!pause.isPause&&!popPopMachine.isWorking)
        {
            if (canDrag)
            {
                Vector2 centerPos = boxCollider.bounds.center;
                Vector2 selectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                selectFlavorPos = (selectPos - centerPos).normalized;
                selectSize = new Vector3(selectScale, selectScale, selectScale);
                unselecSize = new Vector3(unselectScale, unselectScale, unselectScale);
                
                if (selectFlavorPos.x < 0.4f && selectFlavorPos.x > -0.4f && selectFlavorPos.y > 0)
                {
                    Orange.transform.localScale = unselecSize;
                    Grape.transform.localScale = unselecSize;
                    PineApple.transform.localScale = selectSize;
                    Stawberry.transform.localScale = unselecSize;
                    selected = 1;
                }
                else if (selectFlavorPos.x < -0.4f && selectFlavorPos.x > -0.8f)
                {
                    Orange.transform.localScale = unselecSize;
                    Grape.transform.localScale = unselecSize;
                    PineApple.transform.localScale = unselecSize;
                    Stawberry.transform.localScale = selectSize;
                    selected = 2;
                }
                else if (selectFlavorPos.x < 0.8f && selectFlavorPos.x > -0.8f && selectFlavorPos.y < 0)
                {
                    Orange.transform.localScale = unselecSize;
                    Grape.transform.localScale = selectSize;
                    PineApple.transform.localScale = unselecSize;
                    Stawberry.transform.localScale = unselecSize;
                    selected = 3;
                }
                else if (selectFlavorPos.x < 0.8f && selectFlavorPos.x > 0.4f)
                {
                    Orange.transform.localScale = selectSize;
                    Grape.transform.localScale = unselecSize;
                    PineApple.transform.localScale = unselecSize;
                    Stawberry.transform.localScale = unselecSize;
                    selected = 0;
                }
            }
        }
    }
    private void OnMouseUp()
    {
        if (selected != -1)
        {
            switch(selected)
            {
                case 0:
                    popPopMachine.flavorState = sugarFlavor.Orange;
                    break;
                case 1:
                    popPopMachine.flavorState = sugarFlavor.PineApple;
                    break;
                case 2:
                    popPopMachine.flavorState = sugarFlavor.Stawberry;
                    break;
                case 3:
                    popPopMachine.flavorState = sugarFlavor.Grape;
                    break;
            }
        }
        Reset();
    }
    private void Reset()
    {
        selectFlavorPos = Vector2.zero;
        canDrag = false;
        isShow = false;
        anim.SetBool("isShow", isShow);
    }
}
