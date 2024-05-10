using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyClass : MonoBehaviour
{
    //Herencia - Clase enemigo que se usará para los enemigos normales y el jefe 
    [SerializeField] protected float speed = 2;
    [SerializeField] protected int damage = 1;
    [SerializeField] protected float followRange = 2;

    protected Transform player;
    protected Vector3 scale;

    private void Awake()
    {
        scale = transform.localScale;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player myPlayer = collision.gameObject.GetComponent<Player>(); //Acceder a propiedades del jugador
            myPlayer.Lives = myPlayer.Lives - damage;
            float x = myPlayer.Lives;
            if (x <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            Destroy(gameObject);
        }
    }
}
