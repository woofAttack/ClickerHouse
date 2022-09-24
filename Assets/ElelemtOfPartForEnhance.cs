using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ElelemtOfPartForEnhance : MonoBehaviour
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

        SetStateLockOrOpen();

        _textLVLPart.text = _bluePoint_Part.LevelOfPart.ToString();
        _textForPrice.text = _bluePoint_Part.GetCurrentPrice.ToString();


        _localStatOfPart = new List<Stat>();

        int countStat = 0;
        int countMax = _bluePoint_Part.MainStat.Count;

        for (int i = 0; i < 4; i++)
        {
            _textForMainStat[i].text = "";
            _imageForMainStat[i].color = new Color(0, 0, 0, 0);
        }

        for (int i = 0; i < countMax; i++)
        {
            if (_bluePoint_Part.MainStat[i].Value != 0)
            {
                if (countStat != 4)
                {
                    _textForMainStat[countStat].text = string.Format("{0}", _bluePoint_Part.MainStat[i].Value);
                    _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                    _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)_bluePoint_Part.MainStat[i].Bonus];

                    _localStatOfPart.Add(_bluePoint_Part.MainStat[i]);

                    countStat++;
                }
                else
                {
                    Debug.LogWarningFormat("{0} have more then 4 stat", _bluePoint_Part.NameOfPart);
                }
            }
        }

        for (int i = 0; i < countMax; i++)
        {
            if (_bluePoint_Part.SubStat[i].Value != 0)
            {
                if (countStat != 4)
                {
                    _textForMainStat[countStat].text = string.Format("+{0}%", _bluePoint_Part.SubStat[i].Value * 100);
                    _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                    _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)_bluePoint_Part.SubStat[i].Bonus];

                    _localStatOfPart.Add(_bluePoint_Part.SubStat[i]);

                    countStat++;
                }
                else
                {
                    Debug.LogWarningFormat("{0} have more then 4 stat", _bluePoint_Part.NameOfPart);
                }
            }
        }

        for (int i = 0, imax = _bluePoint_Part.CountLevelOfProgression; i < imax; i++)
        {
            //_transformOfRarityPanel.GetChild(i).gameObject.SetActive(true);
        }

        Player.OnLevelUp += SetStateLockOrOpen;
        _bluePoint_Part.OnUpdate += UpdateUI;
    }

    public void EnhancePart()
    {
        if (_open && Player.GetCoinOfPlayer >= _bluePoint_Part.GetCurrentPrice)
        _bluePoint_Part.UpdateValue(1, 1); // FIX IT
    }

    public void UpdateUI()
    {
        _textNameOfPart.text = string.Format("{0} (+{1})", _bluePoint_Part.NameOfPart, _bluePoint_Part.GetLevelEnhance);

        for (int i = 0, imax = _localStatOfPart.Count; i < imax; i++)
        {
            _textForMainStat[i].text = _localStatOfPart[i].typeOfStat == Stat.type.Main ? string.Format("{0}", Math.Round(_localStatOfPart[i].Value, 2)) : string.Format("+{0}%", Math.Round(_localStatOfPart[i].Value * 100, 2));
        }

        _textForPrice.text = _bluePoint_Part.GetCurrentPrice.ToString();
    }

    public void SetStateLockOrOpen()
    {
        _open = Player.GetLevelOfPlayer >= _bluePoint_Part.LevelOfPart;
        _animatorOfPanelForLock.SetBool("Open", _open);
    }

    private void OnEnable()
    {
        _animatorOfPanelForLock.SetBool("Open", _open);
    }
}
