using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLevelSetting : MonoBehaviour
{
    public GameObject[] level;

    private void Awake()
    {
        for(int i = 0;i<level.Length;i++)
        {
            level[i].SetActive(false);
        }
    }
    private void Start()
    {
        for(int i = 0;i<= PlayerStats.passLevel;i++)
        {
            if(i > level.Length-1)
            {
                break;
            }
            level[i].SetActive(true);
        }
    }
}
