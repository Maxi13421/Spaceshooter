using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shop : MonoBehaviour
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
            if (curButton.x >= 5) curButton -= newChooseAction;
            if (curButton.y >= 2) curButton -= newChooseAction;
            
        }

        if (activeButton != curButton)
        {
            transform.GetChild(activeButton.y * 5 + activeButton.x).GetComponent<SpriteRenderer>().color =
                ColorInactive;
            transform.GetChild(curButton.y * 5 + curButton.x).GetComponent<SpriteRenderer>().color =
                ColorActive;
            activeButton = curButton;
        }

        Player player = GameObject.FindWithTag("Player").GetComponent<Player>();
        transform.GetChild(10).GetComponent<TextMeshPro>().text = player.money.ToString();

        if (GameObject.FindWithTag("InputSystemMenu").GetComponent<InputSystemMenu>().acceptAction.ReadValue<float>() >=
            0.5 && !acceptIsPressed)
        {
            acceptIsPressed = true;
            if (activeButton == new Vector2Int(0, 0))
            {
                if (player.money >= (int)(10 * Math.Pow(Ramping, player.hpLevel)))
                {
                    player.money -= (int)(10 * Math.Pow(Ramping, player.hpLevel));
                    player.hpLevel++;
                    transform.GetChild(0).GetComponentInChildren<TextMeshPro>().text =
                        ((int)(10 * Math.Pow(Ramping, player.hpLevel))).ToString();
                }
            }
            if (activeButton == new Vector2Int(1, 0))
            {
                if (player.money >= (int)(10 * Math.Pow(Ramping, player.boostLevel)))
                {
                    player.money -= (int)(10 * Math.Pow(Ramping, player.boostLevel));
                    player.boostLevel++;
                    transform.GetChild(1).GetComponentInChildren<TextMeshPro>().text =
                        ((int)(10 * Math.Pow(Ramping, player.boostLevel))).ToString();
                }
            }
            if (activeButton == new Vector2Int(2, 0))
            {
                DeactivateOldWeapon(player._mainWeapon);
                player._mainWeapon = player.standardWeapon;
                transform.GetChild(2).GetComponentInChildren<TextMeshPro>().text = "Aktiviert";
                
            }
            if (activeButton == new Vector2Int(3, 0))
            {
                if (!player.gotLaser)
                {
                    if (player.money >= 150)
                    {
                        player.money -= 150;
                        player.gotLaser = true;
                        transform.GetChild(3).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
                    }
                }
                else
                {
                    DeactivateOldWeapon(player._mainWeapon);
                    player._mainWeapon = player.laser;
                    transform.GetChild(3).GetComponentInChildren<TextMeshPro>().text = "Aktiviert";
                }
            }
            if (activeButton == new Vector2Int(4, 0))
            {
                
            }
            if (activeButton == new Vector2Int(0, 1))
            {
                if (player.money >= (int)(10 * Math.Pow(Ramping, player.shieldLevel)))
                {
                    player.money -= (int)(10 * Math.Pow(Ramping, player.shieldLevel));
                    player.shieldLevel++;
                    transform.GetChild(5).GetComponentInChildren<TextMeshPro>().text =
                        ((int)(10 * Math.Pow(Ramping, player.shieldLevel))).ToString();
                }
            }
            if (activeButton == new Vector2Int(1, 1))
            {
                if (player.money >= (int)(10 * Math.Pow(Ramping, player.munitionRegenerationLevel)))
                {
                    player.money -= (int)(10 * Math.Pow(Ramping, player.munitionRegenerationLevel));
                    player.munitionRegenerationLevel++;
                    transform.GetChild(6).GetComponentInChildren<TextMeshPro>().text =
                        ((int)(10 * Math.Pow(Ramping, player.munitionRegenerationLevel))).ToString();
                }
            }
            if (activeButton == new Vector2Int(2, 1))
            {
                if (!player.gotBig)
                {
                    if (player.money >= 150)
                    {
                        player.money -= 150;
                        player.gotBig = true;
                        transform.GetChild(7).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
                    }
                }
                else
                {
                    DeactivateOldWeapon(player._mainWeapon);
                    player._mainWeapon = player.bigWeapon;
                    transform.GetChild(7).GetComponentInChildren<TextMeshPro>().text = "Aktiviert";
                }
            }
            if (activeButton == new Vector2Int(3, 1))
            {
                if (!player.gotShrapnel)
                {
                    if (player.money >= 150)
                    {
                        player.money -= 150;
                        player.gotShrapnel = true;
                        transform.GetChild(8).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
                    }
                }
                else
                {
                    DeactivateOldWeapon(player._mainWeapon);
                    player._mainWeapon = player.shrapnelWeapon;
                    transform.GetChild(8).GetComponentInChildren<TextMeshPro>().text = "Aktiviert";
                }
            }
            if (activeButton == new Vector2Int(4, 1))
            {
                
            }
        }

    }

    public void DeactivateOldWeapon(Weapon weapon)
    {
        if(weapon is StandardWeapon) transform.GetChild(2).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
        if(weapon is Laser) transform.GetChild(3).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
        if(weapon is BigWeapon) transform.GetChild(7).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
        if(weapon is ShrapnelWeapon) transform.GetChild(8).GetComponentInChildren<TextMeshPro>().text = "Aktivieren";
    }
}
