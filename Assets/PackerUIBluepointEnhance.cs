using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class PackerUIBluepointEnhance : MonoBehaviour
{

    [SerializeField] private protected Text _textNameOfPart, _textLVLPart, _textBuildPoint, _textForPrice;
    [SerializeField] private protected Image _imageOfPart;

    [SerializeField] private protected Text[] _textForMainStat;
    [SerializeField] private protected Image[] _imageForMainStat;

    [SerializeField] private protected IconsStat _icons;

    private protected BluePoint_Part _bluePoint_Part;
    private protected List<Stat> _localStatOfPart;

    [SerializeField] private protected Animator _animatorOfPanelForLock;
    [SerializeField] private protected bool _open;


    public void SetBluePoint_Part(BluePoint_Part bluePointPart)
    {
        _bluePoint_Part = bluePointPart;

        _imageOfPart.sprite = _bluePoint_Part.MainSpriteOfPart;

        string mes = _bluePoint_Part.GetLevelEnhance == 0 ? "" : string.Format(" (+{0})", _bluePoint_Part.GetLevelEnhance);
        _textNameOfPart.text = _bluePoint_Part.NameOfPart + mes;

        _textLVLPart.text = _bluePoint_Part.LevelOfPart.ToString();
        _textForPrice.text = _bluePoint_Part.GetCurrentPrice.ToString();


        _localStatOfPart = new List<Stat>();

        int countStat = 0;
        int countMax = _bluePoint_Part.MainStat.Count;

        

    }






}
