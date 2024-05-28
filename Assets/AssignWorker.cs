using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AssignWorker : MonoBehaviour
{
    public TMP_Text iyiMuhendis;
    public TMP_Text iyiWorker;
    public TMP_Text kotuWorker;
    public TMP_Text kotuMuhendis;

    public Button iyiMuhIncrease;
    public Button iyiMuhDecrease;

    public Button iyiWorkerIncrease;
    public Button iyiWorkerDecrease;

    public Button kotuWorkerIncrease;
    public Button kotuWorkerDecrease;

    public Button kotuMuhendisIncrease;
    public Button kotuMuhendisDecrease;

    private int iyiMuhendisCount = 0;
    private int iyiWorkerCount = 0;
    private int kotuWorkerCount = 0;
    private int kotuMuhendisCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        iyiMuhIncrease.onClick.AddListener(() => IncreaseCount(ref iyiMuhendisCount, iyiMuhendis));
        iyiMuhDecrease.onClick.AddListener(() => DecreaseCount(ref iyiMuhendisCount, iyiMuhendis));

        iyiWorkerIncrease.onClick.AddListener(() => IncreaseCount(ref iyiWorkerCount, iyiWorker));
        iyiWorkerDecrease.onClick.AddListener(() => DecreaseCount(ref iyiWorkerCount, iyiWorker));

        kotuWorkerIncrease.onClick.AddListener(() => IncreaseCount(ref kotuWorkerCount, kotuWorker));
        kotuWorkerDecrease.onClick.AddListener(() => DecreaseCount(ref kotuWorkerCount, kotuWorker));

        kotuMuhendisIncrease.onClick.AddListener(() => IncreaseCount(ref kotuMuhendisCount, kotuMuhendis));
        kotuMuhendisDecrease.onClick.AddListener(() => DecreaseCount(ref kotuMuhendisCount, kotuMuhendis));

        UpdateTextFields();
    }

    void IncreaseCount(ref int count, TMP_Text textField)
    {
        count++;
        textField.text = count.ToString();
    }

    void DecreaseCount(ref int count, TMP_Text textField)
    {
        if (count > 0)
        {
            count--;
        }
        textField.text = count.ToString();
    }

    void UpdateTextFields()
    {
        iyiMuhendis.text = iyiMuhendisCount.ToString();
        iyiWorker.text = iyiWorkerCount.ToString();
        kotuWorker.text = kotuWorkerCount.ToString();
        kotuMuhendis.text = kotuMuhendisCount.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTextFields();
    }
}
