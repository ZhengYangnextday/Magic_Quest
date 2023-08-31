using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ModeNHealth : MonoBehaviour // 2 unit
{
    private Sprite[] sprites;
    RectTransform rect;
    public Vector2 unitSize;
    private new GameObject camera;
    float cameraHeight;
    float cameraWidth;
    // private bool trackStatus;

    float heartRate = 0; // !

    private int ndx;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject gameobject = this;
        rect = this.GetComponent<RectTransform>();

        camera = GameObject.Find("Main Camera");
        cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        cameraWidth = 2 * camera.GetComponent<Camera>().orthographicSize * camera.GetComponent<Camera>().aspect;

        sprites = Resources.LoadAll<Sprite>("Test_UI/Hero");
        ndx = 0;
        this.GetComponent<Image>().sprite = sprites[ndx];

        // trackStatus = false;
        

        AlignSize(unitSize);
        StablePosition();

        heartRate = 1f;
    }
    void Update()
    {
        cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        cameraWidth = 2 * camera.GetComponent<Camera>().orthographicSize * camera.GetComponent<Camera>().aspect;
        /*
        TODO
        float heartRate = GameObject.Find("Hero").GetComponent<CreatureInformation>().GetHeartRate();
        this.GetComponent<Image>().color = new Color (1f-heartRate, heartRate, 0f, 1f);
        
        */
        // AlignSize(GameObject.Find("ModeNHealth"), unitSize);

        AlignSize(unitSize);
        //rect.sizeDelta = new Vector2(200f, 200f);

        //bool isOverCanvas = EventSystem.current.IsPointerOverGameObject();
        heartRate -= 0.1f * Time.deltaTime;
        // Debug.Log("Heart Rate: " + heartRate);

        this.GetComponent<Image>().color = new Color (1f-heartRate, heartRate, 0f, 1f);

        // if (InBound(rect.anchoredPosition, unitSize*PixelPerUnit()) && Input.GetKey(KeyCode.Mouse0) && !trackStatus)//isOverCanvas && 
        // {
        //     trackStatus = true;
        // }
        // if (trackStatus)//isOverCanvas && 
        // {
            
        //     rect.anchoredPosition = MousePosition();
        //     if (Input.GetKeyUp(KeyCode.Mouse0)){
        //         trackStatus = false;
        //     }
        // }
        // if (!trackStatus){
        //     StablePosition();
        // }
        
        if (InBound(rect.anchoredPosition, unitSize*PixelPerUnit()) && Input.GetKeyDown(KeyCode.Mouse0)){
            ndx = (ndx+1)%2;
            this.GetComponent<Image>().sprite = sprites[6* ndx];
        }
        if (Input.GetKeyDown(KeyCode.R)){
            heartRate = 1f;
        }

        
    }

    private void StablePosition()
    {
        rect.anchoredPosition = new Vector2 (rect.sizeDelta.x / 2 - PixelPerUnit()*cameraWidth / 2, -rect.sizeDelta.y / 2 + PixelPerUnit()*cameraHeight / 2);
    }

    private float PixelPerUnit()
    {
        GameObject camera = GameObject.Find("Main Camera");
        float cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        float canvasHeight = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;

        float ppu = canvasHeight/cameraHeight;
        // Debug.Log(ppu);
        return ppu;
    }

    private void AlignSize(Vector2 unitSize)
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();
        rect.sizeDelta = unitSize * PixelPerUnit();
        // Debug.Log(rect.sizeDelta);
    }

    private Vector2 MousePosition()
    {
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 position = PixelPerUnit() * new Vector2 (cameraPosition.x, cameraPosition.y);
        return position;
    }
    
    private bool InBound(Vector2 postion, Vector2 size)
    {
        Vector2 mousePosition = MousePosition();
        if (mousePosition.x < postion.x - size.x / 2)
        {
            return false;
        }
        if (mousePosition.x > postion.x + size.x / 2)
        {
            return false;
        }
        if (mousePosition.y < postion.y - size.y / 2)
        {
            return false;
        }
        if (mousePosition.y > postion.y + size.y / 2)
        {
            return false;
        }
        return true;
    }
    
}
