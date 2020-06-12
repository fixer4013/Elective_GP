using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class MartinAI : BaseAI
{
    private string mode = "patrol";

    private Vector3 enemyPosition;

    private float enemyRotation; 
    
   
    // Start is called before the first frame update
    public override IEnumerator RunAI()
    {

        yield return Ahead(Random.Range(100, 300));
        
        while (true)
        {
            //During patrol mode I want the ship to go to specific coordinates through a pattern it could be a square for example
            switch (mode)
            { 
                case "patrol":
                    if (mode != "hunt")
                    {
                        yield return TurnLeft(Random.Range(30, 180));
                        yield return Ahead(Random.Range(10, 800));
                        yield return TurnRight(Random.Range(30, 180));
                        yield return Ahead(Random.Range(200, 300));
                        yield return TurnRight(Random.Range(30, 90));
                        yield return TurnLookoutLeft(Random.Range(30, 360));
             
                    }
                    if (Ship.currentHP <= 40)
                    {
                        mode = "flee";
                    }
                    break;    
                    
                
                //I want to get the angle between the two vectors of the positions of my ship and an enemy ship
                //Based on that information I want my ship to rotate sideways of the enemy ship and shoot a cannonball 
                
                case "hunt":
                    
                    yield return FireFront(1);
                    yield return FireLeft(1);
                    yield return FireRight(1);

                    mode = "patrol";

                    /*Vector3 targetDir = enemyPosition - Ship.currentPosition;
                    float angle = Vector3.Angle(targetDir, Vector3.forward);
                    
                    Debug.Log(angle);
                    if (angle < 8)
                    {
                        yield return FireFront(1);
                    }

                    if (angle > 8)
                    {
                        
                    }
                    */
                    
                    break;
                
                case "flee":
                    yield return TurnLeft(180);
                    yield return TurnLookoutLeft(180);
                    yield return Ahead(500);

                    mode = "patrol";
                    
                    break;
            }

            yield return 0;

        }

    }

    // Update is called once per frame
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (e.Name == "Maxym")
        {
            e.Position = enemyPosition;
            e.Rotation = enemyRotation;
            if (Ship.currentHP > 40)
            {
                mode = "hunt";
                
            }
            else 
            {
                mode = "patrol";
            }
        }
        
        if (e.Name == "Ruben")
        {
            e.Position = enemyPosition;
            e.Rotation = enemyRotation;
            if (Ship.currentHP > 40)
            {
                mode = "hunt";
            }
            else
            {
                mode = "patrol";
            }
        }

        if (e.Name == "Aadi")
        {
            e.Position = enemyPosition;
            e.Rotation = enemyRotation;
            if (Ship.currentHP > 40)
            {
                mode = "hunt";
            }
            else
            {
                mode = "patrol";
            }
        }

        if (e.Name == "Sjoeke")
        {
            e.Position = enemyPosition;
            e.Rotation = enemyRotation;
            if (Ship.currentHP > 40)
            {
                mode = "hunt";
            }
            else
            {
                mode = "patrol";
            }
        }

    }

}