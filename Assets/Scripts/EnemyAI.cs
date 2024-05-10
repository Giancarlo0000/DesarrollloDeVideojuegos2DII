using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : EnemyClass
{
    public Transform[] patrolPoints; // Puntos entre los cuales el enemigo patrullar�

    private int currentPatrolIndex = 0; // �ndice del punto de patrulla actual
    private bool isFollowingPlayer = false; // Indica si el enemigo est� siguiendo al jugador


    void Update()
    {
        if (!isFollowingPlayer)
        {
            Patrol(); // Si no sigue al jugador, realiza la patrulla
        }
        else
        {
            FollowPlayer(); // Si sigue al jugador, lo persigue
        }

        CheckDistance();
    }

    void Patrol()
    {
        // Mueve hacia el punto de patrulla actual
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);
        // Si llega al punto de patrulla, avanza al siguiente
        if (Vector2.Distance(transform.position, patrolPoints[currentPatrolIndex].position) < 0.1f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
        if (currentPatrolIndex == 0)
        {
            transform.localScale = new Vector3(scale.x, scale.y, scale.z);
        }
        if (currentPatrolIndex == 1)
        {
            transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
        }
    }

    void FollowPlayer()
    {
        // Si el jugador est� dentro del rango de seguimiento, se mueve hacia �l
        if (Vector2.Distance(transform.position, player.position) <= followRange)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else
        {
            isFollowingPlayer = false; // Si el jugador est� fuera del rango, deja de seguirlo
        }
        
        float distanciaDeJugador = Vector3.Distance(transform.position, player.position);

        if (distanciaDeJugador < followRange)
        {
            Vector3 direccion = player.position - transform.position;
            if (Vector3.Dot(direccion, transform.right) < 0)
            {
                transform.localScale = new Vector3(scale.x, scale.y, scale.z);
            }
            else
            {
                transform.localScale = new Vector3(-scale.x, scale.y, scale.z);
            }
        }
    }

    void CheckDistance()
    {
        if (Vector2.Distance(transform.position, player.position) <= followRange)
        {
            isFollowingPlayer = true;
        }
    }
}