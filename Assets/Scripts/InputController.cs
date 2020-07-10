using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class InputController : MonoBehaviour
{
    [Header("References")]
    public UIController uIController;
    void Update()
    {
        if (SteamVR_Actions._default.MenuUI.GetLastActive(SteamVR_Input_Sources.Any))
        {
            uIController.ShowMenu(SteamVR_Actions._default.MenuUI.state);
        }
    }
}
