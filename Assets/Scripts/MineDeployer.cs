using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDeployer : MonoBehaviour
{
    public float numberOfMines;
    
    public virtual void LayMine(GameObject prefab, Vector3 pos)
    {
        // Make sure we don't run out of mines
        if (numberOfMines > 0)
        {
            // Instantiate the Mine
            GameObject newMine = Instantiate(prefab, pos, Quaternion.identity);

            numberOfMines--;
        }
    }
}
