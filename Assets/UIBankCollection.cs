using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBankCollection : MonoBehaviour
{
    private protected FieldPlaceV2 _currentFieldPlace;

    [SerializeField] private protected Text _textBankCoin, _textBankEXP;
    public UnityEvent StateWorking, StateNotWorking;

    public void CheckStateAndSetCurrentFieldPalce()
    {
        if (HandlerFieldPlace.GetCurrentZoomedFieldPlace.GetStateOfFieldPlace == FieldPlaceV2.StateOfFieldPlace.Working)
        {
            StateWorking.Invoke();
        }
        else
        {
            StateNotWorking.Invoke();
        }
    }

    public void AddListener()
    {
        if (_currentFieldPlace == null || HandlerFieldPlace.GetCurrentZoomedFieldPlace != _currentFieldPlace)
        {
            _currentFieldPlace = HandlerFieldPlace.GetCurrentZoomedFieldPlace;

            _currentFieldPlace._addBankCoin += UpdateValueBankCoin;
            _currentFieldPlace._addBankEXP += UpdateValueBankEXP;

            UpdateValueBankCoin(_currentFieldPlace.bankCoin, _currentFieldPlace.GetStatOfFieldPlace[(int)Prefab_Part.Bonus.MaxCoin].Value);
            UpdateValueBankEXP(_currentFieldPlace.bankEXP, _currentFieldPlace.GetStatOfFieldPlace[(int)Prefab_Part.Bonus.MaxExp].Value);
        }
    }

    public void RemoveListener()
    {
        if (_currentFieldPlace != null || HandlerFieldPlace.GetCurrentZoomedFieldPlace == _currentFieldPlace)
        {
            _currentFieldPlace._addBankCoin -= UpdateValueBankCoin;
            _currentFieldPlace._addBankEXP -= UpdateValueBankEXP;

            _currentFieldPlace = null;
        }
    }

    public void UpdateValueBankCoin(float currentCount, float maxCount)
    {
        _textBankCoin.text = string.Format("{0} / {1}", System.Math.Round(currentCount, 2), System.Math.Round(maxCount, 2));
    }

    public void UpdateValueBankEXP(float currentCount, float maxCount)
    {
        _textBankEXP.text = string.Format("{0} / {1}", System.Math.Round(currentCount, 2), System.Math.Round(maxCount, 2));
    }

    public void CollectBank()
    {
        _currentFieldPlace?.CollectBank();
    }
}
