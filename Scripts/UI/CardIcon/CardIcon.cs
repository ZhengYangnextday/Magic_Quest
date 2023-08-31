using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CardIcon : MonoBehaviour
{
    // ? card properties
    private Vector2 position;//todo public ?
    private Vector2 unitSize;//todo public ?
    private int ndx;
    private int cardNumber_;//todo public ?
    private RectTransform rect; // ? self recttransform
    CanvasManager canvasManager;


    // ? card components
    // ! Initiallized in Awake()
    // TODO update
    // private GameObject cardBackground; // TODO BackgroundNDX()
    private GameObject cardSprite; // TODO switch sprite by click
    private GameObject cardNumber; // TODO CardNumber()

    

    void Awake()
    {
        AssemblyInstantiate();
        ndx = -1;
    }

    // Start is called before the first frame update
    void Start()
    {
        
        AssemblySetParent();
        rect = this.gameObject.GetComponent<RectTransform>();
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        AssemblyUpdate();
        //LoadSprites();

    }

    // Update is called once per frame
    void Update()
    {
        
        AssemblyUpdate();

    }

    public void SetPosition(Vector2 position_){
        position = position_;
    }
    public Vector2 GetPosition(){
        return position;
    }
    public void SetUnitSize(Vector2 unitSize_){
        unitSize = unitSize_;
    }
    public Vector2 GetUnitSize(){
        return unitSize;
    }

    public void SetNDX(int ndx_)
    // ! ndx for both monster and spell is the same
    // TODO everything
    {
        ndx = ndx_;
    }

    public int GetNDX()
    // ! ndx for both monster and spell is the same
    // TODO everything
    {
        return ndx;
    }
    public void SetCardNumber(int cardNumber)
    // TODO everything
    // ! Get CardNumber by NDX from GameObject.GetComponent<Package>().GetCardNumber(NDX)
    {
        cardNumber_ = cardNumber;
    }
    public int GetCardNumber()
    // TODO everything
    {
        return cardNumber_;
    }


    private void AssemblyInstantiate(){
        // cardBackground = Instantiate(Resources.Load("Test_UI/CardIcon/cardBackground") as GameObject);
        cardSprite = Instantiate(Resources.Load("Test_UI/CardIcon/cardSprite") as GameObject);
        cardNumber = Instantiate(Resources.Load("Test_UI/CardIcon/cardNumber") as GameObject);
    }

    private void AssemblySetParent(){
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        // cardBackground.GetComponent<RectTransform>().SetParent(canvasRect);
        cardSprite.GetComponent<RectTransform>().SetParent(canvasRect);
        cardNumber.GetComponent<RectTransform>().SetParent(canvasRect);
    }
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that?
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();
        // cardBackground.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();
        cardSprite.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit() / 2;
        cardNumber.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit() / 3;

        this.gameObject.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        // cardBackground.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        cardSprite.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        cardNumber.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit() + unitSize * canvasManager.PixelPerUnit() / 3;

        cardNumber.GetComponent<Text>().text = "";// TODO

        if (BookSystem.IdInRange(ndx)){
            // cardNumber.GetComponent<Text>().text = BookSystem.GetCardNum(ndx).ToString();// TODO
            //this.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
            //cardSprite.GetComponent<Image>().color = new Color(1f,1f,1f,1f);
            // this.gameObject.GetComponent<Image>().sprite = backgrounds[ndx%9];
            this.gameObject.GetComponent<Image>().sprite = GetBackgroundSprite(ndx);
            BookSystem.GetCardFunction(ndx);

            if (BookSystem.GetSummonMode()){
                cardSprite.GetComponent<Image>().sprite = GetMonsterSprite(ndx);
                cardNumber.GetComponent<Text>().text = BookSystem.GetCardNum(ndx).ToString()+"/"+BookSystem.GetCardSummonNum(ndx).ToString();
                }//monsters[ndx/9]
            if (!BookSystem.GetSummonMode()){
                cardSprite.GetComponent<Image>().sprite = GetFunctionSprite(ndx);
                cardNumber.GetComponent<Text>().text = BookSystem.GetCardNum(ndx).ToString();// TODO
                }// spells[ndx/9]
            }
        if (!BookSystem.IdInRange(ndx)){
            cardNumber.GetComponent<Text>().text = "";// TODO

            // this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Test_UI/Sprites/cardBackground/a1");// ! this line
            this.gameObject.GetComponent<Image>().sprite = Resources.Load<Sprite>("Test_UI/Sprites/CardIcon/unknown");// ! this line

            this.gameObject.GetComponent<Image>().color = new Color(1f,1f,1f,1f);//new Color(0f,0f,0f,0f);
            cardSprite.GetComponent<Image>().color = new Color(0f,0f,0f,0f);
        }

        if (this.gameObject.name == "CardIcon(Clone)"){
            Debug.Log(ndx);
        }
    }
    public void AssemblyDestroy(){
        // Destroy(cardBackground);
        Destroy(cardSprite);
        Destroy(cardNumber);
        Destroy(this.gameObject);
    }

    Sprite[] backgrounds = new Sprite[9];
    Sprite[] monsters = new Sprite[2];
    Sprite[] spells = new Sprite[2];
    // private void LoadSprites()
    // // ! not Equip Sprites
    // // cardBackground cardSprite(monsters & spells)
    // {
    //     backgrounds = Resources.LoadAll<Sprite>("Test_UI/Sprites/cardBackground");
    //     monsters = Resources.LoadAll<Sprite>("Test_UI/Sprites/monsterSprite");
    //     spells = Resources.LoadAll<Sprite>("Test_UI/Sprites/spellSprite");
    // }

    public Sprite GetBackgroundSprite(int ndx){
        string path = "Test_UI/Sprites/cardBackground/";

        if(BookSystem.GetCardRank(ndx) == (int)ElementInformation.RankIndex.S){
            path += "s";
        }
        if(BookSystem.GetCardRank(ndx) == (int)ElementInformation.RankIndex.A){
            path += "a";
        }
        if(BookSystem.GetCardRank(ndx) == (int)ElementInformation.RankIndex.B){
            path += "b";
        }
        if(BookSystem.GetCardRank(ndx) == (int)ElementInformation.RankIndex.C){
            path += "c";
        }
        if(BookSystem.GetCardRank(ndx) == (int)ElementInformation.RankIndex.D){
            path += "d";
        }

        if(BookSystem.GetCardType(ndx) == (int)ElementInformation.ElementIndex.e_fire){
            path += "0";
        }
        if(BookSystem.GetCardType(ndx) == (int)ElementInformation.ElementIndex.e_water){
            path += "1";
        }
        if(BookSystem.GetCardType(ndx) == (int)ElementInformation.ElementIndex.e_wood){
            path += "2";
        }

        return Resources.Load<Sprite>(path);
    }
    public Sprite GetFunctionSprite(int ndx){
        //TODO 0-attack,1-defend,2-heal
        string path = "";
        switch (BookSystem.GetCardFunction(ndx)){
            case 0: path = "Test_UI/Sprites/cardFunction/attack";break;
            case 1: path = "Test_UI/Sprites/cardFunction/defend";break;
            case 2: path = "Test_UI/Sprites/cardFunction/heal";break;
        }
        return Resources.Load<Sprite>(path);
    }
    public Sprite GetMonsterSprite(int ndx){
        return BookSystem.GetCardSprite(ndx);
    }

    public void SetCardActive(bool active){
        Color activeColor = new Color (1f,1f,1f,1f);
        Color nonActiveColor = new Color (0.5f,0.5f,0.5f,1f);//(0f,0f,0f,0f);//
        if(!active){
            this.gameObject.GetComponent<Image>().color = nonActiveColor;
            cardSprite.GetComponent<Image>().color = nonActiveColor;
        }
        if(active){
            this.gameObject.GetComponent<Image>().color = activeColor;
            cardSprite.GetComponent<Image>().color = activeColor;
        }
        // Debug.Log(this.gameObject.GetComponent<Image>().color);
    }

    // private int GetBackgroundNDX()
    // // TODO everything
    // {
    //     return 0;
    // }

    
    public bool IsClicked(){
        if (canvasManager.InBound(rect.anchoredPosition, rect.sizeDelta) && Input.GetKeyDown(KeyCode.Mouse0)){
            return true;
        }
        return false;
    }
}
