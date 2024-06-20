using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parralaxControl : MonoBehaviour
{
    Transform Cam;
    Vector3 cameraStartpos;
    float dist;

    GameObject[] backrounds;
    Material[] mat;
    float[] backspeed;
    float farthest;
    [Range(0f, 1f)] public float paralaxspeed;

    void Start()
    {
        Cam = Camera.main.transform;
        cameraStartpos = Cam.position;

        int backcount = transform.childCount;
        mat = new Material[backcount];
        backspeed = new float[backcount];
        backrounds = new GameObject[backcount];

        for (int i = 0; i < backcount; i++)
        {
            backrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backrounds[i].GetComponent<Renderer>().material;
        }
        backspeedcalc(backcount);

    }

    void backspeedcalc(int backcount)
    {
        for (int i = 0;i < backcount;i++)
        {
            if ((backrounds[i].transform.position.z - Cam.position.z) > farthest)
            {
                farthest = backrounds[i].transform.position.z - Cam.position.z;
            }
        }

        for (int i = 0;i < backcount ; i++)
        {
            backspeed[i] = 1 - (backrounds[i].transform.position.z - Cam.position.z) / farthest;
        }
    }

    private void LateUpdate()
    {
        dist = Cam.position.x - cameraStartpos.x;
        transform.position = Cam.position;

        for (int i = 0;i < backrounds.Length; i++)
        {
            float speed = backspeed[i] * paralaxspeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(dist,0) * speed);
        }
    }
}
