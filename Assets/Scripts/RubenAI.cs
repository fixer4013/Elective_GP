using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubenAI : BaseAI
{
    public override IEnumerator RunAI() {
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(200);
            yield return FireFront(1);
            yield return TurnLookoutLeft(90);
            yield return TurnLeft(360);
            yield return FireLeft(1);
            yield return TurnLookoutRight(360);
            yield return Back(200);
            yield return FireRight(1);
            yield return TurnLookoutLeft(90);
            yield return TurnRight(90);
            //yield return Search();
        }

    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //commented out the debug. - Aadi
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
