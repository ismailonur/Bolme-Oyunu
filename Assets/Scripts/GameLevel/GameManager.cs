using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject karePrefab;
    public GameObject soruPaneli;
    public Transform karelerPaneli;

    private GameObject[] karelerDizisi = new GameObject[25];

    void Start()
    {
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kareleriOlustur();
    }

    public void kareleriOlustur()
    {
        for(int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            karelerDizisi[i] = kare;
        }
        bolumDegerleriniYaz();

        StartCoroutine(DoFadeRoutine());

        Invoke("SoruPaneliniAc", 2f);
    }

    IEnumerator DoFadeRoutine()
    {
        foreach(var kare in karelerDizisi)
        {
            kare.GetComponent<CanvasGroup>().DOFade(1, .2f);

            yield return new WaitForSeconds(0.05f);
        }
    }

    void bolumDegerleriniYaz()
    {
        foreach(var kare in karelerDizisi)
        {
            int rastgeleDeger = Random.Range(1, 13);

            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString(); // karenin içinde texte ulaşıp değiştirmemizi sağlar.
        }
    }

    void SoruPaneliniAc()
    {
        soruPaneli.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
    }
}
