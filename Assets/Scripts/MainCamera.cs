using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
    private void Awake()
    {
        GameSystem.MainCameraGameObject = gameObject;
        GameSystem.MainCamera = gameObject.GetComponent<Camera>();
    }
}
