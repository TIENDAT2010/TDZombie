using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WeaponController : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayerMask = new LayerMask();
    [SerializeField] private WeaponType weaponType;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] AudioSource audioSource = null;
    [SerializeField] AudioClip weaponAudio = null;
    [SerializeField] Transform bulletSpawnPos = null;
    [SerializeField] Sprite[] animationSprite;

    private int damageBullet = 5;

    public bool IsFinishAttack { get; private set; }

    private void Awake()
    {
        IsFinishAttack = true;
    }


    public virtual void OnEnterAttack()
    {

    }


    public virtual void OnExitAttack()
    {

    }


    public virtual void OnAttack()
    {

    }

    public virtual void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }


    public void PlayAttack()
    {
        IsFinishAttack = false;
        StartCoroutine(CRPlayAnimation());
        if (weaponType == WeaponType.Riffle)
        {
            StartCoroutine(RiffleAttack());
        }
        if (weaponType == WeaponType.HandGun)
        {
            StartCoroutine(HandGunAttack());
        }
        if (weaponType == WeaponType.Bat)
        {
            StartCoroutine(BatAttack());
        }
        if (weaponType == WeaponType.Knife)
        {
            StartCoroutine(KnifeAttack());
        }
    }

    private IEnumerator CRPlayAnimation()
    {
        for (int i = 0; i < animationSprite.Length; i++)
        {
            spriteRenderer.sprite = animationSprite[i];
            yield return new WaitForSeconds(0.05f);
        }
        spriteRenderer.sprite = animationSprite[0];
    }


    /// <summary>
    /// Xu ly Riffle Attack
    /// </summary>
    /// <returns></returns>
    private IEnumerator RiffleAttack()
    {
        for (int i = 0; i < 3; i++)
        {
            BulletController bulletspawn = PoolManager.Instance.GetBulletController();
            bulletspawn.transform.position = bulletSpawnPos.transform.position;
            bulletspawn.transform.up = transform.up;
            bulletspawn.OnInit(damageBullet);
            yield return new WaitForSeconds(0.15f);
            audioSource.PlayOneShot(weaponAudio);
        }
        IsFinishAttack = true;
    }

    private IEnumerator HandGunAttack()
    {
        BulletController bulletspawn = PoolManager.Instance.GetBulletController();
        bulletspawn.transform.position = bulletSpawnPos.transform.position;
        bulletspawn.transform.up = transform.up;
        bulletspawn.OnInit(damageBullet);
        audioSource.PlayOneShot(weaponAudio);
        yield return new WaitForSeconds(0.5f);
        IsFinishAttack = true;
    }


    private IEnumerator BatAttack()
    {
        //Kiem tra va cham voi Enemy
        Collider2D enemyCollider2D = Physics2D.OverlapCircle(transform.position + (transform.up * 3f), 2f, enemyLayerMask);
        if (enemyCollider2D != null)
        {
            //Enemy take damage
            EnemyController enemy = enemyCollider2D.gameObject.GetComponent<EnemyController>();
            enemy.OnReceiveDamage(10f, transform.up);
        }
        audioSource.PlayOneShot(weaponAudio);
        yield return new WaitForSeconds(0.7f);
        IsFinishAttack = true;
    }


    private IEnumerator KnifeAttack()
    {
        //Kiem tra va cham voi Enemy
        Collider2D enemyCollider2D = Physics2D.OverlapCircle(transform.position + (transform.up * 2f), 2f, enemyLayerMask);
        if (enemyCollider2D != null)
        {
            //Enemy take damage
            EnemyController enemy = enemyCollider2D.gameObject.GetComponent<EnemyController>();
            enemy.OnReceiveDamage(10f, transform.up);
        }
        audioSource.PlayOneShot(weaponAudio);
        yield return new WaitForSeconds(0.5f);
        IsFinishAttack = true;
    }



}
