using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarLineController : MonoBehaviour
{
    public Vector3 AddPosition;
    public Vector3[] SugarPos = null;
    public GameObject[] SugarOnLine = null;
    public GameObject[] Roter = null;
    private WaitForSeconds seconds = null;

    private void Awake()
    {
        seconds = new WaitForSeconds(0.01f);
        SugarOnLine = new GameObject[4];
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
            Roter[0].transform.Rotate(new Vector3(0, 0, -8));
            Roter[1].transform.Rotate(new Vector3(0, 0, -8));
            Roter[2].transform.Rotate(new Vector3(0, 0, -8));
            Roter[3].transform.Rotate(new Vector3(0, 0, -8));

            if(SugarOnLine[0] != null)
            {
                if (SugarOnLine[0].transform.position.x < SugarPos[0].x)
                {
                    SugarOnLine[0].transform.position += new Vector3(0.12f, 0, 0);
                }
            }
            if (SugarOnLine[1] != null)
            {
                if (SugarOnLine[1].transform.position.x < SugarPos[1].x)
                {
                    SugarOnLine[1].transform.position += new Vector3(0.12f, 0, 0);
                }
            }
            if (SugarOnLine[2] != null)
            {
                if (SugarOnLine[2].transform.position.x < SugarPos[2].x)
                {
                    SugarOnLine[2].transform.position += new Vector3(0.12f, 0, 0);
                }
            }
            if (SugarOnLine[3] != null)
            {
                if (SugarOnLine[3].transform.position.x < SugarPos[3].x)
                {
                    SugarOnLine[3].transform.position += new Vector3(0.12f, 0, 0);
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
