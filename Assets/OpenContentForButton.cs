using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OpenContentForButton : MonoBehaviour
{

    [SerializeField] private protected ScrollRect _scrollRectForContent;
    [SerializeField] private protected RectTransform _contentForView;

    public UnityEvent OnCloseContent;

    public void __OpenContent()
    {
        _scrollRectForContent.content = _contentForView;
        _contentForView.GetComponent<HandlerMovingContainer>().MoveTruePosition();

        //_contentForView.gameObject.SetActive(true);
    }

    public void __CloseContent()
    {

        _contentForView.GetComponent<HandlerMovingContainer>().MoveFalsePosition();

        //_contentForView.gameObject.SetActive(false);
        OnCloseContent.Invoke();
    }

}
