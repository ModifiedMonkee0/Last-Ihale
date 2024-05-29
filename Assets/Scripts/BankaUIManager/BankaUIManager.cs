using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BankaUIManager : MonoBehaviour
{
    /*
    public Button _faizlerEkranButon;
    public Button _kredilerInEkranButon;
    public Button _sirketEkranButon;

    public GameObject _faizlerPanel;
    public GameObject _kredilerPanel;
    public GameObject _sirketPanel;
    */

    public PlayerData playerData;

    public TMP_Text currentMoney;

    public Button ihtiyacKredisiButon;
    public Button işletmeKredisiButon;
    public Button ticariKrediButon;

    private void Start()
    {
        ihtiyacKredisiButon.onClick.AddListener(IhtiyacKrediAldı);
        işletmeKredisiButon.onClick.AddListener(IsletmeKrediAldı);
        ticariKrediButon.onClick.AddListener(TicariKrediAldı);
    }

    private void Update()
    {
        currentMoney.text = "Şuanki Paran: " + playerData.currentMoney;
    }

    private void IhtiyacKrediAldı()
    {
        float krediMiktari = 60000f;
        float faizOrani = 10f;
        float geriOdemeSuresi = 10f; // 5 dakika

        //playerData scriptinin içindeki TakeLoan fonksiyonuna eriştik ve o fonksiyonun içindeki datayı da biz atadık. 
        //playerData sciptinde kredi işlemleri 91. satırda başlar.
        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

    private void IsletmeKrediAldı()
    {
        float krediMiktari = 120000f;
        float faizOrani = 20f;
        float geriOdemeSuresi = 7 * 60f; //7 dakika

        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

    private void TicariKrediAldı()
    {
        float krediMiktari = 230000f;
        float faizOrani = 30f;
        float geriOdemeSuresi = 4 * 60f; // 4 dakika

        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

}
