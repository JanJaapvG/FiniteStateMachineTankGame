using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleeingState : State
{
    public void Start()
    {
        transitions = new List<Transition>
        {
            new Transition(() => Vector3.Distance(transform.position, tank.pond.position) <= 100f, gameObject.GetComponent<ExtinguishingState>())
        };
    }

    public override void OnEnable()
    {
        // aanpassingen aan het object in deze state
        tank.curRotSpeed = 5.0f;
        tank.curSpeed = 200.0f;

        tank.StartTimer();

        tank.destPos = new Vector3(Random.Range(tank.minPosition.x, tank.maxPosition.x), 0.0f,
            Random.Range(tank.minPosition.z, tank.maxPosition.z));
    }

    public override void OnDisable()
    {
        // wanneer aanpassingen weer uit gezet moeten worden
    }

    public override void Update()
    {
        Debug.Log(tank.health);

        Quaternion targetRotation = Quaternion.LookRotation(tank.destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * tank.curRotSpeed);

        //Go Forward
        transform.Translate(Vector3.forward * Time.deltaTime * tank.curSpeed);

        tank.CatchFire();
    }

}
