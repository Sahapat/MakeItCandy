using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMachine_Destory : MonoBehaviour
{
    public GameObject obj_Destory = null;

    private void Update()
    {
        if(obj_Destory != null)
        {
            RequireBox require = obj_Destory.GetComponent<RequireBox>();
            if(require == null)
            {
                SpecialRequireBox spRequire = obj_Destory.GetComponent<SpecialRequireBox>();
                spRequire.BeforeDestroy();
            }
            else
            {
                require.BeforeDestory();
            }
            Destroy(obj_Destory);
        }
    }
}
