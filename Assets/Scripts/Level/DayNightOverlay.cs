using UnityEngine;
using UnityEngine.UI;

public class DayNightOverlay : MonoBehaviour
{
    [Header("Night Overlay Settings")]
    [SerializeField] private GameObject overlayPanel;
    [SerializeField] private Image overlayImage;
    [SerializeField] private Color nightColor = new Color(0f, 0f, 0.3f, 0.5f);
    [SerializeField] private float transitionSpeed = 2f;
    
    private LevelManager levelManager;
    private Color targetColor = Color.clear;

    private void Start()
    {
        levelManager = LevelManager.instance;
        
        if (overlayPanel == null)
        {
            CreateOverlayPanel();
        }
        
        if (overlayImage != null)
        {
            overlayImage.color = Color.clear;
        }
    }

    private void Update()
    {
        if (levelManager == null || overlayImage == null) return;

        targetColor = levelManager.IsDaytime ? Color.clear : nightColor;
        
        overlayImage.color = Color.Lerp(overlayImage.color, targetColor, transitionSpeed * Time.deltaTime);
    }

    private void CreateOverlayPanel()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();
        
        if (canvas == null)
        {
            // Crea un canvas Xd
            GameObject canvasGO = new GameObject("DayNightCanvas");
            canvas = canvasGO.AddComponent<Canvas>();
            canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            canvas.sortingOrder = 100;
            
            CanvasScaler scaler = canvasGO.AddComponent<CanvasScaler>();
            scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            scaler.referenceResolution = new Vector2(1920, 1080);
            
            canvasGO.AddComponent<GraphicRaycaster>();
        }

        overlayPanel = new GameObject("NightOverlay");
        overlayPanel.transform.SetParent(canvas.transform, false);
        
        RectTransform rectTransform = overlayPanel.AddComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;
        
        overlayImage = overlayPanel.AddComponent<Image>();
        overlayImage.color = Color.clear;
        
        overlayImage.raycastTarget = false;
    }
    public void SetNightColor(Color color)
    {
        nightColor = color;
    }
    
    public void SetTransitionSpeed(float speed)
    {
        transitionSpeed = speed;
    }
}