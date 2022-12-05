using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] float speed;
    [SerializeField] float stopDis;
    [SerializeField] float backDis;

    [SerializeField] Transform player;
    [SerializeField] GameObject projectile;

    [SerializeField] float timeShoot;
    [SerializeField] float startShoot;

    [SerializeField] float agroRange;
    [SerializeField] float ShootRange;

    [SerializeField] int health = 100;

    protected override void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        timeShoot = startShoot;
    }

    protected override void UpdateEntity()
    {
        if (player != null)
        {
            float disToPlayer = Vector2.Distance(transform.position, player.position);

            //If player is not withing range it will not seek out the player

            if (disToPlayer < agroRange)
            {
                EnemyMove();
            }
            else
            {
                StopMove();
            }
        }
        
    }

    //Will try to find player position and seek player

    private void EnemyMove()
    {
        if (Vector2.Distance(transform.position, player.position) > stopDis)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, player.position) < stopDis && Vector2.Distance(transform.position, player.position) > backDis)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < backDis)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }


        //This is a move stop script when the enemy is at a certain distance the enemy will stop at its shooting range

        //float RangeToPlayer = Vector2.Distance(transform.position, player.position);
        //if(player != null)
        //{
        //    if (RangeToPlayer < ShootRange)
        //    {
        //        if (timeShoot <= 0)
        //        {
        //            Instantiate(projectile, transform.position, Quaternion.identity);
        //            timeShoot = startShoot;
        //        }
        //        else
        //        {
        //            timeShoot -= Time.deltaTime;
        //        }
        //    }
        //}
                 
    }

    void StopMove()
    {        
            transform.position = this.transform.position;       
    }


    //Taking Damage: Can be called from another script and may be used in different weapons when projectile hits the enemy

    public void TakeDamage (int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }


}
