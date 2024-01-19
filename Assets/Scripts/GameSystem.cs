using System;
using UnityEngine;

public abstract class GameSystem : MonoBehaviour
{
    

    public static GameObject Player;
    public static GameObject MainCameraGameObject;
    public static Camera MainCamera;
    protected const float ZoomInDuration = 1f;
    protected const float ZoomOutDuration = 1f;
    protected float CameraSizeZoomedIn = 2;
    protected float CameraSizeZoomedOut = 5;
    protected Zoom ZoomStatus = Zoom.Level;
    protected virtual void Awake()
    {
        Player = GameObject.FindWithTag("Player");
        MainCameraGameObject = GameObject.FindWithTag("MainCamera");
        MainCamera = MainCameraGameObject.GetComponent<Camera>();
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
    }


    protected void ZoomingIn()
    {
        if (MainCamera.orthographicSize > CameraSizeZoomedIn)
        {
            MainCameraGameObject.transform.Translate(Player.transform.position * Time.deltaTime/ ZoomInDuration);
            MainCamera.orthographicSize *=
                (float) Math.Pow(CameraSizeZoomedIn / CameraSizeZoomedOut, Time.deltaTime / ZoomInDuration);
        }
        else
        {
            MainCamera.orthographicSize = CameraSizeZoomedIn;
            MainCameraGameObject.transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, -10);
            ZoomStatus = Zoom.Shop;
        }
        
    }
    
    protected void ZoomingOut()
    {
        if (MainCamera.orthographicSize < CameraSizeZoomedOut)
        {
            MainCameraGameObject.transform.Translate(-Player.transform.position * Time.deltaTime/ ZoomInDuration);
            MainCamera.orthographicSize *=
                (float) Math.Pow(CameraSizeZoomedOut / CameraSizeZoomedIn, Time.deltaTime / ZoomOutDuration);
        }
        else
        {
            MainCamera.orthographicSize = CameraSizeZoomedOut;
            MainCameraGameObject.transform.position = new Vector3(0, 0, -10);
            ZoomStatus = Zoom.Level;
        }
        
    }
    
    

    protected enum Zoom
    {
        Zoomin,
        Zoomout,
        Shop,
        Level
    }
        
}
