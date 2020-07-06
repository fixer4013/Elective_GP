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
        mode = "search";
        //yield return TurnLeft(45);
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
                //Debug.Log(Ship.currentPosition);
                //Debug.Log(Ship.currentLookOutRotation);
            }
            if (mode == "hunt")
            {

            }
            yield return 0;
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log(e.Health + " " + e.Speed + " " + e.Position + " " + e.Rotation);
    }
}


