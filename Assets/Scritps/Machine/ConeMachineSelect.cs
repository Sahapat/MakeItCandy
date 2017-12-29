using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConeMachineSelect : MonoBehaviour
{
    [SerializeField]
    private GameObject coneChocChoc;
    [SerializeField]
    private GameObject coneVaniVani;
    [SerializeField]
    private GameObject coneOrOr;

    public float unselectScale = 0.45f;
    public float selectScale = 0.7f;
    private ConeMachine coneMachine = null;
    private GamePause pause;
    private Animator anim = null;
    public BoxCollider boxCollider;

    private Vector2 selectConePos;
    private bool canDrag;
    private bool isExitColider;
    private bool isShow;
    private sbyte selected;

    private Vector3 selectSize;
    private Vector3 unselecSize;

    private void Awake()
    {
        coneMachine = FindObjectOfType<ConeMachine>();
        pause = FindObjectOfType<GamePause>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider>();
        canDrag = true;
        isExitColider = false;
    }
    private void Update()
    {
        if(coneMachine.spawnObject == null && !coneMachine.isWorking)
        {
            boxCollider.enabled = true;
        }
        else
        {
            boxCollider.enabled = false;
        }
    }
    private void OnMouseDown()
    {
        if (!pause.isPause)
        {
            isShow = true;
            isExitColider = false;
            canDrag = true;
            anim.SetBool("isShow", isShow);
        }
    }
    private void OnMouseDrag()
    {
        if (!pause.isPause)
        {
            if(canDrag)
            {
                Vector2 centerPos = boxCollider.bounds.center;
                Vector2 selectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                selectConePos = (selectPos - centerPos).normalized;

                if (selectConePos.y < 0 && isExitColider)
                {
                    Reset();
                    selected = -1;
                }
                else if(selectConePos.y >= 0 )
                {
                    selectSize = new Vector3(selectScale, selectScale, selectScale);
                    unselecSize = new Vector3(unselectScale, unselectScale, unselectScale);
                    if (selectConePos.x >= -1 && selectConePos.x < -0.3f)
                    {
                        selected = 1;
                        coneChocChoc.transform.localScale = unselecSize;
                        coneVaniVani.transform.localScale = selectSize;
                        coneOrOr.transform.localScale = unselecSize;
                    }
                    else if (selectConePos.x >=-0.3f && selectConePos.x <= 0.3f )
                    {
                        selected = 0;
                        coneChocChoc.transform.localScale = selectSize;
                        coneVaniVani.transform.localScale = unselecSize;
                        coneOrOr.transform.localScale = unselecSize;
                    }
                    else if (selectConePos.x <= 1 && selectConePos.x > 0.3f)
                    {
                        selected = 2;
                        coneChocChoc.transform.localScale = unselecSize;
                        coneVaniVani.transform.localScale = unselecSize;
                        coneOrOr.transform.localScale = selectSize;
                    }
                }
            }
        }
    }
    private void OnMouseUp()
    {
        if (selected != -1)
        {
            coneMachine.ConeType = selected;
            coneMachine.Add();
        }
        Reset();
    }
    private void OnMouseExit()
    {
        isExitColider = true;
        selected = -1;
    }
    private void Reset()
    {
        selectConePos = Vector2.zero;
        canDrag = false;
        isExitColider = false;
        isShow = false;
        anim.SetBool("isShow", isShow);
    }
}
