using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SimpleRandomWalkParameters_", menuName = "PCG/SimpleRandomWalkData")]
public class SimpleRandomWalkSO : ScriptableObject // will allow us to create menu inside the inspector
{
    public int iterations = 10, walkLength = 10;
    public bool startRandomlyEachIteration = true;
}
