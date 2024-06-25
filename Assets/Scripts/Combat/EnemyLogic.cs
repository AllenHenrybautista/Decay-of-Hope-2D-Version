using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
   public EnemyAI enemyAI;


    private void Update()
    {
        enemyAI.think(this);
    }
}
