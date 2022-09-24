using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolUIDown : MonoBehaviour
{
    [SerializeField] private RectTransform _selfRectTransform;
    [SerializeField] private float _distanceForMove;
    [SerializeField] private float _baseHeightImage, _heightForChange, _heightForChangeOnNormal;
    [SerializeField] private float _speedMove;

    public UnityEvent DisableButton, OnMoveUp;
    public UnityEvent OnChangeUp, OnChangeDown;

    public static bool isOpenedUp;

    private protected Coroutine _coroutineChangeHeightUp, _coroutineChangeHeightDown, _coroutineChangeHeight;
    private protected Coroutine _coroutineMoveUp, _coroutineMoveDown;
    private protected Coroutine _coroutineMove;

    private protected float localMoving;

    private void MoveImageInDistanceY(RectTransform rectTransformImage, float distance)
    {
        float b = rectTransformImage.offsetMin.y;

        rectTransformImage.offsetMin = new Vector3(0, distance);
        rectTransformImage.offsetMax = new Vector3(0, rectTransformImage.offsetMax.y + distance - b);
    }

    private void ChangeHeightImage(RectTransform rectTransformImage, float height)
    {
        rectTransformImage.offsetMax = new Vector3(0, height + rectTransformImage.offsetMin.y);
    }

    public void UIMoveUp()
    {
        if (_coroutineMove != null) StopCoroutine(_coroutineMove);
        _coroutineMove = StartCoroutine(MoveUp(_selfRectTransform));
        OnMoveUp.Invoke();
    }

    public void UIMoveDown()
    {
        if (_coroutineMove != null) StopCoroutine(_coroutineMove);
        _coroutineMove = StartCoroutine(MoveDown(_selfRectTransform, _distanceForMove));
        UIChangeHeightDown();
    }

    public void UIChangeHeightUp()
    {
        _coroutineChangeHeightUp = StartCoroutine(ChangeHeightUp(_selfRectTransform, _heightForChange));
        isOpenedUp = true;
        OnChangeUp.Invoke();
    }

    public void UIChangeHeightDown()
    {
        if (_coroutineChangeHeight != null) StopCoroutine(_coroutineChangeHeight);

        _coroutineChangeHeightDown = StartCoroutine(ChangeHeightDown(_selfRectTransform, _baseHeightImage));
        isOpenedUp = false;
        OnChangeDown.Invoke();
    }

    public void UIChangeHeight(float heightForChange)
    {
        if (_coroutineChangeHeight != null) StopCoroutine(_coroutineChangeHeight);
        if (_coroutineChangeHeightDown != null) StopCoroutine(_coroutineChangeHeightDown);

        _coroutineChangeHeight = StartCoroutine(ChangeHeightV2(_selfRectTransform, heightForChange));

        isOpenedUp = true;

        if (localMoving < heightForChange)
        {
            OnChangeUp?.Invoke();
        }
        else
        {
            if (heightForChange == 0) OnChangeDown?.Invoke();
        }

        localMoving = heightForChange;
    }


    IEnumerator ChangeHeightUp(RectTransform selfRectTransform, float heightForChange)
    {
        if (_coroutineChangeHeightDown != null) StopCoroutine(_coroutineChangeHeightDown);

        while (selfRectTransform.offsetMax.y < heightForChange - 4)
        {
            float deltaTime = Time.deltaTime * _speedMove;
            ChangeHeightImage(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMax.y, heightForChange, deltaTime));
            yield return null;
        }

        ChangeHeightImage(selfRectTransform, heightForChange);
        yield return null;
    }

    IEnumerator ChangeHeightDown(RectTransform selfRectTransform, float heightForChange)
    {
        if (_coroutineChangeHeightUp != null) StopCoroutine(_coroutineChangeHeightUp);

        while (selfRectTransform.offsetMax.y > heightForChange + 4)
        {
            float deltaTime = Time.deltaTime * _speedMove;
            ChangeHeightImage(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMax.y, heightForChange, deltaTime));
            yield return null;
        }

        ChangeHeightImage(selfRectTransform, heightForChange);
        yield return null;
    }

    IEnumerator MoveDown(RectTransform selfRectTransform, float distanceForMove)
    {

        while (selfRectTransform.offsetMin.y > distanceForMove + 6)
        {
            float deltaTime = Time.deltaTime * _speedMove;
            MoveImageInDistanceY(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMin.y, distanceForMove, deltaTime));
            yield return null;
        }

        MoveImageInDistanceY(selfRectTransform, distanceForMove);
        yield return null;
    }

    IEnumerator MoveUp(RectTransform selfRectTransform)
    {

        while (selfRectTransform.offsetMin.y < 0f)
        {
            float deltaTime = Time.deltaTime * _speedMove;
            MoveImageInDistanceY(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMin.y, 0f, deltaTime));
            yield return null;
        }

        MoveImageInDistanceY(selfRectTransform, 0f);
        yield return null;
    }

    IEnumerator ChangeHeight(RectTransform selfRectTransform, float heightForChange)
    {
        float DifferenceBetweenDistances = Mathf.Abs(selfRectTransform.offsetMax.y - heightForChange);
        float deltaTime = Time.deltaTime * _speedMove;

        while (DifferenceBetweenDistances >= 2)
        {
            deltaTime = Time.deltaTime * _speedMove;
            ChangeHeightImage(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMax.y, heightForChange, deltaTime));

            DifferenceBetweenDistances = Mathf.Abs(selfRectTransform.offsetMax.y - heightForChange);

            yield return null;
        }

        ChangeHeightImage(selfRectTransform, heightForChange);
        yield return null;
    }

    IEnumerator ChangeHeightV2(RectTransform selfRectTransform, float heightForChange)
    {
        float DifferenceBetweenDistances = Mathf.Abs(selfRectTransform.offsetMax.y - (heightForChange + selfRectTransform.offsetMin.y));
        float deltaTime = Time.deltaTime * _speedMove;

        while (DifferenceBetweenDistances >= 2)
        {
            deltaTime = Time.deltaTime * _speedMove;
            selfRectTransform.offsetMax = new Vector2(selfRectTransform.offsetMax.x, Mathf.Lerp(selfRectTransform.offsetMax.y, heightForChange + selfRectTransform.offsetMin.y, deltaTime));



            DifferenceBetweenDistances = Mathf.Abs(selfRectTransform.offsetMax.y - (heightForChange + selfRectTransform.offsetMin.y));

            yield return null;
        }

        selfRectTransform.offsetMax = new Vector2(selfRectTransform.offsetMax.x, heightForChange + selfRectTransform.offsetMin.y);

    }

}
