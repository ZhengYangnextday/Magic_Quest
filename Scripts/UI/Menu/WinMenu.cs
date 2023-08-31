using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinMenu : MonoBehaviour
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
        AssemblyUpdate();

    }

    // Update is called once per frame
    
    void Update()
    {

        AssemblyUpdate();
        bool inBoundRestart = canvasManager.InBound(restart.GetComponent<RectTransform>().anchoredPosition, restart.GetComponent<RectTransform>().sizeDelta);
        bool inBoundMenu = canvasManager.InBound(menu.GetComponent<RectTransform>().anchoredPosition, menu.GetComponent<RectTransform>().sizeDelta);
        
        
        
        if (inBoundRestart && Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("restart!");
            SceneManage.BeginScene();
            //! restart
        }
        
        if (inBoundMenu && Input.GetKeyDown(KeyCode.Mouse0)){
            Debug.Log("menu!");
            SceneManager.LoadScene("Scenes/Formal/System/StartMenu");
            //exit.GetComponent<RectTransform>().sizeDelta = new Vector2 (60f, 30f);
            //! go to menu
        }
    }

    private GameObject restart;
    // private GameObject settings;
    private GameObject menu;
    private GameObject background;
    private void AssemblyInstantiate(){
        background = Instantiate(Resources.Load("Test_UI/WinMenu/background") as GameObject);
        restart = Instantiate(Resources.Load("Test_UI/WinMenu/restart") as GameObject);
        // start.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // start.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // start.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 80f);

        // settings = Instantiate(Resources.Load("Test_UI/StartMenu/settings") as GameObject);
        // settings.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // settings.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // settings.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, 0f);

        menu = Instantiate(Resources.Load("Test_UI/WinMenu/menu") as GameObject);
        // exit.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        // exit.GetComponent<RectTransform>().sizeDelta = new Vector2 (100f, 50f);
        // exit.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0f, -80f);
    }

    private void AssemblySetParent(){
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        background.GetComponent<RectTransform>().SetParent(canvasRect);
        restart.GetComponent<RectTransform>().SetParent(canvasRect);
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
        buttonSize = new Vector2(canvasManager.GetCameraSize().x/4f,canvasManager.GetCameraSize().y/4f);
        buttonPositionBase = new Vector2(0f,-canvasManager.GetCameraSize().y/4f);
        buttonPositionBias = new Vector2(-canvasManager.GetCameraSize().x/4f,0f);
        // this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();

        background.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x,canvasManager.GetCameraSize().y));
        restart.GetComponent<Template>().SetUnitSize(buttonSize);
        // settings.GetComponent<Template>().SetUnitSize(buttonSize);
        menu.GetComponent<Template>().SetUnitSize(buttonSize);

        background.GetComponent<Template>().SetPosition(new Vector2(0f,0f));
        restart.GetComponent<Template>().SetPosition(buttonPositionBase+buttonPositionBias);
        // settings.GetComponent<Template>().SetPosition(buttonPositionBase);
        menu.GetComponent<Template>().SetPosition(buttonPositionBase-buttonPositionBias);
    }
    public void AssemblyDestroy(){
        background.GetComponent<Template>().AssemblyDestroy();
        restart.GetComponent<Template>().AssemblyDestroy();
        // settings.GetComponent<Template>().AssemblyDestroy();
        menu.GetComponent<Template>().AssemblyDestroy();
        Destroy(this.gameObject);
    }

}
