using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ToolButtonsUIDown : MonoBehaviour
{
    [SerializeField] private protected Prefab_Part.TypePart typePartForButton;
    private protected FieldPlace_Part.StateOfFieldPlacePart _stateOfFieldPlacePart;
    public FieldPlace_Part.StateOfFieldPlacePart GetStateOfFieldPlacePart { get => _stateOfFieldPlacePart; }

    [SerializeField] private protected ScrollRect scrollRectForContent;

    [Space(10)]

    [SerializeField] private protected GameObject _contentForEmptyStateButton;
    [SerializeField] private protected GameObject _contentForBuildingStateButton;
    [SerializeField] private protected GameObject _contentForWorkingStateButton;
    private protected GameObject _currentContent;

    [Space(30)]

    public UnityEvent onSettingEmptyStateButton;
    public UnityEvent onSettingBuildingStateButton;
    public UnityEvent onSettingWorkingStateButton;

    [Space(30)]

    public UnityEvent onClickOnButtonToOpenContent;
    public UnityEvent onClickOnButtonToCloseContent;

    public UnityEvent onClickedThisButton;
    public UnityEvent onClickedAnotherButton;

    [Space(100)]

    public UnityEvent onClickToClose;

    public UnityEvent onClickToExit;

    public UnityEvent onUnPushed;

    private static ToolButtonsUIDown _activeButton;
    private bool _isCurrentOpenedContent;
    private bool isSelectedPart;

    public void CheckStateForButton()
    {

        FieldPlace_PartV2.StateOfFieldPlacePart stateOfFieldPlacePart = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)typePartForButton].GetStateOfFieldPlacePart;

        if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace != FieldPlaceV2.StateOfFieldPlace.Building)
        {
            if (stateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Empty)
            {
                SetToEmptyStateButton();
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
                OffButton();
            }
        }

    }

    public void OffButton()
    {
        gameObject.SetActive(false);
    }

    public void SetToEmptyStateButton()
    {
        gameObject.SetActive(true);

        if (_currentContent != null) _currentContent.SetActive(false);
        _currentContent = _contentForEmptyStateButton;

        _stateOfFieldPlacePart = FieldPlace_Part.StateOfFieldPlacePart.Empty;

        onSettingEmptyStateButton.Invoke();
    }

    public void SetToBuildingStateButton()
    {
        gameObject.SetActive(true);

        if (_currentContent != null) _currentContent.SetActive(false);
        _currentContent = _contentForBuildingStateButton;

        _stateOfFieldPlacePart = FieldPlace_Part.StateOfFieldPlacePart.Building;

        onSettingBuildingStateButton.Invoke();
    }

    public void SetToWorkingStateButton()
    {
        gameObject.SetActive(true);

        if (_currentContent != null) _currentContent.SetActive(false);
        _currentContent = _contentForWorkingStateButton;
        _stateOfFieldPlacePart = FieldPlace_Part.StateOfFieldPlacePart.Working;

        if (_isCurrentOpenedContent) SetActiveButton();

        onSettingWorkingStateButton.Invoke();

    }

    public void SetActiveButton()
    {
        scrollRectForContent.content = _currentContent.GetComponent<RectTransform>();

        _currentContent.SetActive(true);
        _isCurrentOpenedContent = true;
        onClickedThisButton.Invoke();
    }

    public void SetUnactiveButton()
    {
        _currentContent.SetActive(false);
        _isCurrentOpenedContent = false;
        onClickedAnotherButton.Invoke();
    }

    private void SetActiveContent(bool isActive) => _currentContent.SetActive(isActive);
    private void SetCurrentOpenedContent(bool isOpened) => _isCurrentOpenedContent = isOpened;

    public void ProcessingButtonState()
    {

        if (_isCurrentOpenedContent)
        {
            if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace != FieldPlaceV2.StateOfFieldPlace.Building) onClickOnButtonToCloseContent.Invoke();
        }
        else
        {
            onClickOnButtonToOpenContent.Invoke();
        }

    }


    public void OnUnPushed()
    {
        onUnPushed.Invoke();
    }

    public void OnPushed()
    {
        _activeButton = this;
        _isCurrentOpenedContent = true;
    }


    public void HaveCurrentBlock()
    {
        isSelectedPart = FieldPlace.currentZoomedFieldPlace.isHaveSelfBlock;
    }

    public void HaveCurrentRoof()
    {
        isSelectedPart = FieldPlace.currentZoomedFieldPlace.isHaveSelfRoof;
    }

    public void HaveCurrentField()
    {
        isSelectedPart = FieldPlace.currentZoomedFieldPlace.isHaveSelfField;
    }


    public void OnClicked()
    {
        if (!ToolUIDown.isOpenedUp)
        {
            onClickOnButtonToOpenContent.Invoke();
        }
    }

    public void OnClickButton()
    {
        if (!isSelectedPart)
        {
            if (!ToolUIDown.isOpenedUp)
            {
                onClickOnButtonToOpenContent.Invoke();
            }
            else
            {
                if (_isCurrentOpenedContent)
                {
                    if (FieldPlace.currentZoomedFieldPlace.GetStateOfFieldPlace != FieldPlace.StateOfFieldPlace.Building) onClickToClose.Invoke();
                }
                else
                {
                    _activeButton.OnUnPushed();

                    onClickOnButtonToOpenContent.Invoke();
                }
            }
        }
        

    }

    public void OnClickExit()
    {
        if (!HandlerFieldPlace.IsZooming) onClickToExit.Invoke();
    }

    public void SetEnableSellButton()
    {
        if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace == FieldPlaceV2.StateOfFieldPlace.Working) gameObject.SetActive(true);
    }
    public void OnClickSell()
    {
        if (!HandlerFieldPlace.IsZooming) HandlerFieldPlace.GetCurrentZoomedFieldPlace.SellHouse();
    }


    /*
    public void SetCurrentBlock()
    {
        FieldPlace.currentZoomedFieldPlace.currentPart1 = FieldPlace.currentZoomedFieldPlace.selfBlock;
        FieldPlace.currentZoomedFieldPlace.CurrentSpriteRendererOfPart = FieldPlace.currentZoomedFieldPlace.Block;
    }

    public void SetCurrentRoof()
    {
        FieldPlace.currentZoomedFieldPlace.CurrentSpriteRendererOfPart = FieldPlace.currentZoomedFieldPlace.Roof;
    }

    public void SetCurrentField()
    {
        FieldPlace.currentZoomedFieldPlace.CurrentSpriteRendererOfPart = FieldPlace.currentZoomedFieldPlace.Field;
    }
    */
}
