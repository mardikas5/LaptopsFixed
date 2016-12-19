using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    public Dictionary<LaptopComponentType, LaptopComponent> Components = new Dictionary<LaptopComponentType, LaptopComponent>();
    // Use this for initialization
    void Start()
    {
        ButtonManager.Instance.Laptops.Add(this);

        var childComponents = this.GetComponentsInChildren<LaptopComponent>();

        foreach (var component in childComponents)
        {
            this.Components.Add(component.Type, component);
        }
    }

    public void ShowInfo(LaptopComponentType componentType)
    {
        LaptopComponent component;

        if (Components.TryGetValue(componentType, out component))
        {
            component.Show();
        }
    }

    public void HideInfo(LaptopComponentType componentType)
    {
        LaptopComponent component;

        if (Components.TryGetValue(componentType, out component))
        {
            component.Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -.7f)
        {
            transform.position = RespawnPoint.Instance.transform.position;
        }
    }
}
