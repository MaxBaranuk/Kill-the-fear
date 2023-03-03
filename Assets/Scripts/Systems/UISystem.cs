using Core;
using Systems.Interfaces;
using UnityEngine;

namespace Systems
{
    public class UISystem: BaseSystem,IUISystem
    {
        [SerializeField] GameObject rootCanvas;
    }
}