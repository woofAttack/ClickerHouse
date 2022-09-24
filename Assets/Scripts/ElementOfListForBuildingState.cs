using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementOfListForBuildingState : MonoBehaviour
{
    [SerializeField] private protected Image _imageOfPart;
    [SerializeField] private protected Text _textForNameOfPart;

    [SerializeField] private protected Image _progressImage;
    [SerializeField] private protected Text _progressText;

    [SerializeField] private protected Text[] _textForMainStat;
    [SerializeField] private protected Image[] _imageForMainStat;

    [SerializeField] private protected IconsStat _icons;

    //private protected BluePoint_Part _bluePoint_Part;

    private protected FieldPlaceV2 _currentFieldPlace;
    private protected FieldPlace_PartV2 _prossesingFieldPlace_Part;

    public void SetCurrentBluePointPart()
    {
        _currentFieldPlace = HandlerFieldPlace.GetCurrentZoomedFieldPlace;

        if (_currentFieldPlace.GetStateOfFieldPlace == FieldPlaceV2.StateOfFieldPlace.Building)
        {
            _prossesingFieldPlace_Part = _currentFieldPlace.GetCurrentFieldPlace;

            _imageOfPart.sprite = _prossesingFieldPlace_Part.GetSpriteOfPartNew;
            _textForNameOfPart.text = _prossesingFieldPlace_Part.GetBluePointPart.NameOfPart;

            int countStat = 0;
            int countMax = _prossesingFieldPlace_Part.GetNewStatOfFieldPlace.Count;

            for (int i = 0; i < 4; i++)
            {
                _textForMainStat[i].text = "";
                _imageForMainStat[i].color = new Color(0, 0, 0, 0);

            }

            for (int i = 0; i < countMax; i++)
            {
                if (_prossesingFieldPlace_Part.GetNewStatOfFieldPlace[i].Value != 0)
                {
                    if (countStat != 4)
                    {

                        _textForMainStat[countStat].text = string.Format("{1} -> <color=#00FF00FF>{0}</color>", _prossesingFieldPlace_Part.GetNewStatOfFieldPlace[i].Value, _prossesingFieldPlace_Part.GetOldStatOfFieldPlace[i].Value);

                        _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                        _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)_prossesingFieldPlace_Part.GetNewStatOfFieldPlace[i].Bonus];

                        countStat++;
                    }
                    else
                    {
                        Debug.LogWarningFormat("{0} have more then 4 stat", _prossesingFieldPlace_Part.GetBluePointPart.NameOfPart);
                    }
                }
            }

            for (int i = 0; i < countMax; i++)
            {
                if (_prossesingFieldPlace_Part.GetNewSubStatOfFieldPlace[i].Value != 0)
                {
                    if (countStat != 4)
                    {
                        _textForMainStat[countStat].text = string.Format("+{1}% -> <color=#00FF00FF>{0}%</color>", _prossesingFieldPlace_Part.GetNewSubStatOfFieldPlace[i].Value * 100, _prossesingFieldPlace_Part.GetOldSubStatOfFieldPlace[i].Value * 100);
                        _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                        _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)_prossesingFieldPlace_Part.GetNewStatOfFieldPlace[i].Bonus];
                        countStat++;
                    }
                    else
                    {
                        Debug.LogWarningFormat("{0} have more then 4 stat", _prossesingFieldPlace_Part.GetBluePointPart.NameOfPart);
                    }
                }
            }

            _prossesingFieldPlace_Part.GetBluePointPart.OnUpdate += UpdateUI;

            OnUpdate();
        }
    }

    public void OnUpdate()
    {
        float currentBuildPoints = _currentFieldPlace.GetCurrentBuildPoint;
        float maxBuildPoint = _currentFieldPlace.GetMaxBuildPoint;

        _progressImage.fillAmount = currentBuildPoints / maxBuildPoint;
        _progressText.text = string.Format("{0} / {1}", currentBuildPoints, maxBuildPoint);
    }

    public void UpdateUI()
    {

    }
}
