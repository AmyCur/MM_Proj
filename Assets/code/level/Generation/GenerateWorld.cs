using UnityEngine;
using WFC.Generation;



public class GenerateWorld : MonoBehaviour
{
    
    public Room startRoom;

    void Start()
    {
        Generation.GenerateStartRoom(startRoom);
        Generation.GenerateWorld();
    }
}
