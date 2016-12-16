using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaptopComponent : MonoBehaviour
{
    [Multiline]
    public string Text;
    public LaptopComponentType Type;
    private Coroutine resizeRoutine;
    private float intitialPosition;
    private float shownPosition;
    private float time = 1f;

    public void Start()
    {
        intitialPosition = this.transform.position.y;
        shownPosition = intitialPosition + 10f;
    }

    public void Show()
    {
        Debug.Log("Show: " + Type);
        if (resizeRoutine != null)
        {
            StopCoroutine(resizeRoutine);
        }

        resizeRoutine = StartCoroutine(moveYAxis(shownPosition, time));
    }


    public void Hide()
    {
        Debug.Log("Hide: " + Type);
        if (resizeRoutine != null)
        {
            StopCoroutine(resizeRoutine);
        }

        resizeRoutine = StartCoroutine(moveYAxis(intitialPosition, time));
    }

    private IEnumerator moveYAxis(float targetPos, float time)
    {
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
    }
}
