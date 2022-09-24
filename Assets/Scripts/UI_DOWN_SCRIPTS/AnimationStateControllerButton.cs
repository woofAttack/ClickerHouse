using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateControllerButton : MonoBehaviour
{
    public enum StateOfAnimation
    {
        UnActive,
        Simple,
        Flashing,
        Active
    }

    [SerializeField] private protected Animator _animatorOfButton;
    private protected ToolButtonsUIDown _toolButtons;



    public void __SetAnimation(int stateOfAnimation)
    {
        _animatorOfButton.SetInteger("State", stateOfAnimation);
    }

    public void __SetAnimationIsOpen(bool stateOfAnimation)
    {
        _animatorOfButton.SetBool("isOpen", stateOfAnimation);
    }

    public void __SetAnimation(StateOfAnimation stateOfAnimation)
    {
        _animatorOfButton.SetInteger("State", (int)stateOfAnimation);
    }


}
