using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    void Awake() {
        Application.targetFrameRate = 30;
        Debug.Log($"Framerate set to {Application.targetFrameRate}.");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
