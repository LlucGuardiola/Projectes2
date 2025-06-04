using System.Data;
using UnityEngine;
using UnityEngine.Rendering;


[CreateAssetMenu(fileName ="NewNpcDialogue", menuName = "NPC Dialogue")]
public class NpcDialogue : ScriptableObject
{
    public string npcName;
    public Sprite npcPortrait;
    public string[] dialogueLines;
    public bool[] autoProgressLines; //Para no tener que darle a un boton para avanzar
    public float typingSpeed = 0.008f;
    public AudioClip voiceSound;
    public float VoicePitch = 1f;
    public float autoProgressDelay = 1.5f;





}
