using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParralaxScript : MonoBehaviour
{
    Material mat;
    [Range(0f, 20f)] public float speed = 0.2f;
    float dist;
    void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    void Update()
    {
        dist += Time.deltaTime * speed;
        mat.SetTextureOffset("_MainTex", Vector2.right * dist);
    }
}
