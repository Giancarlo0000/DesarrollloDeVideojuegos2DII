using UnityEngine;

public class Boss : EnemyClass
{
    public float firingFrequency = 2f;

    public Transform shootingPoint;
    public GameObject projectilePrefab;

    private float lastShootTime;

    private void Awake() => scale = transform.localScale;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        lastShootTime = Time.time;
    }

    void Update()
    {
        //Moverse hacia el jugador
        float playerDistance = Vector3.Distance(transform.position, player.position);
        if (playerDistance < followRange)
        {
            Vector3 direction = player.position - transform.position;
            direction.Normalize();
            transform.Translate(direction * speed * Time.deltaTime);
            if (Vector3.Dot(direction, transform.right) < 0)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            else
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
            //Disparar cada cierto tiempo
            if (Time.time - lastShootTime > firingFrequency)
            {
                Shoot();
                lastShootTime = Time.time;
            }
        }
    }
    //Generar proyectil
    void Shoot() => Instantiate(projectilePrefab, shootingPoint.position, shootingPoint.rotation);
}
