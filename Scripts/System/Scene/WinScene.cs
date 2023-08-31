using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class WinScene : MonoBehaviour
{
    // Start is called before the first frame update
// Start is called before the first frame update

    private bool inRange;
    private GameObject hintBox;
    private GameObject hero;
    void Start()
    {
        hero = GameObject.Find("Hero");
    }

    // Update is called once per frame
    void Update()
    {
        StateChange();
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
            string scenePath = "Scenes/Formal/System/WinMenu";
            SceneManager.LoadScene(scenePath);
        }
    }
}
