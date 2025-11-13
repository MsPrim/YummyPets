using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetMovement : MonoBehaviour
{
    public float moveSpeed = 2f;   // How fast your pet moves
    private Transform targetFood;  // Where your pet is going

    private PetStats stats;        // So we can change hunger/happiness later

    void Start()
    {
        stats = GetComponent<PetStats>(); // Get reference to your pet's stats
    }

    void Update()
    {
        // If no food is targeted, look for one
        if (targetFood == null)
        {
            PetFood foundFood = FindNearestFood();
            if (foundFood != null)
            {
                targetFood = foundFood.transform;
            }
        }

        // If we have food to go to, move toward it
        if (targetFood != null)
        {
            MoveToFood();
        }
    }

    // --- Finds the closest piece of food in the scene ---
    PetFood FindNearestFood()
    {
        PetFood[] allFood = GameObject.FindObjectsOfType<PetFood>();
        if (allFood.Length == 0) return null;

        PetFood nearest = allFood[0];
        float nearestDist = Vector3.Distance(transform.position, nearest.transform.position);

        foreach (PetFood f in allFood)
        {
            float dist = Vector3.Distance(transform.position, f.transform.position);
            if (dist < nearestDist)
            {
                nearest = f;
                nearestDist = dist;
            }
        }

        return nearest;
    }

    // --- Moves the pet toward the target food ---
    void MoveToFood()
    {
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetFood.position,
            moveSpeed * Time.deltaTime
        );

        // When close enough, "eat" it
        if (Vector3.Distance(transform.position, targetFood.position) < 0.2f)
        {
            EatFood(targetFood.gameObject);
            targetFood = null;
        }
    }

    // --- Pet eats the food and gains stats ---
    void EatFood(GameObject foodObject)
    {
        PetFood food = foodObject.GetComponent<PetFood>();

        // Increase stats
        stats.hunger += food.hungerGain;
        stats.happiness += food.happinessGain;

        // Keep values within range
        stats.hunger = Mathf.Clamp(stats.hunger, 0, stats.maxHunger);
        stats.happiness = Mathf.Clamp(stats.happiness, 0, stats.maxHappiness);

        // Play bounce animation (short)
        StartCoroutine(EatAnimation());

        // Remove food from world
        Destroy(foodObject);
    }

    IEnumerator EatAnimation()
    {
        Vector3 start = transform.localScale;
        Vector3 big = start * 1.2f;

        // Grow a bit
        float t = 0;
        while (t < 0.15f)
        {
            transform.localScale = Vector3.Lerp(start, big, t / 0.15f);
            t += Time.deltaTime;
            yield return null;
        }

        // Shrink back
        t = 0;
        while (t < 0.15f)
        {
            transform.localScale = Vector3.Lerp(big, start, t / 0.15f);
            t += Time.deltaTime;
            yield return null;
        }
    }
}
