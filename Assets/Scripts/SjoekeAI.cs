using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjoekeAI : BaseAI
{
    private string mode;
    private bool kitInSight;
    private Vector3 kitPosition;
    private float kitRotation;
    private Vector3 enemyBoatPosition;
    private float enemyBoatRotation;
    //private bool boatInSight;

    public override IEnumerator RunAI()
    {
        while (true)
        {
            switch (mode)
            {
                case "sailing":
                    if (mode != "gettingKit" && mode != "shooting")
                    {
                        yield return TurnLeft(180);
                        yield return Ahead(300);
                        yield return TurnRight(75);
                        yield return Ahead(200);
                        yield return TurnRight(180);
                        yield return Ahead(200);
                        yield return TurnLeft(75);
                        yield return Ahead(300);
                        yield return TurnRight(75);
                        yield return Ahead(200);
                        yield return TurnRight(180);
                        yield return Ahead(200);
                        yield return TurnLeft(75);
                    }
                    
                    break;

                case "gettingKit":
                    Debug.Log("Getting kit");

                    yield return Ahead(300);

                    mode = "sailing";

                    break;

                case "shooting":
                    Debug.Log("Shooting");
                    yield return FireFront(5);

                    mode = "sailing";

                    break;
            }

        }
        // I am going to move in Z's all the time, when i see a ship i will fire
        // If my ship sees a kit, it will grab it. then do Z's there
        
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        if (e.Name == "chest(Clone)")
        {
            Debug.Log("I see the chest");
           
            mode = "gettingKit";
        }

        if (e.Name == "Aadi" || e.Name == "Martin" || e.Name == "Maxym" || e.Name == "Ruben")
        {
            mode = "shooting";
        }
        //commented out the debug. - Aadi
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
