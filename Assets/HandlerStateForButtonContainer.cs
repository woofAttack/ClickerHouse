using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerStateForButtonContainer : MonoBehaviour
{
    [SerializeField] private protected List<StateForButtonContainer> _listStateForButtonContainer;

    private protected void Awake()
    {
        if (_listStateForButtonContainer == null)
        {
            Debug.LogWarningFormat("{0} in {1} (HandlerStateForButtonContainer) not assigned", _listStateForButtonContainer.ToString(), gameObject.name);
        }
    }

    public void SetNewButtonState()
    {
        for (int i = 0, imax = _listStateForButtonContainer.Count; i < imax; i++)
        {
            _listStateForButtonContainer[i].SetCurrentFieldPlacePartForButtonState(i);
        }

        CheckActiveStatesOfFieldPlaceForButtonContainer();
    }
    public void CheckActiveStatesOfFieldPlaceForButtonContainer()
    {
        for (int i = 0, imax = _listStateForButtonContainer.Count; i < imax; i++)
        {
            if (_listStateForButtonContainer[i].stateOfFieldPart == FieldPlace_PartV2.StateOfFieldPlacePart.Empty)
            {
                SetEmptyStateForButtonContainer(i);
                break;
            } 
            else if (_listStateForButtonContainer[i].stateOfFieldPart == FieldPlace_PartV2.StateOfFieldPlacePart.Building)
            {
                SetBuildingStateForButtonContainer(i);
                break;
            }

            _listStateForButtonContainer[i].SetState(true);
        }
    }

    public void SetBuildingStateForButtonContainer(int fromNumber)
    {
        for (int i = 0, imax = _listStateForButtonContainer.Count; i < imax; i++)
        {
            _listStateForButtonContainer[i].SetState(i == fromNumber);
        }
    }
    public void SetEmptyStateForButtonContainer(int fromNumber)
    {
        for (int i = 0, imax = _listStateForButtonContainer.Count; i < imax; i++)
        {
            _listStateForButtonContainer[i].SetState(i <= fromNumber);
        }
    }
}



