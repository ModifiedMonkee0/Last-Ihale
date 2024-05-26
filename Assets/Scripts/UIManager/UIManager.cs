using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

    public Button _ihaleEkranButon;
    public Button _sinkedInEkranButon;
    public Button _sirketEkranBuyon;

    public GameObject _ihalePanel;
    public GameObject _sinkedInPanel;
    public GameObject _SirketPanel;

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

    public void SýrketSayfaAc()
    {
        _sinkedInPanel.SetActive(false);
        _ihalePanel.SetActive(false);
        _SirketPanel.SetActive(true);
    }
}
