using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementOfListForEmptyState : MonoBehaviour
{

    [SerializeField] private protected Image _imageOfPart;
    [SerializeField] private protected Text _textForNameOfPart;
    [SerializeField] private protected Text _textForDicriptLevelProgression;
    [SerializeField] private protected Text _textLVLPart;

    [SerializeField] private protected Text[] _textForMainStat;
    [SerializeField] private protected Image[] _imageForMainStat;

    [SerializeField] private protected IconsStat _icons;

    [SerializeField] private protected Animator _animatorOfPanelForLock;
    [SerializeField] private protected bool _open;



    private protected BluePoint_Part _bluePoint_Part;
    private protected List<Stat> _localStatOfPart;

    public void SetCurrentBluePointPart(BluePoint_Part BluePointPart)
    {
        _bluePoint_Part = BluePointPart;
        _imageOfPart.sprite = BluePointPart.MainSpriteOfPart;


        string mes = _bluePoint_Part.GetLevelEnhance == 0 ? "" : string.Format(" (+{0})", _bluePoint_Part.GetLevelEnhance);
        _textForNameOfPart.text = _bluePoint_Part.NameOfPart + mes;

        _textForDicriptLevelProgression.text = string.Format("{0} lvl prog.", BluePointPart.CountLevelOfProgression);
        _textLVLPart.text = string.Format("{0}lvl", BluePointPart.LevelOfPart);

        _localStatOfPart = new List<Stat>();

        SetStateLockOrOpen();

        int countStat = 0;
        int countMax = BluePointPart.MainStat.Count;

        for (int i = 0; i < 4; i++)
        {
            _textForMainStat[i].text = "";
            _imageForMainStat[i].color = new Color(0, 0, 0, 0);
        }

        for (int i = 0; i < countMax; i++)
        {
            if (BluePointPart.MainStat[i].Value != 0)
            {
                if (countStat != 4)
                {
                    _textForMainStat[countStat].text = string.Format("{0}", BluePointPart.MainStat[i].Value);
                    _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                    _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)BluePointPart.MainStat[i].Bonus];

                    _localStatOfPart.Add(_bluePoint_Part.MainStat[i]);

                    countStat++;
                }
                else
                {
                    Debug.LogWarningFormat("{0} have more then 4 stat", BluePointPart.NameOfPart);
                }
            }
        }

        for (int i = 0; i < countMax; i++)
        {
            if (BluePointPart.SubStat[i].Value != 0)
            {
                if (countStat != 4)
                {
                    _textForMainStat[countStat].text = string.Format("+{0}%", BluePointPart.SubStat[i].Value * 100);
                    _imageForMainStat[countStat].color = new Color(1, 1, 1, 1);
                    _imageForMainStat[countStat].sprite = _icons.SpritesOfIcon[(int)BluePointPart.SubStat[i].Bonus];

                    _localStatOfPart.Add(_bluePoint_Part.SubStat[i]);

                    countStat++;
                }
                else
                {
                    Debug.LogWarningFormat("{0} have more then 4 stat", BluePointPart.NameOfPart);
                }
            }
        }

        Player.OnLevelUp += SetStateLockOrOpen;
        BluePointPart.OnUpdate += UpdateUI;
    }

    public void SelectCurrentPartForBuild()
    {
        if (_open) HandlerFieldPlace.GetCurrentZoomedFieldPlace.SetPartForBuilding(_bluePoint_Part);
    }

    public void UpdateUI()
    {
        _textForNameOfPart.text = string.Format("{0} (+{1})", _bluePoint_Part.NameOfPart, _bluePoint_Part.GetLevelEnhance);

        for (int i = 0, imax = _localStatOfPart.Count; i < imax; i++)
        {
            _textForMainStat[i].text = _localStatOfPart[i].typeOfStat == Stat.type.Main ? string.Format("{0}", _localStatOfPart[i].Value) : string.Format("+{0}%", _localStatOfPart[i].Value * 100);
        }

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
