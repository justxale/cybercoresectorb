using UnityEngine;

namespace Dialogue_System
{
    [CreateAssetMenu(fileName = "NPC Dialogue Data", menuName = "NPC Dialogue")]
    public class NPCDialogueData : ScriptableObject
    {
        public string npcName;
        [TextArea(3, 15)] public string[] npcDialogue;
        [TextArea(3, 15)] public string[] playerDialogue;
    }
}
