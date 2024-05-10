using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;

    private Transform player;
    private Vector3 direction;

    void Start()
    {
        //Buscar ubicaci�n del jugador
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player != null)
        {
            direction = (player.position - transform.position).normalized;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        //Moverse a la ubicaci�n obtenida del jugador
        transform.Translate(direction * speed * Time.deltaTime);
        //Destruir el proyectil si est� muy lejos del jugador
        if (Vector3.Distance(transform.position, player.position) > 20f)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.gameObject.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
