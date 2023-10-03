using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    SerializedObject player;
    SerializedProperty isFire;
    SerializedProperty isHidden;
    SerializedProperty MoodValueMAX;
    SerializedProperty MoodValueInit;
    SerializedProperty MoveSpeed;
    SerializedProperty Bullet;
    SerializedProperty bulletSpawnPoint;

    SerializedProperty fireInterval;
    SerializedProperty pigmentsNum;

    SerializedProperty isMirror;
    SerializedProperty MirrorPlayer;
    SerializedProperty MirrorBullet;

    SerializedProperty Menu;
    SerializedProperty win;
    // Start is called before the first frame update
    private void OnEnable()
    {
        player = new SerializedObject(target);
        isFire = player.FindProperty("isFire");
        isHidden = player.FindProperty("isHidden");
        MoodValueMAX = player.FindProperty("MoodValueMAX");
        MoodValueInit = player.FindProperty("MoodValueInit");
        MoveSpeed = player.FindProperty("MoveSpeed");
        Bullet = player.FindProperty("Bullet");
        bulletSpawnPoint = player.FindProperty("bulletSpawnPoint");
        fireInterval = player.FindProperty("fireInterval");
        pigmentsNum = player.FindProperty("pigmentsNum");

        isMirror = player.FindProperty("isMirror");
        MirrorPlayer= player.FindProperty("PlayerMirror");
        MirrorBullet = player.FindProperty("MirrorBullet");

        Menu = player.FindProperty("menu");
        win = player.FindProperty("win");
    }
    public override void OnInspectorGUI()
    {
        player.Update();

        EditorGUILayout.PropertyField(MoodValueMAX);
        EditorGUILayout.PropertyField(MoodValueInit);
        EditorGUILayout.PropertyField(MoveSpeed);
        EditorGUILayout.PropertyField(isHidden);
        EditorGUILayout.PropertyField(isFire);
        if (isFire.boolValue)
        {
            EditorGUILayout.PropertyField(fireInterval);
            EditorGUILayout.PropertyField(Bullet);
            EditorGUILayout.PropertyField(bulletSpawnPoint);
        }

   
        EditorGUILayout.PropertyField(pigmentsNum);

        EditorGUILayout.PropertyField(isMirror);
        if (isMirror.boolValue)
        {
            EditorGUILayout.PropertyField(MirrorPlayer);
            EditorGUILayout.PropertyField(MirrorBullet);
        }
            
        //不更改类型
        EditorGUILayout.PropertyField(Menu);
        EditorGUILayout.PropertyField(win);

        player.ApplyModifiedProperties();
    }
}
