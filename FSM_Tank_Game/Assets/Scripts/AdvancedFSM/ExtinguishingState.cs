using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishingState : State
{
    public void Start()
    {
        transitions = new List<Transition>
        {
            new Transition(() => tank.health == 75, gameObject.GetComponent<ReturningState>())
        };
    }

    public override void OnEnable()
    {
        // aanpassingen aan het object in deze state
        tank.healthChange = 1;
    }

    public override void OnDisable()
    {
        // wanneer aanpassingen weer uit gezet moeten worden
        tank.myTimer.Stop();
    }

    public override void Update()
    {
        tank.CatchFire();
    }
}
