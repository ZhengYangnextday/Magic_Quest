using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasBook : MonoBehaviour
{
    GameObject card0;
    private bool trackStatus;
    // Start is called before the first frame update
    void Start()
    {
        card0 = Instantiate(Resources.Load("Test_UI/Option/optionSurface") as GameObject);
        card0.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());
        card0.GetComponent<RectTransform>().anchoredPosition = new Vector2(0f, 0f);
        card0.GetComponent<Option>().unitSize = new Vector2 (3f,2f);
        Debug.Log(GameObject.Find("Container").GetComponent<RectTransform>().sizeDelta);


    }

    // Update is called once per frame
    void Update()
    {
        // if (InBound(card0.GetComponent<RectTransform>().anchoredPosition, card0.GetComponent<RectTransform>().sizeDelta) && Input.GetKey(KeyCode.Mouse0) && !trackStatus)
        // {
        //     Debug.Log(card0.GetComponent<RectTransform>().sizeDelta);
        //     trackStatus = true;
        // }
        // if (trackStatus)
        // {
        //     card0.GetComponent<RectTransform>().anchoredPosition = MousePosition();
        //     if (Input.GetKeyUp(KeyCode.Mouse0)){
        //         trackStatus = false;
        //         if (InBound(new Vector2(300f,0f), new Vector2(100f,150f))){
        //             card0.GetComponent<RectTransform>().anchoredPosition = new Vector2(300f,0f);
        //             card0.GetComponent<RectTransform>().sizeDelta = new Vector2(100f,150f);
        //         }
                
        //     }
        // }
    }
    private float PixelPerUnit()
    {
        GameObject camera = GameObject.Find("Main Camera");
        float cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        float canvasHeight = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;

        float ppu = canvasHeight/cameraHeight;
        return ppu;
    }
    private Vector2 MousePosition()
    // ? pixel position of the mouse
    {
        GameObject camera = GameObject.Find("Main Camera");
        Vector3 cameraPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);// ! unit
        float cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        float cameraWidth = 2 * camera.GetComponent<Camera>().orthographicSize * camera.GetComponent<Camera>().aspect;

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
