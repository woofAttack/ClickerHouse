using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAnimationOpening : MonoBehaviour
{

    private protected bool _isOpen;
    [SerializeField] private protected Animator _selfAnimatorComponent;

    private void Awake()
    {
        _isOpen = false;

        if (_selfAnimatorComponent == null)
        {
            Debug.LogErrorFormat("{0} of {1} is null!", _selfAnimatorComponent.GetType(), gameObject.name);
        }
    }

    public void __SetAnimation()
    {
        _isOpen = !_isOpen;
        _selfAnimatorComponent.SetBool("Open", _isOpen);
    }

    public void __SetAnimation(bool toOpen)
    {
        _isOpen = toOpen;
        _selfAnimatorComponent.SetBool("Open", toOpen);
    }

    public void __RebindAnimation()
    {
        _selfAnimatorComponent.Rebind();
        _isOpen = false;
    }





}
