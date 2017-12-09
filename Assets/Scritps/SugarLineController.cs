using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarLineController : MonoBehaviour
{
    public int sugarLenght = 4;
    public Vector3 AddPosition;
    public Vector3[] SugarPos = null;
    public GameObject[] SugarOnLine = null;
    public GameObject[] Roter = null;
    private WaitForSeconds seconds = null;

    private void Awake()
    {
        seconds = new WaitForSeconds(0.01f);
        SugarOnLine = new GameObject[sugarLenght];
    }
    public void CheckLineEmpty()
    {
        int emptyPos = -1;
        for(int i =0;i<SugarOnLine.Length;i++)
        {
            if(SugarOnLine[i] == null)
            {
                emptyPos = i;
                break;
            }
        }
        if(emptyPos != -1)
        {
            SwapSugar(emptyPos);
        }
    }
    public IEnumerator MoveSugar()
    {
        for(int i =0;i<25;i++)
        {
            for(int j =0;j<SugarOnLine.Length;j++)
            {
                Roter[j].transform.Rotate(new Vector3(0, 0, -8));
                if (SugarOnLine[j] != null)
                {
                    if (SugarOnLine[j].transform.position.x < SugarPos[j].x)
                    {
                        SugarOnLine[j].transform.position += new Vector3(0.12f, 0, 0);
                    }
                }
            }
            yield return seconds;
        }
        for (int i = 0; i < 4; i++)
        {
            if (SugarOnLine[i] != null)
            {
                Sugar sugar = SugarOnLine[i].GetComponent<Sugar>();
                sugar.LastPosition = SugarPos[i];
            }
        }
    }
    private void SwapSugar(int emptyPos)
    {
        for(int i = emptyPos;i<3;i++)
        {
            GameObject temp = SugarOnLine[i];
            SugarOnLine[i] = SugarOnLine[i + 1];
            SugarOnLine[i + 1] = temp;
        }
    }
    private void Update()
    {
        CheckLineEmpty();
    }
}
