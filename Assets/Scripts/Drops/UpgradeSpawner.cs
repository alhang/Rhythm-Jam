using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawner : Singleton<UpgradeSpawner>
{
    public GameObject upgrade;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnUpgrade(Vector3 pos)
    {
        Instantiate(upgrade, pos, transform.rotation, transform);
    }
}
