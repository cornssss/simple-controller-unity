
using UnityEngine;

public class PlayerStatistics : MonoBehaviour
{
    private float maxHealth;
    private float maxMana;
    private float maxFood;
    private float health;
    private float mana;
    private float food;

    public float Health { get => health; set => health = Mathf.Clamp(value, 0f, maxHealth); }
    public float Mana { get => mana; set => mana = Mathf.Clamp(value, 0f, maxMana); }
    public float Food { get => food; set => food = Mathf.Clamp(value, 0f, maxFood); }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }
    public float MaxMana { get => maxMana; set => maxMana = value; }
    public float MaxFood { get => maxFood; set => maxFood = value; }

    private void Start() {
        maxHealth = 100;
        maxFood = 100;
        maxMana = 100;
    }
}
