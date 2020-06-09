using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PirateShipController : MonoBehaviour
{
    public GameObject CannonBallPrefab = null;
    public Transform CannonFrontSpawnPoint = null;
    public Transform CannonLeftSpawnPoint = null;
    public Transform CannonRightSpawnPoint = null;
    public GameObject Lookout = null;
    public GameObject[] sails = null;
    private BaseAI ai = null;
    public Vector3 rotationZMax = new Vector3(0, 0, 10);
    

    // Added some floats etc while trying to fix my script - Ruben
    
    public Rigidbody PirateShipRigidbody;
    public float turn = 100.0f;
    public float searchSpeed = 180.0f;

    public Vector3 chest;

    public GameObject MinePrefab = null;

    public GameObject MedKit; 

    private float BoatSpeed = 100.0f;
    private float SeaSize = 500.0f;
    private float RotationSpeed = 180.0f;
    public float currentBoatSpeed;
    //Added Inaccuracy. -Sjoeke
    float inaccuracy = 15;
    //float randomAngle = Random.Range(-15, 15);
    


    //all values for the different types of ammo. -Martin, Maxym
    private int maxAmmoCap = 5;
    private int weight;
    public int cannonballs;
    public int mines;
    
    //Added a medkit health
    private int medKitAmmount = 20;
    private Coroutine startRepair;
    
    //maxHP & currentHP to create the skeleton of health of ship obeying Ilja's AI. - Aadi.
    private int maxHP;
    [SerializeField]
    private int currentHP;
    public int LowHP;

    //Speed boost variables -Maxym
    public bool speedBoostCooldown;
    float speedBoostValue = 1;

    // Start is called before the first frame update
    void Awake()
    {
        //currentHp is same as maxHP at the game's start. - Aadi.
        maxHP = 100;
        LowHP = 20;
        currentHP = maxHP;
        cannonballs = 2;
        weight = 2;
    }

    private void Update()
    {
        //calculate boat speed based on the amount of ammo the ship carries. -Martin, Maxym
        currentBoatSpeed = (BoatSpeed - 14 * weight) * speedBoostValue;

    }

    public void SetAI(BaseAI _ai) {
        ai = _ai;
        ai.Ship = this;
    }

    public void StartBattle() {
        Debug.Log("test");
        StartCoroutine(ai.RunAI());
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
      
    }

    void OnTriggerStay(Collider other) {
        if (other.tag == "Boat") {
            ScannedRobotEvent scannedRobotEvent = new ScannedRobotEvent();
            scannedRobotEvent.Distance = Vector3.Distance(transform.position, other.transform.position);
            scannedRobotEvent.Name = other.name;
            ai.OnScannedRobot(scannedRobotEvent);
        }
        // new scannedrobotevent so the ship recognizes the chest
        else if (other.tag == "chest")
        {
            ScannedRobotEvent scannedRobotEvent = new ScannedRobotEvent();
            scannedRobotEvent.Distance = Vector3.Distance(transform.position, other.transform.position);
            scannedRobotEvent.Name = other.name;
            ai.OnScannedRobot(scannedRobotEvent);
        }
    }

    

    public IEnumerator __Ahead(float distance) {
        int numFrames = (int)(distance / (currentBoatSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Translate(new Vector3(0f, 0f, currentBoatSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __Back(float distance) {
        int numFrames = (int)(distance / (currentBoatSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Translate(new Vector3(0f, 0f, -currentBoatSpeed * Time.fixedDeltaTime), Space.Self);
            Vector3 clampedPosition = Vector3.Max(Vector3.Min(transform.position, new Vector3(SeaSize, 0, SeaSize)), new Vector3(-SeaSize, 0, -SeaSize));
            transform.position = clampedPosition;

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __TurnLeft(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __TurnRight(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }
    // added so you can turn towards ammo when weight gets below 2
    public IEnumerator __TurnTowards(Vector3 chest)
    {
        // get the direction by subtraction: the difference is what we need
        if (weight < 2)
        {
        var dirToPosition = chest - transform.position;
        float angle = Vector3.SignedAngle(transform.forward, dirToPosition, Vector3.up);
        Debug.Log(angle);
        if(angle < 0) yield return __TurnLeft(-angle); 
        else yield return __TurnRight(angle); 
        }
        
    }

    public IEnumerator __DoNothing() {
        yield return new WaitForFixedUpdate();
    }

    //Added so you cant shoot when you dont have any cannonballs. -Maxym
    //Added Inaccuracy. -Sjoeke
    public IEnumerator __FireFront(float power) {
        if (cannonballs > 0)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, Quaternion.Euler(0, Random.Range(-inaccuracy, inaccuracy), 0) * CannonFrontSpawnPoint.rotation);
            cannonballs -= 1;
            weight -= 1;
        }
        yield return new WaitForFixedUpdate();
    }

    //Added so you cant shoot when you dont have any cannonballs. -Maxym
    //Added Inaccuracy. -Sjoeke
    public IEnumerator __FireLeft(float power) {
        if (cannonballs > 0)
        {      
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonLeftSpawnPoint.position, Quaternion.Euler(0, Random.Range(-inaccuracy, inaccuracy), 0) * CannonLeftSpawnPoint.rotation);
            cannonballs -= 1;
            weight -= 1;
        }
        yield return new WaitForFixedUpdate();
    }

    //Added so you cant shoot when you dont have any cannonballs. -Maxym
    //Added Inaccuracy. -Sjoeke
    public IEnumerator __FireRight(float power) {
        if (cannonballs > 0)
        {
            GameObject newInstance = Instantiate(CannonBallPrefab, CannonRightSpawnPoint.position, Quaternion.Euler(0, Random.Range(-inaccuracy, inaccuracy), 0) * CannonRightSpawnPoint.rotation);
            cannonballs -= 1;
            weight -= 1;
        }
        yield return new WaitForFixedUpdate();
    }

    public IEnumerator __DropMine()
    {
        if (mines > 0)
        {
            GameObject newInstance = Instantiate(MinePrefab, transform.position, transform.rotation);
            mines -= 1;
            weight -= 3;
        }
        yield return new WaitForFixedUpdate();
    }

    //Started working on the repair kit - Martin
    public IEnumerator __RepairKit()
    {
        yield return new WaitForSeconds(3f);
        if (maxHP - medKitAmmount < currentHP)
        {
            currentHP = maxHP;
        }
        else
        {
            currentHP += medKitAmmount;
        }
       Destroy(MedKit);
    }

    //made a speedboost function that is possible to call during movement -Maxym
    public void __SpeedBoost()
    {
        StartCoroutine(__SpeedBoostCoroutine());
    }

    //made a speedboost that gives more speed for a small time and after less speed for more time -Maxym
    public IEnumerator __SpeedBoostCoroutine()
    {
        if (!speedBoostCooldown)
        {
            speedBoostCooldown = true;
            speedBoostValue = 1.5f;
            yield return new WaitForSeconds(1.5f);
            speedBoostValue = 0.5f;
            yield return new WaitForSeconds(4f);
            speedBoostValue = 1;
            speedBoostCooldown = false;
        }
    }

    public void __SetColor(Color color) {
        foreach (GameObject sail in sails) {
            sail.GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public IEnumerator __TurnLookoutLeft(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            Lookout.transform.Rotate(0f, -RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }

    public IEnumerator __TurnLookoutRight(float angle) {
        int numFrames = (int)(angle / (RotationSpeed * Time.fixedDeltaTime));
        for (int f = 0; f < numFrames; f++) {
            Lookout.transform.Rotate(0f, RotationSpeed * Time.fixedDeltaTime, 0f);

            yield return new WaitForFixedUpdate();            
        }
    }
    //Need to figure out how to connect this Ammo integer to to actual gameobjects, also ammo needs to decrease- Ruben

    //Added code based on what type of ammo you pick up, which is set inside the ammunition script on the ammo crates. -Maxym, partly Martin
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(this.tag);
        if (other.GetComponent<Ammunition>())
        {
            if (other.GetComponent<Ammunition>().ammoType == "Cannonball")
            {
                if (weight < maxAmmoCap)
                {
                    weight += 1;
                    cannonballs += 1;
                }
            }
            if (other.GetComponent<Ammunition>().ammoType == "Mine")
            {
                if (weight < maxAmmoCap - 2)
                {
                    weight += 3;
                    mines += 1;
                }
            }


            Destroy(other.gameObject);
        }

        if (other.tag == "MedKit")
        {
           
            MedKit = other.gameObject;
            startRepair = StartCoroutine(__RepairKit());
        }
    }

    //This is to stop the medKit coroutine - Martin
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "MedKit")
        {
            MedKit = null;
            StopCoroutine(startRepair);
        }
    }

    //Added a function to provide health reduction using the "currentHP" and "damage" integer. - Aadi.
    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void RepairShip()
    {
       
    }

    
    //Perk1 - Aadi.
    public void PerkOne()
    {
        if (currentHP < LowHP)
        {
            //AadiAI perk here.
        }
    }
    //Perk2 - Aadi.
    public void PerkTwo()
    {
        if (currentHP < LowHP)
        {
            //Add the code for PerkTwo.
        }
    }
    //Perk3 - Aadi.
    public void PerkThree()
    {
        if (currentHP < LowHP)
        {
            //Add the code for PerkThree.
        }
    }
    //Perk4 - Aadi.
    public void PerkFour()
    {
        if (currentHP < LowHP)
        {
            //Add the code for PerkFour.
        }
    }
    //Perk5 - Aadi.
    public void PerkFive()
    {
        if (currentHP < LowHP)
        {
            //SjoekeAI perk here.
        }
    }

    //Added a function for rapid fire - Martin
    //Added Inaccuracy. -Sjoeke
    public IEnumerator __RapidFire()
    {
        if (cannonballs > 0)
        {
            var currentCannonBalls = cannonballs;
            for (int i = 0; i < currentCannonBalls; i++)
            {
                GameObject newInstance = Instantiate(CannonBallPrefab, CannonFrontSpawnPoint.position, Quaternion.Euler(0, Random.Range(-inaccuracy, inaccuracy), 0) * CannonFrontSpawnPoint.rotation);
                weight--;
                cannonballs--;
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

   /* public IEnumerator _Search(float distance)
    {
        if (ammunition < 1)
        {
        PirateShipRigidbody.velocity = transform.forward * searchSpeed;

        var chestRotation = Quaternion.LookRotation(chest.position = transform.position);

        PirateShipRigidbody.MoveRotation(Quaternion.RotateTowards(transform.rotation, chestRotation));
        }
    }*/
     ///tried adding Ammosearch function, could not get working. Commented out for the time being otherwise game wont run - Ruben
    
}
