using System.Collections;
using System.Diagnostics;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Tree;

using UnityEngine;
using UnityEngine.UI;

public class NPC : MonoBehaviour, IInteractable
{
    public NpcDialogue dialogueData;
    public GameObject dialoguePanel;
    public TMP_Text dialogueText, nameText;
    public Image portraitImage;


    private int dialogueIndex;
    private bool isTyping, isDialogueActive;

    public bool CanInteract ()
    {
        return !isDialogueActive;
    }

    public void Interact ()
    {
      if (dialogueData == null || (PauseLogic.IsPaused && isDialogueActive))
        {
            return;
        }

      if (isDialogueActive )
        {
            NextLine();
        }
        else
        {
            StartDialogue();
        }
    }

    void StartDialogue ()
    {
        isDialogueActive = true;
        dialogueIndex = 0;

        nameText.SetText (dialogueData.npcName); //npc name!!
        portraitImage.sprite = dialogueData.npcPortrait;

        dialoguePanel.SetActive (true);
        PauseLogic.IsPaused = true;

        StartCoroutine (TypeLine() );
    }


    void NextLine ()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.SetText(dialogueData.dialogueLines[dialogueIndex]);
            isTyping = false;
        }
        else if (++dialogueIndex < dialogueData.dialogueLines.Length)
        {
            StartCoroutine (TypeLine() );
        }
        else
        {
            EndDialogue();
        }
    }

    IEnumerator TypeLine ()
    {
        isTyping = true;

        dialogueText.SetText("");
        foreach (char letter in dialogueData.dialogueLines[dialogueIndex])
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(dialogueData.typingSpeed);
        }

        isTyping = false;
        if (dialogueData.autoProgressLines.Length > dialogueIndex && dialogueData.autoProgressLines[dialogueIndex])
        {
            yield return new WaitForSeconds (dialogueData.autoProgressDelay);
            NextLine();
        }
    }

    public void EndDialogue ()
    {
        StopAllCoroutines ();
        isDialogueActive = false;
        dialogueText.SetText("");
        dialoguePanel.SetActive(false);
        PauseLogic.IsPaused = false;
    }



}

