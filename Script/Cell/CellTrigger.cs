using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellTrigger : MonoBehaviour
{

    [SerializeField, Header("철장")]
    private List<GameObject> cells;

    [Space(10)]

    [SerializeField]
    private GameObject explosion;
    

    public void TriggerCell()
    {
        
        if(cells.Count != 0)
        {
            Destroy(Instantiate(explosion, cells[0].transform.position, cells[0].transform.rotation), 1f);
            Destroy(cells[0]);
            cells.RemoveAt(0);
        }

    }

}
