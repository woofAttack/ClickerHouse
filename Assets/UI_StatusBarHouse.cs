using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusBarHouse : MonoBehaviour
{
    [SerializeField] private protected float _speedMove;
    [SerializeField] private protected RectTransform _selfRectTransform;

    [SerializeField] private protected Text[] _textForStat;


    private protected Coroutine _coroutineMoveUp, _coroutineMoveDown;

    private void MoveImageInDistanceY(RectTransform rectTransformImage, float distance)
    {
        float b = rectTransformImage.offsetMin.y;

        rectTransformImage.offsetMin = new Vector3(0, distance);
        rectTransformImage.offsetMax = new Vector3(0, rectTransformImage.offsetMax.y + distance - b);
    }

    public void SetCurrentCoinClick()
    {
        if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace != FieldPlaceV2.StateOfFieldPlace.Building)
        {
            gameObject.SetActive(true);

            for (int i = 0, imax = _textForStat.Length; i < imax; i++)
            {
                _textForStat[i].text = string.Format("{0}", Math.Round(HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStatOfFieldPlace[i].Value, 2));
            }
        }
        else
        {
            for (int i = 0, imax = _textForStat.Length; i < imax; i++)
            {
                float oldValue = (float)Math.Round(HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetOldStatOfFieldPlace[i].Value, 2);
                float newValue = (float)Math.Round(HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStatOfFieldPlace[i].Value, 2);

                string m1 = newValue != oldValue ? "<color=#00FF00FF>" : "";
                string m2 = newValue != oldValue ? "</color>" : "";

                _textForStat[i].text = string.Format("{1} -> {2}{0}{3}", newValue, oldValue, m1, m2);

            }


        }

    }

    public void UIMoveUp()
    {
        _coroutineMoveUp = StartCoroutine(MoveUp(_selfRectTransform));
    }

    public void UIMoveDown()
    {
        _coroutineMoveDown = StartCoroutine(MoveDown(_selfRectTransform, -112));
    }

    IEnumerator MoveDown(RectTransform selfRectTransform, float distanceForMove)
    {
        if (_coroutineMoveUp != null) StopCoroutine(_coroutineMoveUp);

        while (selfRectTransform.offsetMin.y > distanceForMove + 1)
        {
            float deltaTime = Time.deltaTime * _speedMove;
            MoveImageInDistanceY(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMin.y, distanceForMove, deltaTime));
            yield return null;
        }

        MoveImageInDistanceY(selfRectTransform, distanceForMove);

    }

    IEnumerator MoveUp(RectTransform selfRectTransform)
    {
        if (_coroutineMoveDown != null) StopCoroutine(_coroutineMoveDown);

        while (selfRectTransform.offsetMin.y < -40f)
        {
            float deltaTime = Time.deltaTime * _speedMove;
            MoveImageInDistanceY(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMin.y, -30f, deltaTime));
            yield return null;
        }

        MoveImageInDistanceY(selfRectTransform, -40f);
        yield return null;
    }

}
