using System.Collections;
using System.Collections.Generic;
using UnityEditor.EditorTools;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Color normalColor = Color.white;
    [SerializeField] Color poisonColor = Color.green;
    [SerializeField] Color fireColor = Color.red;
    [SerializeField] WeaponController weaponControllers = null;
    [SerializeField] FootController footControllers = null;
    [SerializeField] private LayerMask ostacleLayer = new LayerMask();
    [SerializeField] private LayerMask enemyLayer = new LayerMask();
    [SerializeField] private float moveSpeed;
    [SerializeField] Vector2 vector3MoveInput;
    [SerializeField] private float playerHealth = 0f;        
    

    private bool isPoison = false;
    private bool isFire = false;

    private Vector2 mousePos;

    private void Start()
    {
        footControllers.SetIdle();
    }


    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        vector3MoveInput = new Vector2(horizontal, vertical);

        RaycastHit2D raycastHit2D = Physics2D.CircleCast(transform.position, 1f, vector3MoveInput.normalized, 1.5f, (ostacleLayer|enemyLayer));
        if(raycastHit2D.collider == null)
        {
            Vector2 playerPos = transform.position;
            playerPos += vector3MoveInput * moveSpeed * Time.deltaTime;
            transform.position = playerPos;

            if (horizontal + vertical != 0 && footControllers.IsIdle == true)
            {
                footControllers.SetMoving();
            }
            else if (vertical + horizontal == 0 && footControllers.IsIdle == false)
            {
                footControllers.SetIdle();
            }
        }


        if (Input.GetMouseButtonUp(0) && weaponControllers.IsFinishAttack == true)
        {
            weaponControllers.PlayAttack();
        }

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.up = (mousePos - (Vector2)transform.position).normalized;

        if (playerHealth <= 0)
        {
            Debug.Log("Game Over !!!");
        }
    }



    private IEnumerator HandlePoisonEffect(float damagePerSecond, EffectController poisonEffect, float timeDamagePoison)
    {
        while(timeDamagePoison > 0)
        {
            yield return new WaitForSeconds(1f);
            playerHealth -= damagePerSecond;
            timeDamagePoison = timeDamagePoison - 1f;
        }
        poisonEffect.transform.SetParent(null);
        poisonEffect.gameObject.SetActive(false);
        isPoison = false;
        weaponControllers.SetColor(normalColor);
    }



    private IEnumerator HandleFireEffect(float damagePerSecond, EffectController fireEffect, float timeDamageFire)
    {
        while(timeDamageFire > 0)
        {
            yield return new WaitForSeconds(1f);
            playerHealth -= damagePerSecond;
            timeDamageFire = timeDamageFire - 1f;
        }
        fireEffect.transform.SetParent(null);
        fireEffect.gameObject.SetActive(false);
        isFire = false;
        weaponControllers.SetColor(normalColor);
    }



    public void OnReceiveNormalAttack(float damage, Vector2 attackDir) 
    {
        playerHealth = playerHealth - damage;
        EffectController redEffect = PoolManager.Instance.GetEffectController(EffectType.Player_Blood_Effect);
        redEffect.transform.position = transform.position;
        redEffect.OnInit(attackDir, true);
    }

    public void OnReceiveFireAttack(float fisrtDamage, float damagePerSecond, float timeDamageFire) 
    {
        playerHealth -= fisrtDamage;
        if (isFire == false)
        {
            weaponControllers.SetColor(fireColor);
            isFire = true;
            EffectController fireEffect = PoolManager.Instance.GetEffectController(EffectType.Player_Burn_Effect);
            fireEffect.transform.SetParent(transform);
            fireEffect.transform.localPosition = new Vector2(-0.35f, -0.45f);
            fireEffect.OnInit(transform.up, false);
            StartCoroutine(HandleFireEffect(damagePerSecond, fireEffect, timeDamageFire));
        }
    }


    public void OnReceivePoisonAttack(float fisrtDamage, float damagePerSecond, float timeDamage) 
    {
        playerHealth -= fisrtDamage;
        if(isPoison == false)
        {
            weaponControllers.SetColor(poisonColor);
            isPoison = true;
            EffectController poisonEffect = PoolManager.Instance.GetEffectController(EffectType.Player_Poison_Effect);
            poisonEffect.transform.SetParent(transform);
            poisonEffect.transform.localPosition = new Vector2(-0.35f, -0.45f);
            poisonEffect.OnInit(transform.up, false);
            StartCoroutine(HandlePoisonEffect(damagePerSecond, poisonEffect, timeDamage));
        }
    }
}
