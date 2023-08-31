using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
class exit{
    public int target_room_id;
    public int target_exit_id;
}
class scene{
    public string template_name;
    //拷贝模板的名字
    public int hard;
    public List<exit> transport;//传送门
    public List<exit> rightpointer;//右出口，找房间左出口
    public List<exit> leftpointer;//左出口，找房间右出口
    public scene(){
        transport = new List<exit>();
        rightpointer = new List<exit>();
        leftpointer = new List<exit>();
    }
    //注意，这里编号一定要与scene中的编号相同
}
public class SceneManage : MonoBehaviour
{
    // Start is called before the first frame update
    private static List<scene> _scenes = new List<scene>();
    private static AsyncOperation loadingOperation;
    private static int nowSceneNum = 0;
    private static int new_room_id;
    private static int exitType;
    private static int toExitType;
    private static int exitId; 
    private static int room_id;
    private static exit searchTargetRoom;
    private static int mode = -1;
    private static bool isCreateNew = false;
    public static bool isBegin = true;
    public static float HeroLife = 30f;
    public static float HeroMaxLife = 30f;

    public static List<string> easySceneName = new List<string>();
    public static List<string> normalSceneName = new List<string>();
    public static List<string> hardSceneName = new List<string>();

    public enum exitTypeIndex{
        transport,
        right,
        left
    }
    //0代表初始场景
    void Awake(){
        easySceneName.Clear();
        //easySceneName.Add("begin");
        easySceneName.Add("ele_1");
        easySceneName.Add("ele_2");
        easySceneName.Add("ele_3");
        easySceneName.Add("ele_4");
        easySceneName.Add("ele_5");
        normalSceneName.Add("Boss");
        for(int i = 0; i < easySceneName.Count; i++){
            string temp = easySceneName[i];
            int id = Random.Range(i, easySceneName.Count);
            easySceneName[i] = easySceneName[id];
            easySceneName[id] = temp;
        }
        // easySceneName.Add("map1");
        // easySceneName.Add("map2");
        // easySceneName.Add("map3");
        // easySceneName.Add("map4");
        // easySceneName.Add("3");
        // easySceneName.Add("4");
    }
    
    void Start()
    {
        HeroLife = 30f;
        HeroMaxLife = 30f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void SceneUpdate(){
        if(mode == -1){

        }else if(mode == 0){
            CreateNewSceneExtra();
        }else if(mode == 1){
            CreateExistSceneExtra();
        }else if(mode == 2){
            BeginSceneExtra();
        }
    }
    public static string ToSceneName(int exitType_, int exitId_, int room_id_){
        exit searchTargetRoom_;
        switch(exitType_){
            case (int)exitTypeIndex.transport:
                searchTargetRoom_ = _scenes[room_id_].transport[exitId_];
                break;
            case (int)exitTypeIndex.left:
                searchTargetRoom_ = _scenes[room_id_].leftpointer[exitId_];
                break;
            case (int)exitTypeIndex.right:
                searchTargetRoom_ = _scenes[room_id_].rightpointer[exitId_];
                break;
            default:
                Debug.LogError("Wrong exit type");
                return "unknown";
        }
        if(searchTargetRoom_.target_room_id == -1){
            return "unknown";
        }else{
            return _scenes[searchTargetRoom_.target_room_id].template_name;
        }
    }
    public static void ChangeScene(int exitType_, int exitId_, int room_id_){
        Debug.Log(exitType_);
        exitType = exitType_;
        exitId = exitId_;
        room_id = room_id_;
        //searchTargetRoom = new exit();
        toExitType = -1;
        switch(exitType){
            case (int)exitTypeIndex.transport:
                searchTargetRoom = _scenes[room_id].transport[exitId];
                toExitType = (int)exitTypeIndex.transport;
                break;
            case (int)exitTypeIndex.left:
                searchTargetRoom = _scenes[room_id].leftpointer[exitId];
                toExitType = (int)exitTypeIndex.right;
                break;
            case (int)exitTypeIndex.right:
                searchTargetRoom = _scenes[room_id].rightpointer[exitId];
                toExitType = (int)exitTypeIndex.left;
                break;
            default:
                Debug.LogError("Wrong exit type");
                return;
        }
        //这里要注意的是，transport对应transport，left对应left，right对应right
        //string nowScenePath = "Scenes/Test/Scene_Change/Temp/" + nowSceneNum.ToString();
        //string nowScenePath = "Assets/Scenes/scene_wy/map" + nowSceneNum.ToString();
        if(searchTargetRoom.target_room_id == -1){
            isCreateNew = true;
            //创建新场景
            new_room_id = _scenes.Count;
            Debug.Log(new_room_id);
            CreateNewScene();
            //SceneManager.LoadScene("Scenes/Test/Scene_Change/Temp/" + new_room_id.ToString(), LoadSceneMode.Single);
            mode = 0;
            // GameObject sceneManager = GameObject.Find("SceneManager");


            // SingleSceneManage singleSceneManage = sceneManager.GetComponent<SingleSceneManage>();
            // SetIdHard(singleSceneManage, new_room_id);

            // int numForTransport = singleSceneManage.transportExit.Count;
            // int numForLeft = singleSceneManage.leftExit.Count;
            // int numForRight = singleSceneManage.rightExit.Count;
            // for(int i = 0; i < numForTransport; i++){
            //     exit newExit = new exit();
            //     newExit.target_exit_id = -1;
            //     newExit.target_room_id = -1;
            //     _scenes[new_room_id].transport.Add(newExit);
            // }
            // for(int i = 0; i < numForLeft; i++){
            //     exit newExit = new exit();
            //     newExit.target_exit_id = -1;
            //     newExit.target_room_id = -1;
            //     _scenes[new_room_id].leftpointer.Add(newExit);
            // }
            // for(int i = 0; i < numForRight; i++){
            //     exit newExit = new exit();
            //     newExit.target_exit_id = -1;
            //     newExit.target_room_id = -1;
            //     _scenes[new_room_id].rightpointer.Add(newExit);
            // }
            // int randomExitIndex;
            // exit nowExit = new exit();
            // nowExit.target_exit_id = exitId;
            // nowExit.target_room_id = room_id;
            // switch(exitType){
            //     case (int)exitTypeIndex.transport:
            //         randomExitIndex = Random.Range(0, numForTransport);
            //         _scenes[new_room_id].transport[randomExitIndex] = nowExit; 
            //         singleSceneManage.InitHero((int)exitTypeIndex.transport, randomExitIndex);
            //         break;
            //     case (int)exitTypeIndex.right:
            //         randomExitIndex = Random.Range(0, numForLeft);
            //         _scenes[new_room_id].leftpointer[randomExitIndex] = nowExit; 
            //         singleSceneManage.InitHero((int)exitTypeIndex.left, randomExitIndex);
            //         break;
            //     case (int)exitTypeIndex.left:
            //         randomExitIndex = Random.Range(0, numForRight);
            //         _scenes[new_room_id].rightpointer[randomExitIndex] = nowExit; 
            //         singleSceneManage.InitHero((int)exitTypeIndex.right, randomExitIndex);
            //         break;
            //     default:
            //         Debug.LogError("Wrong exit type");
            //         return;
            // }
            // searchTargetRoom.target_exit_id = randomExitIndex;
            // searchTargetRoom.target_room_id = new_room_id;

            // //以上代码有点屎山
            // nowSceneNum = new_room_id;//将当前room房间号改为new_room_id
        }//以上为没有链接的情况
        else
        {
            CreateExistScene(searchTargetRoom);
            mode = 1;
            // SceneManager.LoadScene("Scenes/Test/Scene_Change/Temp/" + searchTargetRoom.target_room_id + ".unity", LoadSceneMode.Single);
            // GameObject sceneManager = GameObject.Find("SceneManager");

            // SingleSceneManage singleSceneManage = sceneManager.GetComponent<SingleSceneManage>();
            // SetIdHard(singleSceneManage, searchTargetRoom.target_room_id);
            // singleSceneManage.InitHero(toExitType, searchTargetRoom.target_room_id);
            // AssetDatabase.DeleteAsset(nowScenePath);
            // AssetDatabase.SaveAssets();
            // nowSceneNum = searchTargetRoom.target_room_id;//将当前room房间号改为searchTargetRoom.target_room_id
        }
    }           
    private static void CreateNewScene(){
        int randomIndex;
        string scenePath = "";
        // do{
        //     randomIndex = Random.Range(0, easySceneName.Count);
        //     scenePath = easySceneName[randomIndex];
        // } while(scenePath == _scenes[nowSceneNum].template_name);
        scenePath = easySceneName[_scenes.Count % easySceneName.Count];
        if(_scenes.Count == 6){
            scenePath = normalSceneName[0];
        }
        scene new_scene = new scene();
        new_scene.template_name = scenePath;
        new_scene.hard = _scenes[nowSceneNum].hard + 1;
        _scenes.Add(new_scene);
        scenePath = "Scenes/Formal/Map/" + scenePath;
        SceneManager.LoadScene(scenePath);
        // // string newScenePath = "Scenes/Test/Scene_Change/Temp/" + new_room_id.ToString() + ".unity";
        // // AssetDatabase.CopyAsset(scenePath, newScenePath);
        // // AssetDatabase.SaveAssets();
        // Debug.Log("Scene copied to: " + newScenePath);
    }//这里暂时全部以简单房间举例了，后面再改

    private static void CreateExistScene(exit searchTargetRoom){
        string scenePath = "Scenes/Formal/Map/" + _scenes[searchTargetRoom.target_room_id].template_name;
        SceneManager.LoadScene(scenePath);
        // string newScenePath = "Scenes/Test/Scene_Change/Temp/" + searchTargetRoom.target_room_id.ToString() + ".unity";
        // AssetDatabase.CopyAsset(scenePath, newScenePath);
        // AssetDatabase.SaveAssets();
        // Debug.Log("Scene copied to: " + newScenePath);
    }

    public static void BeginScene(){
        _scenes.Clear();
        string scenePath = "begin";
        scene new_scene = new scene();
        new_scene.template_name = scenePath;
        new_scene.hard = 0;
        _scenes.Add(new_scene);
        scenePath = "Scenes/Formal/Map/begin";
        // // string newScenePath = "Scenes/Test/Scene_Change/Temp/0.unity";
        // // AssetDatabase.CopyAsset(scenePath, newScenePath);
        // // AssetDatabase.SaveAssets();
        SceneManager.LoadScene(scenePath, LoadSceneMode.Single);
        nowSceneNum = 0;
        mode = 2;
        // GameObject sceneManager = GameObject.Find("SceneManager");
        // SingleSceneManage singleSceneManage = sceneManager.GetComponent<SingleSceneManage>();
        // int numForTransport = singleSceneManage.transportExit.Count;
        // int numForLeft = singleSceneManage.leftExit.Count;
        // int numForRight = singleSceneManage.rightExit.Count;
        // for(int i = 0; i < numForTransport; i++){
        //     exit newExit = new exit();
        //     newExit.target_exit_id = -1;
        //     newExit.target_room_id = -1;
        //     _scenes[0].transport.Add(newExit);
        // }
        // for(int i = 0; i < numForLeft; i++){
        //     exit newExit = new exit();
        //     newExit.target_exit_id = -1;
        //     newExit.target_room_id = -1;
        //     _scenes[0].leftpointer.Add(newExit);
        // }
        // for(int i = 0; i < numForRight; i++){
        //     exit newExit = new exit();
        //     newExit.target_exit_id = -1;
        //     newExit.target_room_id = -1;
        //     _scenes[0].rightpointer.Add(newExit);
        // }
        // singleSceneManage.SetHeroBegin();
    }

    private static void SetIdHard(SingleSceneManage singleSceneManage, int room_id){
        singleSceneManage.SetRoomId(room_id);
        singleSceneManage.SetExit();
        singleSceneManage.SetHard(_scenes[room_id].hard);
    }

    private static void CreateNewSceneExtra(){
        GameObject sceneManager = GameObject.Find("SceneManager");
        SingleSceneManage singleSceneManage = sceneManager.GetComponent<SingleSceneManage>();
        SetIdHard(singleSceneManage, new_room_id);
        singleSceneManage.SetExit();

        int numForTransport = singleSceneManage.transportExit.Count;
        int numForLeft = singleSceneManage.leftExit.Count;
        int numForRight = singleSceneManage.rightExit.Count;
        for(int i = 0; i < numForTransport; i++){
            exit newExit = new exit();
            newExit.target_exit_id = -1;
            newExit.target_room_id = -1;
            _scenes[new_room_id].transport.Add(newExit);
        }
        for(int i = 0; i < numForLeft; i++){
            exit newExit = new exit();
            newExit.target_exit_id = -1;
            newExit.target_room_id = -1;
            _scenes[new_room_id].leftpointer.Add(newExit);
        }
        for(int i = 0; i < numForRight; i++){
            exit newExit = new exit();
            newExit.target_exit_id = -1;
            newExit.target_room_id = -1;
            _scenes[new_room_id].rightpointer.Add(newExit);
        }
        int randomExitIndex;
        exit nowExit = new exit();
        nowExit.target_exit_id = exitId;
        nowExit.target_room_id = room_id;
        switch(exitType){
            case (int)exitTypeIndex.transport:
                //Debug.Log("transport");
                randomExitIndex = Random.Range(0, numForTransport);
                _scenes[new_room_id].transport[randomExitIndex] = nowExit; 
                singleSceneManage.InitHero((int)exitTypeIndex.transport, randomExitIndex);
                break;
            case (int)exitTypeIndex.right:
                Debug.Log("left");
                randomExitIndex = Random.Range(0, numForLeft);
                _scenes[new_room_id].leftpointer[randomExitIndex] = nowExit;
                singleSceneManage.InitHero((int)exitTypeIndex.left, randomExitIndex);
                break;
            case (int)exitTypeIndex.left:
                Debug.Log("right");
                randomExitIndex = Random.Range(0, numForRight);
                _scenes[new_room_id].rightpointer[randomExitIndex] = nowExit;
                singleSceneManage.InitHero((int)exitTypeIndex.right, randomExitIndex);
                break;
            default:
                Debug.LogError("Wrong exit type");
                return;
        }
        searchTargetRoom.target_exit_id = randomExitIndex;
        searchTargetRoom.target_room_id = new_room_id;

        //以上代码有点屎山

        nowSceneNum = new_room_id;//将当前room房间号改为new_room_id
        mode = -1;
    }

    private static void CreateExistSceneExtra(){
        GameObject sceneManager = GameObject.Find("SceneManager");
        Debug.Log("Exit");
        SingleSceneManage singleSceneManage = sceneManager.GetComponent<SingleSceneManage>();
        SetIdHard(singleSceneManage, searchTargetRoom.target_room_id);
        singleSceneManage.SetExit();
        singleSceneManage.InitHero(toExitType, searchTargetRoom.target_exit_id);
        Debug.Log("Init");
        nowSceneNum = searchTargetRoom.target_room_id;//将当前room房间号改为searchTargetRoom.target_room_id
        mode = -1;
    }
    private static void BeginSceneExtra(){
        Debug.Log("beginscene");
        GameObject sceneManager = GameObject.Find("SceneManager");
        SingleSceneManage singleSceneManage = sceneManager.GetComponent<SingleSceneManage>();
        int numForTransport = singleSceneManage.transportExit.Count;
        int numForLeft = singleSceneManage.leftExit.Count;
        int numForRight = singleSceneManage.rightExit.Count;
        for(int i = 0; i < numForTransport; i++){
            exit newExit = new exit();
            newExit.target_exit_id = -1;
            newExit.target_room_id = -1;
            _scenes[0].transport.Add(newExit);
        }
        for(int i = 0; i < numForLeft; i++){
            exit newExit = new exit();
            newExit.target_exit_id = -1;
            newExit.target_room_id = -1;
            _scenes[0].leftpointer.Add(newExit);
        }
        for(int i = 0; i < numForRight; i++){
            exit newExit = new exit();
            newExit.target_exit_id = -1;
            newExit.target_room_id = -1;
            _scenes[0].rightpointer.Add(newExit);
        }
        singleSceneManage.SetHeroBegin();
        singleSceneManage.SetExit();
        nowSceneNum = 0;
        mode = -1;
    }
    public static int GetTotalStageNum(){
        return _scenes.Count;
    }
}
