using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    List<GameObject> objectsInRange = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        this.GetComponent<ControllerInput>().OnTriggerPress += OnControllerTriggerPress;
        this.GetComponent<ControllerInput>().OnTriggerUnPress += OnControllerTriggerUnPress;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            Debug.DrawRay(ray.origin, ray.direction, Color.red, 10);
            if (Physics.Raycast(ray, out hit))
            {
                this.transform.position = hit.point;
            }
        }
    }

    private void OnControllerTriggerPress(Transform controller)
    {
        foreach (GameObject other in objectsInRange)
        {
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.parent = this.transform;
        }
    }

    private void OnControllerTriggerUnPress(Transform controller)
    {
        foreach (GameObject other in objectsInRange)
        {
            other.GetComponent<Rigidbody>().isKinematic = false;
            other.GetComponent<Rigidbody>().velocity = this.GetComponent<SetVelocity>().VelocityUnscaled;
            other.transform.parent = null;
        }

        objectsInRange.Clear();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            objectsInRange.Add(other.gameObject);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Grabable"))
        {
            objectsInRange.Remove(other.gameObject);
        }
    }
}
