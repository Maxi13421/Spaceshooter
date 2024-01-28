using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Shoot SFX")]
    [field: SerializeField] public EventReference shot { get; private set; }
    
    [field: Header("Coin SFX")]
    [field: SerializeField] public EventReference coin { get; private set; }
    
    [field: Header("ExplosionSmall SFX")]
    [field: SerializeField] public EventReference explosionSmall { get; private set; }
    
    [field: Header("ExplosionBig SFX")]
    [field: SerializeField] public EventReference explosionBig { get; private set; }
    
    [field: Header("FireSmall SFX")]
    [field: SerializeField] public EventReference fireSmall { get; private set; }
    
    [field: Header("FireBig SFX")]
    [field: SerializeField] public EventReference fireBig { get; private set; }
    
    [field: Header("Death SFX")]
    [field: SerializeField] public EventReference death { get; private set; }
    
    [field: Header("MainTheme")]
    [field: SerializeField] public EventReference mainTheme { get; private set; }
    
    [field: Header("BossTheme")]
    [field: SerializeField] public EventReference bossTheme { get; private set; }
    
    [field: Header("TitleTheme")]
    [field: SerializeField] public EventReference titleTheme { get; private set; }
    
    [field: Header("TitleThemeWithoutStart")]
    [field: SerializeField] public EventReference titleThemeWithoutStart { get; private set; }
    
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one AudioManager in the scene.");
        }

        instance = this;
    }
}
