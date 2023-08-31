using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioClip soundClip; // 要播放的声音
    private Color initcolor;
    private AudioSource audioSource;
    void Awake(){
        //gameObject.transform.localPosition = Vector3.zero;
        audioSource = GetComponent<AudioSource>();
        audioSource.PlayOneShot(soundClip);
    }
    void Start()
    {
        gameObject.transform.localPosition = Vector3.zero;
        initcolor = gameObject.GetComponent<SpriteRenderer>().material.color;
    }
    float timer = 0;
    // Update is called once per frame
    void Update()
    {
        if(audioSource.isPlaying){
            timer += Time.deltaTime;
            gameObject.GetComponent<SpriteRenderer>().material.color = initcolor * new Vector4(1,1,1,1 - timer/audioSource.clip.length);
            if(timer >= audioSource.clip.length - 0.2f){
                audioSource.Stop();
                audioSource.enabled = false;
                Destroy(gameObject);
            }
        }
    }
}
