using System.Collections;
using System.Collections.Generic;
using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.EventSystems;
using Valve.VR;
public class InputModule : BaseInputModule
{

    public Camera camera;

    public SteamVR_Input_Sources targetSource;

    public SteamVR_Action_Boolean clickAction;

    private GameObject currentObject = null;

    private PointerEventData data = null;
    // Start is called before the first frame update
    protected override void Awake()    
    {
        base.Awake();
        data = new PointerEventData(eventSystem);
    }

    public override void Process()
    {
        // reset data, set camera
        data.Reset();    
        data.position = new Vector2(camera.pixelWidth /2, camera.pixelHeight/2);
        // raycast
        eventSystem.RaycastAll(data, m_RaycastResultCache);
        data.pointerCurrentRaycast = FindFirstRaycast(m_RaycastResultCache);
        currentObject = data.pointerCurrentRaycast.gameObject;
        // clear raycast
        m_RaycastResultCache.Clear();
        //hover
        HandlePointerExitAndEnter(data, currentObject);
        //press
        if(clickAction.GetStateDown(targetSource))
            ProcessPress(data);
        //release
        if(clickAction.GetStateUp(targetSource))
            ProcessRelease(data);
    }

    public PointerEventData GetData()
    {
        return data;
    }

    private void ProcessPress(PointerEventData data)
    {
        //set raycast
        data.pointerPressRaycast = data.pointerCurrentRaycast;
        //check for object hit, get the down handler and then call it
        GameObject newPointerPress = ExecuteEvents.ExecuteHierarchy(currentObject, data, ExecuteEvents.pointerDownHandler);
        //if no down handler, get click handler
        if (newPointerPress == null)
        {
            newPointerPress = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);
        }

        //Set data
        data.pressPosition = data.position;
        data.pointerPress = newPointerPress;
        data.rawPointerPress = currentObject;

    }

    private void ProcessRelease(PointerEventData data)
    {
        //execute pointer up
        ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerUpHandler);
        //check for click handler
        GameObject pointerUpHandler = ExecuteEvents.GetEventHandler<IPointerClickHandler>(currentObject);

        //check to see if the object that we pressed and the object that we are moving up the button on are the same
        if (data.pointerPress == pointerUpHandler)
        {
            ExecuteEvents.Execute(data.pointerPress, data, ExecuteEvents.pointerClickHandler);
        }
        //clear selected gameobject
        eventSystem.SetSelectedGameObject(null);

        //Reset Data
        data.pressPosition = Vector2.zero;
        data.pointerPress = null;
        data.rawPointerPress = null;
        
    }
    
    
    // Update is called once per frame
    void Update()
    {
            
    }
}
