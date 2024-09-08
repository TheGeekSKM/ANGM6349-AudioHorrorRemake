using System.Collections;
using System.Collections.Generic;
using SaiUtils.StateMachine;
using UnityEngine;

public class BlankPredicate : IPredicate
{
    public bool Evaluate()
    {
        return false;
    }
}

