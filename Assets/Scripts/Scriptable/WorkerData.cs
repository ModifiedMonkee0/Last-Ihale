using UnityEngine;

[CreateAssetMenu(fileName = "NewWorker", menuName = "Worker")]
public class WorkerData : ScriptableObject
{
    public string workerName;
    public float hiringCost;        // Sat�n al�rken �denmesi gereken para
    public float hourlyWage;        // ���iye saatlik olarak �denmesi gereken para
}
