using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ToolsForFieldsHouses : MonoBehaviour
{
    [SerializeField] private float _speedZoom;
    [SerializeField] private float _inaccuracyForScale;

    public static bool isOpening;
    public static bool isOpened;

    private float positionToZoomX, positionToZoomY;
    private FieldPlace ZoomedFieldPlace;

    public UnityEvent onZoom, onUnZoom;


    private void MoveImage(RectTransform rectTransformImage, float distance)
    {
        float b = rectTransformImage.offsetMin.y;

        rectTransformImage.offsetMin = new Vector3(0, distance);
        rectTransformImage.offsetMax = new Vector3(0, rectTransformImage.offsetMax.y + distance - b);
    }

    public void ZoomFieldOfHouses()
    {
        StartCoroutine(ZoomOnField(FieldPlace.currentPositionX, FieldPlace.currentPositionY));
        onZoom.Invoke();
    }

    public void UnZoomFieldOfHouses()
    {
        StartCoroutine(UnZoomOnField());
        onUnZoom.Invoke();
    }

    IEnumerator ZoomOnField(float toPositionX, float toPositionY)
    {
        isOpening = true;

        while (!isOpened)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;

            if (lScale.x >= 3f - _inaccuracyForScale)
            {
                transform.localPosition = new Vector3(toPositionX, toPositionY);
                transform.localScale = new Vector3(3f, 3f, 0f);
                isOpened = true;
            }
            else
            {
                float deltaTime = Time.deltaTime * _speedZoom;
                transform.localPosition = new Vector3(Mathf.Lerp(lPosition.x, toPositionX, deltaTime), Mathf.Lerp(lPosition.y, toPositionY, deltaTime));
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 3f, deltaTime), Mathf.Lerp(transform.localScale.y, 3f, deltaTime));
            }

            yield return null;
        }

        isOpening = false;
        yield return null;
    }

    IEnumerator UnZoomOnField()
    {
        isOpening = true;

        while (isOpened)
        {
            Vector3 lPosition = transform.localPosition, lScale = transform.localScale;


            if (lScale.x <= 1f + _inaccuracyForScale)
            {
                transform.localPosition = new Vector3(0f, 0f);
                transform.localScale = new Vector3(1f, 1f, 0f);
                isOpened = false;
            }
            else
            {
                float deltaTime = Time.deltaTime * _speedZoom;
                transform.localPosition = new Vector3(Mathf.Lerp(lPosition.x, 0f, deltaTime), Mathf.Lerp(lPosition.y, 0f, deltaTime));
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, 1f, deltaTime), Mathf.Lerp(transform.localScale.y, 1f, deltaTime));
            }

            yield return null;
        }

        isOpening = false;
        yield return null;
    }



}
