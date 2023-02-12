using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoFollowCamera : MonoBehaviour
{
    [SerializeField] private float offsetX = 0;
    [SerializeField] private float offsetY = 0;
    [SerializeField] private Transform followTarget;
    private Vector3 localCameraPosition;


    private void Start()
    {
        localCameraPosition = transform.position;
    }
    void Update()
    {
        transform.position = followTarget.transform.position + (localCameraPosition + new Vector3(offsetX,offsetY));
    }
}
