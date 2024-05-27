using UnityEngine;

[CreateAssetMenu(fileName = "YeniIhaleData", menuName = "IhaleData", order = 1)]
public class IhaleData : ScriptableObject
{
    public string ihaleAdi;
    public float ihaleFiyati;
    public int gerekliIsciler;
    public int gerekliMuhendisler;
    public int ekucretler;

}
