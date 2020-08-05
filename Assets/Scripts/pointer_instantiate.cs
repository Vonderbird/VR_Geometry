using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class pointer_instantiate : MonoBehaviour
{public float dis=5f;
public Rigidbody instance;
public Rigidbody Shape;
public float m_DefaoultLength=10.0f;
public GameObject m_Dot;
private LineRenderer m_LineRenderer=null;
public SteamVR_Action_Boolean IstantiateAction;
public SteamVR_Input_Sources handType;
    
    void Start() 
    {
        Rigidbody instance=Shape;
    }

     public void Awake()
    {
        m_LineRenderer=GetComponent<LineRenderer>();
    }

    void Update() 
    {
       UpdateLine(); 
        if (IstantiateAction.GetStateDown(handType))// && hit.transform.tag == "Cube")
        {
          instance=Instantiate(Shape,transform.position+(transform.forward*dis),transform.rotation)as Rigidbody;
          instance.transform.parent=transform;
        }
        if (IstantiateAction.GetStateUp(handType))
        {
        instance.GetComponent<Rigidbody>().useGravity=true;
        instance.transform.parent=null;
        }
    }
    public void UpdateLine()
    {
        float targetLength=m_DefaoultLength;
        RaycastHit hit =CreateRatcast(targetLength);
        Vector3 endPosition=transform.position + (transform.forward*targetLength);
        if(hit.collider!=null)
        {
         endPosition=hit.point;
        }
        m_Dot.transform.position=endPosition;
        m_LineRenderer.SetPosition(0,transform.position);
        m_LineRenderer.SetPosition(1,endPosition);
    }

    public RaycastHit CreateRatcast(float length)
    {
         RaycastHit hit;
        Ray ray =new Ray (transform.position,transform.forward);
        Physics.Raycast(ray,out hit,m_DefaoultLength);
        return hit;
    }
}
