using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDeployer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void LayMine(GameObject prefab, Vector3 pos)
    {
        // Instantiate the Mine
        GameObject newMine = Instantiate(prefab, pos, Quaternion.identity);
    }
}
