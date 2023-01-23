using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : InteractableObject
{
    // TODO: Spawn coins, items. Chance to spawn nothing.
    [SerializeField] private GameObject coinPrefab;
    private int randomAmount;

    private void Start()
    {
        randomAmount = Random.Range(0, 3);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        SpawnLoot();
    }

    public void SpawnLoot()
    {
        Vector2 trajectory = Random.insideUnitCircle * 200f;
        for (int i = 0; i < randomAmount; i++)
        {
            GameObject coin = Instantiate(coinPrefab, transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100f, 100f) + trajectory.x, Random.Range(50f, 100f) + trajectory.y));
        }
    }
}
