using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SjoekeAI : BaseAI
{
    public override IEnumerator RunAI()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(50);
            yield return RapidFire();
            yield return FireFront(1);
            yield return TurnLookoutLeft(45);
            yield return TurnLeft(360);
            yield return FireLeft(1);
            yield return TurnLookoutRight(150);
            yield return Back(300);
            yield return FireRight(1);
            yield return TurnLookoutLeft(90);
            yield return TurnRight(90);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //commented out the debug. - Aadi
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
