using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandlerClickOnElement : MonoBehaviour
{
    
    private protected List<ClickOnElement> _listOfClickOnElement;
    private protected Transform _selfTransformComponent;
    private protected RectTransform _selfRectTransformComponent;

    [SerializeField] private protected GameObject _element;
    [SerializeField] private protected int _needCountOfElement;
    [SerializeField] private protected float _sizeOfContentView;


    public delegate void ActionOfElement(ClickOnElement eventClickOnElement);
    public ActionOfElement OnOpen, OnClose;




    private void Awake()
    {

    }



    public void ConstructElementOfClick()
    {
        _listOfClickOnElement = new List<ClickOnElement>();
        _selfTransformComponent = transform;
        _selfRectTransformComponent = GetComponent<RectTransform>();

        _listOfClickOnElement.AddRange(_selfTransformComponent.GetComponentsInChildren<ClickOnElement>());

        for (int i = 0; i < _listOfClickOnElement.Count; i++)
        {
            _listOfClickOnElement[i].InstateOfElement(i, this);
            _listOfClickOnElement[i].OnOpen += OnOpeningElement;
            _listOfClickOnElement[i].OnClose += OnClosingElement;
        }


        _sizeOfContentView = _listOfClickOnElement.Count * 157f + 7f;
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView, 0f);
    }

    private void OnOpeningElement(ClickOnElement eventClickOnElement)
    {
        _sizeOfContentView += 203f;
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView + _selfRectTransformComponent.offsetMin.x, 0f);

        OnOpen.Invoke(eventClickOnElement);
    }

    private void OnClosingElement(ClickOnElement eventClickOnElement)
    {
        _sizeOfContentView -= 203f;
        _selfRectTransformComponent.offsetMax = new Vector2(_sizeOfContentView + _selfRectTransformComponent.offsetMin.x, 0f);

        OnClose.Invoke(eventClickOnElement);
    }


}
