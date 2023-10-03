using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vec 
{

    public static void LookAt(Transform oriPos, Transform targetPos)
    {
        Vector2 v = (targetPos.position - oriPos.position).normalized;
        oriPos.transform.right = -v;
    }

    public static void LookAt(Transform oriPos, Vector3 targetPos)
    {
        Vector2 v = (targetPos - oriPos.position).normalized;
        oriPos.transform.right = -v;
    }
    public static void MoveTowardsTarget(Transform source,Vector3 target,float speed)
    {
        if (Vector3.Distance(source.position, target) > .1f)
        {
            Vector3 directionOfTravel = source.position - target;
            directionOfTravel.Normalize();
            source.transform.Translate(
                (directionOfTravel.x * speed * Time.deltaTime),
                (directionOfTravel.y * speed * Time.deltaTime),
                (directionOfTravel.z * speed * Time.deltaTime),
                Space.World);
        }
    }
    public static void PositionInt(Transform transfrom)
    {
        
        transfrom.position = new Vector3((int)Math.Round(transfrom.position.x), (int)Math.Round(transfrom.position.y), (int)transfrom.position.z);
    }


}
