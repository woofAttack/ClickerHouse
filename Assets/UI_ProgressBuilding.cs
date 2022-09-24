using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ProgressBuilding : MonoBehaviour
{

    [SerializeField] private Image _progressImage;
    [SerializeField] private Text _progressText;

    private void Start()
    {
        //MoveImageInDistanceY(GetComponent<RectTransform>(), 50f);
    }

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

    public void OnUpdate()
    {
        float currentBuildPoints = FieldPlace.currentZoomedFieldPlace.currentBuild;
        float maxBuildPoint = FieldPlace.currentZoomedFieldPlace.maxCountBuild;

        _progressImage.fillAmount = currentBuildPoints / maxBuildPoint;
        //_progressText.text = currentBuildPoints.ToString() + " / " + maxBuildPoint.ToString();
        _progressText.text = string.Format("{0} / {1}", currentBuildPoints, maxBuildPoint);
    }

    public void UIMoveDown()
    {
        if (FieldPlace.currentZoomedFieldPlace.GetStateOfFieldPlace == FieldPlace.StateOfFieldPlace.Building) StartCoroutine(MoveDown(GetComponent<RectTransform>(), -100f));
    }

    public void UIMoveUp()
    {
        StartCoroutine(MoveUp(GetComponent<RectTransform>(), 100f));
    }

    IEnumerator MoveDown(RectTransform selfRectTransform, float distanceForMove)
    {

        while (selfRectTransform.offsetMin.y >= distanceForMove + 2f)
        {
            float deltaTime = Time.deltaTime * 2.5f;
            MoveImageInDistanceY(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMin.y, distanceForMove, deltaTime));
            yield return null;
        }

        MoveImageInDistanceY(selfRectTransform, distanceForMove);
        yield return null;
    }

    IEnumerator MoveUp(RectTransform selfRectTransform, float distanceForMove)
    {

        while (selfRectTransform.offsetMin.y <= distanceForMove - 2f)
        {
            float deltaTime = Time.deltaTime * 2.5f;
            MoveImageInDistanceY(selfRectTransform, Mathf.Lerp(selfRectTransform.offsetMin.y, distanceForMove, deltaTime));
            yield return null;
        }

        MoveImageInDistanceY(selfRectTransform, distanceForMove);
        yield return null;
    }

}
