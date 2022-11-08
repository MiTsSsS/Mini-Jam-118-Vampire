using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HighPriest : BaseEnemy
{
    private void OnEnable() {
        PlayerUnit.OnPlayerMove += moveEnemy;
        PlayerUnit.OnPlayerMove += executeRangedAction;
    }

    private void OnDisable() {
        PlayerUnit.OnPlayerMove -= moveEnemy;
        PlayerUnit.OnPlayerMove -= executeRangedAction;
    }

    public override void executeRangedAction() {
        
    }
}
