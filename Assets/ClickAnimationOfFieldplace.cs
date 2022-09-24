using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickAnimationOfFieldplace : MonoBehaviour
{
    [SerializeField] private protected Animation _animationOfFieldPlace;

    private protected Transform _transformComponentOfFieldPlace;

    private protected GameObject _prefabOfBuildPoint;
    [SerializeField] private protected GameObject _prefabOfCoinClickText;
    [SerializeField] private protected GameObject _prefabOfBuildPointClickText;

    private protected void Awake()
    {
        _transformComponentOfFieldPlace = GetComponent<Transform>();
    }

    public void AnimateClickBuild()
    {
        _animationOfFieldPlace.Stop();

        Prefab_Part.TypePart typeOfCurrentBuildingPart = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetCurrentFieldPlace.getTypePart;

        if (typeOfCurrentBuildingPart == Prefab_Part.TypePart.Block)
        {
            _animationOfFieldPlace.Play("PushBlock");
        }
        else if (typeOfCurrentBuildingPart == Prefab_Part.TypePart.Roof)
        {
            _animationOfFieldPlace.Play("PushRoof");
        }
        else
        {
            _animationOfFieldPlace.Play("PushField");
        }

        Instantiate(_prefabOfBuildPointClickText, _transformComponentOfFieldPlace);
    }

    public void AnimateClickCoin()
    {
        _animationOfFieldPlace.Stop();
        _animationOfFieldPlace.Play("PushCoin");

        Instantiate(_prefabOfCoinClickText, _transformComponentOfFieldPlace);
    }

    public void ReturnAnimation()
    {
        _animationOfFieldPlace.Stop();
        _animationOfFieldPlace.Play("ReturnField");    
    }

}
