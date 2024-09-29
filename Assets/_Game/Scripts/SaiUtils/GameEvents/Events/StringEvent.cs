using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SaiUtils.GameEvents {
    
    [CreateAssetMenu(fileName = "New String Event", menuName = "GameEvents/StringEvent")]
    public class StringEvent : BaseGameEvent<string> {}
}