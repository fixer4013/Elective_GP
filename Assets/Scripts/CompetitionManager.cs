using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionManager : MonoBehaviour
{ 
    public GameObject PirateShipPrefab = null;
    public Transform[] SpawnPoints = null;
    //Added a ammospawnpoint - Ruben
    public GameObject AmmunitionPrefab = null;
    //public Transform[] AmmoSpawn = null; //commented it -Maxym
    public bool gameStarted; //made this in order to make the game only start 1 time -Maxym

    private List<PirateShipController> pirateShips = new List<PirateShipController>();

    BaseAI[] aiArray = new BaseAI[]
    {
        //Added 'AadiAI' to the list -Aadi.
            new AadiAI(),
        //Added 'RubenAI' to the list -Ruben
            new RubenAI(),
        //Added 'MaxymAI' to the list -Maxym
            new MaxymAI(),
            new MartinAI(), 
        //Added a "new iljaAI" to the list. - Aadi.
            new SjoekeAI()
    };

    // Start is called before the first frame update
    void Start()
    {
        //moved the aiArray list command to outside. - Aadi.
        //changed the integer value of greater than i from 4 to 5. - Aadi.
        //Increased the size for Spawn Points section in Inspector Window. - Aadi.
        //Cloned the spawnpoint5 from spawnpoint4 and dragndropped it to Element 4. - Aadi.
        for (int i = 0; i < 5; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            PirateShipController pirateShipController = pirateShip.GetComponent<PirateShipController>();
            pirateShipController.SetAI(aiArray[i]);
            pirateShips.Add(pirateShipController);
            //if (i == 0)
            //{
            //    pirateShip.AddComponent<PirateShipController>().PerkOne();
            //}
            //if (i == 1)
            //{
            //    pirateShip.AddComponent<PirateShipController>().PerkTwo();
            //}
            //if (i == 2)
            //{
            //    pirateShip.AddComponent<PirateShipController>().PerkThree();
            //}
            //if (i == 3)
            //{
            //    pirateShip.AddComponent<PirateShipController>().PerkFour();
            //}
            //if (i == 4)
            //{
            //    pirateShip.AddComponent<PirateShipController>().PerkFive();
            //}
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        //made it so you can only start the game once -Maxym
        if (Input.GetKeyDown(KeyCode.Space) && !gameStarted) {
            foreach (var pirateShip in pirateShips) {
                pirateShip.StartBattle();
                // For now ammo is instantiated when the game starts -Ruben
                //Changed the coordinate from (-291, 30, 133) to (-278, 30, -275). - Aadi.
                //Instantiate(AmmunitionPrefab, new Vector3(-278, 30, -275), Quaternion.identity);
            }
            gameStarted = true;
            StartCoroutine(AmmoSpawning());
        }
        //    // Made it so that 5 pieces of amma spawn when pressing the 'TAB' button instead of on start
        //if (Input.GetKeyDown(KeyCode.Tab)) {
        //    Instantiate(AmmunitionPrefab, new Vector3(-278, 30, 275), Quaternion.identity); //commented it -Maxym
        //    Instantiate(AmmunitionPrefab, new Vector3(303, 30, 285), Quaternion.identity);
        //    Instantiate(AmmunitionPrefab, new Vector3(-324, 30, -181), Quaternion.identity);
        //    Instantiate(AmmunitionPrefab, new Vector3(125, 30, -93), Quaternion.identity);
        //    Instantiate(AmmunitionPrefab, new Vector3(355, 30, -296), Quaternion.identity);

        //}

    }

    //made it so that the ammospawning happens the whole time the game plays -Maxym
    IEnumerator AmmoSpawning()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Instantiate(AmmunitionPrefab, new Vector3(Random.Range(-400, 400), 5, Random.Range(-400, 400)), Quaternion.identity);
        }
    }
}
