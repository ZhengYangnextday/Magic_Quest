using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PackageManager : MonoBehaviour
{

    // private Vector2 position;
    // private Vector2 unitSize;
    //private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;
    private int pageNumber;

    private bool instantiated;

    private const int cardNumber = 9;
    // ? components
    // ! Initiallized in Awake()
    // TODO update
    //private GameObject component; // TODO BackgroundNDX()

    void Awake()
    {
        pageNumber = 1;
        // AssemblyInstantiate();
        instantiated = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        // AssemblySetParent();

        // rectTransform = this.gameObject.GetComponent<RectTransform>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        // Debug.Log(canvasManager.GetCameraSize());

        // AssemblyUpdate();

    }

    // Update is called once per frame
    bool[] drag = new bool[cardNumber];
    //GameObject[] cardIconCopy = new GameObject[9];
    GameObject cardIconCopy;
    void Update()
    {
        // AssemblyUpdate();
        // Debug.Log(canvasManager.GetCameraSize());
        // Debug.Log(drag[0]);
        
        if (!instantiated)
        {
            return;
        }
        AssemblyUpdate();
        if (!instantiated)
        {
            return;
        }
        for (int i = 0; i < cardNumber; i++)
        {
            GameObject cardIcon = GameObject.Find("CardIcon" + i.ToString());

            //Debug.Log(drag[i]);
            // Debug.Log(cardIcon.GetComponent<RectTransform>().anchoredPosition-canvasManager.MousePosition());
            // Debug.Log(canvasManager.MousePosition());
            //Debug.Log(canvasManager.InBound(cardIcon.GetComponent<RectTransform>().anchoredPosition,cardIcon.GetComponent<RectTransform>().sizeDelta));
            if (!BookSystem.IdInRange(cardIcon.GetComponent<CardIcon>().GetNDX()))
            {
                break;
            }
            if (!drag[i] && Input.GetKeyDown(KeyCode.Mouse0) && canvasManager.InBound(cardIcon.GetComponent<RectTransform>().anchoredPosition, cardIcon.GetComponent<RectTransform>().sizeDelta))
            {
                drag[i] = true;
                // Debug.Log(drag[i]);//!
                cardIconCopy = CopyCardIcon(cardIcon);// TODO
            }
            if (drag[i])
            {
                int containerIndex = container.GetComponent<Container>().InContainer();
                if (Input.GetKeyUp(KeyCode.Mouse0) && containerIndex == -1)
                {
                    drag[i] = false;
                    cardIconCopy.GetComponent<CardIcon>().AssemblyDestroy();
                }
                if (Input.GetKeyUp(KeyCode.Mouse0) && !(containerIndex == -1))
                {
                    drag[i] = false;
                    container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetPosition(cardIcon.GetComponent<CardIcon>().GetPosition());
                    container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetUnitSize(cardIcon.GetComponent<CardIcon>().GetUnitSize());
                    container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetNDX(cardIcon.GetComponent<CardIcon>().GetNDX());

                    container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetCardActive(true);

                    cardIconCopy.GetComponent<CardIcon>().AssemblyDestroy();

                    BookSystem.SetEquip(containerIndex, container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().GetNDX());// ! test!
                }
                // cardIconCopy[i].GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition()/canvasManager.PixelPerUnit());
                // if (BookSystem.IdInRange(cardIconCopy.GetComponent<CardIcon>().GetNDX())){
                //     cardIconCopy.GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition()/canvasManager.PixelPerUnit());
                // }
                cardIconCopy.GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition() / canvasManager.PixelPerUnit());



                // cardIconCopy.GetComponent<Image>().sprite = Resources.Load<Sprite>("Test_UI/Sprites/cardBackground/a1");
                // cardIconCopy.GetComponent<Image>().sprite = cardIconCopy.GetComponent<CardIcon>().GetBackgroundSprite(cardIconCopy.GetComponent<CardIcon>().GetNDX());
                // cardIcon.GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition()/canvasManager.PixelPerUnit());
            }
        }

        if (GameObject.Find("Button0").GetComponent<Template>().IsClicked())
        {
            pageNumber--;
            if (pageNumber == 0)
            {
                pageNumber = 1;
            }
        }
        if (GameObject.Find("Button1").GetComponent<Template>().IsClicked())
        {
            pageNumber++;
            if (pageNumber == 3)
            {
                pageNumber = 2;
            }
        }

        // if (Input.GetKeyDown(KeyCode.P))
        // {
        //     AssemblyDestroy();
        //     instantiated = false;
        // }
        // for (int i = 0; i < cardNumber; i++)
        // {
        //     GameObject cardIcon = GameObject.Find("CardIcon" + i.ToString());

        //     if (!drag[i]&&Input.GetKeyDown(KeyCode.Mouse0)&&canvasManager.InBound(cardIcon.GetComponent<RectTransform>().anchoredPosition,cardIcon.GetComponent<RectTransform>().sizeDelta)){
        //         drag[i] = true;
        //         cardIconCopy = CopyCardIcon(cardIcon);// TODO
        //     }
        //     if (drag[i]){
        //         int containerIndex = container.GetComponent<Container>().InContainer();
        //         if (Input.GetKeyUp(KeyCode.Mouse0) && containerIndex == -1){
        //             drag[i] = false;
        //             cardIconCopy.GetComponent<CardIcon>().AssemblyDestroy();
        //         }
        //         if (Input.GetKeyUp(KeyCode.Mouse0) && !(containerIndex == -1)){
        //             drag[i] = false;
        //             container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetPosition(cardIcon.GetComponent<CardIcon>().GetPosition());
        //             container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetUnitSize(cardIcon.GetComponent<CardIcon>().GetUnitSize());
        //             container.GetComponent<Container>().cardIcons[containerIndex].GetComponent<CardIcon>().SetNDX(cardIcon.GetComponent<CardIcon>().GetNDX());
        //             cardIconCopy.GetComponent<CardIcon>().AssemblyDestroy();
        //         }
        //         // cardIconCopy[i].GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition()/canvasManager.PixelPerUnit());
        //         cardIconCopy.GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition()/canvasManager.PixelPerUnit());
        //         // cardIcon.GetComponent<CardIcon>().SetPosition(canvasManager.MousePosition()/canvasManager.PixelPerUnit());
        //     }
        // }

        // if (GameObject.Find("Button0").GetComponent<Template>().IsClicked()){
        //     pageNumber --;
        //     if (pageNumber == 0){
        //         pageNumber = 1;
        //     }
        // }
        // if (GameObject.Find("Button1").GetComponent<Template>().IsClicked()){
        //     pageNumber ++;
        //     if (pageNumber == 3){
        //         pageNumber = 2;
        //     }
        // }

        // if (Input.GetKeyDown(KeyCode.K)){
        //     AssemblyDestroy();
        //     instantiated = false;
        // }

    }

    private GameObject CopyCardIcon(GameObject CardIcon)
    {
        RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();
        GameObject CardIconCopy = GameObject.Instantiate(Resources.Load("Test_UI/CardIcon/CardIcon") as GameObject);
        CardIconCopy.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        CardIconCopy.GetComponent<CardIcon>().SetPosition(CardIcon.GetComponent<CardIcon>().GetPosition());
        CardIconCopy.GetComponent<CardIcon>().SetUnitSize(CardIcon.GetComponent<CardIcon>().GetUnitSize());
        CardIconCopy.GetComponent<CardIcon>().SetNDX(CardIcon.GetComponent<CardIcon>().GetNDX());

        CardIconCopy.GetComponent<Image>().sprite = CardIcon.GetComponent<Image>().sprite;

        Debug.Log("take NDX: " + CardIconCopy.GetComponent<CardIcon>().GetNDX());

        return CardIconCopy;
    }
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
    private GameObject[] cardIcons = new GameObject[cardNumber];// Test_UI/CardIcon/CardIcon
    private GameObject[] buttons = new GameObject[2];// Test_UI/Package/buttonRight buttonLeft
    private GameObject pageIndex;// Test_UI/Package/pageIndex
    private GameObject heroIcon;// Test_UI/Package/heroIcon
    private GameObject heroStatus;// Test_UI/Package/heroStatus
    private GameObject container;// Test_UI/Package/container

    private void AssemblyInstantiate()
    // ! Instantiate and SetParent
    {
        // RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        packageBackground = GameObject.Instantiate(Resources.Load("Test_UI/Package/packageBackground") as GameObject);
        //packageBackground.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        for (int i = 0; i < cardNumber; i++)
        {
            cardIcons[i] = GameObject.Instantiate(Resources.Load("Test_UI/CardIcon/CardIcon") as GameObject);
            //cardIcons[i].GetComponent<RectTransform>().SetParent(canvasRectTransform);
            cardIcons[i].name = "CardIcon" + i.ToString();
        }

        buttons[0] = GameObject.Instantiate(Resources.Load("Test_UI/Package/buttonLeft") as GameObject);
        //buttons[0].GetComponent<RectTransform>().SetParent(canvasRectTransform);
        buttons[0].name = "Button0";

        buttons[1] = GameObject.Instantiate(Resources.Load("Test_UI/Package/buttonRight") as GameObject);
        //buttons[1].GetComponent<RectTransform>().SetParent(canvasRectTransform);
        buttons[1].name = "Button1";

        pageIndex = GameObject.Instantiate(Resources.Load("Test_UI/Package/pageIndex") as GameObject);
        //pageIndex.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        heroIcon = GameObject.Instantiate(Resources.Load("Test_UI/Package/heroIcon") as GameObject);
        //heroIcon.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        heroStatus = GameObject.Instantiate(Resources.Load("Test_UI/Package/heroStatus") as GameObject);
        //heroStatus.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        container = GameObject.Instantiate(Resources.Load("Test_UI/Package/container") as GameObject);
        //container.GetComponent<RectTransform>().SetParent(canvasRectTransform);

    }

    private void AssemblySetParent()
    // ! Instantiate and SetParent
    {
        RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        packageBackground.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        buttons[0].GetComponent<RectTransform>().SetParent(canvasRectTransform);

        buttons[1].GetComponent<RectTransform>().SetParent(canvasRectTransform);

        pageIndex.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        heroIcon.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        heroStatus.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        container.GetComponent<RectTransform>().SetParent(canvasRectTransform);

        for (int i = 0; i < cardNumber; i++)
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
        // packageBackground.GetComponent<Template>().SetUnitSize(new Vector2(0f, 0f));
        packageBackground.GetComponent<Template>().SetUnitSize(canvasManager.GetCameraSize());

        for (int i = 0; i < cardNumber; i++)
        {
            cardIcons[i].GetComponent<CardIcon>().SetPosition(CardiconPosition(i));
            cardIcons[i].GetComponent<CardIcon>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 5f));
            cardIcons[i].GetComponent<CardIcon>().SetNDX(i + cardNumber * (pageNumber - 1));

            int ndx = i + 1 + cardNumber * (pageNumber - 1);
            //Debug.Log("NDX: " + ndx.ToString());

            cardIcons[i].GetComponent<CardIcon>().SetNDX(ndx);

            // if (! BookSystem.IdInRange(ndx)){//! test! // +1
            //     cardIcons[i].GetComponent<CardIcon>().SetNDX(-1);
            //     cardIcons[i].GetComponent<CardIcon>().SetCardActive(false);
            // }
            if (BookSystem.IdInRange(ndx))
            {//! test! // +1
                cardIcons[i].GetComponent<CardIcon>().SetCardActive(true);
            }

        }
        for (int i = 0; i < 2; i++)
        {
            buttons[i].GetComponent<Template>().SetPosition(new Vector2(-canvasManager.GetCameraSize().x * 7f / 20f, -canvasManager.GetCameraSize().y * 2f / 5f) + i * new Vector2(canvasManager.GetCameraSize().x * 3f / 10f, 0f));
            // buttons[i].GetComponent<Template>().SetUnitSize(new Vector2(0, 0));
            buttons[i].GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 5f));
        }

        pageIndex.GetComponent<Template>().SetPosition(new Vector2(-canvasManager.GetCameraSize().x / 5f, -canvasManager.GetCameraSize().y * 2f / 5f));
        // pageIndex.GetComponent<Template>().SetUnitSize(new Vector2(0, 0));
        pageIndex.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x / 5f, canvasManager.GetCameraSize().y / 5f));
        pageIndex.GetComponent<Text>().text = pageNumber.ToString();

        heroIcon.GetComponent<Template>().SetPosition(new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 4f));
        // heroIcon.GetComponent<Template>().SetUnitSize(new Vector2(0, 0));
        heroIcon.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 4f));

        heroStatus.GetComponent<Template>().SetPosition(new Vector2(canvasManager.GetCameraSize().x / 4f, canvasManager.GetCameraSize().y / 4f));
        // heroStatus.GetComponent<Template>().SetUnitSize(new Vector2(0, 0));
        heroStatus.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x / 5f, canvasManager.GetCameraSize().y / 4f));

        // TODO activate 3 lines
        int MaxHP = (int)GameObject.Find("Hero").GetComponent<CreatureInformation>().mMaxHeartPoint;
        float HP = MaxHP * (float)GameObject.Find("Hero").GetComponent<CreatureInformation>().GetHeartRate();
        heroStatus.GetComponent<Text>().text = "HP : " + HP.ToString() + '/' + MaxHP.ToString();
        heroStatus.GetComponent<Text>().text += "\n HEART * " + BookSystem.GetHeroHeart().ToString();
        heroStatus.GetComponent<Text>().text += "\n STAGE * " + SceneManage.GetTotalStageNum().ToString();


        //Debug.Log("HP: " + HP.ToString());
        //heroStatus.GetComponent<Text>().text = "HP: 100/100";

        container.GetComponent<Container>().SetPosition(new Vector2(canvasManager.GetCameraSize().x / 5f, -canvasManager.GetCameraSize().y / 4f));
        // container.GetComponent<Template>().SetUnitSize(new Vector2(0, 0));
        container.GetComponent<Container>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x * 2f / 5f, canvasManager.GetCameraSize().y / 4f));


        // this.gameObject.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        // component.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
    }
    public void AssemblyDestroy()
    {
        if (!instantiated){return;}
        packageBackground.GetComponent<Template>().AssemblyDestroy();
        for (int i = 0; i < cardNumber; i++)
        {
            cardIcons[i].GetComponent<CardIcon>().AssemblyDestroy(); ;
        }
        for (int i = 0; i < 2; i++)
        {
            buttons[i].GetComponent<Template>().AssemblyDestroy();
        }
        pageIndex.GetComponent<Template>().AssemblyDestroy();
        heroIcon.GetComponent<Template>().AssemblyDestroy();
        heroStatus.GetComponent<Template>().AssemblyDestroy();
        container.GetComponent<Container>().AssemblyDestroy();
        // Destroy(this.gameObject);
    }

    public void AssemblyActivate()
    {
        AssemblyInstantiate();
        AssemblySetParent();
        AssemblyUpdate();
        instantiated = true;
    }

    public void SetStatus(bool status){
        instantiated = status;
    }

}
