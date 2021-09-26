using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class NewNPCTankController : MonoBehaviour
{
    //data
    protected float elapsedTime;
    public float curRotSpeed;
    public float curSpeed;
    public float shootRate;
    public int health;
    public int healthChange;
    public float fireParticleSpeed;

    //gameObjects
    public GameObject Bullet;
    public GameObject Fire;
    public GameObject[] pointList;

    public Transform player;
    public Transform turret;
    public Transform pond;
    public Transform[] waypoints;
    protected Transform bulletSpawnPoint;
    public Transform fireParent;

    public Vector3 lastPosition;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    public Vector3 destPos;

    public TextMesh StateText;
    public TextMesh HealthText;

    public State lastState;
    public State state;
    public Timer myTimer;


    public void Start()
    {
        health = 100;
        elapsedTime = 0.0f;
        shootRate = 2.0f;
        curRotSpeed = 1f;
        curSpeed = 100f;

        healthChange = -1;

        fireParticleSpeed = 5000f;

        StateText.text = GetComponent<PatrolState>().GetType().Name;

        fireParent = GameObject.Find("FireParent").transform;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        //Get the turret of the tank
        turret = gameObject.transform.GetChild(0).transform;
        bulletSpawnPoint = turret.GetChild(0).transform;

        pond = GameObject.Find("Pond").transform;

        //Get the list of points
        pointList = GameObject.FindGameObjectsWithTag("WandarPoint");

        waypoints = new Transform[pointList.Length];
        int i = 0;
        foreach (GameObject obj in pointList)
        {
            waypoints[i] = obj.transform;
            i++;
        }

        minPosition = new Vector3(pond.position.x - 100, pond.position.y, pond.position.z - 100);
        maxPosition = new Vector3(pond.position.x + 100, pond.position.y, pond.position.z + 100);
    }

    public void Update()
    {
        elapsedTime += Time.deltaTime;
        HealthText.text = health.ToString() + "%";
    }

    /// <summary>
    /// Find the next semi-random patrol point
    /// </summary>
    public void FindNextPoint()
    {
        int rndIndex = UnityEngine.Random.Range(0, waypoints.Length);
        destPos = waypoints[rndIndex].position;
    }

    /// <summary>
    /// Check whether the next random position is the same as current tank position
    /// </summary>
    /// <param name="pos">position to check</param>
    protected bool IsInCurrentRange(Transform trans, Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - trans.position.x);
        float zPos = Mathf.Abs(pos.z - trans.position.z);

        if (xPos <= 50 && zPos <= 50)
            return true;

        return false;
    }

    /// <summary>
    /// Check the collision with the bullet
    /// </summary>
    /// <param name="collision"></param>
    void OnCollisionEnter(Collision collision)
    {
        if (gameObject.GetComponent<RecoveringStateHighLevel>().enabled == true)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Explode();
            }
        } else if (collision.gameObject.CompareTag("Bullet"))
        {
            health -= 25;
        }
    }

    public void Explode()
    {
        float rndX = UnityEngine.Random.Range(10.0f, 30.0f);
        float rndZ = UnityEngine.Random.Range(10.0f, 30.0f);
        for (int i = 0; i < 3; i++)
        {
            GetComponent<Rigidbody>().AddExplosionForce(10000.0f, transform.position - new Vector3(rndX, 10.0f, rndZ), 40.0f, 10.0f);
            GetComponent<Rigidbody>().velocity = transform.TransformDirection(new Vector3(rndX, 20.0f, rndZ));
        }

        Destroy(gameObject, 1.5f);
    }

    /// <summary>
    /// lekker fikken
    /// </summary>
    public void CatchFire()
    {
        GameObject fireParticle = Instantiate(Fire, transform.position, transform.rotation, fireParent);
        fireParticle.GetComponent<Rigidbody>().AddForce(fireParticle.transform.up * fireParticleSpeed);
        Destroy(fireParticle, 1f);
    }

    /// <summary>
    /// Shoot the bullet from the turret
    /// </summary>
    public void ShootBullet()
    {
        if (elapsedTime >= shootRate)
        {
            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            elapsedTime = 0.0f;
        }
    }

    public void StartTimer()
    {
        myTimer = new Timer();
        myTimer.Elapsed += new ElapsedEventHandler(DisplayTimeEvent);
        myTimer.Interval = 1000; // 1000 ms is one second
        myTimer.Start();
    }

    public void DisplayTimeEvent(object source, ElapsedEventArgs e)
    {
        health += healthChange;
    }
}
