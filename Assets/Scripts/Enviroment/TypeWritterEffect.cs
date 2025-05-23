using System.Collections;
using TMPro;
using UnityEngine;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshPro textMesh; // Asigna el TextMeshPro desde el Inspector
    public float delay = 0.05f;  // Tiempo entre letras
    [TextArea] public string fullText; // Texto completo a mostrar

    private Coroutine typingCoroutine;

    private void OnEnable()
    {
        StartTyping();
    }

    public void StartTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText());
    }

    private IEnumerator TypeText()
    {
        textMesh.text = "";
        foreach (char c in fullText)
        {
            textMesh.text += c;
            yield return new WaitForSeconds(delay);
        }
    }
}

    

