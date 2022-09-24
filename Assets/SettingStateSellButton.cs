using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SettingStateSellButton : MonoBehaviour
{

    public UnityEvent OnEnableForSell, OnDisableForSell;

    public void CheckStateForSellButton()
    {

        if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace == FieldPlaceV2.StateOfFieldPlace.Working)
        {
            OnEnableForSell.Invoke();
        }
        else
        {
            OnDisableForSell.Invoke();
        }
     

    }



}
