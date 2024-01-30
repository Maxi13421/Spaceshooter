using System;
using FMOD.Studio;
using TMPro;
using UnityEngine;

public class GameSystem : MonoBehaviour
{
    

    public static GameObject Player;
    public static GameObject MainCameraGameObject;
    public static Camera MainCamera;
    protected const float ZoomInDuration = 1f;
    protected const float ZoomOutDuration = 1f;
    protected float CameraSizeZoomedIn = 3;
    protected float CameraSizeZoomedOut = 10;
    public Zoom ZoomStatus = Zoom.Menu;
    public Target target = Target.Shop;
    protected GameObject Level = null;
    public int LevelCount = 1;
    protected const int BossLevel = -1;
    private float dampness = 0;
    public ColorScheme colorScheme = ColorScheme.Asteroid;
    public float dyingFadeOutDuration = 1;
    public float deadDuration = 1;
    private float _curDeadTime = 0;
    public int moneyLevelStart;
    
    protected virtual void Start()
    {
        //GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
        Player = GameObject.FindWithTag("Player");
        MainCameraGameObject = GameObject.FindWithTag("MainCamera");
        MainCamera = MainCameraGameObject.GetComponent<Camera>();
        GameObject.FindWithTag("Shop").SetActive(false);
        MainCamera.orthographicSize = CameraSizeZoomedIn;
        MainCameraGameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
        Debug.Log(Player.transform.position);
        Debug.Log(MainCameraGameObject.transform.position.ToString());
        GameObject.FindWithTag("Shield").SetActive(false);
        GameObject.FindWithTag("Overlay").transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Awake()
    {
        
    }


    protected virtual void Update()
    {
        switch (ZoomStatus)
        {
            case Zoom.Zoomin:
                ZoomingIn();
                break;
            case Zoom.Zoomout:
                ZoomingOut();
                break;
            case Zoom.Shop:
                break;
            case Zoom.Level:
                break;
        }

        GameObject.FindWithTag("LevelCounter").GetComponent<TextMeshPro>().text = LevelCount.ToString();
    }

    protected void FixedUpdate()
    {
        Debug.Log(ZoomStatus.ToString());
        switch (ZoomStatus)
        {
            case Zoom.Level:
                CheckPlayerDeath();
                if (Level == null)
                {
                    if (LevelCount == BossLevel)
                    {
                        //TODO
                    }
                    else
                    {
                        Level = new GameObject();
                        Level.tag = "Level";
                        
                        switch (LevelCount%6)
                        {
                                
                            case 1:
                                Level.AddComponent<LevelObstacleSpam>();
                                Level.GetComponent<LevelObstacleSpam>().speed = 10+(LevelCount/6);
                                break;
                            case 2:
                                Level.AddComponent<LevelBigSpam>();
                                Level.GetComponent<LevelBigSpam>().speed = 10+(LevelCount/6);
                                break;
                            case 3:
                                Level.AddComponent<LevelMachineGunSpam>();
                                Level.GetComponent<LevelMachineGunSpam>().speed = 10+(LevelCount/6);
                                break;
                            case 4:
                                Level.AddComponent<LevelShrapnelSpam>();
                                Level.GetComponent<LevelShrapnelSpam>().speed = 10+(LevelCount/6);
                                break;
                            case 5:
                                Level.AddComponent<LevelHomingSpam>();
                                Level.GetComponent<LevelHomingSpam>().speed = 10+(LevelCount/6);
                                break;
                            case 0:
                                Level.AddComponent<LevelMixedSpam>();
                                Level.GetComponent<LevelMixedSpam>().speed = 10+(LevelCount/6);
                                break;
                        }
                        Level.transform.position =
                            new Vector3(Camera.main.orthographicSize * Camera.main.aspect + 15, 0, 0);
                    }
                }
                if (Level.GetComponent<Level>().levelEnd + Level.transform.position.x <0)
                {
                    ZoomStatus = Zoom.Zoomin;
                    GameObject.FindWithTag("Overlay").transform.GetChild(0).gameObject.SetActive(false);
                    ResetLevel();
                    if (LevelCount == BossLevel)
                    {
                        target = Target.VictoryScreen;
                    }
                    else
                    {
                        target = Target.Shop;
                    }
                    LevelCount++;
                    //GameObject.FindWithTag("InputSystem").SetActive(false);
                }
                
                break;
            case Zoom.Dying:
                if (GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color.a>=1)
                {
                    ZoomStatus = Zoom.Dead;
                    ResetLevel();
                    GameObject.FindWithTag("Overlay").transform.GetChild(0).gameObject.SetActive(false);
                    _curDeadTime = 0;
                    Camera.main.transform.GetChild(0).gameObject.SetActive(true);
                    break;
                }
                GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color.a+Time.fixedDeltaTime*dyingFadeOutDuration);
                break;
            case Zoom.Dead:
                if (_curDeadTime >= deadDuration)
                {
                    MainCameraGameObject.transform.position = new Vector3(Player.transform.position.x,Player.transform.position.y,-10);
                    MainCamera.orthographicSize = CameraSizeZoomedIn;
                    GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
                    GameObject.FindWithTag("DarkenerExcludeMenu").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
                    ZoomStatus = Zoom.Shop;
                    if (!AudioManager.instance.playingMusic)
                    {
                        if (((LevelCount - 1) / 6) % 2 == 0)
                        {
                            AudioManager.instance.InitializeMusic(FMODEvents.instance.mainTheme);
                        }
                        else
                        {
                            AudioManager.instance.InitializeMusic(FMODEvents.instance.bossTheme);
                        }
                    }
                    AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground",1);
                    ResetLevel();
                    
                }
                _curDeadTime += Time.fixedDeltaTime;
                break;
                

        }
    }

    public void ResetLevel()
    {
        Destroy(GameObject.FindWithTag("Level"));
        Level = null;
    }


    protected void ZoomingIn()
    {
        if (MainCamera.orthographicSize > CameraSizeZoomedIn)
        {
            MainCameraGameObject.transform.Translate(Player.transform.position * Time.deltaTime/ ZoomInDuration);
            //MainCamera.orthographicSize *= (float) Math.Pow(CameraSizeZoomedIn / CameraSizeZoomedOut, Time.deltaTime / ZoomInDuration);
            MainCamera.orthographicSize -= Time.deltaTime/ZoomInDuration * (CameraSizeZoomedOut -CameraSizeZoomedIn);
            dampness=dampness+ Time.deltaTime/ZoomInDuration<=1?dampness+ Time.deltaTime/ZoomInDuration:1;
            AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground", dampness);
        }
        else
        {
            MainCamera.orthographicSize = CameraSizeZoomedIn;
            MainCameraGameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
            ZoomStatus = Zoom.Shop;
            dampness=1;
            Camera.main.transform.GetChild(0).gameObject.SetActive(true);
            AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground", dampness);
            GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            GameObject.FindWithTag("DarkenerExcludeMenu").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
            if (((LevelCount - 1) / 6) % 3 == 0)
            {
                colorScheme = ColorScheme.Asteroid;
            }
            if (((LevelCount - 1) / 6) % 3 == 1)
            {
                colorScheme = ColorScheme.Nature;
            }
            if (((LevelCount - 1) / 6) % 3 == 2)
            {
                colorScheme = ColorScheme.FireIce;
            }
        }
        
    }
    
    protected void ZoomingOut()
    {
        if (MainCamera.orthographicSize < CameraSizeZoomedOut)
        {
            MainCameraGameObject.transform.Translate(-Player.transform.position * Time.deltaTime/ ZoomInDuration);
            //MainCamera.orthographicSize *= (float) Math.Pow(CameraSizeZoomedOut / CameraSizeZoomedIn, Time.deltaTime / ZoomOutDuration);
            MainCamera.orthographicSize += Time.deltaTime/ZoomInDuration * (CameraSizeZoomedOut -CameraSizeZoomedIn);
            dampness=dampness- Time.deltaTime/ZoomInDuration>=0?dampness- Time.deltaTime/ZoomInDuration:0;
            AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground", dampness);
        }
        else
        {
            MainCamera.orthographicSize = CameraSizeZoomedOut;
            MainCameraGameObject.transform.position = new Vector3(0, 0, -10);
            ZoomStatus = Zoom.Level;
            GameObject.FindWithTag("Overlay").transform.GetChild(0).gameObject.SetActive(true);
            dampness=0;
            AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground", dampness);
            if (!AudioManager.instance.playingMusic)
            {
                if (((LevelCount - 1) / 6) % 2 == 0)
                {
                    AudioManager.instance.InitializeMusic(FMODEvents.instance.mainTheme);
                }
                else
                {
                    AudioManager.instance.InitializeMusic(FMODEvents.instance.bossTheme);
                }
            }
        }
        
    }
    
    

    public enum Zoom
    {
        Zoomin,
        Zoomout,
        Shop,
        Level,
        Dying,
        Dead,
        Menu
    }

    public enum ColorScheme
    {
        Asteroid,
        FireIce,
        Nature
    }

    protected void CheckPlayerDeath()
    {
        if (GameObject.FindWithTag("Player").GetComponent<Player>().currenthp <= 0)
        {
            Debug.Log("Died");
            AudioManager.instance.StopMusic(STOP_MODE.IMMEDIATE);
            AudioManager.instance.PlayLevelOneShot(FMODEvents.instance.death,Player.transform.position);
            ZoomStatus = Zoom.Dying;
            GameObject.FindWithTag("Player").GetComponent<Player>().money = moneyLevelStart;
            target = Target.Shop;
        }
    }

    public enum Target
    {
        Shop,
        Menu,
        VictoryScreen
    }
        
}
