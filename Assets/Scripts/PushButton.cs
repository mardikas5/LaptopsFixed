using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    private float unpressedPosition;
    private float pressedPosition;
    private float time = 0.2f;
    private bool pressed;
    public LaptopComponentType Type;
    private Coroutine resizeRoutine;
    private bool interactable = true;

    public delegate void OnButtonClickDelegate(bool pressed, LaptopComponentType type);
    public OnButtonClickDelegate OnButtonChanged;

    // Use this for initialization
    void Start()
    {
        unpressedPosition = this.transform.localPosition.y;
        pressedPosition = unpressedPosition - 0.3f;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (interactable)
        {
            if (resizeRoutine != null)
            {
                StopCoroutine(resizeRoutine);
            }

            if (pressed)
            {
                resizeRoutine = StartCoroutine(moveYAxis(unpressedPosition, time));
            }
            else
            {
                resizeRoutine = StartCoroutine(moveYAxis(pressedPosition, time));
            }


            this.pressed = !pressed;

            if (OnButtonChanged != null)
            {
                Debug.Log("Button: " + Type + " pressed: " + pressed);
                OnButtonChanged(pressed, this.Type);
            }
        }
    }

    private IEnumerator moveYAxis(float targetPos, float time)
    {
        interactable = false;

        Vector3 startPos = this.transform.localPosition;
        Vector3 endPos = startPos;
        endPos.y = targetPos;
        float currentTime = 0.0f;

        do
        {
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, currentTime / time);
            currentTime += Time.deltaTime;

            yield return null;
        } while (currentTime <= time);

        this.transform.localPosition = endPos;

        interactable = true;
    }
}
