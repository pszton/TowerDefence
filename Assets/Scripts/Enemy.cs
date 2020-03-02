using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 10f;
    public int enemyHealth = 100;
    public int value = 50;

    public GameObject deathEffect;

    private Transform target;
    private int waypointIndex = 0;

    private void Start ()
    {
        target = Waypoints.points[0];
    }

    private void Update ()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            GetNextWaypoint();
        }
    }

    public void TakeDamage(int amount)
    {
        enemyHealth -= amount;
        if(enemyHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        PlayerStats.money += value;
        GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }

    private void GetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.points.Length - 1)
        {
            EndPath();
            return;
        }
        waypointIndex++;
        target = Waypoints.points[waypointIndex];
    }

    private void EndPath()
    {
        PlayerStats.lives--;
        Destroy(gameObject);
    }
}
