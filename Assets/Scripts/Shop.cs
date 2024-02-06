using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
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
    private const float Ramping = 1.1f;
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
                    
                    player.hp = (float)(player.basicHp * (1 + player.hpLevel * 0.1) * (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().difficulty ==
                        GameSystem.Difficulty.Easy?GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().easyMultiplier:1));
                    
                    
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
                    player.boostCooldownRechargePerSecond =
                        (float)(player.basicBoostCooldownRechargePerSecond * (1 + player.boostLevel * 0.1));
                }
            }
            if (activeButton == new Vector2Int(2, 0))
            {
                DeactivateOldWeapon(player._mainWeapon);
                player._mainWeapon = player.standardWeapon;
                transform.GetChild(2).GetComponentInChildren<TextMeshPro>().text = "Equipped";
                
            }
            if (activeButton == new Vector2Int(3, 0))
            {
                if (!player.gotLaser)
                {
                    if (player.money >= 150)
                    {
                        player.money -= 150;
                        player.gotLaser = true;
                        transform.GetChild(3).GetComponentInChildren<TextMeshPro>().text = "Equip";
                    }
                }
                else
                {
                    DeactivateOldWeapon(player._mainWeapon);
                    player._mainWeapon = player.laser;
                    transform.GetChild(3).GetComponentInChildren<TextMeshPro>().text = "Equipped";
                }
            }
            if (activeButton == new Vector2Int(4, 0))
            {
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus = GameSystem.Zoom.Zoomout;
                GameObject.FindWithTag("Player").GetComponent<Player>().StartLevel();
                GameObject.FindWithTag("Shop").SetActive(false);
                GameObject.FindWithTag("DarkenerExcludeMenu").GetComponent<SpriteRenderer>().color =
                    new Color(0, 0, 0, 0);
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().moneyLevelStart =
                    GameObject.FindWithTag("Player").GetComponent<Player>().money;
                if (GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount % 6 == 1)
                {
                    AudioManager.instance.StopMusic(STOP_MODE.ALLOWFADEOUT);
                }

            }
            if (activeButton == new Vector2Int(0, 1))
            {
                if (player.money >= (int)(10 * Math.Pow(Ramping, player.shieldLevel)))
                {
                    player.money -= (int)(10 * Math.Pow(Ramping, player.shieldLevel));
                    player.shieldLevel++;
                    transform.GetChild(5).GetComponentInChildren<TextMeshPro>().text =
                        ((int)(10 * Math.Pow(Ramping, player.shieldLevel))).ToString();
                    player.shieldCooldownRechargePerSecond =
                        (float)(player.basicShieldCooldownRechargePerSecond * (1 + player.shieldLevel * 0.1));
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
                    player.weaponCooldownRechargePerSecond =
                        (float)(player.basicWeaponCooldownRechargePerSecond * (1 + player.munitionRegenerationLevel * 0.1));
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
                        transform.GetChild(7).GetComponentInChildren<TextMeshPro>().text = "Equip";
                    }
                }
                else
                {
                    DeactivateOldWeapon(player._mainWeapon);
                    player._mainWeapon = player.bigWeapon;
                    transform.GetChild(7).GetComponentInChildren<TextMeshPro>().text = "Equipped";
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
                        transform.GetChild(8).GetComponentInChildren<TextMeshPro>().text = "Equip";
                    }
                }
                else
                {
                    DeactivateOldWeapon(player._mainWeapon);
                    player._mainWeapon = player.shrapnelWeapon;
                    transform.GetChild(8).GetComponentInChildren<TextMeshPro>().text = "Equipped";
                }
            }
            if (activeButton == new Vector2Int(4, 1))
            {
                //Camera.main.transform.GetChild(0).gameObject.SetActive(false);
                //Camera.main.transform.GetChild(1).gameObject.SetActive(true);
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().LevelCount = 1;
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().moneyLevelStart = 0;
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().colorScheme = GameSystem.ColorScheme.Asteroid;
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().target = GameSystem.Target.Menu;
                GameObject.FindWithTag("GameSystem").GetComponent<GameSystem>().ZoomStatus = GameSystem.Zoom.Dying;
                GameObject.FindWithTag("Player").GetComponent<Player>().currenthp = 100;
                GameObject.FindWithTag("Player").GetComponent<Player>().hp = 100;
                GameObject.FindWithTag("Player").GetComponent<Player>().hpLevel = 0;
                GameObject.FindWithTag("Player").GetComponent<Player>().shieldLevel = 0;
                GameObject.FindWithTag("Player").GetComponent<Player>().boostLevel = 0;
                GameObject.FindWithTag("Player").GetComponent<Player>().munitionRegenerationLevel = 0;
                GameObject.FindWithTag("Player").GetComponent<Player>().gotBig = false;
                GameObject.FindWithTag("Player").GetComponent<Player>().gotLaser = false;
                GameObject.FindWithTag("Player").GetComponent<Player>().gotShrapnel = false;
                GameObject.FindWithTag("Player").GetComponent<Player>().boostCooldownRechargePerSecond= 0.1f;
                GameObject.FindWithTag("Player").GetComponent<Player>().shieldCooldownRechargePerSecond = 0.1f;
                GameObject.FindWithTag("Player").GetComponent<Player>().weaponCooldownRechargePerSecond = 0.3f;
                AudioManager.instance.StopMusic(STOP_MODE.ALLOWFADEOUT);
                //AudioManager.instance.InitializeMusic(FMODEvents.instance.titleThemeWithoutStart);
                

            }
        }

        if (activeButton == new Vector2Int(0, 0))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "Increases the maximum HP from " +
                                                                      ((int)(player.basicHp *
                                                                             (1 + player.hpLevel * 0.1) *
                                                                             (GameObject.FindWithTag("GameSystem")
                                                                                  .GetComponent<GameSystem>()
                                                                                  .difficulty ==
                                                                              GameSystem.Difficulty.Easy
                                                                                 ? GameObject.FindWithTag("GameSystem")
                                                                                     .GetComponent<GameSystem>()
                                                                                     .easyMultiplier
                                                                                 : 1))).ToString() +
                                                                      " to " + ((int)(player.basicHp *
                                                                          (1 + (player.hpLevel + 1) * 0.1) *
                                                                          (GameObject.FindWithTag("GameSystem")
                                                                               .GetComponent<GameSystem>().difficulty ==
                                                                           GameSystem.Difficulty.Easy
                                                                              ? GameObject.FindWithTag("GameSystem")
                                                                                  .GetComponent<GameSystem>()
                                                                                  .easyMultiplier
                                                                              : 1)))
                                                                      .ToString() + ".";

        }
        if (activeButton == new Vector2Int(1, 0))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "Increases the boost regeneration per second from " +
                                                                      ((float)(player.basicBoostCooldownRechargePerSecond *
                                                                             (1 + player.boostLevel * 0.1))).ToString("F2") +
                                                                      " to " + ((float)(player.basicBoostCooldownRechargePerSecond *
                                                                          (1 + (player.boostLevel + 1) * 0.1)))
                                                                      .ToString("F2") + ".";
        }
        
        if (activeButton == new Vector2Int(2, 0))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "A weapon that fires projectiles at a high speed.\nThe projectiles are homing to some extent.";
        }
        if (activeButton == new Vector2Int(3, 0))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "A laser that consistently deals damage to all enemies in its line of fire.";
        }
        if (activeButton == new Vector2Int(4, 0))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "Start the next level.";
        }
        
        if (activeButton == new Vector2Int(0, 1))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "Increases the shield regeneration per second from " +
                                                                      ((float)(player.basicShieldCooldownRechargePerSecond *
                                                                             (1 + player.shieldLevel * 0.1))).ToString("F2") +
                                                                      " to " + ((float)(player.basicShieldCooldownRechargePerSecond *
                                                                          (1 + (player.shieldLevel + 1) * 0.1)))
                                                                      .ToString("F2") + ".";
        }
        if (activeButton == new Vector2Int(1, 1))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "Increases the ammunition regenerated per second from " +
                                                                      ((float)(player.basicWeaponCooldownRechargePerSecond *
                                                                             (1 + player.munitionRegenerationLevel * 0.1))).ToString("F2") +
                                                                      " to " + ((float)(player.basicWeaponCooldownRechargePerSecond *
                                                                          (1 + (player.munitionRegenerationLevel + 1) * 0.1)))
                                                                      .ToString("F2") + ".";
        }
        
        if (activeButton == new Vector2Int(2, 1))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "A weapon that shoots projectiles with high damage.";
        }
        if (activeButton == new Vector2Int(3, 1))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "A weapon that fires projectiles which split into 8 smaller fragments.";
        }
        if (activeButton == new Vector2Int(4, 1))
        {
            transform.GetChild(11).GetComponent<TextMeshPro>().text = "Return to the menu.\nThe game progress will be lost.";
        }
        
        

    }

    public void DeactivateOldWeapon(Weapon weapon)
    {
        if(weapon is StandardWeapon) transform.GetChild(2).GetComponentInChildren<TextMeshPro>().text = "Equip";
        if(weapon is Laser) transform.GetChild(3).GetComponentInChildren<TextMeshPro>().text = "Equip";
        if(weapon is BigWeapon) transform.GetChild(7).GetComponentInChildren<TextMeshPro>().text = "Equip";
        if(weapon is ShrapnelWeapon) transform.GetChild(8).GetComponentInChildren<TextMeshPro>().text = "Equip";
    }
}
