using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Option : MonoBehaviour
{
    private Sprite[] sprites;

    private new GameObject camera;
    private float cameraHeight;
    private float cameraWidth;
    private RectTransform rect;


    public Vector2 position;//todo public ?
    public Vector2 unitSize;//todo public ?
    private bool trackStatus;
    private bool setStatus;

    private GameObject attribute;
    private GameObject optionBackground;
    private GameObject level;
    private GameObject quantity;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject canvas = GameObject.Find("Canvas");

        attribute = Instantiate(Resources.Load("Test_UI/Option/attribute") as GameObject);
        attribute.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
        // attribute.GetComponent<RectTransform>().SetParent(this.gameObject.GetComponent<RectTransform>());

        optionBackground = Instantiate(Resources.Load("Test_UI/Option/optionBackground") as GameObject);
        optionBackground.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
        // optionBackground.GetComponent<RectTransform>().SetParent(this.gameObject.GetComponent<RectTransform>());

        level = Instantiate(Resources.Load("Test_UI/Option/level") as GameObject);
        level.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
        // level.GetComponent<RectTransform>().SetParent(this.gameObject.GetComponent<RectTransform>());

        quantity = Instantiate(Resources.Load("Test_UI/Option/quantity") as GameObject);
        quantity.GetComponent<RectTransform>().SetParent(canvas.GetComponent<RectTransform>());
        // quantity.GetComponent<RectTransform>().SetParent(this.gameObject.GetComponent<RectTransform>());

    }
    void Start()
    {
        rect = this.GetComponent<RectTransform>();
        rect.anchoredPosition = position;
        trackStatus = false;
        setStatus = false;

        camera = GameObject.Find("Main Camera");
        cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;// ! unit
        cameraWidth = 2 * camera.GetComponent<Camera>().orthographicSize * camera.GetComponent<Camera>().aspect;
        //rect.sizeDelta = unitSize;
        
        AlignSize(unitSize);
        StablePosition();
    }
    void Update()
    {
        // bool isOverCanvas = EventSystem.current.IsPointerOverGameObject();
        // //currentMousePosition = MousePosition();

        if (InBound(rect.anchoredPosition, unitSize*PixelPerUnit()) && Input.GetKey(KeyCode.Mouse0) && !trackStatus)//isOverCanvas && 
        {
            trackStatus = true;
        }
        if (trackStatus)//isOverCanvas && 
        {
            rect.anchoredPosition = MousePosition();
            if (Input.GetKeyUp(KeyCode.Mouse0)){
                trackStatus = false;

                // if (InBound(new Vector2(0f,0f), new Vector2(400f,200f))){
                //     GameObject attack = Instantiate(Resources.Load("Test_UI/attack") as GameObject);
                //     attack.GetComponent<Transform>().position = MousePosition()/PixelPerUnit();
                //     Destroy(attribute);
                //     Destroy(optionBackground);
                //     Destroy(level);
                //     Destroy(quantity);
                //     Destroy(gameObject);
                // }

                if (InBound(new Vector2(300f,0f), new Vector2(100f,150f))){
                    setStatus = true;
                    this.gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector2(300f,0f);
                    this.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(100f,150f);
                }
                else{setStatus = false;}
                
            }
        }
        
        
        AlignSize(unitSize);
        StablePosition();
    }
    //!########################################################################################
    private void StablePosition()
    // ? keep the position
    {
        if(trackStatus){return;}
        if(setStatus){return;}
        rect.anchoredPosition = position*PixelPerUnit() + new Vector2 (rect.sizeDelta.x / 2, rect.sizeDelta.y / 2 - PixelPerUnit()*cameraHeight/2);
    }

    private float PixelPerUnit()
    {
        GameObject camera = GameObject.Find("Main Camera");
        float cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        float canvasHeight = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;

        float ppu = canvasHeight/cameraHeight;
        return ppu;
    }

    private void AlignSize(Vector2 unitSize)
    // ? determine size by unit instead of pixel
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = unitSize * PixelPerUnit();
        AlignOption();
    }

    private Vector2 MousePosition()
    // ? pixel position of the mouse
    {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);// ! unit
        float cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        float cameraWidth = 2 * camera.GetComponent<Camera>().orthographicSize * camera.GetComponent<Camera>().aspect;

        Vector3 position = PixelPerUnit() * new Vector2 (cameraPosition.x, cameraPosition.y);
        return position;
    }
    void AlignOption()
    // ? align the position of every component 
    {
        RectTransform attributeRect = attribute.GetComponent<RectTransform>();
        attributeRect.sizeDelta = rect.sizeDelta / 2;
        attributeRect.anchoredPosition = rect.anchoredPosition;

        RectTransform optionBackgroundRect = optionBackground.GetComponent<RectTransform>();
        optionBackgroundRect.sizeDelta = rect.sizeDelta;
        optionBackgroundRect.anchoredPosition = rect.anchoredPosition;
        
        RectTransform levelRect = level.GetComponent<RectTransform>();
        levelRect.sizeDelta = rect.sizeDelta / 3;
        levelRect.anchoredPosition = rect.anchoredPosition + new Vector2 (-rect.rect.width/3, rect.rect.height/3);

        RectTransform quantityRect = quantity.GetComponent<RectTransform>();
        quantityRect.sizeDelta = rect.sizeDelta / 3;
        quantityRect.anchoredPosition = rect.anchoredPosition + new Vector2 (rect.rect.width/3, rect.rect.height/3);

    }

    private bool InBound(Vector2 postion, Vector2 size)
    {
        Vector2 mousePosition = MousePosition();
        if (mousePosition.x < postion.x - size.x / 2)
        {
            return false;
        }
        if (mousePosition.x > postion.x + size.x / 2)
        {
            return false;
        }
        if (mousePosition.y < postion.y - size.y / 2)
        {
            return false;
        }
        if (mousePosition.y > postion.y + size.y / 2)
        {
            return false;
        }
        return true;
    }
    
}
