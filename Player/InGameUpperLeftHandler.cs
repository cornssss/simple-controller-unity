
using UnityEngine;

public class InGameUpperLeftHandler : MonoBehaviour
{
    [SerializeField] private InGameUpperLeftView inGameUpperLeftView;
    [SerializeField] private PlayerStatistics playerStatistics;

    public void ChangeHealthByValue(float value)
    {
        if (playerStatistics.Health >= playerStatistics.MaxHealth) return;

        playerStatistics.Health += value; 
        inGameUpperLeftView.OnHealthRectChanged.Invoke(playerStatistics.Health / playerStatistics.MaxHealth); 
    }
    public void ChangeManaByValue(float value)
    {
        if (playerStatistics.Mana >= playerStatistics.MaxMana) return;

        playerStatistics.Mana += value;
        inGameUpperLeftView.OnManaRectChanged.Invoke(playerStatistics.Mana / playerStatistics.MaxMana); 
    }
    public void ChangeFoodByValue(float value)
    {
        if (playerStatistics.Food >= playerStatistics.MaxFood) return;

        playerStatistics.Food += value;
        inGameUpperLeftView.OnFoodRectChanged.Invoke(playerStatistics.Food / playerStatistics.MaxFood); 
    }
}
