using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnOnClick : MonoBehaviour
{
    public GameObject objectToSpawn; 
    public LayerMask floorLayer;
    public LayerMask destroyLayer; 

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, floorLayer))
            {
                Instantiate(objectToSpawn, hit.point, Quaternion.identity);
            }
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, destroyLayer))
            {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("SpawnedByPlayer"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
