using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetMovement : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 0.5f;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        float offset = Time.deltaTime * scrollSpeed;
        rend.material.mainTextureOffset += new Vector2(0, offset);
    }
}
