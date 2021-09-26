using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturningState : State
{

    public override void OnEnable()
    {
        // aanpassingen aan het object in deze state
        tank.curRotSpeed = 1.0f;
        tank.curSpeed = 100.0f;
    }

    public override void OnDisable()
    {
        // wanneer aanpassingen weer uit gezet moeten worden
    }

    public override void Update()
    {
        //Rotate to the target point
        tank.destPos = tank.lastPosition;

        Quaternion targetRotation = Quaternion.LookRotation(tank.destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);
    }
}
