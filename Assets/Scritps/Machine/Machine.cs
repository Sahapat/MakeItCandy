using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Machine : MonoBehaviour
{
    [SerializeField]
    protected int workingTime = 0;
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
    private int timeCount = 0;
    protected Animator anim;
    protected AudioSource audioSource;

    public void Add()
    {
        if (avilableSlot < maxSlot)
        {
            audioSource.PlayOneShot(AddToMachine);
            OnAddProduct();
            isWorking = true;
            avilableSlot++;
        }
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
            if(timeCount == 0)
            {
                OnStartWorking();
            }
            timeCount += 1;
            if(timeCount == workingTime*100)
            {
                avilableSlot -= 1;
                if(avilableSlot == 0)
                {
                    isWorking = !isWorking;
                }
                timeCount = 0;
                OnFinishWorking();
                audioSource.PlayOneShot(SuccessProduct);
            }
        }
    }
    public virtual void OnAddProduct()
    {
        print("Add Sucess");
    }
    protected virtual void OnStartWorking()
    {
        print("Start Working");
    }
    protected abstract void OnFinishWorking();
}
