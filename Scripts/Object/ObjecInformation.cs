using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjecInformation : MonoBehaviour
{
    // Start is called before the first frame update
    private enum sObjectLabel{
        unknown,
        magic,
        trap,
        treasure
    }
    public int mObjectLabel = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void ObjectCheck(){
        switch(mObjectLabel){
            case (int)sObjectLabel.magic:
                if(gameObject.GetComponent<MagicInformation>() == null){
                    Destroy(gameObject);
                }
                break;
            case (int)sObjectLabel.trap:
                if(gameObject.GetComponent<TrapInformation>() == null){
                    Destroy(gameObject);
                }
                break;
            case (int)sObjectLabel.treasure:
                if(gameObject.GetComponent<TreasureInformation>() == null){
                    Destroy(gameObject);
                }
                break;
            default:
                Destroy(gameObject);
                break;
        }
    }
}
