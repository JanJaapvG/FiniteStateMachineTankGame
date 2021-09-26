using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    public void Start()
    {
        transitions = new List<Transition>
        {
            new Transition(() => Vector3.Distance(transform.position, tank.player.position) <= 200f, gameObject.GetComponent<AttackState>()),
            new Transition(() => Vector3.Distance(transform.position, tank.player.position) >= 300f, gameObject.GetComponent<PatrolState>()),
            new Transition(() => tank.health <= 50, gameObject.GetComponent<RecoveringStateHighLevel>()),
            new Transition(() => tank.health <= 0, gameObject.GetComponent<DeadState>())
        };
    }

    public override void OnEnable()
    {
        // aanpassingen aan het object in deze state
        tank.curRotSpeed = 1.0f;
        tank.curSpeed = 150.0f;
    }

    public override void OnDisable()
    {
        // wanneer aanpassingen weer uit gezet moeten worden
        tank.lastPosition = transform.position;
        tank.lastState = gameObject.GetComponent<ChaseState>();
    }

    public override void Update()
    {
        //Rotate to the target point
        tank.destPos = tank.player.position;

        Quaternion targetRotation = Quaternion.LookRotation(tank.destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);


    }
}
