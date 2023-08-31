using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Container : MonoBehaviour
{
    // ? card properties
    private Vector2 position;
    private Vector2 unitSize;
    private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;
    

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

        // Debug.Log(gameObject.GetComponent<RectTransform>().sizeDelta);
    }

    public void SetPosition(Vector2 position_){
        position = position_;
    }
    public Vector2 GetPosition(){
        return position;
    }
    public void SetUnitSize(Vector2 unitSize_){
        unitSize = unitSize_;
        // Debug.Log(unitSize);
    }
    public Vector2 GetUnitSize(){
        return unitSize;
    }

    public GameObject[] cardIcons = new GameObject[3];
    private void AssemblyInstantiate(){
        for (int i = 0; i < 3; i++){
            cardIcons[i] = GameObject.Instantiate(Resources.Load("Test_UI/CardIcon/CardIcon")as GameObject);
            //cardIcons[i].GetComponent<RectTransform>().SetParent(canvasRectTransform);
            cardIcons[i].name = "ContainerCardIcon" + i.ToString();
            //cardIcons[i].name = "ContainerCardIcon" + i.ToString();
            cardIcons[i].GetComponent<CardIcon>().SetNDX(BookSystem.GetEquip(i));
            // ! cardIcons[i].GetComponent<CardIcon>().SetNDX(-1);
            // ! Get  NDX from GameObject.GetComponent<Package>().GetEquipNDX(i)
        }
    }

    private void AssemblySetParent(){
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();
        for (int i = 0; i <3; i++){
            cardIcons[i].GetComponent<RectTransform>().SetParent(canvasRect);
        }
        // component.GetComponent<RectTransform>().SetParent(canvasRect);
    }
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that?
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();
        this.gameObject.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();

        // int a = (int)ElementInformation.ElementIndex.e_fire;

        for (int i = 0; i <3; i++){
            //! not general
            //TODO adapt
            Vector2 positionBase_ = GetPosition();
            Vector2 positionBias_ = new Vector2((i - 1)*GetUnitSize().x / 3f, 0f);
            Vector2 unitSize_ = new Vector2(GetUnitSize().x / 4f, GetUnitSize().y*3f / 4f);
            cardIcons[i].GetComponent<CardIcon>().SetPosition(positionBase_ + positionBias_);
            cardIcons[i].GetComponent<CardIcon>().SetUnitSize(unitSize_);

            cardIcons[i].GetComponent<CardIcon>().SetNDX(BookSystem.GetEquip(i));

            // Vector2 positionBase_ = new Vector2(canvasManager.GetCameraSize().x*3f / 40f, -canvasManager.GetCameraSize().y / 4f);
            // Vector2 positionBias_ = new Vector2(canvasManager.GetCameraSize().x / 8f, 0f);
            // Vector2 unitSize_ = new Vector2(canvasManager.GetCameraSize().x / 10f, canvasManager.GetCameraSize().y / 5f);
            

        }
    }
    public void AssemblyDestroy(){
        for (int i = 0; i <3; i++){
            cardIcons[i].GetComponent<CardIcon>().AssemblyDestroy();
        }
        Destroy(this.gameObject);
    }
    
    public Vector2 GetCardIconPosition(int ndx){
        return cardIcons[ndx].GetComponent<RectTransform>().anchoredPosition;
    }

    public Vector2 GetCardIconUnitSize(int ndx){
        return cardIcons[ndx].GetComponent<RectTransform>().sizeDelta;
    }

    public int InContainer(){
        for (int i = 0; i<3; i++){
            if(canvasManager.InBound(GetCardIconPosition(i), GetCardIconUnitSize(i))){
                return i;
            }
        }
        return -1;
    }

    public int GetContainerNDX(int i){
        return cardIcons[i].GetComponent<CardIcon>().GetNDX();
    }
}
