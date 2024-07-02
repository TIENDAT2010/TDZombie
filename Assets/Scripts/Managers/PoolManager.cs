using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private BulletController bulletPrefab;
    [SerializeField] private BulletEffectController bulletEffectPrefab;
    [SerializeField] private BossBodyController bossBodyPrefab;
    [SerializeField] private EnemyController[] enemyPrefabs;
    [SerializeField] private EffectController[] bloodEffectPrefabs = null;


    private List<BulletController> listBullet = new List<BulletController>();
    private List<EnemyController> listEnemy = new List<EnemyController>();
    private List<EffectController> listBloodEffect = new List<EffectController>();
    private List<BossBodyController> listBossBody = new List<BossBodyController>();
    private List<BulletEffectController> listBulletEffect = new List<BulletEffectController>();


    private void Awake()
    {
        if (Instance != null)
        {
            Instance = null;
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    public BulletController GetBulletController()
    {
        BulletController resultBullet = listBullet.Where(a => !a.gameObject.activeSelf).FirstOrDefault();
        if (resultBullet == null)
        {
            BulletController prefab = bulletPrefab;
            resultBullet = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listBullet.Add(resultBullet);
        }
        resultBullet.gameObject.SetActive(true);
        return resultBullet;
    }



    public BulletEffectController GetBulletEffectController()
    {
        BulletEffectController resultBulletEffect = listBulletEffect.Where(a => !a.gameObject.activeSelf).FirstOrDefault();
        if (resultBulletEffect == null)
        {
            BulletEffectController prefab = bulletEffectPrefab;
            resultBulletEffect = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listBulletEffect.Add(resultBulletEffect);
        }
        resultBulletEffect.gameObject.SetActive(true);
        return resultBulletEffect;
    }


    public EnemyController GetEnemyController(EnemyID enemyID)
    {
        EnemyController enemy = listEnemy.Where(a => a.EnemyID.Equals(enemyID) && !a.gameObject.activeSelf).FirstOrDefault();
        if (enemy == null)
        {
            EnemyController prefab = enemyPrefabs.Where(a => a.EnemyID.Equals(enemyID)).FirstOrDefault();
            enemy = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listEnemy.Add(enemy);
        }
        enemy.gameObject.SetActive(true);
        return enemy;
    }




    public EffectController GetEffectController(EffectType effectType)
    {
        EffectController resultEffect = listBloodEffect.Where(a => a.EffectType.Equals(effectType) && !a.gameObject.activeSelf).FirstOrDefault();
        if (resultEffect == null)
        {
            EffectController prefab = bloodEffectPrefabs.Where(a => a.EffectType.Equals(effectType)).FirstOrDefault(); ;
            resultEffect = Instantiate(prefab, Vector3.zero, Quaternion.identity);
            listBloodEffect.Add(resultEffect);
        }
        resultEffect.gameObject.SetActive(true);
        return resultEffect;
    }


    public BossBodyController GetBossBodyController()
    {
        BossBodyController bossbody = listBossBody.Where(a => !a.gameObject.activeSelf).FirstOrDefault();
        if (bossbody == null)
        {
            bossbody = Instantiate(bossBodyPrefab, Vector3.zero, Quaternion.identity);
            listBossBody.Add(bossbody);
        }
        bossbody.gameObject.SetActive(true);
        return bossbody;
    }
}
