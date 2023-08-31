using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    private RectTransform rectTransform; // ? self recttransform
    private Sprite[] modeSprites = new Sprite[2];

    CanvasManager canvasManager;
    PauseMenu pauseMenu;
    PackageManager packageManager;

    void Awake()
    {
        AssemblyInstantiate();
    }

    void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        pauseMenu = GameObject.Find("Canvas").GetComponent<PauseMenu>();
        packageManager = GameObject.Find("Canvas").GetComponent<PackageManager>();
        rectTransform = this.gameObject.GetComponent<RectTransform>();

        open = false;
        guide = false;

        AssemblySetParent();

        // mode = false; // false for spell & true for monster
        modeSprites[0] = Resources.Load<Sprite>("Test_UI/Sprites/mode/spell");
        modeSprites[1] = Resources.Load<Sprite>("Test_UI/Sprites/mode/monster");
    }

    private bool open;
    private bool guide;
    private GameObject guideBook = null;

    void Update()
    {
        // MonsterInformation[] monsterInformation = FindObjectsOfType<MonsterInformation>();
        //GameObject[] monsters = FindObjectsOfType<GameObject>();

        //Debug.Log("open: " + open.ToString());
        AssemblyUpdate();
        if (open)
        {
            // GameObject[] monsters = FindObjectsOfType(typeof(MonsterInformation)) as GameObject[];
            // 遍历这个数组，并打印每个游戏对象的名字和 MonsterInformation 的属性
            Time.timeScale = 0;

            if (!guide && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
            {
                open = false;
                Time.timeScale = 1;
                //! clear canvas
                pauseMenu.AssemblyDestroy();
                pauseMenu.SetStatus(false);
                packageManager.AssemblyDestroy();
                packageManager.SetStatus(false);
            }
            if (guide && (Input.GetKeyDown(KeyCode.Escape)))
            {
                open =true;
                guide = false;
                Time.timeScale = 0;
                //! clear canvas
                Destroy(guideBook);
            }

            if (pauseMenu.GetComponent<PauseMenu>().GetStatus())
            {
                
                if (pauseMenu.GetComponent<PauseMenu>().GuideIsClicked())
                {
                    Debug.LogWarning("GuideBook");

                    //! guide
                    open = true;
                    guide = true;
                    guideBook = GameObject.Instantiate(Resources.Load("Test_UI/PauseMenu/guideBook") as GameObject);
                    guideBook.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
                    // guideBook.GetComponent<Template>().SetUnitSize(canvasManager.GetCameraSize());
                    
                    guideBook.GetComponent<Template>().SetPosition(new Vector2(0f,0f));
                    // open = false;
                    // Time.timeScale = 1;
                    //! clear canvas
                    
                }
            }

            return;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            open = true;
            //! freeze game
            //! activate Pause Menu
            pauseMenu.AssemblyActivate();
            pauseMenu.SetStatus(true);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            open = true;
            //! freeze game
            //! activate Package System
            packageManager.AssemblyActivate();
            packageManager.SetStatus(true);
        }


    }



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
        hPBackground.GetComponent<Template>().SetPosition(new Vector2(canvasManager.GetCameraSize().y / 8f - canvasManager.GetCameraSize().x / 2f, canvasManager.GetCameraSize().y * 3f / 8f));
        hPBackground.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().y / 4f, canvasManager.GetCameraSize().y / 4f));

        HPRate = GameObject.Find("Hero").GetComponent<CreatureInformation>().GetHeartRate();

        if (HPRate > 0.5) { hPBackground.GetComponent<Image>().color = new Color(1 - HPRate, HPRate, 1 - HPRate, 1f); }
        if (HPRate <= 0.5) { hPBackground.GetComponent<Image>().color = new Color(1 - HPRate, HPRate, HPRate, 1f); }

        modeIcon.GetComponent<Template>().SetPosition(hPBackground.GetComponent<Template>().GetPosition());
        modeIcon.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().y / 5f, canvasManager.GetCameraSize().y / 5f));
        if (BookSystem.GetSummonMode()) { modeIcon.GetComponent<Image>().sprite = modeSprites[1]; }
        if (!BookSystem.GetSummonMode()) { modeIcon.GetComponent<Image>().sprite = modeSprites[0]; }

        container.GetComponent<Container>().SetPosition(new Vector2(canvasManager.GetCameraSize().y * 1f / 4f - canvasManager.GetCameraSize().x / 2f, -canvasManager.GetCameraSize().y * 5f / 12f));
        container.GetComponent<Container>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().y * 1f / 2f, canvasManager.GetCameraSize().y / 6f));

        for (int i = 0; i < 3; i++)
        {
            container.GetComponent<Container>().cardIcons[i].GetComponent<CardIcon>().SetCardActive(i == BookSystem.GetEquipNum());
            // Debug.Log(i == BookSystem.GetEquipNum());
        }

        if (guideBook != null){
            guideBook.GetComponent<Template>().SetUnitSize(canvasManager.GetCameraSize());
        }
    }
    private void AssemblyDestroy()
    {

    }
}
