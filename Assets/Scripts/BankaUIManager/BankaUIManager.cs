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
    public Button i�letmeKredisiButon;
    public Button ticariKrediButon;

    private void Start()
    {
        ihtiyacKredisiButon.onClick.AddListener(IhtiyacKrediAld�);
        i�letmeKredisiButon.onClick.AddListener(IsletmeKrediAld�);
        ticariKrediButon.onClick.AddListener(TicariKrediAld�);
    }

    private void Update()
    {
        currentMoney.text = "�uanki Paran: " + playerData.currentMoney;
    }

    private void IhtiyacKrediAld�()
    {
        float krediMiktari = 60000f;
        float faizOrani = 10f;
        float geriOdemeSuresi = 10f; // 5 dakika

        //playerData scriptinin i�indeki TakeLoan fonksiyonuna eri�tik ve o fonksiyonun i�indeki datay� da biz atad�k. 
        //playerData sciptinde kredi i�lemleri 91. sat�rda ba�lar.
        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

    private void IsletmeKrediAld�()
    {
        float krediMiktari = 120000f;
        float faizOrani = 20f;
        float geriOdemeSuresi = 7 * 60f; //7 dakika

        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

    private void TicariKrediAld�()
    {
        float krediMiktari = 230000f;
        float faizOrani = 30f;
        float geriOdemeSuresi = 4 * 60f; // 4 dakika

        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

}
