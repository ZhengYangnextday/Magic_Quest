using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private float cameraHeight;
    private float cameraWidth;
    private float canvasHeight;
    private float canvasWidth;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCameraCanvasSize();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraCanvasSize();
        // DebugCameraCanvasSize();
    }

    public float PixelPerUnit()
    {
        float ppu = canvasHeight / cameraHeight;
        // Debug.Log(ppu);
        return ppu;
    }

    public void UpdateCameraCanvasSize()
    // ! size of camera & canvas
    //? CC stands for Camera&Canvas
    {
        GameObject camera = GameObject.Find("Main Camera");
        GameObject canvas = GameObject.Find("Canvas");
        // ! H&W are units instead of pixels
        // ! with PixelPerUnit() we konw the size of canvas
        cameraHeight = 2 * camera.GetComponent<Camera>().orthographicSize;
        cameraWidth = cameraHeight * camera.GetComponent<Camera>().aspect;

        canvasHeight = canvas.GetComponent<RectTransform>().sizeDelta.y;
        canvasWidth = canvas.GetComponent<RectTransform>().sizeDelta.x;
    }

    public Vector2 GetCameraSize(){
        // Debug.Log(new Vector2(cameraWidth, cameraHeight));
        return new Vector2(cameraWidth, cameraHeight);
    }

    public Vector2 GetCanvasSize(){
        return new Vector2(canvasWidth, canvasHeight);
    }

    public Vector2 MousePosition()
    // ? pixel position of the mouse
    {
        Vector3 cameraMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);// ! unit!
        Vector3 cameraPosition = GameObject.Find("Main Camera").GetComponent<Transform>().position;
        Vector3 position = PixelPerUnit() * (new Vector2 (cameraMousePosition.x-cameraPosition.x, cameraMousePosition.y - cameraPosition.y));
        // Debug.Log(cameraMousePosition == cameraPosition);
        return position;
    }

    public void DebugCameraCanvasSize(){
        Debug.Log("cameraWidth: "+cameraWidth.ToString());
        Debug.Log("canvasWidth: "+canvasWidth.ToString());
        Debug.Log("cameraHeight: "+cameraHeight.ToString());
        Debug.Log("canvasHeight: "+canvasHeight.ToString());
    }

    public bool InBound(Vector2 postion, Vector2 size)
    // ! pixel
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
