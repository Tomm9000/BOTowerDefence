using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public GameObject CurTarget;
    private Vector3 ProjDir;
    private Vector3 target;

    private List<GameObject> DamageableEnemies = new();

    public Tower Tower;
    public bool doesExplosion;

    [SerializeField] float Speed;
    [SerializeField] float LifeTime = 0.5f;

    private void Start()
    {
        Tower = GameObject.FindGameObjectWithTag("Tower").GetComponent<Tower>();
        Speed = 10f;
        ProjDir = (CurTarget.transform.position - transform.position).normalized;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (doesExplosion == true)
            {
                DamageableEnemies.Add(collision.gameObject);
                //Debug.Log(DamageableEnemies.Count);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
                DamageableEnemies.Remove(collision.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (doesExplosion == true)
        {
            for (int i = 0; i < DamageableEnemies.Count; i++)
            {
                DamageableEnemies[i].GetComponent<Enemy>().Damage(15);
            }
            InstantiateExplosion();
        }
        else
        {
            DamageableEnemies.Add(collision.gameObject);
            for (int i = 0; i < DamageableEnemies.Count; i++)
            {
                DamageableEnemies[i].GetComponent<Enemy>().Damage(30);
            }   
        }
    }
    void Update()
    {
            transform.Translate(ProjDir * Speed * Time.deltaTime);
    }

    public UnityEngine.GameObject LoadPrefab(string filename)
    {
        var loadedPrefab = Resources.Load("Prefabs/" + filename, typeof(GameObject)) as GameObject;
        if(loadedPrefab == null)
        {
            Debug.Log("Prefab " + filename + " not found");
        }
        return loadedPrefab;
    }

    public void towerType()
    {

    }
    private void InstantiateExplosion()
    {
        GameObject Explosion = Instantiate(LoadPrefab("ExplosionRadius"));
        Explosion.transform.position = transform.position;
        Destroy(Explosion, LifeTime);
        Destroy(gameObject);
    }
}
