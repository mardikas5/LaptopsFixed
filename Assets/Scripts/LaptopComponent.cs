using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LaptopComponent : MonoBehaviour
{
    public string BasicDescription;
    [Multiline]
    public string TechnicalDescription;

    public LaptopComponentType Type;

    public int Value;
    private Image comparisonImage;
    private Image componentTextPanel;
    private Text componentText;

    private Coroutine resizeRoutine;
    private Vector3 intitialPosition;
    private Vector3 shownPosition;
    private float time = 1f;

    public Quaternion localRot;

    public void Start()
    {
        comparisonImage = transform.Find("Scale/Canvas/PanelComponent").GetComponent<Image>();
        Assert.IsFalse(comparisonImage == null);
        componentTextPanel = transform.Find("Scale/Canvas/PanelDescription").GetComponent<Image>();
        Assert.IsFalse(componentTextPanel == null);
        componentText = transform.Find("Scale/Canvas/PanelDescription/Text").GetComponent<Text>();
        Assert.IsFalse(componentText == null);

        componentText.text = this.BasicDescription;

        intitialPosition = this.transform.localPosition;
        localRot = this.transform.localRotation;
        shownPosition = intitialPosition + new Vector3(0, 1f, 0);
    }

    public void SetBackgroundColor(Color color)
    {
        this.comparisonImage.color = color;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        Debug.Log(Vector3.Distance(Camera.main.transform.position, this.transform.position));
    }

    public void Show()
    {
        if (resizeRoutine != null)
        {
            StopCoroutine(resizeRoutine);
        }

        Quaternion initQ = transform.localRotation;
        Vector3 init = transform.localPosition;

        transform.localPosition = shownPosition;
        transform.LookAt(Camera.main.transform);
        transform.localEulerAngles += new Vector3(0, 0, 180);

        Quaternion rot = transform.localRotation;

        transform.localPosition = init;
        transform.localRotation = initQ;

        resizeRoutine = StartCoroutine(moveYAxis(shownPosition, time, rot));
        componentTextPanel.enabled = true;
    }

    public void Hide()
    {
        Debug.Log("Hide: " + Type);
        if (resizeRoutine != null)
        {
            StopCoroutine(resizeRoutine);
        }

        resizeRoutine = StartCoroutine(moveYAxis(intitialPosition, time, localRot));
        componentTextPanel.enabled = false;

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
