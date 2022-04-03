using System.Collections.Generic;
using System.Linq;

namespace LevelGeneration
{
    public class MainRoomSelector
    {
        private int _count;

        public MainRoomSelector(int count)
        {
            _count = count;
        }

        public List<RoomGameObject> Select(List<RoomGameObject> rooms)
        {
            return rooms.OrderByDescending(x => x.transform.localScale.sqrMagnitude).Take(_count).ToList();
        }
    }
}
