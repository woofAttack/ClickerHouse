using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIUp : MonoBehaviour
{

    [SerializeField] private protected Text _textCountCoin;

    [SerializeField] private protected Text _textProgressEXP;
    [SerializeField] private protected Text _textLevelPlayer;
    [SerializeField] private protected Image _imageProgress1, _imageProgress2;


    private void Awake()
    {
        UpdateUICoin();
        UpdateUIEXP();

        Player.OnAddCoin += UpdateUICoin;
        Player.OnAddEXP += UpdateUIEXP;
    }

    private void UpdateUICoin()
    {
        _textCountCoin.text = string.Format("{0}", Math.Round(Player.GetCoinOfPlayer));
    }

    private void UpdateUIEXP()
    {
        float progress = Player.GetEXPOfPlayer / Player.GetMaxOfPlayer;

        _textProgressEXP.text = string.Format("{0}%", Mathf.Round(progress * 100));
        _imageProgress1.fillAmount = progress;
        _imageProgress2.fillAmount = progress;

        _textLevelPlayer.text = string.Format("{0}", Player.GetLevelOfPlayer);
    }


}
