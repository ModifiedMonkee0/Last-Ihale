using UnityEngine;

[CreateAssetMenu(fileName = "NewWorker", menuName = "Worker")]
public class WorkerData : ScriptableObject
{
    public string workerName;
    public float hiringCost;        // Satýn alýrken ödenmesi gereken para
    public float hourlyWage;        // Ýþçiye saatlik olarak ödenmesi gereken para
    public float workerScore;
    public float satýþFiyatý;       //Ýþçiyi iþten cýkartýrken? Neden para kazanalým ki 
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