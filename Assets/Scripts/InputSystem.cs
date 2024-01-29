using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputSystem : MonoBehaviour
{
    public InputAction moveAction;
    [FormerlySerializedAs("shoot")] public InputAction fireAction;
    [FormerlySerializedAs("shield")] public InputAction shieldAction;
    [FormerlySerializedAs("boost")] public InputAction boostAction;
    public InputAction PauseResumeAction;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        moveAction.Enable();
        fireAction.Enable();
        shieldAction.Enable();
        boostAction.Enable();
        PauseResumeAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        fireAction.Disable();
        shieldAction.Disable();
        boostAction.Disable();
        PauseResumeAction.Disable();
    }
}
