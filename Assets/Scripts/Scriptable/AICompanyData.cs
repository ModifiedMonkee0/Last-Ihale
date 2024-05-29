using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "AICompanyData", menuName = "AI/AICompanyData")]
public class AICompanyData : ScriptableObject
{
    public string companyName;
    public float totalMoney;
    public int totalWorkers;
    public List<IhaleData> purchasedIhaleler;
    public int difficultyLevel; // 1: Kolay, 4: Zor
}
