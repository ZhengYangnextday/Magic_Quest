using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossUI : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasManager canvasManager;
    void Start()
    {
        canvasManager = GameObject.Find("Canvas").GetComponent<CanvasManager>();
        
        HP = 1f;
        instantiated = false;
    }

    private bool instantiated;
    // Update is called once per frame
    void Update()
    {
        if (GetBossIsShown()){
            if (!instantiated){
                AssemblyInstantiate();
                AssemblySetParent();
                instantiated = true;
            }
            else {
                
            }
        }
        else {
            if (instantiated){
                AssemblyDestroy();
                instantiated = false;
            }
            else{

            }
        }
        AssemblyUpdate();
        HP -= 0.1f * Time.deltaTime; // !
    }

    private GameObject HPBar;
    private GameObject HPBackground;
    private GameObject bossName;
    public GameObject Boss;

    private void AssemblyInstantiate(){
        HPBar = GameObject.Instantiate(Resources.Load("Test_UI/Boss_UI/HPBar") as GameObject);
        HPBackground = GameObject.Instantiate(Resources.Load("Test_UI/Boss_UI/HPBackground") as GameObject);
        bossName = GameObject.Instantiate(Resources.Load("Test_UI/Boss_UI/bossName") as GameObject);
    }

    private void AssemblySetParent(){
        RectTransform canvasRectTransform = GameObject.Find("Canvas").GetComponent<RectTransform>();

        HPBackground.GetComponent<RectTransform>().SetParent(canvasRectTransform);
        HPBar.GetComponent<RectTransform>().SetParent(canvasRectTransform);
        bossName.GetComponent<RectTransform>().SetParent(canvasRectTransform);
    }
    private void AssemblyUpdate(){
        float HPRate = GetBossHeartRate();

        HPBar.GetComponent<Template>().SetPosition(new Vector2((HPRate-1)* canvasManager.GetCameraSize().x/4f,canvasManager.GetCameraSize().y*11f/32f));
        HPBar.GetComponent<Template>().SetUnitSize(new Vector2(HPRate * canvasManager.GetCameraSize().x/2f,canvasManager.GetCameraSize().y/16f));
        if (HPRate > 0.5) { HPBar.GetComponent<Image>().color = new Color(1 - HPRate, HPRate, 1 - HPRate, 1f); }
        if (HPRate <= 0.5) { HPBar.GetComponent<Image>().color = new Color(1 - HPRate, HPRate, HPRate, 1f); }
        Debug.Log(HPBar.GetComponent<Image>().color);

        HPBackground.GetComponent<Template>().SetPosition(new Vector2(0f,canvasManager.GetCameraSize().y*11f/32f));
        HPBackground.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x/2f,canvasManager.GetCameraSize().y/16f));
        HPBackground.GetComponent<Image>().color = new Color(0f,0f,0f,1f);

        bossName.GetComponent<Template>().SetPosition(new Vector2(0f,canvasManager.GetCameraSize().y*7f/16f));
        bossName.GetComponent<Template>().SetUnitSize(new Vector2(canvasManager.GetCameraSize().x/2f,canvasManager.GetCameraSize().y/8f));
        bossName.GetComponent<Text>().text = "DEATH"; // !
        bossName.GetComponent<Text>().color = Color.black;
    }
    private void AssemblyDestroy(){
        Destroy(HPBar);
        Destroy(HPBackground);
        Destroy(bossName);
    }

    private float HP;
    private float GetBossHeartRate(){
        // !
        if(Boss){
            return Boss.GetComponent<CreatureInformation>().GetHeartRate();
        }else{
            Destroy(this);
            return 0;
        }
    }

    private bool GetBossIsShown(){
        // !
        return true;
    }
}
