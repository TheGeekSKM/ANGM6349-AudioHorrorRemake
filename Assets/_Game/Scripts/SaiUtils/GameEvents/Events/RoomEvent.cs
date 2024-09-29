using UnityEngine;

namespace SaiUtils.GameEvents
{
    [CreateAssetMenu(fileName = "New Room Event", menuName = "GameEvents/RoomEvent")]
    public class RoomEvent : BaseGameEvent<RoomData> {}

    // I don't need to add a Raise method here because it is already implemented in the BaseGameEvent class
    // because the T in the BaseGameEvent class is now an int, the Raise method will now automatically take an int as a parameter
}