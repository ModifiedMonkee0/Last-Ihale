using UnityEngine;

[CreateAssetMenu(fileName = "NewWorker", menuName = "Worker")]
public class WorkerData : ScriptableObject
{
    public string workerName;
    public float hiringCost;        // Satýn alýrken ödenmesi gereken para
    public float hourlyWage;        // Ýþçiye saatlik olarak ödenmesi gereken para
}
