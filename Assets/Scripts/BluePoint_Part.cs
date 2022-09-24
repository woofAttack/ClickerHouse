using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class BluePoint_Part
{

    public readonly Prefab_Part BasePrefabPart;

    public readonly string NameOfPart;
    public readonly Prefab_Part.Rarity RarityOfPart;
    public readonly Sprite MainSpriteOfPart;
    public readonly Prefab_Part.TypePart TypeOfPart;

    public readonly int LevelOfPart;
    public readonly int CountLevelOfProgression;

    public readonly List<OnePart> ProgressionPart;
    public readonly List<Stat> MainStat, SubStat;
    public readonly List<Stat> AllStat, NonZeroStat;

    public readonly float[] countBuildPointToMax;
    public readonly float[] countDiff;

    private protected int  levelEnhance;
    private protected float localPrice, currentPrice;
    private protected bool _isUnlocked;

    public int GetLevelEnhance { get => levelEnhance; }
    public float GetCurrentPrice { get => currentPrice; }
    public bool GetIsUnlocked { get => _isUnlocked; }



    public UnityEvent UpdateValueOfBluepointPart;

    public UnityAction OnUpdate, OnUnlocked;

    public void UpdateValue(int countLevel, float price)
    {
        levelEnhance += countLevel;

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            MainStat[i].Value = BasePrefabPart.MainStats[i].Value * (1 + (levelEnhance * 0.1f));
            SubStat[i].Value = BasePrefabPart.SubStats[i].Value * (1 + (levelEnhance * 0.1f));
        }

        Player.AddCoin(-price);
        currentPrice = localPrice * (1 + (levelEnhance * 0.2f));
        

        OnUpdate.Invoke();
    }

    public BluePoint_Part (Prefab_Part PrefabPart)
    {
        BasePrefabPart = PrefabPart;

        NameOfPart = PrefabPart.NameOfPart;
        RarityOfPart = PrefabPart.RarityOfPart;
        MainSpriteOfPart = PrefabPart.MainSprite;
        TypeOfPart = PrefabPart.TypeOfPart;

        LevelOfPart = PrefabPart.LevelOfPart;

        localPrice = 2f;
        currentPrice = 2f;

        ProgressionPart = new List<OnePart>(PrefabPart.Parts);

        MainStat = new List<Stat>();
        SubStat = new List<Stat>();
        AllStat = new List<Stat>();
        NonZeroStat = new List<Stat>();

        for (int i = 0, imax = Prefab_Part.CountBonus; i < imax; i++)
        {
            MainStat.Add(new Stat((Prefab_Part.Bonus)i));
            MainStat[i].Value = PrefabPart.MainStats[i].Value;
            MainStat[i].typeOfStat = Stat.type.Main;

            SubStat.Add(new Stat((Prefab_Part.Bonus)i));
            SubStat[i].Value = PrefabPart.SubStats[i].Value;
            SubStat[i].typeOfStat = Stat.type.Sub;
        }

        if (PrefabPart.Parts != null)
        {
            CountLevelOfProgression = PrefabPart.Parts.Count;

            countBuildPointToMax = new float[CountLevelOfProgression];
            countDiff = new float[CountLevelOfProgression];

            for (int i = 0; i < CountLevelOfProgression; i++)
            {
                countBuildPointToMax[i] = PrefabPart.Parts[i].CountToBuild;
                countDiff[i] = PrefabPart.Parts[i].Diff;
            }
        }
        else
        {
            CountLevelOfProgression = 0;
            Debug.LogWarning("Чертеж " + NameOfPart + " не имеет уровней прогрессии!");
        }

        AllStat.AddRange(MainStat);
        AllStat.AddRange(SubStat);

        for (int i = 0, imax = AllStat.Count; i < imax; i++)
        {
            if (AllStat[i].Value != 0) NonZeroStat.Add(AllStat[i]);
        }

        _isUnlocked = Player.GetLevelOfPlayer >= LevelOfPart;

        if (!_isUnlocked)
        {
            Player.OnLevelUp += SetStateLockOrOpen;
        }
            


    }

    public void SetStateLockOrOpen()
    {
        _isUnlocked = Player.GetLevelOfPlayer >= LevelOfPart;

        if (_isUnlocked)
        {
            Player.OnLevelUp -= SetStateLockOrOpen;
            Debug.Log("ITISPROBLEMA");
            OnUnlocked.Invoke();
        }
    }

}
