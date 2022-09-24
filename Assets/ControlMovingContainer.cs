using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ControlMovingContainer : MonoBehaviour
{

    [SerializeField] private protected float _leftPadding, _downPadding, _spacing;
    
    private protected Transform _selfTransformComponent;
    private protected HandlerMovingContainer _handlerClickOnElement;

    private protected bool _isOpen;
    [SerializeField] private protected bool _isManualCreateFromAwake;
    [SerializeField] private protected int _numberInRow;
    private protected float localOffset, localPosition;

    public UnityEvent OnMoveFalsePosition;


    private void Awake()
    {
        _selfTransformComponent = transform;
        _handlerClickOnElement = GetComponentInParent<HandlerMovingContainer>();

        if (!_isManualCreateFromAwake) _numberInRow = _handlerClickOnElement.AddControlMovingContainer(this);

        _selfTransformComponent.localPosition = new Vector3(_leftPadding + _numberInRow * (154f + _spacing), _downPadding, 0f);
    }

    public void __MovingContainer()
    {
        if (!_isOpen)
        {
            _handlerClickOnElement.MoveContainersRight(_numberInRow);
            _isOpen = true;
        }
        else
        {
            _handlerClickOnElement.MoveContainersLeft(_numberInRow);
            _isOpen = false;
        }
    }

    public void __MovingContainer(bool toOpen)
    {
        if (toOpen == !_isOpen)
        {
            if (!_isOpen)
            {
                _handlerClickOnElement.MoveContainersRight(_numberInRow);
                _isOpen = true;
            }
            else
            {
                _handlerClickOnElement.MoveContainersLeft(_numberInRow);
                _isOpen = false;
            }
        }
    }

    public void ReturnPosition()
    {
        StopAllCoroutines();

        if (_isOpen) OnMoveFalsePosition.Invoke();

        _isOpen = false;
        localOffset = 0f;
        localPosition = 0f;
        _selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f, 0, 0);
    }

    private void OnDisable()
    {
        StopAllCoroutines();

        _isOpen = false;
        localOffset = 0f;
        localPosition = 0f;
        _selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f, 0, 0);
    }

    public void MoveContainerRight()
    {

            StopAllCoroutines();
            localOffset += 203;

            StartCoroutine(MoveRight());
            //_selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f + localOffset, 0, 0);

    }

    public void MoveContainerLeft()
    {

        StopAllCoroutines();
        localOffset -= 203;

        StartCoroutine(MoveLeft());
        //_selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f + localOffset, 0, 0);

    }

    IEnumerator MoveRight()
    {

        float distance = localOffset - localPosition;

        while (localPosition < localOffset)
        {
            //float deltaTime = Time.deltaTime;
            localPosition = Mathf.MoveTowards(localPosition, localOffset, distance * Time.deltaTime * 2f);


            _selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f + localPosition + 2f, 0, 0);

            yield return null;
        }


        localPosition = localOffset;
        _selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f + localPosition, 0, 0);

        yield return null;
    }

    IEnumerator MoveLeft()
    {

        float distance = localPosition - localOffset;

        while (localPosition > localOffset)
        {
            //float deltaTime = Time.deltaTime;
            localPosition = Mathf.MoveTowards(localPosition, localOffset, distance * Time.deltaTime * 2f);

            _selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f + localPosition + 2f, 0, 0);

            yield return null;
        }


        localPosition = localOffset;
        _selfTransformComponent.localPosition = new Vector3(7f + _numberInRow * 157f + localPosition, 0, 0);

        yield return null;
    }



}
