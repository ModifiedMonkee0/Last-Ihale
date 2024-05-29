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
    public Button iþletmeKredisiButon;
    public Button ticariKrediButon;

    private void Start()
    {
        ihtiyacKredisiButon.onClick.AddListener(IhtiyacKrediAldý);
        iþletmeKredisiButon.onClick.AddListener(IsletmeKrediAldý);
        ticariKrediButon.onClick.AddListener(TicariKrediAldý);
    }

    private void Update()
    {
        currentMoney.text = "Þuanki Paran: " + playerData.currentMoney;
    }

    private void IhtiyacKrediAldý()
    {
        float krediMiktari = 60000f;
        float faizOrani = 10f;
        float geriOdemeSuresi = 10f; // 5 dakika

        //playerData scriptinin içindeki TakeLoan fonksiyonuna eriþtik ve o fonksiyonun içindeki datayý da biz atadýk. 
        //playerData sciptinde kredi iþlemleri 91. satýrda baþlar.
        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

    private void IsletmeKrediAldý()
    {
        float krediMiktari = 120000f;
        float faizOrani = 20f;
        float geriOdemeSuresi = 7 * 60f; //7 dakika

        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

    private void TicariKrediAldý()
    {
        float krediMiktari = 230000f;
        float faizOrani = 30f;
        float geriOdemeSuresi = 4 * 60f; // 4 dakika

        playerData.TakeLoan(krediMiktari, faizOrani, geriOdemeSuresi);
    }

}
