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
        
    }

    private void ProcessRelease(PointerEventData data)
    {
            
    }
    // Update is called once per frame
    void Update()
    {
            
    }
}
