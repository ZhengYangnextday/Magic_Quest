using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    // Start is called before the first frame update
    static GameObject text;
    static CanvasManager canvasManager;
    public static float showTime = 0.3f;
    public static float showTimeCounter;
    public static bool isShowTime = false;
    public static Vector2 thisPosition = Vector2.zero;
    void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();

        text = GameObject.Instantiate(Resources.Load("Test_UI/Text_UI/text") as GameObject);
        text.GetComponent<RectTransform>().SetParent(GameObject.Find("Canvas").GetComponent<RectTransform>());

        text.GetComponent<Template>().SetUnitSize(new Vector2(10f,10f));
        text.GetComponent<Template>().SetPosition(new Vector2(0f,0f));
        int fontSize = (int)(canvasManager.GetCanvasSize().y/canvasManager.GetCameraSize().y)/2;
        text.GetComponent<Text>().fontSize = fontSize;

    }

    // Update is called once per frame
    void Update()
    {
        // int fontSize = (int)(canvasManager.GetCanvasSize().y/canvasManager.GetCameraSize().y)/2;
        //string s = "hello wrld";
        // text.GetComponent<Text>().text = "<size=" +fontSize + ">" + s + "</size>";
        // text.GetComponent<Text>().fontSize = fontSize;
        ShowTime();
    }
    public static void SetText(string input_text){
        showTimeCounter = 0f;
        isShowTime = true;
        int fontSize = (int)(canvasManager.GetCanvasSize().y/canvasManager.GetCameraSize().y)/2;
        //string s = "hello wrld";
        // text.GetComponent<Text>().text = "<size=" +fontSize + ">" + s + "</size>";
        text.GetComponent<Text>().fontSize = fontSize;
        text.GetComponent<Text>().text = input_text;
    }
    public static void SetPosition(Vector2 position){
        text.GetComponent<Template>().SetUnitSize(new Vector2(10f,10f));
        text.GetComponent<Template>().SetPosition(position);
    }
    private static void ShowTime(){
        if(isShowTime){
            showTimeCounter += Time.deltaTime;
            int fontSize = (int)(canvasManager.GetCanvasSize().y/canvasManager.GetCameraSize().y)/2;
            text.GetComponent<Text>().fontSize = (int)((int)fontSize * 3 * (showTime - showTimeCounter) / showTime);
        }
        if(showTimeCounter > showTime){
            showTimeCounter = 0;
            SetText("");
            isShowTime = false;
        }
    }
    // public static void CanvasAvoid(){
    //     GameObject canvas = GameObject.Find("Canvas");
    //     if(canvas != null){
            
    //     }
    // }
    
}
