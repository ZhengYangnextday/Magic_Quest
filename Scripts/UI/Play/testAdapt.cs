using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class testAdapt : MonoBehaviour
{
    //被改变的物体
    public GameObject hero;

    private Sprite[] sprites;
    private int ndx;

    // Start is called before the first frame update
    void Start()
    {
        //找到要被改变图片的物体
        hero = GameObject.Find("Hero");
        ndx = 0;

        sprites = Resources.LoadAll<Sprite>("Sprites"); // 加载所有子图片

        
    }

    // Update is called once per frame
    void Update()
    {

        Sprite sprite = sprites[ndx]; // 获取第二个子图片
        if (Input.GetKeyDown(KeyCode.M)){

            ndx ++;
            //改变图片
            //Debug.Log(sprite.bounds.size); 
            hero.GetComponent<SpriteRenderer>().sprite = sprite;
            //hero.GetComponent<Transform>().sizeDelta = sprite.bounds.size;

            // SpriteRenderer rend = Hero.GetComponent<SpriteRenderer>();
            // rend.material.SetTexture("_MainTex", targetImg); // textures[0]是你想要的贴图
        }
    }
}
