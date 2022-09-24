using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandlerSettingContentAtStateFieldPlace : MonoBehaviour
{

    private protected SettingContentAtStateFieldPlace[] _settingsContentAtStateFieldPlace;
    private protected int _countButtonWithComponent;

    public UnityEvent onCheck;

    private void Awake()
    {
        _settingsContentAtStateFieldPlace = GetComponentsInChildren<SettingContentAtStateFieldPlace>();
        _countButtonWithComponent = _settingsContentAtStateFieldPlace.Length;
    }

    public void __SetContentAtStateFieldPlaceForButton()
    {
        for (int i = 0; i < _countButtonWithComponent; i++)
        {
            if (_settingsContentAtStateFieldPlace[i].CheckStateForButtonForEmpty())
            {
                for (int j = i + 1; j < _countButtonWithComponent; j++)
                {
                    _settingsContentAtStateFieldPlace[j].SetDisable();
                }

                break;
            }
        }

        onCheck.Invoke();
    }

}
