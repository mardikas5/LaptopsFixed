using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    List<GameObject> objectsInRange = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        this.transform.parent.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        this.transform.parent.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            var MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            var direction = (MousePos -Camera.main.transform.position).normalized;

            Ray ray = new Ray(Camera.main.transform.position, direction);

            RaycastHit hit;
            Debug.DrawRay(Camera.main.transform.position, direction, Color.red, 10);
            if (Physics.Raycast(ray, out hit))
            {
                this.transform.position = hit.point;
            }
        }
    }

    private void OnControllerTriggerPress(Transform controller)
    {
        Debug.Log("Grab");
        foreach (GameObject other in objectsInRange)
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = this.transform;
        }
    }

    private void OnControllerTriggerUnPress(Transform controller)
    {
        Debug.Log("UnGrab");

        foreach (GameObject other in objectsInRange)
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().velocity = this.GetComponent<SetVelocity>().Velocity;
            other.transform.parent = null;
        }

        objectsInRange.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            Debug.Log("Add " + other.name);
            objectsInRange.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            Debug.Log("Remove " + other.name);
            objectsInRange.Remove(other.gameObject);
        }
    }
}
