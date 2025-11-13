using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetFood : MonoBehaviour
{
    public int hungerGain = 20;
    public int happinessGain = 5;

    // Optional: Add a friendly label in the inspector
    [TextArea(1, 2)]
    public string description = "Food item: increases pet stats when eaten.";
}

