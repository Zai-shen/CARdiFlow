using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstantRotation : MonoBehaviour
{
    public Vector3 rotationDirection = new Vector3(0f,0f,1f);
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Rotate(rotationDirection * Time.fixedDeltaTime);
    }
}
