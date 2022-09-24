using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Prefab_Part : ScriptableObject
{
    public enum Bonus
    {
        SellCoin = 6,
        SellExp = 7,
        MaxCoin = 4,
        MaxExp = 5,
        ClickCoin = 0,
        ClickExp = 1,
        TimeCoin = 2,
        TimeExp = 3
    }
    public static int CountBonus => typeof(Bonus).GetEnumNames().Length;

    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Legendary
    }

    public enum TypePart
    {
        Block,
        Roof,
        Field
    }

    public string NameOfPart;
    public Rarity RarityOfPart;
    public int LevelOfPart;

    public Sprite MainSprite;
    public TypePart TypeOfPart;

    public List<Stat> MainStats, SubStats;
    public List<OnePart> Parts;

}

[System.Serializable]
public class Stat
{
    public Prefab_Part.Bonus Bonus;
    public float Value;
    public type typeOfStat;

    public enum type
    {
        Main,
        Sub
    }

    public Stat() { }
    public Stat(Prefab_Part.Bonus bonus)
    {
        this.Bonus = bonus;
        Value = 0f;
    }
}

[System.Serializable]
public class OnePart
{
    public Sprite Sprite;
    public float CountToBuild;

    public float Diff;

    public List<Stat> MainStats, SubStats;
}
