using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleOrder : MonoBehaviour
{
    [SerializeField]
    private Vector3[] HandPos;
    [SerializeField]
    private Vector3 DefualtPos;
    public bool avilable = true;
    public bool isRemove = false;
    private Animator anim;
    private GameRequirement requirement;
    private WaitForSeconds seconds;
    private WaitForSeconds cooldown;
    private WaitForSeconds cooldown2;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        requirement = FindObjectOfType<GameRequirement>();
        seconds = new WaitForSeconds(0.01f);
        cooldown = new WaitForSeconds(1.5f);
        cooldown2 = new WaitForSeconds(2f);
    }

    IEnumerator moveDownClap()
    {
        for (int i = 0;i<25;i++)
        {
            transform.position += new Vector3(0, -0.093f, 0);
            yield return seconds;
        }
        anim.SetTrigger("Clap");
        yield return cooldown;
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(0, 0.16f, 0);
            yield return seconds;
        }
        yield return cooldown2;
        ToDefault();
        isRemove = false;
    }
    IEnumerator moveDownAdd()
    {
        anim.SetTrigger("Add");
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(0, -0.097f, 0);
            yield return seconds;
        }
        yield return cooldown;
        for (int i = 0; i < 25; i++)
        {
            transform.position += new Vector3(0, 0.1f, 0);
            yield return seconds;
        }
        ToDefault();
    }
    public void MoveToPos(int index)
    {
        transform.position = HandPos[index];
    }
    public void Add()
    {
        if (!isRemove)
        {
            avilable = false;
            StartCoroutine(moveDownAdd());
        }
    }
    public void Remove()
    {
        avilable = false;
        isRemove = true;
        StartCoroutine(moveDownClap());
    }
    private void ToDefault()
    {
        transform.position = DefualtPos;
        avilable = true;
    }
}
