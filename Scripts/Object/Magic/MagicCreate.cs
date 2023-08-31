using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MagicPrefabContainer{
    public GameObject[] magicobjects = new GameObject[8];
} 
public class MagicCreate : MonoBehaviour
{
    static Dictionary<int, MagicPrefabContainer> MagicDir = new Dictionary<int, MagicPrefabContainer>();
    private enum MagicPrefabIndex{
        D,
        C,
        B,
        A,
        S,
        Defend,
        Heal,
        Disappear
    }
    public enum MagicStateindex{
        Attack,
        Defend,
        Heal
    }
    // Start is called before the first frame update
    void Awake()
    {
        MagicDir.Clear();
        MagicPrefabContainer container = new MagicPrefabContainer();
        SetContainer(container, "Prefabs/Magic/Green");
        MagicDir.Add((int)ElementInformation.ElementIndex.e_wood, container);
        container = new MagicPrefabContainer();
        SetContainer(container, "Prefabs/Magic/Red");
        MagicDir.Add((int)ElementInformation.ElementIndex.e_fire, container);
        container = new MagicPrefabContainer();
        SetContainer(container, "Prefabs/Magic/Blue");
        MagicDir.Add((int)ElementInformation.ElementIndex.e_water, container);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void CreateAttackMagic(int rank, int type, Vector3 direction, Vector3 position, string tag, bool iskilledByMap = true){
        MagicPrefabContainer magic_container = MagicDir[type];
        if(direction.magnitude != 0){
            direction = direction / direction.magnitude;
        }else{
            direction = Vector3.zero;
        }
        if(magic_container == null){
            Debug.Log("Container Not Found");
            return;
        }
        GameObject magic = magic_container.magicobjects[rank];
        if(magic == null){
            Debug.Log("Now Magic Not Found");
            return;
        }
        GameObject now_magic = Instantiate(magic);
        MagicInformation magicInformation = now_magic.GetComponent<MagicInformation>();
        magicInformation.isDirectionSet = true;
        magicInformation.setDirection = direction;
        magicInformation.killedByMap = iskilledByMap;
        if(rank >= 3){
            magicInformation.killedByMap = false;
        }
        now_magic.tag = tag;
        magicInformation.Init();
        now_magic.transform.position = position;
    }
    public static void CreateDefendMagic(int rank, int type, GameObject Follower){
        MagicPrefabContainer magic_container = MagicDir[type];
        if(magic_container == null){
            Debug.Log("Container Not Found");
            return;
            
        }
        GameObject magic = magic_container.magicobjects[(int)MagicPrefabIndex.Defend];
        if(magic == null){
            Debug.Log("Now Magic Not Found");
            return;
        }
        GameObject now_magic = Instantiate(magic);
        MagicInformation magicInformation = now_magic.GetComponent<MagicInformation>();
        magicInformation.killedByMap = false;
        ElementInformation elementInformation = now_magic.GetComponent<ElementInformation>();
        elementInformation.SetElementRank(rank);
        magicInformation.isDirectionSet = true;
        magicInformation.min_speed = -1f;
        magicInformation.setDirection = Vector3.zero;
        magicInformation.killedByMap = false;
        //now_magic.GetComponent<Rigidbody2D>().isKinematic = true;//禁用组件
        now_magic.tag = Follower.tag;
        now_magic.transform.SetParent(Follower.transform);
        magicInformation.Init();
        now_magic.transform.localPosition = Vector3.zero;
    }
    public static void CreateHealMagic(int rank, int type, GameObject Follower){
        MagicPrefabContainer magic_container = MagicDir[type];
        if(magic_container == null){
            Debug.Log("Container Not Found");
            return;
        }
        GameObject magic = magic_container.magicobjects[(int)MagicPrefabIndex.Heal];
        if(magic == null){
            Debug.Log("Now Magic Not Found");
            return;
        }
        GameObject now_magic = Instantiate(magic);
        Follower.GetComponent<CreatureInformation>().Heal((rank + 1) * 2);
        now_magic.transform.SetParent(Follower.transform);
        now_magic.transform.localPosition = Vector3.zero;
        now_magic.tag = Follower.tag;
    }
    private void SetContainer(MagicPrefabContainer container, string address){
        // container = new MagicPrefabContainer();
        container.magicobjects[0] = Resources.Load(address + "/d") as GameObject;
        container.magicobjects[1] = Resources.Load(address + "/c") as GameObject;
        container.magicobjects[2] = Resources.Load(address + "/b") as GameObject;
        container.magicobjects[3] = Resources.Load(address + "/a") as GameObject;
        container.magicobjects[4] = Resources.Load(address + "/s") as GameObject;
        container.magicobjects[5] = Resources.Load(address + "/Defend") as GameObject;
        container.magicobjects[6] = Resources.Load(address + "/Heal") as GameObject;
        container.magicobjects[7] = Resources.Load(address + "/Disappear") as GameObject;
    }
    private void CheckContainer(MagicPrefabContainer container){
        for(int i = 0; i < 8; i++){
            if(container.magicobjects[i] == null){
                Debug.Log("Wrong Error");
            }
        }
    }
}
