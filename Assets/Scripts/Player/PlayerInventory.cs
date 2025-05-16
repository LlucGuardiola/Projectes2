using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static List<int> PlayerCards { get; private set; }

    public static bool PlayerHasSword = false;

    void Start()
    {
        PlayerCards = new List<int>();
    }

    public static void AddCard(int index)
    {
        PlayerCards.Add(index);
    }
}
