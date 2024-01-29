using System;
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
    public Zoom ZoomStatus = Zoom.Level;
    protected GameObject Level = null;
    protected int LevelCount = 1;
    protected const int BossLevel = 20;
    private float dampness = 0;
    public ColorScheme colorScheme = ColorScheme.Asteroid;
    public float dyingFadeOutDuration = 1;
    public float deadDuration = 1;
    private float _curDeadTime = 0;
    
    protected virtual void Start()
    {
        //GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1f);
        Player = GameObject.FindWithTag("Player");
        MainCameraGameObject = GameObject.FindWithTag("MainCamera");
        MainCamera = MainCameraGameObject.GetComponent<Camera>();
        GameObject.FindWithTag("Shop").SetActive(false);
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
                        
                        switch (LevelCount%4)
                        {
                                
                            case 1:
                                Level.AddComponent<LevelObstacleSpam>();
                                Level.GetComponent<LevelObstacleSpam>().speed = 10;
                                break;
                            case 2:
                                Level.AddComponent<LevelBigSpam>();
                                Level.GetComponent<LevelBigSpam>().speed = 10;
                                break;
                            case 3:
                                Level.AddComponent<LevelMachineGunSpam>();
                                Level.GetComponent<LevelMachineGunSpam>().speed = 10;
                                break;
                            case 4:
                                Level.AddComponent<LevelShrapnelSpam>();
                                Level.GetComponent<LevelShrapnelSpam>().speed = 10;
                                break;
                            case 5:
                                Level.AddComponent<LevelHomingSpam>();
                                Level.GetComponent<LevelHomingSpam>().speed = 10;
                                break;
                            case 0:
                                Level.AddComponent<LevelMixedSpam>();
                                Level.GetComponent<LevelMixedSpam>().speed = 10;
                                break;
                        }
                        Level.transform.position =
                            new Vector3(Camera.main.orthographicSize * Camera.main.aspect + 15, 0, 0);
                    }
                }
                if (Level.GetComponent<Level>().levelEnd + Level.transform.position.x <0)
                {
                    ZoomStatus = Zoom.Zoomin;
                    GameObject.FindWithTag("Overlay").SetActive(false);
                    //GameObject.FindWithTag("InputSystem").SetActive(false);
                }
                
                break;
            case Zoom.Dying:
                if (GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color.a>=1)
                {
                    ZoomStatus = Zoom.Dead;
                    ResetLevel();
                    GameObject.FindWithTag("Overlay").SetActive(false);
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
                    AudioManager.instance.InitializeMusic(FMODEvents.instance.mainTheme);
                    AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground",1);
                }
                _curDeadTime += Time.fixedDeltaTime;
                break;
                

        }
    }

    protected void ResetLevel()
    {
        Destroy(GameObject.FindWithTag("Level"));
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
            AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground", dampness);
            GameObject.FindWithTag("Darkener").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            GameObject.FindWithTag("DarkenerExcludeMenu").GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.5f);
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
            dampness=0;
            AudioManager.instance.SetMainThemeDampnessParameter("MainThemeBackground", dampness);
        }
        
    }
    
    

    public enum Zoom
    {
        Zoomin,
        Zoomout,
        Shop,
        Level,
        Dying,
        Dead
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
            AudioManager.instance.StopMusic();
            AudioManager.instance.PlayOneShot(FMODEvents.instance.death,Player.transform.position);
            ZoomStatus = Zoom.Dying;
        }
    }
        
}
