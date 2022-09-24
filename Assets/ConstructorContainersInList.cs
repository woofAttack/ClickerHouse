using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructorContainersInList : MonoBehaviour
{
    public AllBlock AllPartOfGame;
    public List<BluePoint_Part> BluePoints_Block, BluePoints_Roof, BluePoints_Field;

    [SerializeField] private protected Transform _listOfContainersForEnhanceBlock;
    [SerializeField] private protected Transform _listOfContainersForEnhanceRoof;
    [SerializeField] private protected Transform _listOfContainersForEnhanceField;
    [SerializeField] private protected GameObject _containersForEnhance;

    [SerializeField] private protected Transform _listOfContainersForChooseBlock;
    [SerializeField] private protected Transform _listOfContainersForChooseRoof;
    [SerializeField] private protected Transform _listOfContainersForChooseField;
    [SerializeField] private protected GameObject _containersForChoose;


    private void Awake()
    {


        for (int i = 0, imax = AllPartOfGame.Block.Length; i < imax; i++) // DELETE LATER
        {
            BluePoints_Block.Add(new BluePoint_Part(AllPartOfGame.Block[i]));
            Instantiate<GameObject>(_containersForEnhance, _listOfContainersForEnhanceBlock).GetComponent<ContainerBluepointForEnhance>().SetBluepointPart(BluePoints_Block[i]);
            Instantiate<GameObject>(_containersForChoose, _listOfContainersForChooseBlock).GetComponent<ContainerBluepointForEnhance>().SetBluepointPart(BluePoints_Block[i]);
        }

        for (int i = 0, imax = AllPartOfGame.Roof.Length; i < imax; i++) // DELETE LATER
        {
            BluePoints_Roof.Add(new BluePoint_Part(AllPartOfGame.Roof[i]));
            Instantiate<GameObject>(_containersForEnhance, _listOfContainersForEnhanceRoof).GetComponent<ContainerBluepointForEnhance>().SetBluepointPart(BluePoints_Roof[i]);
            Instantiate<GameObject>(_containersForChoose, _listOfContainersForChooseRoof).GetComponent<ContainerBluepointForEnhance>().SetBluepointPart(BluePoints_Roof[i]);
        }

        for (int i = 0, imax = AllPartOfGame.Field.Length; i < imax; i++) // DELETE LATER
        {
            BluePoints_Field.Add(new BluePoint_Part(AllPartOfGame.Field[i]));
            Instantiate<GameObject>(_containersForEnhance, _listOfContainersForEnhanceField).GetComponent<ContainerBluepointForEnhance>().SetBluepointPart(BluePoints_Field[i]);
            Instantiate<GameObject>(_containersForChoose, _listOfContainersForChooseField).GetComponent<ContainerBluepointForEnhance>().SetBluepointPart(BluePoints_Field[i]);
        }



    }


}
