using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Gun))]
public class GunInspector : Editor
{
    Gun gun;
    private bool showFire;
    private bool showReload;
    private bool showMove;

    public override void OnInspectorGUI()
    {
        gun = (Gun)target;

        gun.gunType = (GunType)EditorGUILayout.EnumPopup("Gun Type", gun.gunType);

        EditorGUILayout.Space();

        gun.name = EditorGUILayout.TextField("Name", gun.name);
        gun.bulletType = (GameObject)EditorGUILayout.ObjectField("Bullet Type", gun.bulletType, typeof(GameObject), true);
        if(!((int)gun.gunType == 5) && !((int)gun.gunType == 6))
        {
            gun.ammunition = EditorGUILayout.IntField("Ammunition", gun.ammunition);
        }

        EditorGUILayout.Space();

        showFire = EditorGUILayout.Foldout(showFire, "Fire Settings");
        if(showFire)
        {
            

            if (!((int)gun.gunType == 3))
            {
                gun.fireGap = EditorGUILayout.IntField("Fire Gap", gun.fireGap);
            }
            if ((int)gun.gunType == 2 || (int)gun.gunType == 6)
            {
                gun.shotGunNum = EditorGUILayout.IntField("Bullet Number", gun.shotGunNum);
            }
            if ((int)gun.gunType == 3)
            {
                gun.sniperFireGap = EditorGUILayout.FloatField("Fire Gap", gun.sniperFireGap);
            }
            if ((int)gun.gunType == 4)
            {
                gun.rifleFireGap = EditorGUILayout.FloatField("Rifle Fire Gap", gun.rifleFireGap);
            }
            if (!((int)gun.gunType == 3))
            {
                gun.horizontalRange = EditorGUILayout.FloatField("Horizontal Range", gun.horizontalRange);
            }
            EditorGUILayout.Space();
        }



        if (!((int)gun.gunType == 5) && !((int)gun.gunType == 6)) showReload = EditorGUILayout.Foldout(showReload, "Reload Settings");
        if(showReload)
        {
            gun.reloadTime = EditorGUILayout.FloatField("Reload Time", gun.reloadTime);
            EditorGUILayout.Space();
        }



        showMove = EditorGUILayout.Foldout(showMove, "Move Settings");
        if(showMove)
        {
            gun.runSpeedCoefficient = EditorGUILayout.FloatField("Speed Coefficient", gun.runSpeedCoefficient);
            if (!((int)gun.gunType == 5) && !((int)gun.gunType == 6) && !((int)gun.gunType == 2) && !((int)gun.gunType == 3))
                gun.staticHorizontalRangeCoefficient = EditorGUILayout.FloatField("Static Coefficient", gun.staticHorizontalRangeCoefficient);
        }

        EditorGUILayout.Space();

        base.OnInspectorGUI();
    }

}
