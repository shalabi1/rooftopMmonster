using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{

    


    [SerializeField] private KeyCode PauseKey;

    public GameObject Player;
    public GameObject Pause;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(PauseKey))
        {
            print("you paused");
            //GetComponent<PlayerMovment2>().enabled = false;
            Player.GetComponent<LookScript>().enabled = false;
            Pause.active = true;
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
        }
    }


    public void resume()
    {
        Player.GetComponent<LookScript>().enabled = true;
        Pause.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
        print("You Resumed");
    }
}
