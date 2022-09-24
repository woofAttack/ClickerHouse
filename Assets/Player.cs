using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public AllBlock AllBlockOfGame;
    public List<BluePoint_Part> BluePoints_Block, BluePoints_Roof, BluePoints_Field;

    [SerializeField] private protected GameObject ContentBlockEmptyState, ContentRoofEmptyState, ContentFieldEmptyState;
    [SerializeField] private protected GameObject ContentBlockForEnhance, ContentRoofForEnhance, ContentFieldForEnhance;
    [SerializeField] private protected GameObject ElementOfListPartEmptyState, ElementOfPartForEnhance;

    public static UnityAction OnAddCoin, OnAddEXP, OnLevelUp;
    private protected static float _coinOfPlayer, _expCurrentOfPlayer, _expMaxOfPlayer;
    private protected static int _levelOfPlayer;
    private protected static float _powerOfHammer;
    public static float GetCoinOfPlayer { get => _coinOfPlayer; }
    public static float GetEXPOfPlayer { get => _expCurrentOfPlayer; }
    public static float GetMaxOfPlayer { get => _expMaxOfPlayer; }
    public static int GetLevelOfPlayer { get => _levelOfPlayer; }
    public static float GetPowerOfHammer { get => _powerOfHammer; }


    private void Awake()
    {
        _levelOfPlayer = 1;
        _powerOfHammer = 1;
        _expMaxOfPlayer = _levelOfPlayer * 100;

        for (int i = 0, imax = AllBlockOfGame.Block.Length; i < imax; ++i)
        {
            //BluePoints_Block.Add(new BluePoint_Part(AllBlockOfGame.Block[i]));
            //Instantiate<GameObject>(ElementOfListPartEmptyState, ContentBlockEmptyState.transform).GetComponent<ElementOfListForEmptyState>().SetCurrentBluePointPart(BluePoints_Block[i]);
            //Instantiate<GameObject>(ElementOfPartForEnhance, ContentBlockForEnhance.transform).GetComponent<ElelemtOfPartForEnhance>().SetBluePoint_Part(BluePoints_Block[i]);
        }

        //ContentBlockForEnhance.GetComponent<HandlerClickOnElement>().ConstructElementOfClick();


        for (int i = 0, imax = AllBlockOfGame.Roof.Length; i < imax; ++i)
        {
            //BluePoints_Roof.Add(new BluePoint_Part(AllBlockOfGame.Roof[i]));
//Instantiate<GameObject>(ElementOfListPartEmptyState, ContentRoofEmptyState.transform).GetComponent<ElementOfListForEmptyState>().SetCurrentBluePointPart(BluePoints_Roof[i]);
            //Instantiate<GameObject>(ElementOfPartForEnhance, ContentRoofForEnhance.transform).GetComponent<ElelemtOfPartForEnhance>().SetBluePoint_Part(BluePoints_Roof[i]);
        }

       // ContentRoofForEnhance.GetComponent<HandlerClickOnElement>().ConstructElementOfClick();

        for (int i = 0, imax = AllBlockOfGame.Field.Length; i < imax; ++i)
        {
            //BluePoints_Field.Add(new BluePoint_Part(AllBlockOfGame.Field[i]));
            //Instantiate<GameObject>(ElementOfListPartEmptyState, ContentFieldEmptyState.transform).GetComponent<ElementOfListForEmptyState>().SetCurrentBluePointPart(BluePoints_Field[i]);
            //Instantiate<GameObject>(ElementOfPartForEnhance, ContentFieldForEnhance.transform).GetComponent<ElelemtOfPartForEnhance>().SetBluePoint_Part(BluePoints_Field[i]);
        }

        //ContentFieldForEnhance.GetComponent<HandlerClickOnElement>().ConstructElementOfClick();

        //ContentBlockEmptyState.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -(BluePoints_Block.Count * 96));
        //ContentRoofEmptyState.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -(BluePoints_Roof.Count * 96));
        //ContentFieldEmptyState.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -(BluePoints_Field.Count * 96));

        //ContentBlockForEnhance.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -(BluePoints_Block.Count * 96));
        //ContentRoofForEnhance.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -(BluePoints_Roof.Count * 96));
        //ContentFieldForEnhance.GetComponent<RectTransform>().offsetMin = new Vector2(0f, -(BluePoints_Field.Count * 96));

        //Debug.Log(ppp.NameOfPart);

    }

    public static void AddCoin(float value)
    {
        _coinOfPlayer += value;
        OnAddCoin.Invoke();
    }

    public static void AddEXP(float value)
    {
        _expCurrentOfPlayer += value;

        while (_expCurrentOfPlayer >= _expMaxOfPlayer)
        {
            _expCurrentOfPlayer -= _expMaxOfPlayer;
            _levelOfPlayer++;
            _expMaxOfPlayer = _levelOfPlayer * 100;
            OnLevelUp?.Invoke();
        }

        OnAddEXP.Invoke();
    }

}
