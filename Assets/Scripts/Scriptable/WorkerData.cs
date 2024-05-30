using UnityEngine;

[CreateAssetMenu(fileName = "NewWorker", menuName = "Worker")]
public class WorkerData : ScriptableObject
{
    public string workerName;
    public float hiringCost;        // Sat�n al�rken �denmesi gereken para
    public float hourlyWage;        // ���iye saatlik olarak �denmesi gereken para
    public float workerScore;
    public float sat��Fiyat�;       //���iyi i�ten c�kart�rken? Neden para kazanal�m ki 
}

[CreateAssetMenu(fileName = "GoodEngineer", menuName = "Workers/GoodEngineer")]
public class GoodEngineer : WorkerData
{
    public int count;
}



[CreateAssetMenu(fileName = "BadEngineer", menuName = "Workers/BadEngineer")]
public class BadEngineer : WorkerData
{
    public int count;
}

[CreateAssetMenu(fileName = "GoodWorker", menuName = "Workers/GoodWorker")]
public class GoodWorker : WorkerData
{
    public int count;
}

[CreateAssetMenu(fileName = "BadWorker", menuName = "Workers/BadWorker")]
public class BadWorker : WorkerData
{
    public int count;
}