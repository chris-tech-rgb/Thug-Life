using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{

    [HideInInspector]
    public GunType gunType;
    [HideInInspector]
    public string name;

    [HideInInspector]
    public GameObject bulletType;
    [HideInInspector]
    public int ammunition;

    [HideInInspector]
    public int fireGap;
    [HideInInspector]
    public int shotGunNum;
    [HideInInspector]
    public float horizontalRange;
    [HideInInspector]
    public float sniperFireGap;
    [HideInInspector]
    public float rifleFireGap;
    [Header("Sound")]
    public AudioClip fireSound;
    public AudioClip reloadSound;
    [HideInInspector]
    public float reloadTime;
    [HideInInspector]
    public float runSpeedCoefficient;
    [HideInInspector]
    public float staticHorizontalRangeCoefficient;
}

public enum GunType
{
    normal = 1, shotGun, sniper, rifle, heavyMachineGun, special
};