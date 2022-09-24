using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementOfListForWorkingState : MonoBehaviour
{
    [SerializeField] private protected Prefab_Part.TypePart _typePart;

    [SerializeField] private protected Image _imageOfPart;
    [SerializeField] private protected Text _textForNameOfPart;
    [SerializeField] private protected Text _textForDicriptLevelProgression;
    [SerializeField] private protected Button _buttonForUpgrade;

    [SerializeField] private protected Text[] _textForMainStat;
    [SerializeField] private protected Image[] _imageForMainStat;

    [SerializeField] private protected IconsStat _icons;

    private protected FieldPlace_PartV2 _bluePoint_Part;

    public void SetCurrentBluePointPart()
    {
        FieldPlace_PartV2 FieldPlacePart = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)_typePart];
        _bluePoint_Part = FieldPlacePart;

        if (FieldPlacePart.GetStateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Working)
        {
            _imageOfPart.sprite = FieldPlacePart.GetSpriteOfPartNew;
            _textForNameOfPart.text = FieldPlacePart.GetBluePointPart.NameOfPart;

            _textForDicriptLevelProgression.text = string.Format("{0} / {1} lvl progression", FieldPlacePart.GetCurrentLevelProgression, FieldPlacePart.GetBluePointPart.CountLevelOfProgression);

            _buttonForUpgrade.enabled = (FieldPlacePart.GetCurrentLevelProgression != FieldPlacePart.GetBluePointPart.CountLevelOfProgression);
            _buttonForUpgrade.interactable = (FieldPlacePart.GetCurrentLevelProgression != FieldPlacePart.GetBluePointPart.CountLevelOfProgression);

            int countStat = 0;
            int countMax = FieldPlacePart.GetNewStatOfFieldPlace.Count;

            for (int i = 0; i < 4; i++)
            {
                _textForMainStat[i].text = "";
                _imageForMainStat[i].color = new Color(0, 0, 0, 0);
            }

            for (int i = 0; i < countMax; i++)
            {
                if (FieldPlacePart.GetNewStatOfFieldPlace[i].Value != 0)
                {
                    if (countStat != 4)
                    {
                        _textForMainStat[countStat].text = string.Format("{0}", FieldPlacePart.GetNewStatOfFieldPlace[i].Value);
                        _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                        _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)FieldPlacePart.GetNewStatOfFieldPlace[i].Bonus];
                        countStat++;
                    }
                    else
                    {
                        Debug.LogWarningFormat("{0} have more then 4 stat", FieldPlacePart.GetBluePointPart.NameOfPart);
                    }
                }
            }

            for (int i = 0; i < countMax; i++)
            {
                if (FieldPlacePart.GetNewSubStatOfFieldPlace[i].Value != 0)
                {
                    if (countStat != 4)
                    {
                        _textForMainStat[countStat].text = string.Format("+{0}%", FieldPlacePart.GetNewSubStatOfFieldPlace[i].Value * 100);
                        _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                        _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)FieldPlacePart.GetNewSubStatOfFieldPlace[i].Bonus];
                        countStat++;
                    }
                    else
                    {
                        Debug.LogWarningFormat("{0} have more then 4 stat", FieldPlacePart.GetBluePointPart.NameOfPart);
                    }
                }
            }


            FieldPlacePart.GetBluePointPart.OnUpdate += UpdateUI;
        }
    }


    

    public void SelectCurrentPartForBuild()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.SetCurrentBuildForLeveling(_bluePoint_Part);
    }

    public void UpdateUI()
    {

    }

}
