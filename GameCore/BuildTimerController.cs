using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildTimerController : MonoBehaviour, IUtility
{
    [Header("Hlavní načítací Bar.")]
    [Tooltip("To kolečko.")]
    public Image[] mainTimeBars;

    [Header("Čísla časovače.")]
    public TextMeshProUGUI timerText;

    [Header("Odpočet v sekundách.")]
    public float countDownTime;

    private TextMeshProUGUI title;
    private float timeSafe;

    private Vector3 pos;

    private RectTransform timerTransform;

    private float sizingConstant = 0.007f; // Proč?? Neptej se vyexperymentoval jsem

    Vector3 positionOnScreen;

    Vector3 positionToCalculateOn; // Pozice, kde má objekt zůstat

    int minutes;
    int seconds;
    // Start is called before the first frame update
    void Start() {
            //startTime = Time.time;
        if (mainTimeBars.Length == 0)
            mainTimeBars = Settings.FindComponentsInChildrenWithTag<Image>(transform, Settings.UI_TAG_BAR);
        if (timerText == null)
            timerText = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(transform, Settings.UI_TAG_TEXT);
        if (title == null)
            title = Settings.FindComponentInChildrenWithTag<TextMeshProUGUI>(transform, Settings.UI_TAG_TITLE);
        if (timerTransform == null)
            timerTransform = GetComponent<RectTransform>();

    }

    public void SetData(float countDownTime)
    {

        this.countDownTime = countDownTime;
        this.timeSafe = countDownTime;
        this.positionToCalculateOn = transform.position;

        if (timerTransform != null)
            this.timerTransform.localScale = new Vector3(1f, 1f, 1f);// * Settings.timerSize;
        if (title != null)
            this.title.text = TextHolder.TIMER_TITLE;


        InvokeRepeating("UpdateText", 0.1f, 1f);
    }

    public void ReloadText()
    {
        if (title!=null)
            this.title.text = TextHolder.TIMER_TITLE;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(countDownTime >= .0f)
            countDownTime -= Time.deltaTime;

         minutes = (int) countDownTime / 60;
         seconds = (int) countDownTime % 60;

        // Dopočítáme procenta pro převod 360 vyplnění
        float filled = countDownTime / timeSafe;

        if (filled > 0)
            mainTimeBars[0].fillAmount = 1-filled;
            mainTimeBars[1].fillAmount = 0.9f - filled;


        float distScaleFactor = Vector3.Distance(Manager.Instance.mainCamera.transform.position, pos);

        float size = Mathf.Clamp(1 / (distScaleFactor * 10 * sizingConstant), 0.2f, 1f);
        timerTransform.localScale = new Vector3(size, size, size);
        
        positionOnScreen = Manager.Instance.mainCamera.WorldToScreenPoint(positionToCalculateOn,Camera.MonoOrStereoscopicEye.Mono);

        if (positionOnScreen.z >= 0) // Proč?? Mě se neptej... Psali to tunaj v komentech a funguje https://www.youtube.com/watch?v=7CGejTHPvU4
            timerTransform.position = positionOnScreen; // Jinak to zobrazuje UI i po 180 rotaci kamery, což skutečně nechceme
    }

    void UpdateText()
    {
        timerText.text = minutes + ":" + seconds;
    }
}
