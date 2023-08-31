using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class PauseMenu : MonoBehaviour
{
    // ? card properties
    private Vector2 position;
    private Vector2 unitSize;
    private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;
    private Vector2 buttonSize;
    private Vector2 buttonPositionBase;
    private Vector2 buttonPositionBias;

    private bool instantiated;


    // ? components
    // ! Initiallized in Awake()
    // TODO update
    // private GameObject component = null; // TODO BackgroundNDX()

    void Awake()
    {
        //AssemblyInstantiate();
        instantiated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        //AssemblySetParent();

        rectTransform = this.gameObject.GetComponent<RectTransform>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        // buttonSize = new Vector2(canvasManager.GetCameraSize().x/4f,canvasManager.GetCameraSize().y/4f);
        // buttonPositionBase = new Vector2(canvasManager.GetCameraSize().x/4f,0f);
        // buttonPositionBias = new Vector2(0f,canvasManager.GetCameraSize().y/4f);

        // Debug.Log("buttonSize : "+ buttonSize.ToString());
        // Debug.Log("buttonPositionBase : "+ buttonPositionBase.ToString());
        // Debug.Log("buttonPositionBias : "+ buttonPositionBias.ToString());



        //AssemblyUpdate();

    }

    // Update is called once per frame

    void Update()
    {
        if (!instantiated)
        {
            // if (Input.GetKeyDown(KeyCode.Escape))
            // {
            //     AssemblyActivate();
            // }
            return;
        }

        AssemblyUpdate();
        // InBound(Vector2 postion, Vector2 size);
        bool inBoundContinue = canvasManager.InBound(guide.GetComponent<RectTransform>().anchoredPosition, guide.GetComponent<RectTransform>().sizeDelta);
        // bool inBoundSettings = canvasManager.InBound(settings.GetComponent<RectTransform>().anchoredPosition, settings.GetComponent<RectTransform>().sizeDelta);
        bool inBoundMenu = canvasManager.InBound(menu.GetComponent<RectTransform>().anchoredPosition, menu.GetComponent<RectTransform>().sizeDelta);

        // Debug.Log(canvasManager.MousePosition()-restart.GetComponent<RectTransform>().anchoredPosition);
        // Debug.Log(restart.GetComponent<RectTransform>().anchoredPosition);

        if (inBoundContinue && Input.GetKeyDown(KeyCode.Mouse0))
        {//restart.GetComponent<Template>().IsClicked()//
            Debug.Log("continue_!");
            //! continue
        }
        // if (inBoundSettings && Input.GetKeyDown(KeyCode.Mouse0)){
        //     Debug.Log("settings!");
        //     //settings.GetComponent<RectTransform>().sizeDelta = new Vector2 (60f, 30f);
        // }
        if (inBoundMenu && Input.GetKeyDown(KeyCode.Mouse0))
        {//menu.GetComponent<Template>().IsClicked()//
            Debug.Log("menu!");
            SceneManager.LoadScene("Scenes/Formal/System/StartMenu");
            //! go to start menu 
        }
    }

    private GameObject guide;
    // private GameObject settings;
    private GameObject menu;
    private GameObject background;
    private void AssemblyInstantiate()
    {
        background = Instantiate(Resources.Load("Test_UI/PauseMenu/background") as GameObject);
        guide = Instantiate(Resources.Load("Test_UI/PauseMenu/guide") as GameObject);// !
        // start.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // start.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // start.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 80f);

        // settings = Instantiate(Resources.Load("Test_UI/StartMenu/settings") as GameObject);
        // settings.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // settings.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // settings.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);

        menu = Instantiate(Resources.Load("Test_UI/PauseMenu/menu") as GameObject);//!
        // exit.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // exit.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // exit.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, -80f);
    }

    private void AssemblySetParent()
    {
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        background.GetComponent<RectTransform>().SetParent(canvasRect);
        guide.GetComponent<RectTransform>().SetParent(canvasRect);
        // settings.GetComponent<RectTransform>().SetParent(canvasRect);
        menu.GetComponent<RectTransform>().SetParent(canvasRect);

        // component.GetComponent<RectTransform>().SetParent(canvasRect);
    }
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that? 
    {
        
        float x = canvasManager.GetCameraSize().x / 4f;
        float y = canvasManager.GetCameraSize().y / 4f;
        buttonSize = new Vector2(x, y);

        buttonPositionBase = new Vector2(canvasManager.GetCameraSize().x / 4f, 0f);
        buttonPositionBias = new Vector2(0f, canvasManager.GetCameraSize().y / 4f);
        // this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();

        background.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x, canvasManager.GetCameraSize().y));
        guide.GetComponent<Template>().SetUnitSize(buttonSize);
        // settings.GetComponent<Template>().SetUnitSize(buttonSize);
        menu.GetComponent<Template>().SetUnitSize(buttonSize);

        // this.gameObject.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        background.GetComponent<Template>().SetPosition(new Vector2(0f, 0f));
        guide.GetComponent<Template>().SetPosition(buttonPositionBase + buttonPositionBias);
        // settings.GetComponent<Template>().SetPosition(buttonPositionBase);
        menu.GetComponent<Template>().SetPosition(buttonPositionBase - buttonPositionBias);
    }
    public void AssemblyDestroy()
    {
        if (!instantiated) { return; }
        background.GetComponent<Template>().AssemblyDestroy();
        guide.GetComponent<Template>().AssemblyDestroy();
        // settings.GetComponent<Template>().AssemblyDestroy();
        menu.GetComponent<Template>().AssemblyDestroy();
        // Destroy(this.gameObject);
    }
    public void AssemblyActivate()
    {
        AssemblyInstantiate();
        AssemblySetParent();
        AssemblyUpdate();
        //instantiated = true;
    }

    public void SetStatus(bool status)
    {
        instantiated = status;
    }

    public bool GetStatus()
    {
        return instantiated;
    }

    public bool GuideIsClicked()
    {
        return guide.GetComponent<Template>().IsClicked();
    }

}

