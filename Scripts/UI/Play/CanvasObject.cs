using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasObject : MonoBehaviour
{
    public Vector2 unitSize;
    private RectTransform rect;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private float PixelPerUnit()
    {
        GameObject camera = GameObject.Find("Main Camera");
        float cameraHeight = camera.GetComponent<Camera>().orthographicSize;
        float canvasHeight = GameObject.Find("Canvas").GetComponent<RectTransform>().sizeDelta.y;

        float ppu = canvasHeight/(cameraHeight*2f);
        return ppu;
    }

    private void AlignSize()
    {
        rect = this.GetComponent<RectTransform>();
        rect.sizeDelta = unitSize * PixelPerUnit();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
