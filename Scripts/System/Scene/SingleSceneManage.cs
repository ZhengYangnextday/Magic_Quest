using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleSceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> transportExit;
    public List<GameObject> leftExit;
    public List<GameObject> rightExit;
    public GameObject beginPoint;//只有最初的地图才有
    private int roomId;
    private int hard;
    void Awake(){
        SceneManage.SceneUpdate();
    }
    void Start()
    {
        //SceneManage.SceneUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        //SceneManage.SceneUpdate();
        SetExit();
    }
    public void SetExit(){
        int i = 0;
        foreach(GameObject exits in transportExit){
            exits.GetComponent<ExitManage>().SetRoomId(roomId);
            exits.GetComponent<ExitManage>().SetExit((int)SceneManage.exitTypeIndex.transport,i);
            i++;
        }
        i = 0;
        foreach(GameObject exits in leftExit){
            exits.GetComponent<ExitManage>().SetRoomId(roomId);
            exits.GetComponent<ExitManage>().SetExit((int)SceneManage.exitTypeIndex.left,i);
            i++;
        }
        i = 0;
        foreach(GameObject exits in rightExit){
            exits.GetComponent<ExitManage>().SetRoomId(roomId);
            exits.GetComponent<ExitManage>().SetExit((int)SceneManage.exitTypeIndex.right,i);
            i++;
        }
        //GameObject hero = GameObject.Find("Hero");
        //hero.SetActive(true);
    }
    public void SetRoomId(int sceneRoomId){
        roomId = sceneRoomId;
    }
    public void SetHard(int sceneHard){
        hard = sceneHard;
    }
    public void InitHero(int exitType, int exitId){
        GameObject hero = GameObject.Find("Hero");
        GameObject exit;
        switch(exitType){
            case (int)SceneManage.exitTypeIndex.transport:
                exit = transportExit[exitId];
                break;
            case (int)SceneManage.exitTypeIndex.right:
                exit = rightExit[exitId];
                break;
            case (int)SceneManage.exitTypeIndex.left:
                exit = leftExit[exitId];
                break;
            default:
                return;
        }
        hero.transform.position = new Vector3(exit.transform.position[0], exit.transform.position[1], 0);//
        Debug.Log(hero.transform.position);
        hero.SetActive(true);
    }
    public void SetHeroBegin(){
        GameObject hero = GameObject.Find("Hero");
        Debug.Log(hero == null);
        Debug.Log("SetHeroBegin");
        roomId = 0;
        hard = 0;
        if(beginPoint != null && SceneManage.isBegin == true){
            hero.transform.position = new Vector3(beginPoint.transform.position[0], beginPoint.transform.position[1], 0);
            Debug.Log("SetHeroBegin");
            hero.SetActive(true);
            Debug.Log("a");
            SceneManage.isBegin = false;
            return;
        }else{
            Debug.Log("This is not beginning map");
        }
    }
}
