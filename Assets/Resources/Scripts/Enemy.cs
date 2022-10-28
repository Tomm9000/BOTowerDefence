using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    // Waypoint Variables
    [SerializeField] int At = 0;
    [SerializeField] Vector3 target;
    List<Transform> Waypoints = new List<Transform>();
    private int Path;
    private Transform Coords;

    // Type Variables
    private enum EnemyTypes{FootSoldier, Cavalry};
    [SerializeField] EnemyTypes EnemyType;
    private float TypeDamage;

    // Enemy Variables
    private float _enemySpeed;
    public Image healthBar;
    private float _enemyHealth;
    private float _enemyStartHealth;
    private float _enemyEnergyDrop;

    private Stats _stats;
    private bool _isDead = false;
    
    private void Start()
    {
        if (EnemyType == EnemyTypes.FootSoldier) FootSoldier();

        else if (EnemyType == EnemyTypes.Cavalry) Cavalry();

        PathFinder();

        _enemyStartHealth = _enemyHealth;

        _stats = GameObject.FindGameObjectWithTag("Stats").GetComponent<Stats>();
    }

    void Update()
    {
        if (transform.position != Waypoints[At].transform.position)
        {
            target = Waypoints[At].transform.position;
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * _enemySpeed);
        }
        else if (Waypoints.Count > At + 1) At++;

        else AtEnd();

        if (_enemyHealth < 0)
        {
            Destroy(gameObject);
            _stats.EnemyKill(_enemyEnergyDrop);
        } 
    }

    public void PathFinder()
    {
        Coords = GameObject.Find("Waypoints " + Path).transform;
        for (int i = 0; i < Coords.childCount; i++) Waypoints.Add(Coords.GetChild(i).transform);
    }
    public void FootSoldier()
    {
        _enemySpeed = 3;
        _enemyHealth = 100;
        Path = 0;
        TypeDamage = 1;
        _enemyEnergyDrop = 2;
    }
    public void Cavalry()
    {
        _enemySpeed = 6;
        _enemyHealth = 50;
        Path = 0;
        TypeDamage = 2;
        _enemyEnergyDrop = 3;
    }
    public void Damage(float Damage)
    {
        _enemyHealth -= Damage;
        Debug.Log(_enemyHealth);
    }

    public void AtEnd()
    {
        if (_isDead) return;


        _isDead = true;
        _stats.EnemyThrough(TypeDamage);
        Destroy(gameObject);
    }
}
