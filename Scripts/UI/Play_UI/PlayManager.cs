using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{

    // private Vector2 position;
    // private Vector2 unitSize;
    private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;

    //private bool instantiated;

    // private const int cardNumber = 9;

    // private bool mode;
    private Sprite[] modeSprites = new Sprite[2];
    // ? components
    // ! Initiallized in Awake()
    // TODO update
    //private GameObject component; // TODO BackgroundNDX()

    void Awake()
    {
        
        AssemblyInstantiate();
        //instantiated = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        AssemblySetParent();

        // mode = false; // false for spell & true for monster
        modeSprites[0] = Resources.Load<Sprite>("Test_UI/Sprites/mode/spell");
        modeSprites[1] = Resources.Load<Sprite>("Test_UI/Sprites/mode/monster");

        rectTransform = this.gameObject.GetComponent<RectTransform>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        // Debug.Log(canvasManager.GetCameraSize());

        // AssemblyUpdate();
        // HPRate = 1; // !

    }

    // Update is called once per frame
    // bool[] drag = new bool[cardNumber];
    //GameObject[] cardIconCopy = new GameObject[9];
    // GameObject cardIconCopy;
    void Update()
    {
        // AssemblyUpdate();
        // Debug.Log(canvasManager.GetCameraSize());
        // HPRate -= 0.1f* Time.deltaTime;
        // if (HPRate<0){HPRate=0;}//! test

        // if (!instantiated){
        //     if (Input.GetKeyDown(KeyCode.L)){
        //         AssemblyInstantiate();
        //         AssemblySetParent();
        //         AssemblyUpdate();
        //         instantiated = true;
        //     }
        //     return;
        // }
        
        // else{
        //     if (Input.GetKeyDown(KeyCode.M)){
        //         mode = !mode;
        //     }
        //     AssemblyUpdate();
            

        // }
        AssemblyUpdate();
        
        
    }

    // private GameObject CopyCardIcon(GameObject CardIcon){
    //     RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
    //     GameObject CardIconCopy = GameObject.Instantiate(Resources.Load("Test_UI/CardIcon/CardIcon")as GameObject);
    //     CardIconCopy.GetComponent<RectTransform>().SetParent(canvasRectTransform);

    //     CardIconCopy.GetComponent<CardIcon>().SetPosition(CardIcon.GetComponent<CardIcon>().GetPosition());
    //     CardIconCopy.GetComponent<CardIcon>().SetUnitSize(CardIcon.GetComponent<CardIcon>().GetUnitSize());
    //     CardIconCopy.GetComponent<CardIcon>().SetNDX(CardIcon.GetComponent<CardIcon>().GetNDX());

    //     return CardIconCopy;
    // }
    // public void SetPosition(Vector2 position_)
    // {
    //     position = position_;
    // }
    // public Vector2 GetPosition()
    // {
    //     return position;
    // }
    // public void SetUnitSize(Vector2 unitSize_)
    // {
    //     unitSize = unitSize_;
    // }
    // public Vector2 GetUnitSize()
    // {
    //     return unitSize;
    // }

    /*
    * components
    *
    * TOP LEFT
    * HPBackground
    * modeIcon
    * 
    * BOTTOM LEFT
    * Container
    */
    GameObject hPBackground;
    GameObject modeIcon;
    GameObject container;

    private void AssemblyInstantiate()
    // ! Instantiate and SetParent
    {
        hPBackground = GameObject.Instantiate(Resources.Load("Test_UI/block") as GameObject);
        hPBackground.name = "hPBackground";
        modeIcon = GameObject.Instantiate(Resources.Load("Test_UI/block") as GameObject);
        modeIcon.name = "modeIcon";
        container = GameObject.Instantiate(Resources.Load("Test_UI/Package/container") as GameObject);
        container.name = "playContainer";
    }

    private void AssemblySetParent()
    // ! Instantiate and SetParent
    {
        RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        hPBackground.GetComponent<RectTransform>().SetParent(canvasRectTransform);
        hPBackground.GetComponent<Image>().sprite = Resources.Load<Sprite>("Test_UI/Sprites/mode/background");

        modeIcon.GetComponent<RectTransform>().SetParent(canvasRectTransform);
        container.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        //TODO component.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        
    }
    float HPRate;
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that?
    {
        
        // TODO activate 3 lines
        // int MaxHP = (int)GameObject.Find("hero").GetComponent<CreatureInformation>().mMaxHeartPoint;
        // int HP = MaxHP * (int)GameObject.Find("  ").GetComponent<CreatureInformation>().GetHeartRate();
        // heroStatus.GetComponent<Text>().text = HP.ToString() +'/'+MaxHP.ToString();
        hPBackground.GetComponent<Template>().SetPosition(new Vector2(canvasManager.GetCameraSize().y/8f - canvasManager.GetCameraSize().x/2f, canvasManager.GetCameraSize().y*3f/8f));
        hPBackground.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().y/4f, canvasManager.GetCameraSize().y/4f));
        
        HPRate = GameObject.Find("Hero").GetComponent<CreatureInformation>().GetHeartRate();

        if(HPRate>0.5){hPBackground.GetComponent<Image>().color = new Color (1 - HPRate,HPRate,1 - HPRate,1f);}
        if(HPRate<=0.5){hPBackground.GetComponent<Image>().color = new Color (1 - HPRate,HPRate,HPRate,1f);}

        modeIcon.GetComponent<Template>().SetPosition(hPBackground.GetComponent<Template>().GetPosition());
        modeIcon.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().y/5f, canvasManager.GetCameraSize().y/5f));
        if (BookSystem.GetSummonMode()){modeIcon.GetComponent<Image>().sprite = modeSprites[1];}
        if (!BookSystem.GetSummonMode()){modeIcon.GetComponent<Image>().sprite = modeSprites[0];}

        container.GetComponent<Container>().SetPosition(new Vector2(canvasManager.GetCameraSize().y*1f/4f - canvasManager.GetCameraSize().x/2f, -canvasManager.GetCameraSize().y*5f/12f));
        container.GetComponent<Container>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().y*1f/2f, canvasManager.GetCameraSize().y/6f));
        
        for (int i = 0; i < 3; i++){
            container.GetComponent<Container>().cardIcons[i].GetComponent<CardIcon>().SetCardActive(i == BookSystem.GetEquipNum());
            // Debug.Log(i == BookSystem.GetEquipNum());
        }

        // this.gameObject.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        // component.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
    }
    private void AssemblyDestroy()
    {
        
    }

    

}
