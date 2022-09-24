using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SellPanel : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private protected GameObject _gameObjectToDisableOrEnable;

    [Space(10)]
    [SerializeField] private protected Image _imageComponentOfPreField;
    [SerializeField] private protected Image _imageComponentOfPreBlock;
    [SerializeField] private protected Image _imageComponentOfPreRoof;

    [Space(10)]
    [SerializeField] private protected Text _textCoinSell;
    [SerializeField] private protected Text _textEXPSell;

    private protected FieldPlaceV2 _currentFieldPlace;

    public void CreateSellPanelForFieldPlace()
    {
        _currentFieldPlace = HandlerFieldPlace.GetCurrentZoomedFieldPlace;

        _imageComponentOfPreField.sprite = _currentFieldPlace.GetFieldPlace_Part[(int)Prefab_Part.TypePart.Field].GetSpriteOfPartNew;
        _imageComponentOfPreBlock.sprite = _currentFieldPlace.GetFieldPlace_Part[(int)Prefab_Part.TypePart.Block].GetSpriteOfPartNew;
        _imageComponentOfPreRoof.sprite = _currentFieldPlace.GetFieldPlace_Part[(int)Prefab_Part.TypePart.Roof].GetSpriteOfPartNew;

        _textCoinSell.text = string.Format("+{0}", _currentFieldPlace.GetStatOfFieldPlace[(int)Prefab_Part.Bonus.SellCoin].Value);
        _textEXPSell.text = string.Format("+{0}", _currentFieldPlace.GetStatOfFieldPlace[(int)Prefab_Part.Bonus.SellExp].Value);



        _gameObjectToDisableOrEnable?.SetActive(true);
    }

    public void DisablePanel()
    {
        _gameObjectToDisableOrEnable?.SetActive(false);
    }

    public void SellFieldPlace()
    {
        _currentFieldPlace?.SellHouse();
        _gameObjectToDisableOrEnable?.SetActive(false);
    }







}
