using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControls : MonoBehaviour
{
    [SerializeField]
    Transform target;
    [SerializeField]
    float followSpeed;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 offset = new Vector3(0, 0, -10);
        transform.position = Vector3.Lerp(transform.position, target.position + offset, Time.deltaTime * followSpeed);
    }
}
