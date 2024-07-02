using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapController : MonoBehaviour
{
    [SerializeField] WaypointController[] waypointControllers = null;


    private void Start()
    {
        //ZombieController zombie = waypointControllers[0].CreateEnemy(EnemyID.Zombie_1);
        //zombie.OnInit(waypointControllers[0]);
        for (int i = 0; i < waypointControllers.Length; i++)
        {
            waypointControllers[i].CreateEnemy();
        }
    }
}
