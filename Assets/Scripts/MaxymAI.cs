using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoClass : MonoBehaviour
{
    public float test = 10;
}

public class MaxymAI : BaseAI
{
    string mode = null;
    
    public override IEnumerator RunAI() {
        yield return TurnLeft(45);
        yield return FireFront(1);
        yield return FireFront(1);
        yield return Ahead(400);
        while (true)
        {
            if (mode == "search")
            {
                yield return Ahead(25);
                yield return TurnRight(5);
                yield return TurnLookoutLeft(5);
            }
            if (mode == "hunt")
            {

            }
            yield return 0;
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //commented out the debug. - Aadi
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}


