using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    [SerializeField] private GameObject _sprite;

    [SerializeField] private float _positionX;
    [SerializeField] private float _positionY;

    [SerializeField] private GameObject UI_MainScreen;
    [SerializeField] private GameObject UI_HouseScreen;
    [SerializeField] private ButtonHouseScreen Button_HouseScreen;

    [SerializeField] private SpriteRenderer Block, Krysha, Field;

    private static bool CanIMove = false;

    private void MoveImage(RectTransform rectTransformImage, float distance)
    {
        float b = rectTransformImage.offsetMin.y;

        rectTransformImage.offsetMin = new Vector3(0, distance);
        rectTransformImage.offsetMax = new Vector3(0, rectTransformImage.offsetMax.y + distance - b);
    }

    public void CallClick()
    {

        CanIMove = false;

        Debug.Log("Allo!");


        StartCoroutine(enumerator());

        Button_HouseScreen.renderBlock = Block;
        Button_HouseScreen.renderKrysha = Krysha;
        Button_HouseScreen.renderField = Field;


        //_sprite.transform.localPosition = new Vector3(14.65f, 0);
        //_sprite.transform.localScale = new Vector3(3, 3, 0);

        //gameObject.SetActive(false);

    }

    public void CallClickBack()
    {

        CanIMove = true;

        Debug.Log("A110!");

        StartCoroutine(BackClick());

        //_sprite.transform.localPosition = new Vector3(14.65f, 0);
        //_sprite.transform.localScale = new Vector3(3, 3, 0);

        //gameObject.SetActive(false);

    }

    IEnumerator enumerator()
    {
        StopCoroutine(BackClick());


        while (_sprite.transform.localScale.x < 3f && !CanIMove)
        {

            if (_sprite.transform.localScale.x >= 3f - 0.01f)
            {
                _sprite.transform.localPosition = new Vector3(_positionX, _positionY);
                _sprite.transform.localScale = new Vector3(3f, 3f, 0f);

                MoveImage(UI_MainScreen.GetComponent<RectTransform>(), -129);
                MoveImage(UI_HouseScreen.GetComponent<RectTransform>(), 0);

                //UI_MainScreen.transform.position = new Vector3(0, -19f);
                //UI_HouseScreen.transform.position = new Vector3(0, -15f);



            }
            else
            {
                _sprite.transform.localPosition = new Vector3(Mathf.Lerp(_sprite.transform.localPosition.x, _positionX, Time.deltaTime * 2.5f), Mathf.Lerp(_sprite.transform.localPosition.y, _positionY, Time.deltaTime * 2.5f));
                _sprite.transform.localScale = new Vector3(Mathf.Lerp(_sprite.transform.localScale.x, 3f, Time.deltaTime * 2.5f), Mathf.Lerp(_sprite.transform.localScale.y, 3f, Time.deltaTime * 2.5f), 0f);

                MoveImage(UI_MainScreen.GetComponent<RectTransform>(), Mathf.Lerp(UI_MainScreen.GetComponent<RectTransform>().offsetMin.y, -129, Time.deltaTime * 2.5f));
                MoveImage(UI_HouseScreen.GetComponent<RectTransform>(), Mathf.Lerp(UI_HouseScreen.GetComponent<RectTransform>().offsetMin.y, 0, Time.deltaTime * 2.5f));

                //UI_MainScreen.transform.position = new Vector3(0f, Mathf.Lerp(UI_MainScreen.transform.position.y, -19f, Time.deltaTime * 2.5f));
                //UI_HouseScreen.transform.position = new Vector3(0f, Mathf.Lerp(UI_HouseScreen.transform.position.y, -15f, Time.deltaTime * 2.5f));
            }

            Debug.Log("Exit - 1");
            yield return null;
        }

        CanIMove = true;
        Debug.Log("Exit - 2");
        yield return null;
    }

    IEnumerator BackClick()
    {
        StopCoroutine(enumerator());

        while (_sprite.transform.localScale.x > 1f && CanIMove)
        {

            if (_sprite.transform.localScale.x <= 1.01f)
            {
                _sprite.transform.localPosition = new Vector3(0f, 0f);
                _sprite.transform.localScale = new Vector3(1f, 1f, 0f);

                MoveImage(UI_MainScreen.GetComponent<RectTransform>(), 0);
                MoveImage(UI_HouseScreen.GetComponent<RectTransform>(), -129);
            }
            else
            {
                _sprite.transform.localPosition = new Vector3(Mathf.Lerp(_sprite.transform.localPosition.x, 0f, Time.deltaTime * 2.5f), Mathf.Lerp(_sprite.transform.localPosition.y, 0f, Time.deltaTime * 2.5f));
                _sprite.transform.localScale = new Vector3(Mathf.Lerp(_sprite.transform.localScale.x, 1f, Time.deltaTime * 2.5f), Mathf.Lerp(_sprite.transform.localScale.y, 1f, Time.deltaTime * 2.5f), 0f);

                MoveImage(UI_MainScreen.GetComponent<RectTransform>(), Mathf.Lerp(UI_MainScreen.GetComponent<RectTransform>().offsetMin.y, 0, Time.deltaTime * 2.5f));
                MoveImage(UI_HouseScreen.GetComponent<RectTransform>(), Mathf.Lerp(UI_HouseScreen.GetComponent<RectTransform>().offsetMin.y, -129, Time.deltaTime * 2.5f));
            }

            Debug.Log("Exit - 1");
            yield return null;
        }

        CanIMove = false;
        Debug.Log("Exit - 2");
        yield return null;
    }

}
