using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : EnemyController
{
    [SerializeField] private float m_Health = 0f;
    private bool isDead = false;


    public override void OnInit(WaypointController waypointController)
    {
        PlayerController = FindObjectOfType<PlayerController>();
        WaypointController = waypointController;
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        navMeshAgent.Warp(WaypointController.CurrentPoint);
        OnNextState(EnemyState.Idle_State);
    }


    public override void OnNextState(EnemyState nextState)
    {
        if (nextState == EnemyState.Idle_State)
        {
            idleController.EnterEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if (nextState == EnemyState.Walk_State)
        {
            idleController.ExitEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.EnterEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if (nextState == EnemyState.Dead_State)
        {
            idleController.ExitEnemyIdle();
            deadController.EnterEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if (nextState == EnemyState.Attack_State)
        {
            idleController.ExitEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.EnterEnemyAttack();
            chaseController.ExitEnemyChase();
        }
        else if (nextState == EnemyState.Chase_State)
        {
            idleController.ExitEnemyIdle();
            deadController.ExitEnemyDead();
            walkController.ExitEnemyWalk();
            attackController.ExitEnemyAttack();
            chaseController.EnterEnemyChase();
        }
    }




    public override void OnReceiveDamage(float damage, Vector2 attackDir)
    {
        //Tao hieu ung blood
        EffectController bossBloodEffect = PoolManager.Instance.GetEffectController(EffectType.Boss_Blood_Effect);
        bossBloodEffect.transform.position = transform.position;
        bossBloodEffect.OnInit(attackDir, true);


        //Xu ly hp
        m_Health = m_Health - damage;
        if (m_Health <= 0 && isDead == false)
        {
            isDead = true;
            OnNextState(EnemyState.Dead_State);
        }
    }
    
    
    public override bool IsInRangeChase()
    {
        float distance = Vector2.Distance(PlayerController.transform.position, transform.position);
        if (distance < 20f)
        {
            return true;
        }
        return false;

    }



    public override bool IsInRangeAttack()
    {
        float distance = Vector2.Distance(PlayerController.transform.position, transform.position);
        if (distance < attackController.GetRangeAttack())
        {
            return true;
        }
        return false;
    }



    public override bool PlayerIsInView()
    {
        Vector2 fowardPlayer = (PlayerController.transform.position - transform.position).normalized;
        float dotProduct = Vector2.Dot(transform.up, fowardPlayer);
        if(dotProduct > 0)
        {
            float angle = Vector2.Angle(transform.up, fowardPlayer);
            return (angle < 80) ? true : false;
        }
        else
        {
            return false;
        }
    }



    public override void PlaySoundEffect(AudioClip audioClip)
    {
        base.PlaySoundEffect(audioClip);
    }
}
