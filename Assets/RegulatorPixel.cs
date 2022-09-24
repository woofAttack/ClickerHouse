using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegulatorPixel : MonoBehaviour
{
    

    private void Start()
    {

        /*
        Transform[] transformsChildrens = transform.GetComponentsInChildren<Transform>();
        foreach (Transform transformChildren in transformsChildrens)
        {
            Debug.Log(TechSetting.modifScale);
            transformChildren.localScale = new Vector3(TechSetting.modifScale, TechSetting.modifScale, 1f);
        }
        */

        GetComponent<CanvasScaler>().scaleFactor = Mathf.Floor(TechSetting.modifScale);
        //GetComponent<CanvasScaler>().scaleFactor = (float)System.Math.Round(TechSetting.modifScale, 1);
    }



}
