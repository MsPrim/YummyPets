using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PetStats : MonoBehaviour
{
    // Maximum values for each stat
    public int maxHunger = 100;
    public int maxHappiness = 100;
    public int maxEnergy = 100;

    // Current values (will change during gameplay)
    public int hunger;
    public int happiness;
    public int energy;

    // Time between stat decreases (in seconds)
    public float decayInterval = 5f;
    private float timer = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Make the stats full when the game starts 
        hunger = maxHunger;
        happiness = maxHappiness;
        energy = maxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        // Count time
        timer += Time.deltaTime;

        // If enough time has passed, decrease the stats
        if (timer >= decayInterval)
        {
            // Decrease each stat
            hunger -= 1;
            happiness -= 1;
            energy -= 1;

            //Don't go below 0
            hunger = Mathf.Clamp(hunger, 0, maxHunger);
            happiness = Mathf.Clamp(happiness, 0, maxHappiness);
            energy = Mathf.Clamp(energy, 0, maxEnergy);

            // Reset timer
            timer = 0f;

            // Debug shows the results in the Console
            Debug.Log("Stats decreased! Hunger: " + hunger + ", Happiness: " + happiness + ", Energy: " + energy);
        }
    }
}
