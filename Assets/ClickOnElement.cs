using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnElement : MonoBehaviour, IPointerClickHandler
{

    private protected bool _isOpen, _isOpening;

    private protected Animator _selfAnimatorComponent;
    private protected Transform _selfTransformComponent;
    [SerializeField] private protected HandlerClickOnElement _handlerClickOnElement;


    private protected float locPos, localDiffence;
    private protected int _numberInList;
    public int GetNumberInList { get => _numberInList; }

    public delegate void ActionOfElement(ClickOnElement eventClickOnElement);
    public ActionOfElement OnOpen, OnClose;

    private void Awake()
    {

    }

    public void InstateOfElement(int number, HandlerClickOnElement hcoe)
    {
        _selfAnimatorComponent = GetComponent<Animator>();
        _selfTransformComponent = transform;
        _handlerClickOnElement = hcoe;

        _handlerClickOnElement.OnOpen += MoveRight;
        _handlerClickOnElement.OnClose += MoveLeft;

        _numberInList = number;
        _selfTransformComponent.localPosition = new Vector3(7f + number * 157f, 0f, 0f);
    }


    public void __SetOpening_True()
    {
        _isOpening = true;
    }

    public void __SetOpening_False()
    {
        _isOpening = false;
    }


    public void MoveRight(ClickOnElement eventClickOnElement)
    {
        if (eventClickOnElement.GetNumberInList < _numberInList)
        {
            StopAllCoroutines();
            localDiffence += 203;

            StartCoroutine(MoveRight());
            //_selfTransformComponent.localPosition = new Vector3(7f + _numberInList * 157f + localDiffence, 0, 0);
        }
    }

    public void MoveLeft(ClickOnElement eventClickOnElement)
    {
        if (eventClickOnElement.GetNumberInList < _numberInList)
        {
            StopAllCoroutines();
            localDiffence -= 203;

            StartCoroutine(MoveLeft());
            //_selfTransformComponent.localPosition = new Vector3(7f + _numberInList * 157f + localDiffence, 0, 0);
        }
    }

    

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        if (!_isOpening)
        {
            _isOpen = !_isOpen;

            _selfAnimatorComponent.SetBool("Open", _isOpen);

            if (_isOpen) OnOpen.Invoke(this);
            else OnClose.Invoke(this);
        }
    }

    IEnumerator MoveRight()
    {
        
        float distance = localDiffence - locPos;

        while (locPos < localDiffence)
        {
            //float deltaTime = Time.deltaTime;
            locPos = Mathf.MoveTowards(locPos, localDiffence, distance * Time.deltaTime * 2f);


            _selfTransformComponent.localPosition = new Vector3(7f + _numberInList * 157f + locPos + 2f , 0, 0);

            yield return null;
        }


        locPos = localDiffence;
        _selfTransformComponent.localPosition = new Vector3(7f + _numberInList * 157f + locPos, 0, 0);

        yield return null;
    }


    IEnumerator MoveLeft()
    {

        float distance = locPos - localDiffence;

        while (locPos > localDiffence)
        {
            //float deltaTime = Time.deltaTime;
            locPos = Mathf.MoveTowards(locPos, localDiffence, distance * Time.deltaTime * 2f);

            _selfTransformComponent.localPosition = new Vector3(7f + _numberInList * 157f + locPos + 2f , 0, 0);

            yield return null;
        }


        locPos = localDiffence;
        _selfTransformComponent.localPosition = new Vector3(7f + _numberInList * 157f + locPos, 0, 0);

        yield return null;
    }

}
