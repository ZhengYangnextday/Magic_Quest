using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    GameObject cardTest;
    // Start is called before the first frame update
    void Start()
    {
        cardTest = Instantiate(Resources.Load("Test_UI/CardIcon/CardIcon") as GameObject);
        cardTest.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        cardTest.GetComponent<CardIcon>().SetUnitSize(new Vector2(5f, 5f));
        cardTest.GetComponent<CardIcon>().SetPosition(new Vector2(0f, 0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
