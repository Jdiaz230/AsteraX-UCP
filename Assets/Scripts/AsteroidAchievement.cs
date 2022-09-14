using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

[RequireComponent(typeof(Asteroid))]
public class AsteroidAchievement : MonoBehaviour
{
   
    AchievementManager achievementManager;
    Asteroid asteroid;
    bool collided = false;
    float timer = 0;
    void Start()
    {
        achievementManager = FindObjectOfType<AchievementManager>();
        asteroid = GetComponent<Asteroid>();
        
    }
    void Update()
    {
        EnablePlayerIfNot();
    }

    private void EnablePlayerIfNot()
    {
        var player = PlayerShip.S.gameObject;
        if (!player.activeInHierarchy)
        {

            timer += Time.deltaTime;
            if (timer>2)
            {
                player.SetActive(true);
            }

        }
        else
        {
            timer = 0;
        }
    }

    public void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "Player"  && !collided)
        {
            achievementManager.TakeJump();
            Jump(coll.gameObject);
            //coll.gameObject.SetActive(true);
            collided = true;
            
        }
        if (coll.gameObject.tag == "Bullet")
        {
            if (!asteroid.immune)
            {
                if (asteroid.size == 1)
                {
                    achievementManager.AddPoints(50);
                }
                return;
            }
            int pointCount = 0;
            switch (asteroid.size)
            {
                case 1:
                    pointCount = 50;
                    break;
                case 2:
                    pointCount = 30;
                    
                    break;
                case 3:
                    pointCount = 10;
                    
                    break;
            }
            achievementManager.AddPoints(pointCount);
        }
    }

    void Jump(GameObject player)
    {
        player.SetActive(false);
        
        Vector3 pos;
        do
        {
            pos = ScreenBounds.RANDOM_ON_SCREEN_LOC;
            var asteroids = FindObjectsOfType<Asteroid>();
            foreach (var item in asteroids)
            {
                if((item.gameObject.transform.position - PlayerShip.POSITION).magnitude < AsteraX.MIN_ASTEROID_DIST_FROM_PLAYER_SHIP){
                    break;
                }
            }
        } while (false);
        
        player.transform.position = pos;  
       
        
    }
    
}
