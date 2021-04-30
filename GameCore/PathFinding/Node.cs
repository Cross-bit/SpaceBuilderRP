using System;
using System.Collections.Generic;
using UnityEngine;


public class Node{
    public Vector3 position;
    public Dictionary<Vector3, float> neighbours;
    public bool hasInnerPath;
}

