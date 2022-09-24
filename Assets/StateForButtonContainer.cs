using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class StateForButtonContainer : MonoBehaviour
{
    [SerializeField] private protected Prefab_Part.TypePart _typePart;

    private protected FieldPlace_PartV2 _currentFieldPlacePartForButtonState;
    private protected FieldPlace_PartV2.StateOfFieldPlacePart _stateOfFieldPart;
    public FieldPlace_PartV2.StateOfFieldPlacePart stateOfFieldPart { get => _stateOfFieldPart; }

    public UnityEvent OnSetEmptyState, OnSetBuildState, OnSetWorkingState, OnSetActiveState, OnSetUnactiveState;


    public void SetCurrentFieldPlacePartForButtonState()
    {
        _currentFieldPlacePartForButtonState = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)_typePart];

        CheckStateOfFieldPlacePart();
    }
    public void SetCurrentFieldPlacePartForButtonState(Prefab_Part.TypePart typePart)
    {
        _currentFieldPlacePartForButtonState = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)typePart];
        _typePart = typePart;

        CheckStateOfFieldPlacePart();
    }
    public void SetCurrentFieldPlacePartForButtonState(int typePart)
    {
        _currentFieldPlacePartForButtonState = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[typePart];
        _typePart = (Prefab_Part.TypePart)typePart;

        CheckStateOfFieldPlacePart();
    }


    public void CheckStateOfFieldPlacePart()
    {
        _stateOfFieldPart = _currentFieldPlacePartForButtonState.GetStateOfFieldPlacePart;

        switch (_stateOfFieldPart)
        {
            case FieldPlace_PartV2.StateOfFieldPlacePart.Empty: SetEmptyState(); break;
            case FieldPlace_PartV2.StateOfFieldPlacePart.Building: SetBuildState(); break;
            case FieldPlace_PartV2.StateOfFieldPlacePart.Working: SetWorkingState(); break;
        }
    }

    public void SetEmptyState()
    {
        _stateOfFieldPart = FieldPlace_PartV2.StateOfFieldPlacePart.Empty;
        OnSetEmptyState?.Invoke();
    }
    public void SetWorkingState()
    {
        _stateOfFieldPart = FieldPlace_PartV2.StateOfFieldPlacePart.Working;
        OnSetWorkingState?.Invoke();
    }
    public void SetBuildState()
    {
        _stateOfFieldPart = FieldPlace_PartV2.StateOfFieldPlacePart.Building;
        OnSetBuildState?.Invoke();
    }


    public void SetState(bool isActive)
    {
        if (isActive) OnSetActiveState?.Invoke();
        else
        {
            OnSetUnactiveState?.Invoke();
        }
    }

}
