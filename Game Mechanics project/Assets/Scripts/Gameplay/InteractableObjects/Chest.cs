using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : InteractableObject
{
    // TODO: Spawn treasure, items
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private GameObject keyPrefab;
    [SerializeField] private float yOffset = 1;
    private int randomAmount;

    private void Start()
    {
        randomAmount = Random.Range(2, 5);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        SpawnChestLoot();
    }

    public void SpawnChestLoot()
    {
        Vector2 trajectory = Random.insideUnitCircle * 200f;
        for (int i = 0; i < randomAmount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100f, 100f) + trajectory.x, Random.Range(50f, 100f) + trajectory.y));
            Debug.Log("Coins spawned: " + randomAmount);
        }
        Instantiate(keyPrefab, new Vector2(transform.position.x, transform.position.y + yOffset), Quaternion.identity);
    }
}
