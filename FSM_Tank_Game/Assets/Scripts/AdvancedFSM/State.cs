using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    public List<Transition> transitions;

    protected NewNPCTankController tank;

    public virtual void Awake()
    {
        tank = GetComponent<NewNPCTankController>();

        transitions = new List<Transition>();

        //Setup your transitions
    }

    public virtual void OnEnable()
    {
        //Develop state's initializations
        
    }

    public virtual void OnDisable()
    {
        //Develop state's finalization
    }

    public virtual void Update()
    {
        //Develop behaviour
    }

    public void LateUpdate()
    {
        foreach (Transition t in transitions)
        {
            if (t.condition())
            {
                t.target.enabled = true;
                this.enabled = false;
                tank.StateText.text = t.target.GetType().Name;
                return;
            }
            continue;
        }
    }
}
