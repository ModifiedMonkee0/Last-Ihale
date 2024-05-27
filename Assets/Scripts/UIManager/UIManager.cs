using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    //PC UI
    public Button _ihaleEkranButon;
    public Button _sinkedInEkranButon;
    public Button _sirketEkranBuyon;

    public GameObject _ihalePanel;
    public GameObject _sinkedInPanel;
    public GameObject _SirketPanel;
    //Pc UI

    //Karakter UI
    public TMP_Text currentMoney;
    public PlayerData playerData;
    //Karakter UI
    private void Start()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(true);
        _SirketPanel.SetActive(false);
    }
    public void SinkedSayfaInAc()
    {
        _sinkedInPanel.SetActive(true);
        _ihalePanel.SetActive(false);
        _SirketPanel.SetActive(false);

    }
    
    public void IhalelerSayfaAc ()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(true);
        _SirketPanel.SetActive(false);
    }

    public void SirketSayfaAc()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(false);
        _SirketPanel.SetActive(true);
    }

    //PC UI

    //Karakter UI
    private void Update()
    {

        currentMoney.text = "Þuanki Paran: " + playerData.currentMoney.ToString("C2");
    }
    //Karakter UI

}
