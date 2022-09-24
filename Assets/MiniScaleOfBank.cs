using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniScaleOfBank : MonoBehaviour
{

    [SerializeField] private protected FieldPlaceV2 _fieldPlaceComponent;

    [SerializeField] private protected Image _scaleCoin;
    [SerializeField] private protected Image _scaleExp;

    private protected void Awake()
    {
        _fieldPlaceComponent._addBankCoin += SetValueScaleCoin;
        _fieldPlaceComponent._addBankEXP += SetValueScaleExp;
    }

    private protected void SetValueScaleCoin(float currentCount, float maxCount)
    {
        _scaleCoin.fillAmount = currentCount / maxCount;
    }

    private protected void SetValueScaleExp(float currentCount, float maxCount)
    {
        _scaleExp.fillAmount = currentCount / maxCount;
    }



}
