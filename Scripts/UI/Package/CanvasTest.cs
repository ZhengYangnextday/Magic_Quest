using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasTest : MonoBehaviour
{

    private Vector2 position;
    private Vector2 unitSize;
    private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;


    // ? components
    // ! Initiallized in Awake()
    // TODO update
    //private GameObject component; // TODO BackgroundNDX()

    void Awake()
    {
        AssemblyInstantiate();
        AssemblySetParent();
    }

    // Start is called before the first frame update
    void Start()
    {

        rectTransform = this.gameObject.GetComponent<RectTransform>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        // Debug.Log(canvasManager.GetCameraSize());

        // AssemblyUpdate();

    }

    // Update is called once per frame
    void Update()
    {
        AssemblyUpdate();
        // Debug.Log(canvasManager.GetCameraSize());

        for (int i = 0; i < 9; i++)
        {
            GameObject cardIcon = GameObject.Find("CardIcon" + i.ToString());
            // Debug.Log("Canvas/CardIcon" + i.ToString());
            if (cardIcon.GetComponent<CardIcon>().IsClicked())
            {
                // cardIcon.GetComponent<CardIcon>().IsClicked()
                Debug.Log("CardIcon" + i.ToString() + " is clicked");
            }
        }
        for (int i = 0; i < 2; i++)
        {
            GameObject button = GameObject.Find("Button" + i.ToString());
            // Debug.Log("Canvas/CardIcon" + i.ToString());
            if (button.GetComponent<Template>().IsClicked())
            {
                // cardIcon.GetComponent<CardIcon>().IsClicked()
                Debug.Log("Button" + i.ToString() + " is clicked");
            }
        }
    }

    public void SetPosition(Vector2 position_)
    {
        position = position_;
    }
    public Vector2 GetPosition()
    {
        return position;
    }
    public void SetUnitSize(Vector2 unitSize_)
    {
        unitSize = unitSize_;
    }
    public Vector2 GetUnitSize()
    {
        return unitSize;
    }

    /*
    * components
    *
    * packageSystem background "Assets/Resources/Test_UI/Sprites/Package/book.png"
    *
    * LEFT
    * 9 cardicons (with 9 cardnumber)
    * 2 button
    * 1 text
    *
    * RIGHT
    * 1 herosprite
    * 1 text
    * 1 container
    */
    private GameObject packageBackground;// Test_UI/Package/packageBackground
    private GameObject[] cardIcons = new GameObject[9];// Test_UI/CardIcon/CardIcon


    private void AssemblyInstantiate()
    {
        packageBackground = GameObject.Instantiate(Resources.Load("Test_UI/Package/packageBackground") as GameObject);
        for (int i = 0; i < 9; i++){
            cardIcons[i] = GameObject.Instantiate(Resources.Load("Test_UI/CardIcon/CardIcon")as GameObject);
            cardIcons[i].name = "CardIcon" + i.ToString();
        }
        

    }

    private void AssemblySetParent()
    {
        RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        packageBackground.GetComponent<RectTransform>().SetParent(canvasRectTransform);
        for (int i = 0; i < 9; i++)
        {
            cardIcons[i].GetComponent<RectTransform>().SetParent(canvasRectTransform);
        }
        
    }
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that?
    {
        Vector2 CardiconPosition(int ndx)
        // ? pixel position of the card icon by ndx
        {
            ndx = ndx % 9;

            // Vector2 cardIconSize = new Vector2(canvasWidth/10f, canvasHeight/5f);
            // Vector2 cardIconGap = new Vector2(canvasWidth/40f, canvasHeight/20f);

            Vector2 cardIconSize = new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 5f);
            Vector2 cardIconGap = new Vector2(canvasManager.GetCameraSize().x / 40f, canvasManager.GetCameraSize().y / 20f);

            float x = cardIconSize.x / 2 + (ndx % 3 - 3) * (cardIconSize.x + cardIconGap.x);
            float y = (1 - ndx / 3) * (cardIconSize.y + cardIconGap.y) + canvasManager.GetCameraSize().y / 10f;

            return new Vector2(x, y);
        }
        //Debug.Log(canvasManager.GetCameraSize());
        // this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();
        packageBackground.GetComponent<Template>().SetPosition(new Vector2(0f, 0f));
        packageBackground.GetComponent<Template>().SetUnitSize(canvasManager.GetCameraSize());
        for (int i = 0; i < 9; i++)
        {
            cardIcons[i].GetComponent<CardIcon>().SetPosition(CardiconPosition(i));
            cardIcons[i].GetComponent<CardIcon>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 5f));
            cardIcons[i].GetComponent<CardIcon>().SetNDX(i);
        }
        
    }
    

}
