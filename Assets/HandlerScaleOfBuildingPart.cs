using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HandlerScaleOfBuildingPart : MonoBehaviour
{
    private protected float _deltaMaxBuildPoint;

    private protected float _maxBuildPoint;
    private protected float _currentBuildPoint;

    [SerializeField] private protected Text _textForCurrentBuild;
    [SerializeField] private protected Image _scaleImage;

    public void __SetBuildPointToScale()
    {
        _maxBuildPoint = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetMaxBuildPoint;
        _currentBuildPoint = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetCurrentBuildPoint;

        _deltaMaxBuildPoint = 1f / _maxBuildPoint;

        _textForCurrentBuild.text = string.Format("{0} / {1}", _currentBuildPoint.ToString(), _maxBuildPoint.ToString());
        _scaleImage.fillAmount = _deltaMaxBuildPoint * _currentBuildPoint;
    }

    public void __ChangeScale()
    {
        _currentBuildPoint = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetCurrentBuildPoint;

        _textForCurrentBuild.text = string.Format("{0} / {1}", _currentBuildPoint.ToString(), _maxBuildPoint.ToString());
        _scaleImage.fillAmount = _deltaMaxBuildPoint * _currentBuildPoint;
    }





}
