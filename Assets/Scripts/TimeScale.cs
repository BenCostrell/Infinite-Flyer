using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeScale  {

    public TimeScale(string unitString, int unitsUntilNext, string endMessage)
    {
        unit = unitString;
        numUnitsUntilNextTimeScale = unitsUntilNext;
        transitionMessage = endMessage;
    }

    public TimeScale Then(TimeScale next)
    {
        nextTimeScale = next;
        return this;
    }

    public string unit;
    public TimeScale nextTimeScale;
    public int numUnitsUntilNextTimeScale;
    public string transitionMessage;
    
}
