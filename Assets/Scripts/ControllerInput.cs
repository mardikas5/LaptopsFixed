using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{

    private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)track.index); } }
    [SerializeField]
    private SteamVR_TrackedObject track;

    public delegate void ClickAction(Transform controller);
    public event ClickAction OnTriggerPress;
    public event ClickAction OnTriggerUnPress;

    // Use this for initialization
    void Start()
    {
        //track = this.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (track != null)
        {
            if (controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (OnTriggerPress != null)
                {
                    OnTriggerPress(this.transform);
                }
            }

            if (controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
            {
                if (OnTriggerUnPress != null)
                {
                    OnTriggerUnPress(this.transform);
                }
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (OnTriggerPress != null)
                {
                    OnTriggerPress(this.transform);
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (OnTriggerUnPress != null)
                {
                    OnTriggerUnPress(this.transform);
                }
            }
        }
    }
}
