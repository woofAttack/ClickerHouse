using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContainerBluepointForSlot : MonoBehaviour
{
    [SerializeField] private protected Prefab_Part.TypePart _typePartOfSlot;
    private protected FieldPlace_PartV2 _fieldPlace_Part;
    private protected BluePoint_Part _linkBluepoint_Part;

    

    [SerializeField] private protected ListTextForContainer _listTextForContainer;
    [SerializeField] private protected ListImageForContainer _listImageForContainer;
    [SerializeField] private protected ListStatForContainer[] _listStatForContainer;

    [SerializeField] private protected GameObject _buttonForSelect, _imageSquare, _buttonForCancelBuild, _buttonForUpPart, _panelForUpPart, _imageOfBuildingState;

    [Space(10)]

    [SerializeField] private protected Transform _transformOfProgressionPanel;
    private protected List<Image> _star;
    private protected int _countActiveStar;

    [Space(10)]

    [SerializeField] private protected Animator _animatorOfPanelForLock;

    [Space(10)]

    [SerializeField] private protected IconsStat _iconsOfStat;
    [SerializeField] private protected ListOfSprites _rarityOfMainBlock, _rarityBackGround, _rarityOfPanel, _typeOfStat, _progState;

    private protected void Awake()
    {
        _star = new List<Image>();

        for (int i = 0, imax = _transformOfProgressionPanel.childCount; i < imax; i++)
        {
            _star.Add(_transformOfProgressionPanel.GetChild(i).GetComponent<Image>());
        }
    }

    public void SetFieldPlacePartToSlot(bool isBuildingState)
    {
        bool isNewFieldPart = SetFieldPartAndBluepoint();

        if (isNewFieldPart)
        {
            SetTextOfBluepointToContainer();
            SetImageOfBluepointToContainer();
            SetOffStart();
        }

        SetTextBuildPoint();
        SetActiveStars(isBuildingState);
        SetTextToStat(isBuildingState);

        // Add Hammer Animation to Main Square
        // Add Button for Cancel Build

        _imageSquare.SetActive(true);
        _buttonForSelect.SetActive(false);
        _imageOfBuildingState.SetActive(isBuildingState);
        _buttonForCancelBuild.SetActive(isBuildingState);
        _panelForUpPart.SetActive(!isBuildingState);
        

    }


    private protected bool SetFieldPartAndBluepoint()
    {
        if (_fieldPlace_Part != HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)_typePartOfSlot])
        {
            _fieldPlace_Part = HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetFieldPlace_Part[(int)_typePartOfSlot];
            _linkBluepoint_Part = _fieldPlace_Part.GetBluePointPart;
            return true;
        }

        return false;
    }
    private protected void SetTextOfBluepointToContainer()
    {
        _listTextForContainer.textNameOfPart.text = _linkBluepoint_Part.NameOfPart;
        _listTextForContainer.textLevelOfPart.text = _linkBluepoint_Part.LevelOfPart.ToString();
    }
    private protected void SetTextBuildPoint()
    {
        if (_fieldPlace_Part.GetCurrentLevelProgression == _linkBluepoint_Part.CountLevelOfProgression)
        {
            _buttonForUpPart.SetActive(false);
        }
        else
        {
            _buttonForUpPart.SetActive(true);
            _listTextForContainer.textCountOfBuildPoints.text = _linkBluepoint_Part.countBuildPointToMax[_fieldPlace_Part.GetCurrentLevelProgression].ToString();
        }      
    }
    private protected void SetImageOfBluepointToContainer()
    {
        if (_linkBluepoint_Part.TypeOfPart == Prefab_Part.TypePart.Field)
        {
            _listImageForContainer.imageOfPartField.sprite = _linkBluepoint_Part.MainSpriteOfPart;
            _listImageForContainer.imageOfPartField.gameObject.SetActive(true);


            //Debug.LogFormat("Field {0} have {1}", bluePointPart.NameOfPart, _imagesOfContainer.imageOfPartField.sprite.rect.width);
        }
        else if (_linkBluepoint_Part.TypeOfPart == Prefab_Part.TypePart.Block)
        {
            _listImageForContainer.imageOfPartBlockOrRoof.sprite = _linkBluepoint_Part.MainSpriteOfPart;
            _listImageForContainer.imageOfPartBlockOrRoof.gameObject.SetActive(true);

        }
        else
        {
            _listImageForContainer.imageOfPartBlockOrRoof.sprite = _linkBluepoint_Part.MainSpriteOfPart;
            _listImageForContainer.imageOfPartBlockOrRoof.gameObject.SetActive(true);

            float width = _listImageForContainer.imageOfPartBlockOrRoof.sprite.rect.width >= 72 ? 72f : _listImageForContainer.imageOfPartField.sprite.rect.width;

            _listImageForContainer.imageOfPartBlockOrRoof.rectTransform.offsetMin = new Vector2(-width, -width + 3f);
            _listImageForContainer.imageOfPartBlockOrRoof.rectTransform.offsetMax = new Vector2(width, width + 3f);
        }

        int rarity = (int)_linkBluepoint_Part.RarityOfPart;
        _listImageForContainer.mainSquare.sprite = _rarityOfMainBlock.Sprites[rarity];
        _listImageForContainer.backgroundOfPanelButton.sprite = _rarityBackGround.Sprites[rarity];
        _listImageForContainer.backgroundOfPanelProgression.sprite = _rarityBackGround.Sprites[rarity];
        _listImageForContainer.backgroundOfPanelStat.sprite = _rarityBackGround.Sprites[rarity];
        _listImageForContainer.panelOfLevelOfPart.sprite = _rarityOfPanel.Sprites[rarity];
        _listImageForContainer.panelOfNameOfPart.sprite = _rarityOfPanel.Sprites[rarity];
    }
    private protected void SetOffStart()
    {
        int newCountActiveStar = _linkBluepoint_Part.CountLevelOfProgression;

        for (int i = newCountActiveStar, imax = _countActiveStar; i < imax; i++)
        {
            _star[i].gameObject.SetActive(false);
        }

        _countActiveStar = newCountActiveStar;
    }
    private protected void SetActiveStars(bool isBuildingState)
    {
        for (int i = 0, imax = _linkBluepoint_Part.CountLevelOfProgression; i < imax; i++)
        {
            _star[i].gameObject.SetActive(true);
            if (i < _fieldPlace_Part.GetCurrentLevelProgression) _star[i].sprite = _progState.Sprites[0];
            else if (i == _fieldPlace_Part.GetCurrentLevelProgression) _star[i].sprite = _progState.Sprites[isBuildingState ? 2 : 1];
            else _star[i].sprite = _progState.Sprites[1];
        }
    }
    private protected void SetTextToStat(bool isBuildingState)
    {
        for (int i = 0, imax = _linkBluepoint_Part.NonZeroStat.Count; i < imax; i++)
        {
            int numberStat = (int)_linkBluepoint_Part.NonZeroStat[i].Bonus;

            if (_linkBluepoint_Part.NonZeroStat[i].typeOfStat == Stat.type.Main)
            {
                _listStatForContainer[i].textOfStat.text = isBuildingState ? 
                    string.Format("{0} -> {1}", _fieldPlace_Part.GetOldStatOfFieldPlace[numberStat].Value, _fieldPlace_Part.GetNewStatOfFieldPlace[numberStat].Value) :
                    string.Format("{0}", _fieldPlace_Part.GetNewStatOfFieldPlace[numberStat].Value);

                _listStatForContainer[i].imageStats.sprite = _iconsOfStat.SpritesOfIcon[numberStat];
                _listStatForContainer[i].backgroundStats.sprite = _typeOfStat.Sprites[0];
            }
            else
            {
                _listStatForContainer[i].textOfStat.text = isBuildingState ?
                    string.Format("+ {0}% -> {1}% ", _fieldPlace_Part.GetOldSubStatOfFieldPlace[numberStat].Value, _fieldPlace_Part.GetNewSubStatOfFieldPlace[numberStat].Value) :
                    string.Format("+ {0}%", _fieldPlace_Part.GetNewSubStatOfFieldPlace[numberStat].Value);

                _listStatForContainer[i].imageStats.sprite = _iconsOfStat.SpritesOfIcon[numberStat];
                _listStatForContainer[i].backgroundStats.sprite = _typeOfStat.Sprites[1];
            }
        }
    }



    public void SelectCurrentPartForBuild()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.SetCurrentBuildForLeveling(_fieldPlace_Part);
    }

}
