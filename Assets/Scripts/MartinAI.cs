using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinAI : BaseAI
{
    private string mode = "patrol";
     
   
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
                    yield return TurnLookoutRight(180);
                    yield return TurnLeft(Random.Range(30, 180));
                    yield return Ahead(Random.Range(10, 500));
                    yield return TurnLookoutLeft(180);
                    yield return TurnRight(Random.Range(30, 180));
                    
                    
                    if (Ship.currentHP <= 40)
                    {
                        mode = "flee";
                    }
                    break;    
                    
                case "hunt":
                    yield return 0;
                    break;
                
                case "flee":
                    yield return RapidFire();
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
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        if (e.Name == "boat")
        {
            mode = "hunt";
        }
    }


    

}