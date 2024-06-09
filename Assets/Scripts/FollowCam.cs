using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public float followspeed;
    public Transform target;
    public float dist;
    void Update()
    {
        Vector3 newpos = new Vector3(target.position.x, target.position.y + dist, -10f);
        transform.position = Vector3.Slerp(transform.position,newpos,followspeed*Time.deltaTime);
    }
}
