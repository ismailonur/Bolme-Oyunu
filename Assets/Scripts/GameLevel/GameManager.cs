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

    public Text soruText;

    private GameObject[] karelerDizisi = new GameObject[25];

    List<int> bolumDegerleriListesi = new List<int>();

    int bolenSayi, bolunenSayi;
    int kacinciSoru;
    int butonDegeri;
    int dogruSonuc;

    int kalanHak;

    bool butonaBasilsinmi;

    string sorununZorlukDerecesi;

    KalanHaklarManager kalanHaklarManager;

    PuanManager puanManager;

    private void Awake()
    {
        kalanHak = 3;

        kalanHaklarManager = Object.FindObjectOfType<KalanHaklarManager>();
        puanManager = Object.FindObjectOfType<PuanManager>();

        kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
    }

    void Start()
    {
        butonaBasilsinmi = false;
        soruPaneli.GetComponent<RectTransform>().localScale = Vector3.zero;

        kareleriOlustur();
    }

    public void kareleriOlustur()
    {
        for(int i = 0; i < 25; i++)
        {
            GameObject kare = Instantiate(karePrefab, karelerPaneli);
            kare.transform.GetComponent<Button>().onClick.AddListener(() => ButonaBasildi());
            karelerDizisi[i] = kare;
        }
        bolumDegerleriniYaz();

        StartCoroutine(DoFadeRoutine());

        Invoke("SoruPaneliniAc", 2f);
    }

    void ButonaBasildi()
    {
        if (butonaBasilsinmi)
        {
            butonDegeri = int.Parse(UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Text>().text);

            SonucuKontrolEt();
        }
    }

    void SonucuKontrolEt()
    {
        if(butonDegeri == dogruSonuc)
        {
            puanManager.PuaniArttir(sorununZorlukDerecesi);

            bolumDegerleriListesi.RemoveAt(kacinciSoru);

            SoruPaneliniAc();
        }
        else
        {
            kalanHak--;
            kalanHaklarManager.KalanHaklariKontrolEt(kalanHak);
        }
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
            bolumDegerleriListesi.Add(rastgeleDeger);

            kare.transform.GetChild(0).GetComponent<Text>().text = rastgeleDeger.ToString(); // karenin içinde texte ulaşıp değiştirmemizi sağlar.
        }
    }

    void SoruPaneliniAc()
    {
        SoruyuSor();
        butonaBasilsinmi = true;
        soruPaneli.GetComponent<RectTransform>().DOScale(1, .5f).SetEase(Ease.OutBack);
    }
    
    void SoruyuSor()
    {
        bolenSayi = Random.Range(2, 11);

        kacinciSoru = Random.Range(0, bolumDegerleriListesi.Count);

        dogruSonuc = bolumDegerleriListesi[kacinciSoru];

        bolunenSayi = bolenSayi * dogruSonuc;

        if(bolunenSayi <= 40)
        {
            sorununZorlukDerecesi = "kolay";
        }
        else if(bolunenSayi > 40 && bolunenSayi <= 80)
        {
            sorununZorlukDerecesi = "orta";
        }
        else
        {
            sorununZorlukDerecesi = "zor";
        }

        soruText.text = bolunenSayi.ToString() + " / " + bolenSayi.ToString();
    }
}
