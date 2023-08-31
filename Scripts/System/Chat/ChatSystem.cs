using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatSystem : MonoBehaviour
{
    // Start is called before the first frame update
    public string[] dialogue;
    public GameObject dialogueBox;
    public Text dialogueText;
    private int state = -1;
    private bool inRange = false;
    public GameObject hint;
    void Awake(){
        hint.transform.position = new Vector3(transform.position[0], transform.position[1] + 3, 1);
    }
    void Start()
    {
        dialogueBox.SetActive(false);
    }

    void Update()
    {
        StateChange();
        Show();
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
            state = -1;
            dialogueBox.SetActive(false);
        }
    }

    void StateChange(){
        if (Input.GetKeyDown(KeyCode.Space) && inRange){
            state += 1;
            if(state >= dialogue.Length){
                state = -1;
            }
        }
    }

    void Show(){
        if(inRange){
            if(state == -1){
                dialogueBox.SetActive(false);
                hint.SetActive(true);
            }else{
                dialogueBox.SetActive(true);
                hint.SetActive(false);
                dialogueText.text = dialogue[state];
            }
        }else{
            dialogueBox.SetActive(false);
            hint.SetActive(false);
        }
    }
}
