using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FieldPlaceV2 : MonoBehaviour 
{
    public enum StateOfFieldPlace
    {
        NotWorking,
        Building,
        Working
    }


    private protected HandlerFieldPlace _handlerFieldPlace;

    private protected float _positionX, _positionY;
    private protected float _currentBuildPoint, _maxBuildPoint;

    private protected float _bankCoin, _bankEXP;

    private protected StateOfFieldPlace _stateOfFieldPlace;

    private protected List<FieldPlace_PartV2> _fieldPlace_Part;
    private protected FieldPlace_PartV2 _currentPartForBuilding;

    private protected List<Stat> _statOfFieldPlace, _oldStatOfFieldPlace;

    [SerializeField] private protected ClickAnimationOfFieldplace _animationChanger;

    public delegate void Adding(float currentCount, float maxCount);
    public Adding _addBankCoin, _addBankEXP;

    public float GetPositionX { get => _positionX; }
    public float GetPositionY { get => _positionY; }
    public float GetCurrentBuildPoint { get => _currentBuildPoint; }
    public float GetMaxBuildPoint { get => _maxBuildPoint; }
    public StateOfFieldPlace GetStateOfFieldPlace { get => _stateOfFieldPlace; }
    public List<FieldPlace_PartV2> GetFieldPlace_Part { get => _fieldPlace_Part; }
    public FieldPlace_PartV2 GetCurrentFieldPlace { get => _currentPartForBuilding; }
    public List<Stat> GetStatOfFieldPlace { get => _statOfFieldPlace; }
    public List<Stat> GetOldStatOfFieldPlace { get => _oldStatOfFieldPlace; }
    public ClickAnimationOfFieldplace animationChanger { get => _animationChanger; }
    public float bankCoin { get => _bankCoin; }
    public float bankEXP { get => _bankEXP; }

    private void Awake()
    {
        _positionX = gameObject.transform.localPosition.x;
        _positionY = gameObject.transform.localPosition.y;

        _fieldPlace_Part = new List<FieldPlace_PartV2>
        {
            new FieldPlace_PartV2(Prefab_Part.TypePart.Block, GetComponent<Transform>()),
            new FieldPlace_PartV2(Prefab_Part.TypePart.Roof, GetComponent<Transform>()),
            new FieldPlace_PartV2(Prefab_Part.TypePart.Field, GetComponent<Transform>())
        };

        _fieldPlace_Part[0].OnUpdate += UpdateValue;
        _fieldPlace_Part[1].OnUpdate += UpdateValue;
        _fieldPlace_Part[2].OnUpdate += UpdateValue;

        _statOfFieldPlace = new List<Stat>();
        _oldStatOfFieldPlace = new List<Stat>();

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            _statOfFieldPlace.Add(new Stat((Prefab_Part.Bonus)i));
            _oldStatOfFieldPlace.Add(new Stat((Prefab_Part.Bonus)i));
        }

        _handlerFieldPlace = GameObject.FindObjectOfType<HandlerFieldPlace>();
    }

    public void SetPartForBuilding(BluePoint_Part bluePointPart)
    {
        int intTypePart = (int)bluePointPart.TypeOfPart;

        _currentPartForBuilding = _fieldPlace_Part[intTypePart];
        _currentPartForBuilding.SetBluePointInFieldPlace_Part(bluePointPart);

        _stateOfFieldPlace = StateOfFieldPlace.Building;

        for (int i = 0, imax = _statOfFieldPlace.Count; i < imax; i++)
        {
            _oldStatOfFieldPlace[i].Value = _statOfFieldPlace[i].Value;

            _statOfFieldPlace[i].Value = (_fieldPlace_Part[0].GetNewStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetNewStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetNewStatOfFieldPlace[i].Value) *
                (1 + _fieldPlace_Part[0].GetNewSubStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetNewSubStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetNewSubStatOfFieldPlace[i].Value);
        }

        _currentBuildPoint = 0f;
        _maxBuildPoint = bluePointPart.ProgressionPart[0].CountToBuild;

        _handlerFieldPlace.onStartBuild.Invoke();
    }

    public void BuildPoint()
    {
        _currentBuildPoint++;

        _currentPartForBuilding.AddPointBuild(_currentBuildPoint / _maxBuildPoint);

       if (_currentBuildPoint >= _maxBuildPoint)
        {
            CheckStateOfFieldPlace(); // FIX
            _handlerFieldPlace.onEndBuild.Invoke();
        }

    }

    public void SetCurrentBuildForLeveling(FieldPlace_PartV2 PartForBuild)
    {
        _currentPartForBuilding = PartForBuild;
        _currentPartForBuilding.SetBuild();

        _stateOfFieldPlace = StateOfFieldPlace.Building;

        for (int i = 0, imax = _statOfFieldPlace.Count; i < imax; i++)
        {
            _oldStatOfFieldPlace[i].Value = _statOfFieldPlace[i].Value;

            _statOfFieldPlace[i].Value = (_fieldPlace_Part[0].GetNewStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetNewStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetNewStatOfFieldPlace[i].Value) *
                (1 + _fieldPlace_Part[0].GetNewSubStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetNewSubStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetNewSubStatOfFieldPlace[i].Value);
        }

        _currentBuildPoint = 0f;

        _maxBuildPoint = _currentPartForBuilding.GetBluePointPart.ProgressionPart[_currentPartForBuilding.GetCurrentLevelProgression].CountToBuild;

        _handlerFieldPlace.onStartBuild.Invoke();
    }

    private void CheckStateOfFieldPlace()
    {
        if (_fieldPlace_Part[0].GetStateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Working &&
            _fieldPlace_Part[1].GetStateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Working &&
            _fieldPlace_Part[2].GetStateOfFieldPlacePart == FieldPlace_PartV2.StateOfFieldPlacePart.Working)
        {
            _stateOfFieldPlace = StateOfFieldPlace.Working;

            StartCoroutine(TimeCoin());
        }
        else
        {
            _stateOfFieldPlace = StateOfFieldPlace.NotWorking;
        }

        for (int i = 0, imax = _statOfFieldPlace.Count; i < imax; i++)
        {
            //_statOfFieldPlace[i].Value = (_fieldPlace_Part[0].GetStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetStatOfFieldPlace[i].Value) *
                //(1 + _fieldPlace_Part[0].GetSubStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetSubStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetSubStatOfFieldPlace[i].Value);
        }
    }

    public void ClickCoin()
    {
        Player.AddCoin(_statOfFieldPlace[(int)Prefab_Part.Bonus.ClickCoin].Value);
        Player.AddEXP(_statOfFieldPlace[(int)Prefab_Part.Bonus.ClickExp].Value);
    }

    public void SellHouse()
    {
        StopCoroutine(TimeCoin());

        Player.AddCoin(_statOfFieldPlace[(int)Prefab_Part.Bonus.SellCoin].Value);
        Player.AddEXP(_statOfFieldPlace[(int)Prefab_Part.Bonus.SellExp].Value);

        _stateOfFieldPlace = StateOfFieldPlace.NotWorking;

        CollectBank();

        for (int i = 0, imax = _statOfFieldPlace.Count; i < imax; i++)
        {
            _statOfFieldPlace[i].Value = 0f;
            _oldStatOfFieldPlace[i].Value = 0f;
        }

        _fieldPlace_Part[0].SetEmptyFieldPlace_Part();
        _fieldPlace_Part[1].SetEmptyFieldPlace_Part();
        _fieldPlace_Part[2].SetEmptyFieldPlace_Part();

        _handlerFieldPlace.onSellCoin.Invoke();
    }

    public void OnClickFieldPlace()
    {
        _handlerFieldPlace.HandlerClick(this);
    }

    public void UpdateValue()
    {
        for (int i = 0, imax = _statOfFieldPlace.Count; i < imax; i++)
        {
            _statOfFieldPlace[i].Value = (_fieldPlace_Part[0].GetNewStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetNewStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetNewStatOfFieldPlace[i].Value) *
                (1 + _fieldPlace_Part[0].GetNewSubStatOfFieldPlace[i].Value + _fieldPlace_Part[1].GetNewSubStatOfFieldPlace[i].Value + _fieldPlace_Part[2].GetNewSubStatOfFieldPlace[i].Value);
        }
    }

    public void CollectBank()
    {
        Player.AddCoin(_bankCoin);
        Player.AddEXP(_bankEXP);

        _bankCoin = 0f;
        _bankEXP = 0f;

        _addBankCoin?.Invoke(_bankCoin, _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxCoin].Value);
        _addBankEXP?.Invoke(_bankEXP, _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxExp].Value);
    }

    IEnumerator TimeCoin()
    {
        while (_stateOfFieldPlace == StateOfFieldPlace.Working)
        {
            //Player.AddCoin(_statOfFieldPlace[(int)Prefab_Part.Bonus.ClickCoin].Value);
            //Player.AddEXP(_statOfFieldPlace[(int)Prefab_Part.Bonus.ClickExp].Value);

            if (_bankCoin < _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxCoin].Value)
            {
                _bankCoin += _statOfFieldPlace[(int)Prefab_Part.Bonus.ClickCoin].Value;

                if (_bankCoin > _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxCoin].Value) _bankCoin = _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxCoin].Value;

                _addBankCoin?.Invoke(_bankCoin, _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxCoin].Value);
            }
            if (_bankEXP < _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxExp].Value)
            {
                _bankEXP += _statOfFieldPlace[(int)Prefab_Part.Bonus.ClickExp].Value;

                if (_bankEXP > _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxExp].Value) _bankEXP = _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxExp].Value;

                _addBankEXP?.Invoke(_bankEXP, _statOfFieldPlace[(int)Prefab_Part.Bonus.MaxExp].Value);
            }

            yield return new WaitForSeconds(1f);
        }
    }
}

[System.Serializable]
public class FieldPlace_PartV2
{
    [System.Serializable]
    public enum StateOfFieldPlacePart
    {
        Empty,
        Building,
        Working
    }

    private protected Prefab_Part.TypePart _typePart;

    private protected Image _imageRendererOfPart, _imageRendererOfShadowPart;
    private protected Image _helperImageRendererOfPart_Left, _helperImageRendererOfPart_Right;
   
    private protected StateOfFieldPlacePart _stateOfFieldPlacePart;
    
    private protected BluePoint_Part _bluePointPart;

    private protected int _currentLevelProgression;
    private protected float _progressBuild;

    private protected UnityAction _actionForSetBuilding, _actionBuldingPoint, _actionEndBuilding;

    private protected Sprite _spriteOfPartNew, _spriteOfPartOld;

    private protected List<Stat> _localStatOfBluePointPart, _localSubStatOfBluePointPart;
    private protected List<Stat> _NewStatOfFieldPlace, _NewSubStatOfFieldPlace;
    private protected List<Stat> _OldStatOfFieldPlace, _OldSubStatOfFieldPlace;

    public int GetCurrentLevelProgression { get => _currentLevelProgression; }
    public StateOfFieldPlacePart GetStateOfFieldPlacePart { get => _stateOfFieldPlacePart; }
    public BluePoint_Part GetBluePointPart { get => _bluePointPart; }
    public Sprite GetSpriteOfPartNew { get => _spriteOfPartNew; }
    public Sprite GetSpriteOfPartOld { get => _spriteOfPartOld; }
    public Prefab_Part.TypePart getTypePart { get => _typePart; }

    public List<Stat> GetNewStatOfFieldPlace { get => _NewStatOfFieldPlace; }
    public List<Stat> GetNewSubStatOfFieldPlace { get => _NewSubStatOfFieldPlace; }

    public List<Stat> GetOldStatOfFieldPlace { get => _OldStatOfFieldPlace; }
    public List<Stat> GetOldSubStatOfFieldPlace { get => _OldSubStatOfFieldPlace; }

    public UnityAction OnUpdate;

    public FieldPlace_PartV2(Prefab_Part.TypePart typePart, Transform fieldPlace)
    {
        _typePart = typePart;

        _localStatOfBluePointPart = new List<Stat>();
        _localSubStatOfBluePointPart = new List<Stat>();

        _NewStatOfFieldPlace = new List<Stat>();
        _NewSubStatOfFieldPlace = new List<Stat>();

        _OldStatOfFieldPlace = new List<Stat>();
        _OldSubStatOfFieldPlace = new List<Stat>();

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            _localStatOfBluePointPart.Add(new Stat((Prefab_Part.Bonus)i));
            _localSubStatOfBluePointPart.Add(new Stat((Prefab_Part.Bonus)i));

            _NewStatOfFieldPlace.Add(new Stat((Prefab_Part.Bonus)i));
            _NewSubStatOfFieldPlace.Add(new Stat((Prefab_Part.Bonus)i));

            _OldStatOfFieldPlace.Add(new Stat((Prefab_Part.Bonus)i));
            _OldSubStatOfFieldPlace.Add(new Stat((Prefab_Part.Bonus)i));
        }

        switch (_typePart)
        {
            case Prefab_Part.TypePart.Block:
                {
                    _imageRendererOfShadowPart = fieldPlace.Find("Block Shadow").GetComponent<Image>();

                    _imageRendererOfPart = _imageRendererOfShadowPart.transform.Find("Block").GetComponent<Image>();

                    _actionForSetBuilding += SetBuildBlockOrField;
                    _actionBuldingPoint += SetBuildingProgressBlockOrField;
                    break;
                }
            case Prefab_Part.TypePart.Field:
                {
                    _imageRendererOfShadowPart = fieldPlace.Find("Field Shadow").GetComponent<Image>();

                    _imageRendererOfPart = _imageRendererOfShadowPart.transform.Find("Field").GetComponent<Image>();

                    _actionForSetBuilding += SetBuildBlockOrField;
                    _actionBuldingPoint += SetBuildingProgressBlockOrField;
                    break;
                }
            case Prefab_Part.TypePart.Roof:
                {
                    _imageRendererOfShadowPart = fieldPlace.Find("Roof Shadow").GetComponent<Image>();

                    _imageRendererOfPart = _imageRendererOfShadowPart.transform.Find("Roof").GetComponent<Image>();
                    _helperImageRendererOfPart_Left = _imageRendererOfShadowPart.transform.Find("An.Help Left Roof").GetComponent<Image>();
                    _helperImageRendererOfPart_Right = _imageRendererOfShadowPart.transform.Find("An.Help Right Roof").GetComponent<Image>();

                    _actionForSetBuilding += SetBuildRoof;
                    _actionBuldingPoint += SetBuildingProgressRoof;
                    break;
                }
        }
    }


    public void SetBluePointInFieldPlace_Part(BluePoint_Part BluePointPart)
    {
        _bluePointPart = BluePointPart;

        _spriteOfPartNew = _bluePointPart.ProgressionPart[0].Sprite;
        _spriteOfPartOld = _bluePointPart.ProgressionPart[0].Sprite;

        _currentLevelProgression = 0;

        _stateOfFieldPlacePart = StateOfFieldPlacePart.Building;

        _localStatOfBluePointPart = new List<Stat>(BluePointPart.MainStat);
        _localSubStatOfBluePointPart = new List<Stat>(BluePointPart.SubStat);

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            _NewStatOfFieldPlace[i].Value = _localStatOfBluePointPart[i].Value * BluePointPart.countDiff[_currentLevelProgression];
            _NewSubStatOfFieldPlace[i].Value = _localSubStatOfBluePointPart[i].Value * BluePointPart.countDiff[_currentLevelProgression];
        }

        //Debug.Log(string.Format("{0} sell, {1} lvl Prog., {2} mod % ", _statOfFieldPlace[0].Value, _currentLevelProgression, _bluePointPart.countDiff[_currentLevelProgression]));

        BluePointPart.OnUpdate += UpdateValue;
        _actionForSetBuilding.Invoke();
    }
    public void AddPointBuild(float progress)
    {
        _progressBuild = progress;

        if (progress >= 1f)
        {
            _currentLevelProgression++;
            _stateOfFieldPlacePart = StateOfFieldPlacePart.Working;
        }

        _actionBuldingPoint.Invoke();
    }
    public void SetBuild()
    {

        _spriteOfPartNew = _bluePointPart.ProgressionPart[_currentLevelProgression].Sprite;
        _spriteOfPartOld = _bluePointPart.ProgressionPart[_currentLevelProgression - 1].Sprite;

        _stateOfFieldPlacePart = StateOfFieldPlacePart.Building;

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            _OldStatOfFieldPlace[i].Value = _localStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression - 1];
            _OldSubStatOfFieldPlace[i].Value = _localSubStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression - 1];

            _NewStatOfFieldPlace[i].Value = _localStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression];
            _NewSubStatOfFieldPlace[i].Value = _localSubStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression];
        }

        //Debug.Log(string.Format("{0} sell, {1} lvl Prog., {2} mod % ", _statOfFieldPlace[0].Value, _currentLevelProgression, _bluePointPart.countDiff[_currentLevelProgression]));


        _actionForSetBuilding.Invoke();
    }
    public void SetEmptyFieldPlace_Part()
    {
        _stateOfFieldPlacePart = StateOfFieldPlacePart.Empty;
        _imageRendererOfShadowPart.gameObject.SetActive(false);

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            _NewStatOfFieldPlace[i].Value = 0f;
            _NewSubStatOfFieldPlace[i].Value = 0f;

            _OldStatOfFieldPlace[i].Value = 0f;
            _OldSubStatOfFieldPlace[i].Value = 0f;
        }

    }


    private void SetBuildBlockOrField()
    {
        _imageRendererOfPart.gameObject.SetActive(true);
        _imageRendererOfShadowPart.gameObject.SetActive(true);

        _imageRendererOfPart.sprite = _spriteOfPartNew;
        _imageRendererOfShadowPart.sprite = _spriteOfPartOld;

        _imageRendererOfPart.type = Image.Type.Filled;
        _imageRendererOfShadowPart.type = Image.Type.Simple;

        _imageRendererOfPart.fillAmount = 0f;
    }

    private void SetBuildRoof()
    {
        _imageRendererOfPart.gameObject.SetActive(true);
        _imageRendererOfShadowPart.gameObject.SetActive(true);
        _helperImageRendererOfPart_Left.gameObject.SetActive(true);
        _helperImageRendererOfPart_Right.gameObject.SetActive(true);


        _imageRendererOfPart.sprite = _spriteOfPartNew;
        _helperImageRendererOfPart_Left.sprite = _spriteOfPartNew;
        _helperImageRendererOfPart_Right.sprite = _spriteOfPartNew;
        _imageRendererOfShadowPart.sprite = _spriteOfPartOld;

        _imageRendererOfPart.type = Image.Type.Filled;
        _helperImageRendererOfPart_Left.type = Image.Type.Filled;
        _helperImageRendererOfPart_Right.type = Image.Type.Filled;
        _imageRendererOfShadowPart.type = Image.Type.Simple;

        _imageRendererOfPart.fillAmount = 0f;
        _helperImageRendererOfPart_Left.fillAmount = 0f;
        _helperImageRendererOfPart_Right.fillAmount = 0f;
    }

    private void SetBuildingProgressBlockOrField() => _imageRendererOfPart.fillAmount = Mathf.Min(_progressBuild, 1f);
 
    private void SetBuildingProgressRoof()
    {

        if (_progressBuild < 0.5f)
        {
            float d = _progressBuild * 0.5f;
            _helperImageRendererOfPart_Left.fillAmount = d;
            _helperImageRendererOfPart_Right.fillAmount = d;
        }
        else
        {
            _helperImageRendererOfPart_Left.fillAmount = 0f;
            _helperImageRendererOfPart_Right.fillAmount = 0f;
            _imageRendererOfPart.fillAmount = _progressBuild;
        }



    }


    private void UpdateValue()
    {
        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            if (_currentLevelProgression != 0)
            {
                _OldStatOfFieldPlace[i].Value = _localStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression - 1];
                _OldSubStatOfFieldPlace[i].Value = _localSubStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression - 1];
            }

            if (_currentLevelProgression != _bluePointPart.CountLevelOfProgression)
            {
                _NewStatOfFieldPlace[i].Value = _localStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression];
                _NewSubStatOfFieldPlace[i].Value = _localSubStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression];
            }
            else
            {
                _NewStatOfFieldPlace[i].Value = _localStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression - 1];
                _NewSubStatOfFieldPlace[i].Value = _localSubStatOfBluePointPart[i].Value * _bluePointPart.countDiff[_currentLevelProgression - 1];
            }
        }

        OnUpdate.Invoke();
    }
}
