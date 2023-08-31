using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
// TODO everything
{
    private Vector2 position;// = this.GetComponent<RectTransform>().anchoredPosition;
    public float scale = 1f;
    private float scaleBase;

    private GameObject canvas;// = GameObject.Find("Canvas");
    //private RectTransform canvas_transform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    
    #region component
    private GameObject cardBackground;
    private GameObject text;
    private GameObject textBackground;
    private GameObject title;
    private GameObject titleBackground;
    private GameObject element;
    private GameObject level;
    private GameObject image;
    #endregion

    #region constant bias 
    private Vector2 cardBias;// = new Vector2 (0f, 0f);
    private Vector2 textBias;// = new Vector2 (0f, -65f);
    private Vector2 titleBias;// = new Vector2 (0f, 75f);
    private Vector2 elementBias;// = new Vector2 (-45f, 75f);
    private Vector2 levelBias;// = new Vector2 (45f, 75f);
    private Vector2 imageBias;// = new Vector2 (0f, 10f);
    #endregion

    void Awake() 
    {
        canvas = GameObject.Find("Canvas");
        cardBias = new Vector2 (0f, 0f);
        textBias = new Vector2 (0f, -65f);
        titleBias = new Vector2 (0f, 75f);
        elementBias = new Vector2 (-45f, 75f);
        levelBias = new Vector2 (45f, 75f);
        imageBias = new Vector2 (0f, 10f);
    }

    private void LocateCard(Vector2 position_, float scale_)
    {
        void LocateComponent(GameObject gameobject__, Vector2 position__, float scale__)
        {
            gameobject__.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
            gameobject__.GetComponent<RectTransform>().anchoredPosition = position__;
            gameobject__.GetComponent<RectTransform>().sizeDelta = new Vector2 (scale__, scale__);
        }

        LocateComponent(cardBackground, position_, scale_);
        LocateComponent(text, position_ + textBias * scale_, scale_);
        LocateComponent(textBackground, position_ + textBias * scale_, scale_);
        LocateComponent(title, position_ + titleBias * scale_, scale_);
        LocateComponent(titleBackground, position_ + titleBias * scale_, scale_);
        LocateComponent(element, position_ + elementBias * scale_, scale_);
        LocateComponent(level, position_ + levelBias * scale_, scale_);
        LocateComponent(image, position_ + imageBias * scale_, scale_);
    }

    private float PixelPerUnit()
    {
        GameObject camera = GameObject.Find("Main Camera");
        float cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        float canvasHeight = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;

        float ppu = canvasHeight/cameraHeight;
        return ppu;
    }

    // Start is called before the first frame update
    void Start()
    {
        scaleBase = 180f;
        
        cardBackground = Instantiate(Resources.Load("Test_UI/Card/cardBackground") as GameObject);
        text = Instantiate(Resources.Load("Test_UI/Card/text") as GameObject);
        textBackground = Instantiate(Resources.Load("Test_UI/Card/textBackground") as GameObject);
        title = Instantiate(Resources.Load("Test_UI/Card/title") as GameObject);
        titleBackground = Instantiate(Resources.Load("Test_UI/Card/titleBackground") as GameObject);
        element = Instantiate(Resources.Load("Test_UI/Card/element") as GameObject);
        level = Instantiate(Resources.Load("Test_UI/Card/level") as GameObject);
        image = Instantiate(Resources.Load("Test_UI/Card/image") as GameObject);

        LocateCard(position, scale*PixelPerUnit()/scaleBase);//scale/scaleBase*BitPerUnit()
        this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2 (scale*PixelPerUnit()/scaleBase, scale*PixelPerUnit()/scaleBase);
        //Debug.Log(PixelPerUnit());
        position = this.GetComponent<RectTransform>().anchoredPosition;//new Vector2 (0f, 0f); 
    }

    // Update is called once per frame
    void Update()
    {
        position = this.GetComponent<RectTransform>().anchoredPosition;
        LocateCard(position, scale*PixelPerUnit()/scaleBase);
    }
}
