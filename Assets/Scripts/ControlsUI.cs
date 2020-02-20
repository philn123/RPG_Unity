using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsUI : MonoBehaviour
{

    public GameObject controlsUI;
    // Start is called before the first frame update
    //void Start()
    //{
        
    //}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Controls"))
        {
            controlsUI.SetActive(!controlsUI.activeSelf);
        }
        
    }
}
