using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    // Enemy detection
    public List<GameObject> curEnemiesInRange = new List<GameObject>();
    
    // Shooting stuff
    private float ProLiveTime = 0.5f;
    public float nextShot = 0.25f;
    public float Delay = 0.5f;


    CircleCollider2D circleCollider;
    private float colliderySize;

    // Tower Types
    public enum TowerType {Bomb, Energy, Archer};
    public TowerType Type;

    // Prefab
    private GameObject _bullet;
    private Stats _stats;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            curEnemiesInRange.Add(collision.gameObject);
            //Debug.Log("Enemy enter");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            curEnemiesInRange.Remove(collision.gameObject);
            //Debug.Log("Enemy exit");
        }

    }

    private void Start()
    {
        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
        _bullet = LoadPrefab("Bullet");
    }

    private void Update()
    {
        // Tower Type
        if (Type == TowerType.Bomb)
        {
            Delay = 1f;
        }
        else if (Type == TowerType.Archer)
        {
            Delay = 1.2f;
        }
        else {
            Delay = 3f;
            if (Time.time > nextShot)
            {
                _stats.EnergyGenerator(2);
                nextShot = Time.time + Delay;
            }
        } 
        // Shooting
        if(curEnemiesInRange.Count > 0)
        {
            //Debug.Log("Shoot");
            if (Time.time > nextShot)
            {
                if (Type == TowerType.Bomb || Type == TowerType.Archer)
                {
                    GameObject pro = Instantiate(_bullet);
                    ProjectileScript pros = pro.GetComponent<ProjectileScript>();
                    if (Type == TowerType.Bomb)
                    {
                        pros.doesExplosion = true;
                    }
                    pro.transform.position = transform.position;
                    pros.CurTarget = curEnemiesInRange[0];
                    Destroy(pro, ProLiveTime);   
                }

                nextShot = Time.time + Delay;
            }
        }
    }
    public UnityEngine.GameObject LoadPrefab(string filename)
    {
        var loadedPrefab = Resources.Load("Prefabs/" + filename, typeof(GameObject)) as GameObject;
        if (loadedPrefab == null)
        {
            Debug.Log("Prefab " + filename + " not found");
        }
        return loadedPrefab;
    }
}
