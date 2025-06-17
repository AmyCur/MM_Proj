using System.Collections.Generic;
using UnityEngine;

using WFC.Directions;

[CreateAssetMenu(fileName = "New Room", menuName = "Rooms/Create New Room", order = 0)]
public class Room : ScriptableObject
{
    public GameObject roomObj;
    public GameObject worldObj;

    // This will contain a dictionary of the Rooms and the possble ways they can connect
    // public Dictionary<Room, Directions> possibleRooms;

    public List<RDic> possibleRooms;

    [System.Serializable]
    public class RDic
    {
        public Room room;
        public Directions direction;
    }

    // This will contain the ways that this can connect
    public Directions directions;

    public Vector2Int coords;

}
