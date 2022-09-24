using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class EventTriggerOnClick : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private protected bool _isOpening;

    [SerializeField] private protected bool _isAvailable;

    [Space(10)]

    public UnityEvent OnClick;
    public bool GetIsOpening { get; }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (_isAvailable && !_isOpening) OnClick.Invoke();
    }

    public void __SetOpening()
    {
        _isOpening = !_isOpening;
    }

    private void OnDisable()
    {
        _isOpening = false;
    }

    public void __MakeAvailableEvent()
    {
        _isAvailable = true;
    }
}
