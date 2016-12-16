using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laptop : MonoBehaviour
{
    private Dictionary<LaptopComponentType, LaptopComponent> components = new Dictionary<LaptopComponentType, LaptopComponent>();
    // Use this for initialization
    void Start()
    {
        var childComponents = this.GetComponentsInChildren<LaptopComponent>();

        foreach (var component in childComponents)
        {
            this.components.Add(component.Type, component);
        }
    }

    public void ShowInfo(LaptopComponentType componentType)
    {
        LaptopComponent component;

        if (components.TryGetValue(componentType, out component))
        {
            component.Show();
        }
    }

    public void HideInfo(LaptopComponentType componentType)
    {
        LaptopComponent component;

        if (components.TryGetValue(componentType, out component))
        {
            component.Hide();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
