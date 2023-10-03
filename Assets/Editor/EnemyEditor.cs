using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    SerializedObject enemy;
    SerializedProperty animator;
    SerializedProperty animator2;
    SerializedProperty life;
    SerializedProperty speed;
    SerializedProperty isProbe;
    SerializedProperty ProbeDistance;
    SerializedProperty Bullet;
    SerializedProperty isBullet;
    SerializedProperty isChase;
    SerializedProperty col;
    SerializedProperty row;
    SerializedProperty fireInterval;
    SerializedProperty isBackDouble;
    // Start is called before the first frame update
    private void OnEnable()
    {
        enemy = new SerializedObject(target);
        animator = enemy.FindProperty("animator");
        animator2 = enemy.FindProperty("animator2");
        life = enemy.FindProperty("life");
        speed = enemy.FindProperty("speed");
        isBackDouble = enemy.FindProperty("isBackDouble");
        isProbe = enemy.FindProperty("isProbe");
        isChase = enemy.FindProperty("isChase");
        col= enemy.FindProperty("col");
        row = enemy.FindProperty("row");
        ProbeDistance = enemy.FindProperty("ProbeDistance");
        Bullet= enemy.FindProperty("Bullet");
        isBullet = enemy.FindProperty("isBullet");
        fireInterval = enemy.FindProperty("fireInterval");
    }
    public override void OnInspectorGUI()
    {
        enemy.Update();
        EditorGUILayout.PropertyField(animator);
        EditorGUILayout.PropertyField(animator2);
        EditorGUILayout.PropertyField(life);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(isBackDouble);
        EditorGUILayout.PropertyField(isChase);
        if(isChase.boolValue)
            EditorGUILayout.PropertyField(isProbe);
        else
        {
            
            EditorGUILayout.PropertyField(row);
            EditorGUILayout.PropertyField(col);
        }
        if (isChase.boolValue && isProbe.boolValue)
            EditorGUILayout.PropertyField(ProbeDistance);
        
        EditorGUILayout.PropertyField(isBullet);
        if (isBullet.boolValue)
        {
            EditorGUILayout.PropertyField(Bullet);
            EditorGUILayout.PropertyField(fireInterval);
        }
           
        enemy.ApplyModifiedProperties();
    }
}
