using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box3 : Box
{
    protected override void Awake()
    {
        Hits = 5;
        friutsNum = 7;
        base.Awake();

    }
}
