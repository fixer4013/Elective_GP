using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AadiAI : BaseAI
{
    
    private string mode = null;
    public override IEnumerator RunAI()
    {
        mode = "circle";
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(10);
            yield return TurnLeft(5);
            yield return Ahead(50);
            yield return FireFront(1);
            yield return TurnLookoutRight(45);
            yield return TurnLeft(10);
            yield return FireRight(1);
            yield return Ahead(100);
            yield return TurnLookoutLeft(90);
            yield return FireFront(1);
            yield return FireLeft(1);
            yield return FireRight(1);
            if (mode == "circle")
            {
                yield return TurnRight(45);
                yield return Ahead(20);

            }
            yield return Back(10);
            yield return TurnRight(5);
            yield return Ahead(50);
            yield return TurnLookoutLeft(5);
            yield return Ahead(50);
            yield return TurnRight(5);
            yield return Back(10);
            yield return TurnLookoutLeft(5);
            yield return Ahead(50);
            yield return Ahead(50);
            yield return TurnRight(5);
            yield return Back(10);
            yield return Ahead(50);
            yield return Ahead(50);
            yield return TurnRight(5);
            yield return Back(10);
            yield return TurnLookoutLeft(5);
            yield return Ahead(50);
            yield return TurnLookoutLeft(5);
            yield return TurnRight(5);
            yield return RapidFire();
            yield return Back(10);
            yield return Ahead(50);
            yield return Ahead(50);
            yield return TurnRight(5);
            yield return Back(10);
            yield return TurnLookoutLeft(5);
            yield return Ahead(50);
            yield return TurnLookoutLeft(5);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //commented out the debug. - Aadi
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
        if (e.Name == "boat")
        {
            FireFront(1);
            FireLeft(1);
        }
    }
}
