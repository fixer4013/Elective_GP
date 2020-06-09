using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMedkit : MonoBehaviour
{
    public GameObject MedKitPrefab;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnMedKit());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator SpawnMedKit()
    {
        while (true)
        {
            yield return new WaitForSeconds(60);
            Instantiate(MedKitPrefab, new Vector3(Random.Range(-400, 400), 0, Random.Range(-400, 400)), Quaternion.identity);

        }
    }
}
