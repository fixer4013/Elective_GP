using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinAI : BaseAI
{
    private string mode = "patrol";
    
   
    // Start is called before the first frame update
    public override IEnumerator RunAI()
    {

        yield return Ahead(500);
        yield return TurnLookoutLeft(180);
        yield return TurnLeft(90);
        yield return TurnLookoutLeft(180);
        yield return Ahead(500);

        while (true)
        {
            // 
            switch (mode)
            { 
                case "patrol":
                    yield return TurnLookoutRight(180);
                    yield return TurnRight(30);
                    yield return Ahead(500);
                    if (Ship.cannonballs == 3)
                    {
                        mode = "hunt";
                    }
                    break;    
                    
                case "hunt":
                    yield return 0;
                    break;
                
                case "flee":
                    yield return TurnLookoutRight(5);
                    yield return TurnRight(2);
                    yield return Ahead(100);
                    
                    break;
            }

            yield return 0;

        }

    }

    // Update is called once per frame
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
    
    
}