using UnityEngine;
using TMPro;
public class IhaleUIManager : MonoBehaviour
{
    public IhaleData[] ihaleDataArray;  // ScriptableObject referanslarý

    // 50 adet ihale isim ve fiyat metin referanslarý
    public TMP_Text[] ihaleIsimArray = new TMP_Text[50];
    public TMP_Text[] ihaleFiyatArray = new TMP_Text[50];

    void Start()
    {
        for (int i = 0; i < ihaleDataArray.Length && i < 50; i++)
        {
            if (ihaleDataArray[i] != null)
            {
                ihaleIsimArray[i].text = ihaleDataArray[i].ihaleAdi;
                ihaleFiyatArray[i].text = ihaleDataArray[i].ihaleFiyati.ToString("C2");
            }
        }
    }
}
/*
public class IhaleUIManager : MonoBehaviour
{
    public IhaleData ihaleData1;  // ScriptableObject referansý
    public IhaleData ihaleData2;

    //1
    public TMP_Text _ihaleisim_1;
    public TMP_Text _ihaleFiyat_1;
    //2
    public TMP_Text _ihaleisim_2;
    public TMP_Text _ihaleFiyat_2;


    void Start()
    {
        if (ihaleData1 != null && _ihaleisim_1 != null)
        {
            _ihaleisim_1.text = ihaleData1.ihaleAdi;  
            _ihaleFiyat_1.text = ihaleData1.ihaleFiyati.ToString("C2");

            _ihaleisim_2.text = ihaleData2.ihaleAdi;  
            _ihaleFiyat_2.text = ihaleData2.ihaleFiyati.ToString("C2");
        }
    }
}
*/


