using Entities.Creatures.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalSystems
{
    /// <summary>
    /// Система, хранящая информацию о главном игроке, которым управляет пользователь
    /// </summary>
    public class MainPlayerGS : MonoBehaviour
    {
        public Player player = null;
    }
}
