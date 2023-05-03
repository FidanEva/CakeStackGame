using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CremeMahineRotation : MonoBehaviour
{
    private Transform _transform;    
    [SerializeField] private float _rotateSpeed = 1;
    void Start()
    {
        _transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        _transform.Rotate( new Vector3(0, 180, 0) * _rotateSpeed * Time.deltaTime);
    }
}
