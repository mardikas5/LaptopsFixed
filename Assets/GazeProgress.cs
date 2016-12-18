using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GazeProgress : MonoBehaviour
{

    public static GazeProgress Instance { get; private set; }

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetProgress( float f )
    {
        GetComponent<Image>().fillAmount = f;
    }
}
