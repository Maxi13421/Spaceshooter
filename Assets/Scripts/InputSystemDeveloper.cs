using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class InputSystemDeveloper : MonoBehaviour
{
    public InputAction coinAction;
    public InputAction levelAction;
    private bool _coinIsPressed = false;
    private bool _levelIsPressed = false;
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!_coinIsPressed)
        {
            GameObject.FindWithTag("Player").GetComponent<Player>().money +=
                50 * Convert.ToInt16(coinAction.ReadValue<float>());
            if (GameObject.FindWithTag("Player").GetComponent<Player>().money < 0)
            {
                GameObject.FindWithTag("Player").GetComponent<Player>().money = 0;
            }

            _coinIsPressed = true;
        }

        if (coinAction.ReadValue<float>() == 0)
        {
            _coinIsPressed = false;
        }
        if (!_levelIsPressed)
        {
            GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount +=
                Convert.ToInt16(levelAction.ReadValue<float>());
            if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount < 1)
            {
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount = 1;
            }

            _levelIsPressed = true;
        }

        if (levelAction.ReadValue<float>() == 0)
        {
            _levelIsPressed = false;
        }
    }

    private void OnEnable()
    {
        coinAction.Enable();
        levelAction.Enable();
    }

    private void OnDisable()
    {
        coinAction.Disable();
        levelAction.Disable();
    }
}