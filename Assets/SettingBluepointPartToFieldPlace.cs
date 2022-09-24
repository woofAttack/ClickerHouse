using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingBluepointPartToFieldPlace : MonoBehaviour
{

    private protected BluePoint_Part _linkBluePointPart;

    public void SetBluepointForChoose(BluePoint_Part bluePointPart)
    {
        _linkBluePointPart = bluePointPart;
    }

    public void SelectCurrentPartForBuild()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.SetPartForBuilding(_linkBluePointPart);
    }

}
