using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{

    [Header("Enemy References")]
    [SerializeField] protected EnemyID enemyID = EnemyID.Zombie_1;
    [SerializeField] protected EnemyIdleController idleController = null;
    [SerializeField] protected EnemyWalkController walkController = null;
    [SerializeField] protected EnemyChaseController chaseController = null;
    [SerializeField] protected EnemyAttackController attackController = null;
    [SerializeField] protected EnemyDeadController deadController = null;
    [SerializeField] protected NavMeshAgent navMeshAgent = null;
    [SerializeField] protected SpriteRenderer spriteRenderer = null;
    [SerializeField] protected AudioSource audioSource = null;
    [SerializeField] protected Collider2D col2D = null;

    public EnemyID EnemyID => enemyID;


    public Collider2D Colider => col2D;
    public NavMeshAgent NavMeshAgent => navMeshAgent;
    public SpriteRenderer SpriteRenderer => spriteRenderer;
    public PlayerController PlayerController { protected set; get; }
    public WaypointController WaypointController { protected set; get; }

    public virtual void OnInit(WaypointController waypoint) { }


    public virtual void OnNextState(EnemyState nextState) { }
    

    public virtual void OnReceiveDamage(float damage, Vector2 attackDir) { }


    public virtual bool IsInRangeChase()
    {
        return false;
    }    

    public virtual bool IsInRangeAttack()
    {
        return false;
    }

    public virtual bool PlayerIsInView()
    {
        return false ;
    }



    public virtual void PlaySoundEffect(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
