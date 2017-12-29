using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StockPath : MonoBehaviour
{
    public Vector3[] requireBoxPos;
    public GameObject[] requireBox;

    private void Start()
    {
        requireBox = new GameObject[requireBoxPos.Length];
    }
    public bool CheckEmpty(ref int emptypos)
    {
        for(int i =0;i<requireBox.Length;i++)
        {
            if(requireBox[i] == null)
            {
                emptypos = i;
                return true;
            }
        }
        return false;
    }
    public bool CheckSuccess(ref int successPos)
    {
        for (int i = 0; i < requireBox.Length; i++)
        {
            if (requireBox[i] != null)
            {
                RequireBox temp = requireBox[i].GetComponent<RequireBox>();
                if (temp == null)
                {
                    SpecialRequireBox temp2 = requireBox[i].GetComponent<SpecialRequireBox>();
                    if (temp2.isSuccessOrder)
                    {
                        successPos = i;
                        return true;
                    }
                }
                else
                {
                    if (temp.isSuccessOrder)
                    {
                        successPos = i;
                        return true;
                    }
                }
            }
        }
        return false;
    }
}
