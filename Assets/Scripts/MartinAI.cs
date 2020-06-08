using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MartinAI : BaseAI
{
    // Start is called before the first frame update
    public override IEnumerator RunAI()
    {
        for (int i = 0; i < 10; i++)
        {
            yield return Ahead(500);
            yield return FireFront(1);
            yield return TurnLookoutLeft(90);
            yield return TurnLeft(360);
            yield return FireLeft(1);
            yield return TurnLookoutRight(360);
            yield return Back(200);
            yield return FireRight(1);
            yield return TurnLookoutLeft(90);
            yield return TurnRight(90);
        }
    }

    // Update is called once per frame
    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}