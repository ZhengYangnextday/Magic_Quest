using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        // GameObject card = Instantiate(Resources.Load("cardSurface") as GameObject);
        // card.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);
        // card.GetComponent<Card>().scale = 8f;

        GameObject modeNHealth = Instantiate(Resources.Load("Test_UI/ModeNHealth/ModeNHealth") as GameObject);
        modeNHealth.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        modeNHealth.GetComponent<ModeNHealth>().unitSize = new Vector2 (1f, 1f);
        // modeNHealth.GetComponent<ModeNHealth>().position = new Vector2 (2f,0f);

        GameObject option0 = Instantiate(Resources.Load("Test_UI/Option/optionSurface") as GameObject);
        option0.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        option0.GetComponent<Option>().unitSize = new Vector2 (0.5f, 0.5f);
        option0.GetComponent<Option>().position = new Vector2 (-2f, 0f);

        GameObject option1 = Instantiate(Resources.Load("Test_UI/Option/optionSurface") as GameObject);
        option1.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        option1.GetComponent<Option>().unitSize = new Vector2 (0.5f, 0.5f);
        option1.GetComponent<Option>().position = new Vector2 (0f, 0f);

        GameObject option2 = Instantiate(Resources.Load("Test_UI/Option/optionSurface") as GameObject);
        option2.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        option2.GetComponent<Option>().unitSize = new Vector2 (0.5f, 0.5f);
        option2.GetComponent<Option>().position = new Vector2 (2f,0f);

        // option.GetComponent<Option>().size = new Vector2 (100f, 100f);
        // option.GetComponent<Option>().position = new Vector2 (0f, 0f);

        // GameObject.Find("ModeNHealth").GetComponent<ModeNHealth>().unitSize = new Vector2 (2f, 2f);
        // Debug.Log(GameObject.Find("ModeNHealth").GetComponent<ModeNHealth>().unitSize);
    }

    private float PixelPerUnit()
    {
        GameObject camera = GameObject.Find("Main Camera");
        float cameraHeight = camera.GetComponent<Camera>().orthographicSize;
        float canvasHeight = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;

        float ppu = canvasHeight/(cameraHeight*2f);
        return ppu;
    }

    private void AlignSize(GameObject gameObject, Vector2 unitSize)
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = unitSize * PixelPerUnit();
    }

    // Update is called once per frame
    void Update()
    {
        //AlignSize(GameObject.Find("ModeNHealth"), new Vector2 (2f,4f));
    }

}
