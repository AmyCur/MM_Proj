using System.Collections.Generic;

namespace WFC.Grid
{
    [System.Serializable]
    public class WFCGrid
    {
        public List<List<Room>> grid;

        public WFCGrid(List<List<Room>> g)
        {
            grid = g;
        }

    }
}
