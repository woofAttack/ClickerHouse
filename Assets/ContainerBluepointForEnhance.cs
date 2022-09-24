using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ContainerBluepointForEnhance : MonoBehaviour
{

    private protected BluePoint_Part _linkBluePointPart;


    [SerializeField] private protected ListTextForContainer _textsOfContainer;
    [SerializeField] private protected ListImageForContainer _imagesOfContainer;
    [SerializeField] private protected ListStatForContainer[] _UIStat;

    [SerializeField] private protected SettingBluepointPartToFieldPlace _buttonForSettingBluepointPartToFieldPlace;

    [Space(10)]

    [SerializeField] private protected Transform _transformOfProgressionPanel;
    private protected List<Image> _star;

    [Space(10)]

    [SerializeField] private protected Animator _animatorOfPanelForLock;

    [Space(10)]

    [SerializeField] private protected IconsStat _iconsOfStat;
    [SerializeField] private protected ListOfSprites _rarityOfMainBlock, _rarityBackGround, _rarityOfPanel, _typeOfStat, _progState;

    [Space(10)]

    public UnityEvent OnUnlocked;

    public void SetBluepointPart(BluePoint_Part bluePointPart)
    {
        _linkBluePointPart = bluePointPart;
        
        _textsOfContainer.textNameOfPart.text = bluePointPart.NameOfPart;
        _textsOfContainer.textLevelOfPart.text = bluePointPart.LevelOfPart.ToString();
        _textsOfContainer.textCountOfBuildPoints.text = bluePointPart.countBuildPointToMax[0].ToString();

        if (bluePointPart.TypeOfPart == Prefab_Part.TypePart.Field)
        {
            _imagesOfContainer.imageOfPartField.sprite = bluePointPart.MainSpriteOfPart;
            _imagesOfContainer.imageOfPartField.gameObject.SetActive(true);


            //Debug.LogFormat("Field {0} have {1}", bluePointPart.NameOfPart, _imagesOfContainer.imageOfPartField.sprite.rect.width);
        }
        else if (bluePointPart.TypeOfPart == Prefab_Part.TypePart.Block)
        {
            _imagesOfContainer.imageOfPartBlockOrRoof.sprite = bluePointPart.MainSpriteOfPart;
            _imagesOfContainer.imageOfPartBlockOrRoof.gameObject.SetActive(true);

        }
        else
        {
            _imagesOfContainer.imageOfPartBlockOrRoof.sprite = bluePointPart.MainSpriteOfPart;
            _imagesOfContainer.imageOfPartBlockOrRoof.gameObject.SetActive(true);

            float width = _imagesOfContainer.imageOfPartBlockOrRoof.sprite.rect.width >= 72 ? 72f : _imagesOfContainer.imageOfPartField.sprite.rect.width;

            _imagesOfContainer.imageOfPartBlockOrRoof.rectTransform.offsetMin = new Vector2(-width, -width + 3f);
            _imagesOfContainer.imageOfPartBlockOrRoof.rectTransform.offsetMax = new Vector2(width, width + 3f);
        }


        int rarity = (int)bluePointPart.RarityOfPart;
        _imagesOfContainer.mainSquare.sprite = _rarityOfMainBlock.Sprites[rarity];
        _imagesOfContainer.backgroundOfPanelButton.sprite = _rarityBackGround.Sprites[rarity];
        _imagesOfContainer.backgroundOfPanelProgression.sprite = _rarityBackGround.Sprites[rarity];
        _imagesOfContainer.backgroundOfPanelStat.sprite = _rarityBackGround.Sprites[rarity];
        _imagesOfContainer.panelOfLevelOfPart.sprite = _rarityOfPanel.Sprites[rarity];
        _imagesOfContainer.panelOfNameOfPart.sprite = _rarityOfPanel.Sprites[rarity];

        _buttonForSettingBluepointPartToFieldPlace?.SetBluepointForChoose(bluePointPart);

        _star = new List<Image>();

        for (int i = 0, imax = bluePointPart.CountLevelOfProgression; i < imax; i++)
        {
            _star.Add(_transformOfProgressionPanel.GetChild(i).GetComponent<Image>());
            _star[i].gameObject.SetActive(true);
            _star[i].sprite = _progState.Sprites[0];
        }

        for (int i = 0, imax = bluePointPart.NonZeroStat.Count; i < imax; i++)
        {
            if (bluePointPart.NonZeroStat[i].typeOfStat == Stat.type.Main)
            {
                _UIStat[i].textOfStat.text = string.Format("{0}", bluePointPart.NonZeroStat[i].Value);
                _UIStat[i].imageStats.sprite = _iconsOfStat.SpritesOfIcon[(int)bluePointPart.NonZeroStat[i].Bonus];
                _UIStat[i].backgroundStats.sprite = _typeOfStat.Sprites[0];
            }
            else
            {
                _UIStat[i].textOfStat.text = string.Format("+{0}%", bluePointPart.NonZeroStat[i].Value);
                _UIStat[i].imageStats.sprite = _iconsOfStat.SpritesOfIcon[(int)bluePointPart.NonZeroStat[i].Bonus];
                _UIStat[i].backgroundStats.sprite = _typeOfStat.Sprites[1];
            }
        }

        if (bluePointPart.GetIsUnlocked)
        {
            MakeAvailableContainer();
        }
        else
        {
            bluePointPart.OnUnlocked += MakeAvailableContainer;
        }
    }
    public void MakeAvailableContainer()
    {
        _animatorOfPanelForLock.SetBool("Open", true);
        _linkBluePointPart.OnUnlocked -= MakeAvailableContainer;

        OnUnlocked.Invoke();
    }
    public void SelectCurrentPartForBuild()
    {
        HandlerFieldPlace.GetCurrentZoomedFieldPlace.SetPartForBuilding(_linkBluePointPart);
    }
}
