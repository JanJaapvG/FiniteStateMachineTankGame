using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PatrolState : State
{
    public void Start()
    {
        transitions = new List<Transition>
        {
            new Transition(() => Vector3.Distance(transform.position, tank.player.position) <= 300f, gameObject.GetComponent<ChaseState>()),
            new Transition(() => tank.health <= 50, gameObject.GetComponent<RecoveringStateHighLevel>()),
            new Transition(() => tank.health <= 0, gameObject.GetComponent<DeadState>())
        };        
    }

    public override void OnEnable()
    {
        // aanpassingen aan het object in deze state
        tank.curRotSpeed = 1.0f;
        tank.curSpeed = 100.0f;
    }

    public override void OnDisable()
    {
        // wanneer aanpassingen weer uit gezet moeten worden
        tank.lastPosition = transform.position;
        tank.lastState = gameObject.GetComponent<PatrolState>();
    }

    public override void Update()
    {
        //Find another random patrol point if the current point is reached

        if (Vector3.Distance(transform.position, tank.destPos) <= 100.0f)
        {
            Debug.Log("Reached to the destination point\ncalculating the next point");
            tank.FindNextPoint();
        }

        //Rotate to the target point
        Quaternion targetRotation = Quaternion.LookRotation(tank.destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);

    }
}
