using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject squarePrefab;
    public int maxCustomers = 3;
    private Queue<GameObject> customerQueue = new Queue<GameObject>();

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && customerQueue.Count < maxCustomers)
        {
            GameObject newCustomer = Instantiate(squarePrefab, transform.position, Quaternion.identity);
            customerQueue.Enqueue(newCustomer);
            AdjustCustomerPositions();
        }
    }

    private void AdjustCustomerPositions()
    {
        int index = 1;
        foreach (GameObject customer in customerQueue)
        {
            customer.GetComponent<PathFollow2>().SetInitialPosition(index);
            index++;
        }
    }
}