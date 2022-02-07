using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepRotation : MonoBehaviour
{
    Quaternion initRotation;
    Transform trans;
    
    private void Start(){
        trans = GetComponent<Transform>();
        initRotation = trans.rotation;
    }
    
    private void Update(){
        trans.rotation = initRotation;
    }
}
