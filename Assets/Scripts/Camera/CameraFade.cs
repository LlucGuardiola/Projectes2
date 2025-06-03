using Unity.VisualScripting;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    public KeyCode key = KeyCode.Space;
    public static float SpeedScale = 1f;
    public Color fadeColor = Color.black;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1),
        new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public bool startFadedOut = false;

    private float alpha = 0f;
    private Texture2D texture;
    private static int direction = 0;
    private float time = 0f;

    private static bool Fade;
    private static bool inAndOut;
    private bool waiting;
    private static float delay;

    private void Start()
    {
        if (startFadedOut) { alpha = 1f; }
        else { alpha = 0f; }
        texture = new Texture2D(1, 1);
        texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
        texture.Apply();
    }

    private void Update()
    {
        if (direction == 0 && Fade && !waiting)
        {
            Fade = false;
            if (alpha >= 1f) // Start fade out
            {
                alpha = 1f;
                time = 0f;
                direction = 1;
            }
            else // Start fade in
            {
                alpha = 0f;
                time = 1f;
                direction = -1;
            }
        }
    }
    public void OnGUI()
    {
        if (alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture);
        if (direction != 0)
        {
            time += direction * Time.deltaTime * SpeedScale;
            alpha = Curve.Evaluate(time);
            texture.SetPixel(0, 0, new Color(fadeColor.r, fadeColor.g, fadeColor.b, alpha));
            texture.Apply();
            if (alpha <= 0f || alpha >= 1f) 
            { 
                if (inAndOut)
                {
                    waiting = true;
                    Invoke("InvertFade", delay);
                }
                else { direction = 0; }
            }
        }
    }

    public static void StartFade(bool InAndOut, float InAndOutDelay)
    {
        if (direction != 0) return;
        inAndOut = InAndOut;
        Fade = true;
        delay = InAndOutDelay;
    }

    private void InvertFade()
    {
        direction *= -1;
        inAndOut = false;
        waiting = false;
    }
}