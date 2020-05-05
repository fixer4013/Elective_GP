using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    //A new integer is used to replace the parameter of TakeDamage(int damage) method in PirateShipController. - Aadi.
    public int newDamage = 10;
    // Start is called before the first frame update
    void Start()
    {
    }
    //to identify who has been hit by cannonball. - Aadi.
    //Uses the OnTriggerEnter method to check if the "other" gameobject has a PirateShipController, if yes than gets the PirateShipController component of that gameobject and gives the "newDamage" integer as the new parameter for the TakeDamage(int damage) method in the PirateShipController and finally destroys the gameobject which is using the 'CannonBall'script. - Aadi.
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<PirateShipController>())
        {
            PirateShipController pirateShipController = other.gameObject.GetComponent<PirateShipController>();
            //Debug.Log("Been hit by Cannonball");
            pirateShipController.TakeDamage(newDamage);
            Destroy(this.gameObject);
        }
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(new Vector3(0f, 0f, 500 * Time.fixedDeltaTime), Space.Self);
    }
}
