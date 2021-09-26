using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadState : State
{
    public void Start()
    {
        transitions = new List<Transition>();
    }

    public override void OnEnable()
    {
        // aanpassingen aan het object in deze state
        tank.Explode();
    }

    public override void OnDisable()
    {
        // wanneer aanpassingen weer uit gezet moeten worden
    }

    public override void Update()
    {

    }
}
