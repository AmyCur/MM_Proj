using System.Collections.Generic;
using UnityEngine;
using WFC.Enums;

/*
namespace WaveFunctionCollapse
{
    [System.Serializable]
    public static class WFC
    {

    
        public class Directions
        {
            public List<DEnum> directions;

            // Checks if the directions are valid and, if they are, retursn a list of all the valid directions
            public bool directionValid(Directions otherDirections, out Directions dEnum)
            {
                dEnum = new(null);

                foreach (DEnum d in directions)
                {
                    foreach (DEnum dd in otherDirections.directions)
                    {
                        // Inverse so they fit together like lego
                        if (d == Dicts.inverseDic[dd])
                        {
                            dEnum.directions.Add(d);
                        }
                    }
                }

                if (dEnum.directions.Count > 0)
                {
                    return true;
                }

                else
                {
                    dEnum = null;
                    return false;
                }
            }

            public Directions(List<DEnum> di)
            {
                directions = di;
            }
            
            [System.Serializable]
            public class WFCGrid
            {
                public List<List<Room>> grid;

            }
        }

        
    }
}*/