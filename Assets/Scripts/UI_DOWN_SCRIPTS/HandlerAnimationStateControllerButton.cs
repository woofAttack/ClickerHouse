using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerAnimationStateControllerButton : MonoBehaviour
{

    private protected ToolButtonsUIDown[] _toolButtonsUIDowns;
    private protected AnimationStateControllerButton[] _animationStateControllerButton;

    private protected int _countButtons;

    private void Awake()
    {
        _toolButtonsUIDowns = GetComponentsInChildren<ToolButtonsUIDown>();
        _animationStateControllerButton = GetComponentsInChildren<AnimationStateControllerButton>();

        _countButtons = _toolButtonsUIDowns.Length;

        string message = string.Format("Loaded {0} ToolButtonsUIDown and {1} AnimationStateControllerButton", _toolButtonsUIDowns.Length, _animationStateControllerButton.Length);
        Debug.Log(message);
    }

    public void CheckStateOfButtons()
    {
        for (int i = 0; i < _countButtons; i++)
        {
            _toolButtonsUIDowns[i].CheckStateForButton();
        }

        for (int i = 0; i < _countButtons; i++)
        {
            if (_toolButtonsUIDowns[i].GetStateOfFieldPlacePart == FieldPlace_Part.StateOfFieldPlacePart.Empty)
            {
                _animationStateControllerButton[i].__SetAnimation(AnimationStateControllerButton.StateOfAnimation.Flashing);
                for (int j = i + 1; j < _countButtons; j++)
                {
                    _animationStateControllerButton[i].__SetAnimation(AnimationStateControllerButton.StateOfAnimation.UnActive);                  
                }
                break;
            }
            else
            {
                _animationStateControllerButton[i].__SetAnimation(AnimationStateControllerButton.StateOfAnimation.Simple);
            }
        }

        //Debug.Log("If u see it message - so wrong code");
    }






}
