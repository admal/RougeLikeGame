using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Doors
{
    public interface IDoor
    {

        
    }
    public class DoorController
    {
        private IDoor _door;
        
        public DoorController(IDoor door)
        {
            _door = door;
        }
    }
}
