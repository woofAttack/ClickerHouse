using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HandlerFieldPlace : MonoBehaviour
{
    private protected static bool isZoomed = false, isZooming = false;

    [SerializeField] private protected float _powerOfZoom;
    [SerializeField] private protected float _speedZoom;
    [SerializeField] private protected float _inaccuracyForScale;


    private protected static FieldPlaceV2 _currentZoomedFieldPlace;
    private protected float _posX, _posY, _posZoomX, _posZoomY;
    private protected Coroutine _coroutineZoom, _coroutineUnzoom;
    private protected Coroutine _coroutineMoveUp, _coroutineMoveDown;


    public static FieldPlaceV2 GetCurrentZoomedFieldPlace { get => _currentZoomedFieldPlace; }
    public static bool IsZooming { get => isZooming; }


    public UnityEvent onZoom, onUnZoom;
    public UnityEvent onStartBuild, onBuildPoint, onEndBuild;
    public UnityEvent onClickCoin, onSellCoin;


    private void Awake()
    {
        _posX = transform.localPosition.x;
        _posY = transform.localPosition.y;
    }


    public void HandlerClick(FieldPlaceV2 fieldPlace)
    {
        if (!isZooming)
        {
            if (isZoomed)
            {
                if (_currentZoomedFieldPlace == fieldPlace)
                {
                    if ( _currentZoomedFieldPlace.GetStateOfFieldPlace == FieldPlaceV2.StateOfFieldPlace.Building)
                    {
                        _currentZoomedFieldPlace.BuildPoint();
                        onBuildPoint.Invoke();
                    }
                    else if (_currentZoomedFieldPlace.GetStateOfFieldPlace == FieldPlaceV2.StateOfFieldPlace.Working)
                    {
                        _currentZoomedFieldPlace.ClickCoin();
                        onClickCoin.Invoke();
                    }
                }
            }
            else
            {
                _currentZoomedFieldPlace = fieldPlace;
                ZoomFieldOfHouses(fieldPlace.GetPositionX * _powerOfZoom, fieldPlace.GetPositionY * _powerOfZoom);
            }
        }
    }


    public void MoveUpFieldPlace()
    {
        _coroutineMoveUp = StartCoroutine(MoveUp1(46f));
    }

    public void MoveDownFieldPlace()
    {
        _coroutineMoveDown = StartCoroutine(MoveDown1(0f));
    }

    public void ZoomFieldOfHouses(float toPositionX, float toPositionY)
    {
        _posZoomX = -(toPositionX - _posX);
        _posZoomY = -(toPositionY - _posY);

        _coroutineZoom = StartCoroutine(ZoomOnField1(_posZoomX, _posZoomY));
        onZoom.Invoke();
    }

    public void UnZoomFieldOfHouses()
    {
        _coroutineUnzoom = StartCoroutine(UnZoomOnField1());
        onUnZoom.Invoke();
    }


    /*IEnumerator ZoomOnField(float toPositionX, float toPositionY)
    {
        if (_coroutineUnzoom != null) StopCoroutine(_coroutineUnzoom);

        isZooming = true;

        while (transform.localScale.x < _powerOfZoom)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;

            if (lScale.x >= _powerOfZoom - _inaccuracyForScale)
            {
                transform.localPosition = new Vector3(toPositionX, toPositionY);
                transform.localScale = new Vector3(_powerOfZoom, _powerOfZoom, 1f);
                isZoomed = true;
            }
            else
            {
                float deltaTime = Time.deltaTime * _speedZoom;
                transform.localPosition = new Vector3(Mathf.Lerp(lPosition.x, toPositionX, deltaTime), Mathf.Lerp(lPosition.y, toPositionY, deltaTime));
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, _powerOfZoom, deltaTime), Mathf.Lerp(transform.localScale.y, _powerOfZoom, deltaTime), 1f);
            }

            yield return null;
        }

        isZooming = false;
        yield return null;
    }
    */

    /*IEnumerator UnZoomOnField()
    {
        if (_coroutineZoom != null) StopCoroutine(_coroutineZoom);

        isZooming = true;

        while (transform.localScale.x > 1f)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;


            if (lScale.x <= 1f + _inaccuracyForScale)
            {
                transform.localPosition = new Vector3(_posX, _posY);
                transform.localScale = new Vector3(1f, 1f, 1f);
                isZoomed = false;
            }
            else
            {
                float deltaTime = Time.deltaTime * _speedZoom;
                transform.localPosition = new Vector3(Mathf.Lerp(lPosition.x, _posX, deltaTime), Mathf.Lerp(lPosition.y, _posY, deltaTime));
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1f, deltaTime), Mathf.Lerp(transform.localScale.y, 1f, deltaTime), 1f);
            }

            yield return null;
        }

        isZooming = false;
        yield return null;
    }
    */

    IEnumerator ZoomOnField(float toPositionX, float toPositionY)
    {
        if (_coroutineUnzoom != null) StopCoroutine(_coroutineUnzoom);

        isZooming = true;

        float deltaPowerOfZoom = _powerOfZoom * 1.01f;
        float deltaPosX = toPositionX * 1.01f;
        float deltaPosY = toPositionY * 1.01f;

        while (transform.localScale.x < _powerOfZoom)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;

            float deltaTime = Time.deltaTime * _speedZoom;

            transform.localPosition = new Vector3(Mathf.Lerp(lPosition.x, deltaPosX, deltaTime), Mathf.Lerp(lPosition.y, deltaPosY, deltaTime));
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, deltaPowerOfZoom, deltaTime), Mathf.Lerp(transform.localScale.y, deltaPowerOfZoom, deltaTime), 1f);

            yield return null;
        }

        Debug.Log("ZoomOnField end");

        transform.localPosition = new Vector3(toPositionX, toPositionY);
        transform.localScale = new Vector3(_powerOfZoom, _powerOfZoom, 1f);

        isZoomed = true;
        isZooming = false;

        yield return null;
    }

    IEnumerator UnZoomOnField()
    {
        if (_coroutineZoom != null) StopCoroutine(_coroutineZoom);

        isZooming = true;

        float deltaPowerOfZoom = 0.99f;

        while (transform.localScale.x > 1f)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;

            float deltaTime = Time.deltaTime * _speedZoom;
            transform.localPosition = new Vector3(Mathf.Lerp(lPosition.x, _posX, deltaTime), Mathf.Lerp(lPosition.y, _posY, deltaTime));
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, deltaPowerOfZoom, deltaTime), Mathf.Lerp(transform.localScale.y, deltaPowerOfZoom, deltaTime), 1f);

            yield return null;
        }

        Debug.Log("UnZoomOnField end");

        transform.localPosition = new Vector3(0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 1f);

        isZoomed = false;
        isZooming = false;

        yield return null;
    }

    IEnumerator MoveUp(float moveDistanse)
    {
        if (_coroutineMoveDown != null) StopCoroutine(_coroutineMoveDown);

        float posY = _posZoomY + moveDistanse;
        float deltaPosY = _posZoomY + (moveDistanse * 1.1f);

        while (transform.localPosition.y < posY)
        {
            Vector3 lPosition = transform.localPosition;

            float deltaTime = Time.deltaTime * _speedZoom;
            transform.localPosition = new Vector3(lPosition.x, Mathf.Lerp(lPosition.y, deltaPosY, deltaTime));

            yield return null;
        }

        Debug.Log("MoveUp end");

        transform.localPosition = new Vector3(transform.localPosition.x, posY);
        yield return null;

    }

    IEnumerator MoveDown()
    {
        if (_coroutineMoveUp != null) StopCoroutine(_coroutineMoveUp);

        float posY = _posZoomY;
        float deltaPosY = _posZoomY - 7f;

        while (transform.localPosition.y > posY)
        {
            Vector3 lPosition = transform.localPosition;

            float deltaTime = Time.deltaTime * _speedZoom;
            transform.localPosition = new Vector3(lPosition.x, Mathf.Lerp(lPosition.y, deltaPosY, deltaTime));

            yield return null;
        }

        Debug.Log("MoveDown end");

        transform.localPosition = new Vector3(transform.localPosition.x, posY);
        yield return null;

    }


    float localZoomX, localZoomY, localMoveX, localMoveY;

    IEnumerator ZoomOnField1(float toPositionX, float toPositionY)
    {
        if (_coroutineUnzoom != null) StopCoroutine(_coroutineUnzoom);

        isZooming = true;

        float deltaPowerOfZoom = _powerOfZoom * 1.01f;
        float deltaPosX = toPositionX * 1.01f;
        float deltaPosY = toPositionY * 1.01f;

        while (transform.localScale.x < _powerOfZoom)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;

            float deltaTime = Time.deltaTime * _speedZoom;

            localZoomX = Mathf.Lerp(localZoomX, deltaPosX, deltaTime);
            localZoomY = Mathf.Lerp(localZoomY, deltaPosY, deltaTime);

            transform.localPosition = new Vector3(localZoomX + localMoveX, localZoomY + localMoveY);
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, deltaPowerOfZoom, deltaTime), Mathf.Lerp(transform.localScale.y, deltaPowerOfZoom, deltaTime), 1f);

            yield return null;
        }

        localZoomX = toPositionX;
        localZoomY = toPositionY;

        transform.localPosition = new Vector3(localZoomX + localMoveX, localZoomY + localMoveY);
        transform.localScale = new Vector3(_powerOfZoom, _powerOfZoom, 1f);

        isZoomed = true;
        isZooming = false;

        yield return null;
    }

    IEnumerator UnZoomOnField1()
    {
        if (_coroutineZoom != null) StopCoroutine(_coroutineZoom);

        isZooming = true;

        float deltaPowerOfZoom = 0.99f;

        while (transform.localScale.x > 1f)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;
            float deltaTime = Time.deltaTime * _speedZoom;

            localZoomX = Mathf.Lerp(localZoomX, _posX, deltaTime);
            localZoomY = Mathf.Lerp(localZoomY, _posY, deltaTime);

            transform.localPosition = new Vector3(localZoomX + localMoveX, localZoomY + localMoveY);
            transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, deltaPowerOfZoom, deltaTime), Mathf.Lerp(transform.localScale.y, deltaPowerOfZoom, deltaTime), 1f);

            yield return null;
        }

        localZoomX = _posX;
        localZoomY = _posY;

        transform.localPosition = new Vector3(localZoomX + localMoveX, localZoomY + localMoveY);
        transform.localScale = new Vector3(1f, 1f, 1f);

        isZoomed = false;
        isZooming = false;

        yield return null;
    }

    IEnumerator MoveUp1(float moveDistanse)
    {
        if (_coroutineMoveDown != null) StopCoroutine(_coroutineMoveDown);

        //float posY = _posZoomY + moveDistanse;
        //float deltaPosY = _posZoomY + (moveDistanse * 1.1f);

        while (localMoveY < moveDistanse)
        {
            Vector3 lPosition = transform.localPosition;

            float deltaTime = Time.deltaTime * _speedZoom;

            localMoveY = Mathf.Lerp(localMoveY, moveDistanse + 7f, deltaTime);

            transform.localPosition = new Vector3(lPosition.x, localZoomY + localMoveY);

            yield return null;
        }

        localMoveY = moveDistanse;

        transform.localPosition = new Vector3(transform.localPosition.x, localZoomY + localMoveY);
        yield return null;

    }

    IEnumerator MoveDown1(float moveDistanse)
    {
        if (_coroutineMoveUp != null) StopCoroutine(_coroutineMoveUp);

        //float posY = _posZoomY + moveDistanse;
        //float deltaPosY = _posZoomY + (moveDistanse * 1.1f);

        while (localMoveY > moveDistanse)
        {
            Vector3 lPosition = transform.localPosition;

            float deltaTime = Time.deltaTime * _speedZoom;

            localMoveY = Mathf.Lerp(localMoveY, moveDistanse - 7f, deltaTime);

            transform.localPosition = new Vector3(lPosition.x, localZoomY + localMoveY);

            yield return null;
        }

        localMoveY = moveDistanse;

        transform.localPosition = new Vector3(transform.localPosition.x, localZoomY + localMoveY);
        yield return null;

    }

}
