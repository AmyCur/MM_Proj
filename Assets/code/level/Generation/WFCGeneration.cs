using System.Collections.Generic;
using UnityEngine;

using WFC.Enums;
using WFC.Consts;
using WFC.Grid;

namespace WFC.Generation
{
    public static class Generation
    {
        public static WFCGrid worldGrid = new
        (
            new()
            {
                new(){null, null, null, null},
                new(){null, null, null, null},
                new(){null, null, null, null},
                new(){null, null, null, null}
            }
        );
        
        public static List<DEnum> possibleDirections(Room room)
        {
            List<DEnum> d = new();

            foreach (var kvp in room.possibleRooms)
            {
                foreach (DEnum dEnum in kvp.direction.directions)
                {
                    Vector2Int v2 = new Vector2Int(room.coords.x + Dicts.directionDic[dEnum].x, room.coords.y + Dicts.directionDic[dEnum].y);
                    // var item = worldGrid.grid[room.coords.y + Dicts.directionDic[dEnum].x][room.coords.x + Dicts.directionDic[dEnum].y];

                    // Sees if it is in the grid
                    bool inRange =
                        v2.x < 0 &&
                        v2.x > worldGrid.grid[0].Count &&
                        v2.y < 0 &&
                        v2.y > worldGrid.grid.Count
                    ;

                    if (inRange)
                    {
                        if (dEnum == DEnum.up && !d.Contains(DEnum.up)) { d.Add(DEnum.up); }
                        if (dEnum == DEnum.down && !d.Contains(DEnum.down)) { d.Add(DEnum.down); }
                        if (dEnum == DEnum.left && !d.Contains(DEnum.left)) { d.Add(DEnum.left); }
                        if (dEnum == DEnum.right && !d.Contains(DEnum.right)) { d.Add(DEnum.right); }
                    }
                }
            }
            #region Old Code that uses a Dictionary
            /*          
                        foreach (var dic in room.possibleRooms)
                        {
                            foreach (DEnum dEnum in dic.Value.directions)
                            {
                                bool inRange = worldGrid.grid[room.coords.y + Dicts.directionDic[dEnum].x][room.coords.x + Dicts.directionDic[dEnum].y];

                                if (inRange)
                                {
                                    if (dEnum == DEnum.up && !d.Contains(DEnum.up)) { d.Add(DEnum.up); }
                                    if (dEnum == DEnum.down && !d.Contains(DEnum.down)) { d.Add(DEnum.down); }
                                    if (dEnum == DEnum.left && !d.Contains(DEnum.left)) { d.Add(DEnum.left); }
                                    if (dEnum == DEnum.right && !d.Contains(DEnum.right)) { d.Add(DEnum.right); }
                                }

                            }
                        }
            */
            #endregion
            return d;
        }

        public static List<Room> RoomsOfTargetDirection(DEnum targetDirection, Room room)
        {
            List<Room> r = new();

            foreach (var kvp in room.possibleRooms)
            {
                foreach (DEnum d in kvp.direction.directions)
                {
                    if (d == targetDirection)
                    {
                        r.Add(kvp.room);
                    }
                }
            }

            return r;
        }

        public static void GenerateRoom(Room room)
        {
            // Gets the possbile directions (This is probably less efficient than just using a lot)
            // of random numbers, but that feels icky
            List<DEnum> pDs = possibleDirections(room);

            // Decide on a target direction
            DEnum targetDir = Dicts.inverseDic[pDs[Random.Range(0, pDs.Count)]];

            // Get a list of the possible rooms
            List<Room> possibleRooms = RoomsOfTargetDirection(targetDir, room);

            // Create a new room randomly
            Room newRoom = possibleRooms[Random.Range(0, possibleRooms.Count)];

            // Set the coordinates of the new room, based on the coordinates of the old one
            newRoom.coords = new Vector2Int(room.coords.y + Dicts.directionDic[targetDir].y, room.coords.x + Dicts.directionDic[targetDir].x);
            worldGrid.grid[newRoom.coords.x][newRoom.coords.y] = newRoom;

            newRoom.worldObj = GameObject.Instantiate(
                newRoom.roomObj,
                new Vector3(
                    newRoom.coords.x * 40,
                    0,
                    newRoom.coords.y * 40
                ),
                Quaternion.identity
            );
        }

        public static void GenerateWorld()
        {
            for (int i = 0; i < 10_000; i++)
            {
                foreach (var row in worldGrid.grid)
                {
                    foreach (Room room in row)
                    {
                        if (room != null)
                        {
                            GenerateRoom(room);
                        }
                    }
                }
            }
            
        }

        public static void GenerateStartRoom(Room startRoom)
        {
            Vector2Int spawnPos = new(
                Random.Range(0, worldGrid.grid.Count),
                Random.Range(0, worldGrid.grid.Count)
            );

            startRoom.worldObj = GameObject.Instantiate(
                startRoom.roomObj,
                new Vector3(
                    spawnPos.x * 40,
                    0,
                    spawnPos.y * 40
                ),
                Quaternion.identity
            );

            worldGrid.grid[spawnPos.y][spawnPos.x] = startRoom;
        }
    }

}