using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopComponent : MonoBehaviour
{
    [Multiline]
    public string Text;
    public LaptopComponentType Type;
    private Coroutine resizeRoutine;
    private Vector3 intitialPosition;
    private Vector3 shownPosition;
    private float time = 1f;

    public Quaternion localRot;

    public void Start()
    {
        intitialPosition = this.transform.localPosition;
        localRot = this.transform.localRotation;
        shownPosition = intitialPosition + new Vector3(0, 1f, 0);
    }

    public void Show()
    {
        Debug.Log("Show: " + Type);
        if (resizeRoutine != null)
        {
            StopCoroutine(resizeRoutine);
        }

        var direction = Quaternion.LookRotation(this.shownPosition - Camera.current.transform.position, Camera.current.transform.up);

        var l = direction * Quaternion.Inverse(this.transform.parent.rotation);

        resizeRoutine = StartCoroutine(moveYAxis(shownPosition, time, l));
    }

    public void Hide()
    {
        Debug.Log("Hide: " + Type);
        if (resizeRoutine != null)
        {
            StopCoroutine(resizeRoutine);
        }

        resizeRoutine = StartCoroutine(moveYAxis(intitialPosition, time, localRot));
    }

    private IEnumerator moveYAxis(Vector3 targetPos, float time, Quaternion rot)
    {
        Vector3 startPos = this.transform.localPosition;
        Quaternion startRotation = this.transform.localRotation;
        Vector3 endPos = targetPos;
        float currentTime = 0.0f;

        do
        {
            this.transform.localPosition = Vector3.Lerp(startPos, endPos, currentTime / time);
            this.transform.localRotation = Quaternion.Lerp(startRotation, rot, currentTime / time);
            currentTime += Time.deltaTime;

            yield return null;
        } while (currentTime <= time);

        this.transform.localRotation = rot;
        this.transform.localPosition = endPos;
    }
}
