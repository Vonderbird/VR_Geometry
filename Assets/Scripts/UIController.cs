using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    public GameObject menu;
   
    public void ShowMenu(bool state)
    {
        menu.SetActive(state);
    }
}
