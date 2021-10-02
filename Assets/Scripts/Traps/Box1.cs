using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box1 : Box
{
    protected override void Awake()
    {
        Hits = 1;
        friutsNum = 3;
        base.Awake();

    }

}
