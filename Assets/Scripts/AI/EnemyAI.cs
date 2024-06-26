using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class EnemyAI : ScriptableObject
{
    public abstract void think(EnemyLogic logic);
}
