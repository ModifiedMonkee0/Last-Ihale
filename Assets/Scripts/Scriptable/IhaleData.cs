using UnityEngine;

[CreateAssetMenu(fileName = "YeniIhaleData", menuName = "IhaleData", order = 1)]
public class IhaleData : ScriptableObject
{
    public string ihaleAdi;
    public string ihaleKodu;
    public string ihaleSahibi;
    public float ihaleFiyati;
    public string ihaleTarihi;
}
