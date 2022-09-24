using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class HandlerClickOfStatePart : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private StateForButtonContainer _stateForButtonContainer;
    [SerializeField] private ControlPushedStateOfButton _controlPushedStateOfButton;

    private protected static HandlerClickOfStatePart _currentHandlerClickOfStatePart;
    private protected bool isActive;

    public bool setActiveButton { get { return isActive; } set { isActive = value; } }

    public UnityEvent PartIsNotWork, PartIsBuilding, PartIsWorking, OnUnpushClick, OnUnpush;




    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (isActive && _controlPushedStateOfButton.isPushed)
        {
            if (_stateForButtonContainer.stateOfFieldPart == FieldPlace_PartV2.StateOfFieldPlacePart.Empty)
            {
                if (_currentHandlerClickOfStatePart == null)
                {
                    _currentHandlerClickOfStatePart = this;
                    PartIsNotWork?.Invoke();
                }
                else
                {
                    if (_currentHandlerClickOfStatePart == this)
                    {
                        Debug.Log("BOOM");
                        UnpushClickFromEmpty();
                        _currentHandlerClickOfStatePart = null;
                    }
                    else
                    {
                        _currentHandlerClickOfStatePart.UnpushFromEmpty();
                        _currentHandlerClickOfStatePart = this;
                        PartIsNotWork?.Invoke();
                    }
                }
            }
            else if (_stateForButtonContainer.stateOfFieldPart == FieldPlace_PartV2.StateOfFieldPlacePart.Building)
            {
                PartIsBuilding?.Invoke();
            }
            else
            {
                PartIsWorking?.Invoke();
            }
        }
    }
  
    public void UnpushFromEmpty()
    {
        if (_stateForButtonContainer.stateOfFieldPart == FieldPlace_PartV2.StateOfFieldPlacePart.Empty)
        {
            OnUnpush?.Invoke();
            if (_currentHandlerClickOfStatePart != null) _currentHandlerClickOfStatePart = null;
        }
    }

    public void UnpushClickFromEmpty()
    {
        OnUnpushClick?.Invoke();
    }
}
