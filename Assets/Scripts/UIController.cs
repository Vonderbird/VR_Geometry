using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{

    public GameObject menu;
    public GameObject controller;

    public void ShowMenu(bool state)
    {
        menu.SetActive(state);
    }
    public void InstantiateObject(Transform objectPrefab)
    {
        Instantiate(objectPrefab, controller.transform.position, controller.transform.rotation, controller.transform);
        
    }

}
