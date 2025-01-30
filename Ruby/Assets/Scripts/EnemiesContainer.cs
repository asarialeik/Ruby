using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesContainer : MonoBehaviour
{
    public int currentBrokenEnemies;
    public int enemiesTotal;

    public void AddEnemy()
    {
        enemiesTotal += 1;
        currentBrokenEnemies = enemiesTotal;
        UIHandler.instance.EnemyAmountDisplay(currentBrokenEnemies);
    }

    public void RemoveEnemy()
    {
        currentBrokenEnemies -= 1;
        UIHandler.instance.EnemyAmountDisplay(currentBrokenEnemies);
    }
}
