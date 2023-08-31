using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManage : MonoBehaviour
{
    // Start is called before the first frame update
    public int exitType;
    private int exitId;
    public int roomId;
    private bool inRange;
    private GameObject hint;
    private GameObject hero;
    void Start()
    {
        hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        StateChange();
        Show();
    }

    public void SetExit(int sceneExitType, int sceneExitId){
        exitType = sceneExitType;
        exitId = sceneExitId;
    }
    public void SetRoomId(int sceneRoomId){
        roomId = sceneRoomId;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Hero>())
        {
            Debug.Log("Enter");
            inRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Hero>())
        {
            inRange = false;
            //hintBox.SetActive(false);
        }
    }
    void StateChange(){
        if (Input.GetKeyDown(KeyCode.Space) && inRange){
            SceneManage.HeroLife = hero.GetComponent<CreatureInformation>().mheartPoint;
            SceneManage.HeroMaxLife = hero.GetComponent<CreatureInformation>().mMaxHeartPoint;
            SceneManage.ChangeScene(exitType, exitId, roomId);
        }
    }
    void Show(){
        
        Transform child = transform.Find(SceneManage.ToSceneName(exitType, exitId, roomId));
        Debug.Log(SceneManage.ToSceneName(exitType, exitId, roomId));
            if(child){
                hint = child.gameObject;
            if(inRange){
                if(hint){
                    hint.SetActive(true);
                }
            }else{
                if(hint){
                    hint.SetActive(false);
                }
            }
        }
    }
}
