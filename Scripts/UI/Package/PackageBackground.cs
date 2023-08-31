using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackageBackground : MonoBehaviour
{
    // ? card properties
    private Vector2 position;
    private Vector2 unitSize;
    private RectTransform rectTransform; // ? self recttransform
    CanvasManager canvasManager;


    // ? components
    // ! Initiallized in Awake()
    // TODO update
    private GameObject component = null; // TODO BackgroundNDX()

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

        AssemblyUpdate();

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


    private void AssemblyInstantiate(){
        
    }

    private void AssemblySetParent(){
        RectTransform canvasRect = GameObject.Find("Canvas").GetComponent<RectTransform>();

        if (!component==null){component.GetComponent<RectTransform>().SetParent(canvasRect);}
    }
    private void AssemblyUpdate()
    // cardBackground cardSprite cardNumber
    // TODO update components' properties -- sizeDelta & anchoredPosition
    // ! staying in the initial position!
    // ? we really want that?
    {
        this.gameObject.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();
        if (!component==null){component.GetComponent<RectTransform>().sizeDelta = unitSize * canvasManager.PixelPerUnit();}

        this.gameObject.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();
        if (!component==null){component.GetComponent<RectTransform>().anchoredPosition = position * canvasManager.PixelPerUnit();}
    }
    private void AssemblyDestroy(){
        if (!component==null){Destroy(component);}
        Destroy(this.gameObject);
    }
    
    public bool IsClicked(){
        if (canvasManager.InBound(rectTransform.anchoredPosition, rectTransform.sizeDelta) && Input.GetKeyDown(KeyCode.Mouse0)){
            return true;
        }
        return false;
    }
}
