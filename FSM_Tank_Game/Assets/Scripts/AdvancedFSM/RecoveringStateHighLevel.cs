using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveringStateHighLevel : StateHighLevel
{

    public override void Awake()
    {
        base.Awake();
        states = new List<State>
        {
            gameObject.GetComponent<FleeingState>(),
            gameObject.GetComponent<ExtinguishingState>(),
            gameObject.GetComponent<ReturningState>()
        };

        stateInitial = gameObject.GetComponent<FleeingState>();

    }

    // Start is called before the first frame update
    public void Start()
    {
        transitions = new List<Transition>
        {
            new Transition(() =>  Vector3.Distance(transform.position, tank.lastPosition) < 100f && tank.health >= 75, tank.lastState)
        };

        tank.StateText.text = stateInitial.GetType().Name;
    }
}
