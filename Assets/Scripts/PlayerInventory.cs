using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public List<GameObject> inventory;
    [SerializeField] private GameObject blackHole;
    [SerializeField] private GameObject slotPrefab; // used to show the item in the UI Bar
    [SerializeField] private Transform UIBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1))
        {
            if(inventory.Count > 0)
            {
                // "respawn" item
                inventory[0].SetActive(true);
                inventory[0].transform.position = blackHole.transform.GetChild(0).position;
                inventory[0].GetComponent<Attractable>().Shoot(blackHole);

                // remove from inventory
                inventory.RemoveAt(0);
                Destroy(UIBar.GetChild(0).gameObject);
            }           
        }
    }

    public void AddToInventory(GameObject obj)
    {
        inventory.Add(obj);
        GameObject newItem = Instantiate(slotPrefab, UIBar);

        // TODO: Code to get image of object
    }
}