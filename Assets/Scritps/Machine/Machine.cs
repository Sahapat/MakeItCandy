using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    [SerializeField]
    protected float workingTime = 0;
    [SerializeField]
    protected int maxSlot = 0;
    [SerializeField]
    private AudioClip AddToMachine;
    [SerializeField]
    private AudioClip SuccessProduct;

    public int avilableSlot = 0;
    private bool _isWorking = false;
    public bool isWorking
    {
        get
        {
            return _isWorking;
        }
        protected set
        {
            _isWorking = value;
            anim.SetBool("isWorking", value);
        }
    }
    protected bool isSuccess = false;
    [SerializeField]
    private int timeCount = 0;
    protected Animator anim;
    protected AudioSource audioSource;

    public bool Add()
    {
        if (avilableSlot < maxSlot)
        {
            audioSource.PlayOneShot(AddToMachine);
            OnAddProduct();
            isWorking = true;
            avilableSlot++;
            return true;
        }
        else return false;
    }
    private void Awake()
    {
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }
    private void FixedUpdate()
    {
        if(isWorking)
        {
            timeCount = (timeCount > workingTime*100)?timeCount:timeCount+1;
            if(timeCount >= workingTime * 100)
            {
                if(OnFinishWorking())
                {
                    avilableSlot -= 1;
                    if(avilableSlot == 0)
                    {
                        isWorking = !isWorking;
                    }
                    timeCount = 0;
                    audioSource.PlayOneShot(SuccessProduct);
                }
            }
        }
    }
    public virtual void OnAddProduct()
    {
        print("Add Sucess");
    }
    protected abstract bool OnFinishWorking();
}
