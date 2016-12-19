using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class LaptopComponent : MonoBehaviour
{
    public static Dictionary<LaptopComponentType, LaptopComponent> bestComponents;


    public string BasicDescription;
    [Multiline]
    public string TechnicalDescription;

    public LaptopComponentType Type;

    public int Value;
    [SerializeField]
    private Image comparisonImage;
      [SerializeField]
    private Image componentTextPanel;
      [SerializeField]
    private Text componentText;
    private Canvas canvasComponent;

    private Coroutine resizeRoutine;
    private Vector3 intitialPosition;
    private Vector3 shownPosition;
    private float time = 1f;

    public Quaternion localRot;

    public bool Shown { get; private set; }

    private Billboard billboard;
    private static Vector3 ShownOffset = new Vector3( 0, 1f, 0 );


    public void Start()
    {
        addToComparison( this );

        billboard = GetComponent<Billboard>();
        Assert.IsNotNull( billboard, "Billboard missing from component" );
        canvasComponent = GetComponentInChildren<Canvas>();

        Shown = false;
        canvasComponent.enabled = false;

        comparisonImage = transform.Find( "Scale/Canvas/PanelComponent" ).GetComponent<Image>();
        Assert.IsFalse( comparisonImage == null );
        componentTextPanel = transform.Find( "Scale/Canvas/PanelDescription" ).GetComponent<Image>();
        Assert.IsFalse( componentTextPanel == null );
        componentText = transform.Find( "Scale/Canvas/PanelDescription/Text" ).GetComponent<Text>();
        Assert.IsFalse( componentText == null );

        componentText.text = this.BasicDescription;

        intitialPosition = this.transform.localPosition;
        localRot = this.transform.localRotation;
        shownPosition = intitialPosition + ShownOffset;
    }

    public void SetBackgroundColor( Color color )
    {
        this.comparisonImage.color = color;
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        float distance = Vector3.Distance( Camera.main.transform.position, this.transform.position );
        if( distance < 2f )
        {
            billboard.isEnabled = false;
        }
        else
        {
            billboard.isEnabled = true;
        }

        if( distance > 1f )
        {
            this.componentText.text = this.BasicDescription;
        }
        else
        {
            this.componentText.text = this.TechnicalDescription;
        }
        //Debug.Log( Vector3.Distance( Camera.main.transform.position, this.transform.position ) );
    }

    public void Show()
    {
        if( resizeRoutine != null )
        {
            StopCoroutine( resizeRoutine );
        }

        Quaternion initQ = transform.localRotation;
        Vector3 init = transform.localPosition;

        Vector3 moveTo = shownPosition;
        if( bestComponents[Type] == this )
        {
           moveTo += new Vector3( 0, 1f, 0 );
        }

        transform.localPosition = moveTo;
        transform.LookAt( Camera.main.transform );
        transform.localEulerAngles += new Vector3( 0, 0, 180 );

        Quaternion rot = transform.localRotation;

        transform.localPosition = init;
        transform.localRotation = initQ;

        resizeRoutine = StartCoroutine( moveYAxis( moveTo, time, rot ) );
        componentTextPanel.enabled = true;

        canvasComponent.enabled = true;
        billboard.isEnabled = true;
        Shown = true;
    }

    public void Hide()
    {
        Debug.Log( "Hide: " + Type );
        if( resizeRoutine != null )
        {
            StopCoroutine( resizeRoutine );
        }

        resizeRoutine = StartCoroutine( moveYAxis( intitialPosition, time, localRot ) );
        componentTextPanel.enabled = false;

        billboard.isEnabled = false;
        Shown = false;
    }

    private IEnumerator moveYAxis( Vector3 targetPos, float time, Quaternion rot )
    {
        Vector3 startPos = this.transform.localPosition;
        Quaternion startRotation = this.transform.localRotation;
        Vector3 endPos = targetPos;
        float currentTime = 0.0f;

        do
        {
            this.transform.localPosition = Vector3.Lerp( startPos, endPos, currentTime / time );
            this.transform.localRotation = Quaternion.Lerp( startRotation, rot, currentTime / time );
            currentTime += Time.deltaTime;

            yield return null;
        } while( currentTime <= time );

        canvasComponent.enabled = Shown;
        this.transform.localRotation = rot;
        this.transform.localPosition = endPos;
    }

    public static void addToComparison( LaptopComponent t )
    {
        if( bestComponents == null )
        {
            bestComponents = new Dictionary<LaptopComponentType, LaptopComponent>();
            bestComponents.Add( t.Type, t );
        }
        else
        {
            if( bestComponents.ContainsKey( t.Type ) )
            {
                if( bestComponents[t.Type].Value < t.Value )
                {
                    bestComponents[t.Type] = t;
                }
            }
            else
            {
                bestComponents.Add( t.Type, t );
            }
        }
    }

    public void MarkAsBestOfType()
    {

    }

}
