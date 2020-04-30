using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompetitionManager : MonoBehaviour
{
    public GameObject PirateShipPrefab = null;
    public Transform[] SpawnPoints = null;

    private List<PirateShipController> pirateShips = new List<PirateShipController>();

    // Start is called before the first frame update
    void Start()
    {
        BaseAI[] aiArray = new BaseAI[] {
            new IljaAI(),
            new PondAI(),
            new PondAI(),
            new PondAI(),
        //Added a "new iljaAI" to the list. - Aadi.
            new IljaAI()
        };
        //changed the integer value of greater than i from 4 to 5. - Aadi.
        //Increased the size for Spawn Points section in Inspector Window. - Aadi.
        //Cloned the spawnpoint5 from spawnpoint4 and dragndropped it to Element 4. - Aadi.
        for (int i = 0; i < 5; i++)
        {
            GameObject pirateShip = Instantiate(PirateShipPrefab, SpawnPoints[i].position, SpawnPoints[i].rotation);
            PirateShipController pirateShipController = pirateShip.GetComponent<PirateShipController>();
            pirateShipController.SetAI(aiArray[i]);
            pirateShips.Add(pirateShipController);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            foreach (var pirateShip in pirateShips) {
                pirateShip.StartBattle();
            }
        }
    }
}
