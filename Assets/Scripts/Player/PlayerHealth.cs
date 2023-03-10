using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] int maxHealth;
    private int currentHealth;

    private void Start()
    {
        currentHealth = maxHealth;
    }
}
