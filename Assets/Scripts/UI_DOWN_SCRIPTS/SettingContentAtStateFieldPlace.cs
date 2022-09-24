using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class SettingContentAtStateFieldPlace : MonoBehaviour
{
    [SerializeField] private protected Prefab_Part.TypePart _typePartForButton;
    [SerializeField] private protected ScrollRect _scrollRectForContent;

    [Space(10)]

    [SerializeField] private protected RectTransform _contentForEmptyStateButton;
    [SerializeField] private protected RectTransform _contentForBuildingStateButton;
    [SerializeField] private protected RectTransform _contentForWorkingStateButton;

    [Space(10)]

    private protected FieldPlace_Part.StateOfFieldPlacePart _stateOfFieldPlacePart;
    private protected RectTransform _currentContent;

    [Space(10)]

    public UnityEvent onSettingEmptyStateButton;
    public UnityEvent onSettingBuildingStateButton;
    public UnityEvent onSettingWorkingStateButton;
    public UnityEvent OnEnable, OnDisable;

    public FieldPlace_Part.StateOfFieldPlacePart GetStateOfFieldPlacePart { get => _stateOfFieldPlacePart; }


    public bool CheckStateForButtonForEmpty()
    {

        FieldPlace_PartV2.StateOfFieldPlacePart stateOfFieldPlacePart = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)_typePartForButton].GetStateOfFieldPlacePart;

        if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace != FieldPlaceV2.StateOfFieldPlace.Building)
        {
            if (stateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Empty)
            {
                SetToEmptyStateButton();
                return true;
            }
            else
            {
                SetToWorkingStateButton();
            }
        }
        else
        {
            if (stateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Building)
            {
                SetToBuildingStateButton();
            }
            else
            {
                SetDisable();
            }
        }

        return false;

    }

    public void SetEnable()
    {
        OnEnable.Invoke();
    }
    public void SetDisable()
    {
        OnDisable.Invoke();
    }


    public void __OpenContent()
    {
        _scrollRectForContent.content = _currentContent;
        _currentContent.gameObject.SetActive(true);
    }

    public void __CloseContent()
    {
        //_scrollRectForContent.content = null;
        _currentContent.gameObject.SetActive(false);

        if (_stateOfFieldPlacePart == FieldPlace_Part.StateOfFieldPlacePart.Empty) { onSettingEmptyStateButton.Invoke(); }
    }


    private void SetToEmptyStateButton()
    {
        SetEnable();

        if (_currentContent != null) _currentContent.gameObject.SetActive(false);
        _currentContent = _contentForEmptyStateButton;

        _stateOfFieldPlacePart = FieldPlace_Part.StateOfFieldPlacePart.Empty;

        onSettingEmptyStateButton.Invoke();
    }

    private void SetToBuildingStateButton()
    {
        SetEnable();

        //if (_currentContent != null) _currentContent.gameObject.SetActive(false);

        _currentContent.gameObject.SetActive(false);
        _currentContent = _contentForBuildingStateButton;
        //_currentContent.gameObject.SetActive(true);


        _stateOfFieldPlacePart = FieldPlace_Part.StateOfFieldPlacePart.Building;

        onSettingBuildingStateButton.Invoke();
    }

    private void SetToWorkingStateButton()
    {
        SetEnable();

        _currentContent.gameObject.SetActive(false);
        _currentContent = _contentForWorkingStateButton;
        //_currentContent.gameObject.SetActive(true);

        _stateOfFieldPlacePart = FieldPlace_Part.StateOfFieldPlacePart.Working;

        //if (_isCurrentOpenedContent) SetActiveButton();

        onSettingWorkingStateButton.Invoke();

    }


}
