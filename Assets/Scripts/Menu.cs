using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public Vector2Int curButton = new Vector2Int(0, 0);
    public Vector2Int activeButton = new Vector2Int(0, 0);
    private Vector2Int lastChooseAction = Vector2Int.zero;
    private bool acceptIsPressed = true;
    private Color ColorInactive = new Color(0.1f, 0.1f, 0.1f);
    private Color ColorActive = new Color(0.5f, 0.5f, 0.5f);
    private const float Ramping = 1.2f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        
        
        if (GameObject.FindWithTag("InputSystemMenu").GetComponent<InputSystemMenu>().acceptAction.ReadValue<float>() <
            0.5) acceptIsPressed = false;
        Vector2 choose = GameObject.FindWithTag("InputSystemMenu").GetComponent<InputSystemMenu>().chooseAction
            .ReadValue<Vector2>();
        Vector2Int newChooseAction = new Vector2Int(0, 0);
        if (choose.magnitude < 0.4) newChooseAction = new Vector2Int(0, 0);
        else if (choose.x > 0 && Math.Abs(choose.y) <= choose.x) newChooseAction = new Vector2Int(1, 0);
        else if (choose.x < 0 && Math.Abs(choose.y) <= Math.Abs(choose.x)) newChooseAction = new Vector2Int(-1, 0);
        else if (choose.y > 0 && Math.Abs(choose.y) >= Math.Abs(choose.x)) newChooseAction = new Vector2Int(0, -1);
        else if (choose.y < 0 && Math.Abs(choose.y) >= Math.Abs(choose.x)) newChooseAction = new Vector2Int(0, 1);

        if (lastChooseAction != newChooseAction)
        {
            lastChooseAction = newChooseAction;
            curButton += newChooseAction;
            if (curButton.x < 0) curButton -= newChooseAction;
            if (curButton.y < 0) curButton -= newChooseAction;
            if (curButton.x >= 1) curButton -= newChooseAction;
            if (curButton.y >= 2) curButton -= newChooseAction;
            
        }

        if (activeButton != curButton)
        {
            transform.GetChild(activeButton.y).GetComponent<SpriteRenderer>().color =
                ColorInactive;
            transform.GetChild(curButton.y).GetComponent<SpriteRenderer>().color =
                ColorActive;
            activeButton = curButton;
        }

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();

        if (GameObject.FindWithTag("InputSystemMenu").GetComponent<InputSystemMenu>().acceptAction.ReadValue<float>() >=
            0.5 && !acceptIsPressed)
        {
            acceptIsPressed = true;
            if (activeButton == new Vector2Int(0, 0))
            {
                GameSystem gameSystem = GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>();
                gameSystem.target = GameSystem.Target.Shop;
                gameSystem.ZoomStatus = GameSystem.Zoom.Zoomout;
                AudioManager.instance.StopMusic(STOP_MODE.ALLOWFADEOUT);
                Camera.main.transform.GetChild(1).gameObject.SetActive(false);
                //Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            }
            
            if (activeButton == new Vector2Int(0, 1))
            {
                Application.Quit();
            }
            
        }

    }

    private void OnDisable()
    {
        Debug.Log("Disabled");
    }
}