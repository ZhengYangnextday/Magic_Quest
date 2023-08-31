using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasStartMenu : MonoBehaviour

{
    // ? card properties
    private Vector2 position;
    private Vector2 unitSize;
    private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;
    private Vector2 buttonSize;
    private Vector2 buttonPositionBase;
    private Vector2 buttonPositionBias;


    // ? components
    // ! Initiallized in Awake()
    // TODO update
    // private GameObject component = null; // TODO BackgroundNDX()

    void Awake()
    {
        AssemblyInstantiate();
    }

    // Start is called before the first frame update
    void Start()
    {
        AssemblySetParent();

        rectTransform = this.gameObject.GetComponent<RectTransform>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        
        // buttonSize = new Vector2(canvasManager.GetCameraSize().x/4f,canvasManager.GetCameraSize().y/4f);
        // buttonPositionBase = new Vector2(canvasManager.GetCameraSize().x/4f,0f);
        // buttonPositionBias = new Vector2(0f,canvasManager.GetCameraSize().y/4f);

        // Debug.Log("buttonSize : "+ buttonSize.ToString());
        // Debug.Log("buttonPositionBase : "+ buttonPositionBase.ToString());
        // Debug.Log("buttonPositionBias : "+ buttonPositionBias.ToString());

        

        AssemblyUpdate();

    }

    // Update is called once per frame
    
    void Update()
    {

        AssemblyUpdate();
        // InBound(Vector2 postion, Vector2 size);
        bool inBoundStart = canvasManager.InBound(start.GetComponent<RectTransform>().anchoredPosition, start.GetComponent<RectTransform>().sizeDelta);
        // bool inBoundSettings = canvasManager.InBound(settings.GetComponent<RectTransform>().anchoredPosition, settings.GetComponent<RectTransform>().sizeDelta);
        bool inBoundExit = canvasManager.InBound(exit.GetComponent<RectTransform>().anchoredPosition, exit.GetComponent<RectTransform>().sizeDelta);
        
        
        
        if (inBoundStart && Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("strat!");
            Time.timeScale = 1;
            SceneManage.BeginScene();
            //start.GetComponent<RectTransform>().sizeDelta = new Vector2 (60f, 30f);
        }
        // if (inBoundSettings && Input.GetKeyDown(KeyCode.Mouse0)){
        //     Debug.Log("settings!");
        //     //settings.GetComponent<RectTransform>().sizeDelta = new Vector2 (60f, 30f);
        // }
        if (inBoundExit && Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("exit!");
            //exit.GetComponent<RectTransform>().sizeDelta = new Vector2 (60f, 30f);
            Application.Quit();
        }
    }

    private GameObject start;
    // private GameObject settings;
    private GameObject exit;
    private GameObject background;
    private void AssemblyInstantiate(){
        background = Instantiate(Resources.Load("Test_UI/StartMenu/background") as GameObject);
        start = Instantiate(Resources.Load("Test_UI/StartMenu/start") as GameObject);
        // start.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // start.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // start.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 80f);

        // settings = Instantiate(Resources.Load("Test_UI/StartMenu/settings") as GameObject);
        // settings.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // settings.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // settings.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);

        exit = Instantiate(Resources.Load("Test_UI/StartMenu/exit") as GameObject);
        // exit.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // exit.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // exit.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, -80f);
    }

    private void AssemblySetParent(){
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        background.GetComponent<RectTransform>().SetParent(canvasRect);
        start.GetComponent<RectTransform>().SetParent(canvasRect);
        // settings.GetComponent<RectTransform>().SetParent(canvasRect);
        exit.GetComponent<RectTransform>().SetParent(canvasRect);

        // component.GetComponent<RectTransform>().SetParent(canvasRect);
    }
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that?
    {
        buttonSize = new Vector2(canvasManager.GetCameraSize().x/4f,canvasManager.GetCameraSize().y/4f);
        buttonPositionBase = new Vector2(canvasManager.GetCameraSize().x/4f,0f);
        buttonPositionBias = new Vector2(0f,canvasManager.GetCameraSize().y/4f);
        // this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();

        background.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x,canvasManager.GetCameraSize().y));
        start.GetComponent<Template>().SetUnitSize(buttonSize);
        // settings.GetComponent<Template>().SetUnitSize(buttonSize);
        exit.GetComponent<Template>().SetUnitSize(buttonSize);

        background.GetComponent<Template>().SetPosition(new Vector2(0f,0f));
        start.GetComponent<Template>().SetPosition(buttonPositionBase+buttonPositionBias);
        // settings.GetComponent<Template>().SetPosition(buttonPositionBase);
        exit.GetComponent<Template>().SetPosition(buttonPositionBase-buttonPositionBias);
    }
    public void AssemblyDestroy(){
        background.GetComponent<Template>().AssemblyDestroy();
        start.GetComponent<Template>().AssemblyDestroy();
        // settings.GetComponent<Template>().AssemblyDestroy();
        exit.GetComponent<Template>().AssemblyDestroy();
        Destroy(this.gameObject);
    }

}
