using UnityEngine;
using UnityEngine.UI;
using TMPro;


#if UNITY_EDITOR
using UnityEditor;
#endif

public class OrientationSwitcher : MonoBehaviour
{
    [Header("Что меняем")]
    [SerializeField] bool _canvas;
    [SerializeField] bool _image;
    [SerializeField] bool _text, _TMP;
    [SerializeField] bool _transform;

    [Header("Canvas настройки")]
    public Canvas targetCanvas;
    public RenderMode portraitCanvasMode = RenderMode.ScreenSpaceOverlay;
    public RenderMode landscapeCanvasMode = RenderMode.WorldSpace;

    [Header("Картинки для разных ориентаций")]
    public Sprite portraitSprite;
    public Sprite landscapeSprite;
    public Image targetImage;

    [Header("Тексты для разных ориентаций")]
    public int portrait;
    public int landscape;
    [Multiline]public string _textPortrait = "<color=pink>Дорогая Гинара Митхатовна</color>!\nС наступающим 2026 годом!\nПусть этот год войдёт в вашу жизнь тихо, красиво и по-настоящему по-добромy - с успехом, светом, тёплыми людьми рядом и только хорошим настроением каждый день.\nВы умеете дарить уют, поддержку и вдохновение всем вокруг - и пусть мир возвращает вам это вдвойне.\nИ отдельно, с уважением и благодарностью,\nГайрат дарит Вам сертификат на школьные услуги на сумму - <size=100><color=green>1 000 000</color></size> сум.\nЭто маленький знак большой признательности за вашу доброту и невероятную душевность.\nПусть 2026 год станет годом радости, тепла и больших личных побед.\nС любовью и искренним теплом!";
    [Multiline]public string _textLandscape = "Дорогая Гинара Митхатовна!\nС наступающим 2026 годом!\nПусть этот год войдёт в вашу жизнь тихо, красиво и по-настоящему по-добромy - с успехом, светом, тёплыми людьми рядом и только хорошим настроением каждый день.\nВы умеете дарить уют, поддержку и вдохновение всем вокруг - и пусть мир возвращает вам это вдвойне.\nИ отдельно, с уважением и благодарностью,\nГайрат дарит Вам сертификат на школьные услуги на сумму - <size=50><color=green>1 000 000</color></size> сум.\nЭто маленький знак большой признательности за вашу доброту и невероятную душевность.\nПусть 2026 год станет годом радости, тепла и больших личных побед.\nС любовью и искренним теплом!";    public Text targetText;
    public TextMeshProUGUI targetTMP;

    [Header("Трансформации для разных ориентаций")]
    public Vector3 portraitPosition = new Vector3(0, 0, 0);
    public Vector3 landscapePosition = new Vector3(200, 0, 0);
    public Transform targetTransform;

    private bool isPortrait;

    void Start()
    {
        UpdateOrientation();
    }

    void Update()
    {
        bool currentPortrait = Screen.width < Screen.height;
        if (currentPortrait != isPortrait)
        {
            isPortrait = currentPortrait;
            UpdateOrientation();
        }
    }

    void UpdateOrientation()
    {
        // Canvas
        if (_canvas && targetCanvas != null)
        {
            targetCanvas.renderMode = isPortrait ? portraitCanvasMode : landscapeCanvasMode;
        }

        // Image
        if (_image && targetImage != null)
        {
            targetImage.sprite = isPortrait ? portraitSprite : landscapeSprite;
        }

        // Text
        if (_text && targetText != null)
        {
            targetText.fontSize = isPortrait ? portrait : landscape;
        }
        if (_TMP && targetTMP != null)
        {
            if(isPortrait)
            {
                targetTMP.fontSize = portrait;
                targetTMP.text = _textPortrait;
            }
            else if(!isPortrait)
            {
                targetTMP.fontSize = landscape;
                targetTMP.text = _textLandscape;
            }
            //targetTMP.fontSize = isPortrait ? portrait : landscape;
        }

        // Transform
        if (_transform && targetTransform != null)
        {
            targetTransform.localPosition = isPortrait ? portraitPosition : landscapePosition;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("Simulate Portrait")]
    void SimulatePortrait()
    {
        isPortrait = true;
        UpdateOrientation();
    }

    [ContextMenu("Simulate Landscape")]
    void SimulateLandscape()
    {
        isPortrait = false;
        UpdateOrientation();
    }
#endif
}