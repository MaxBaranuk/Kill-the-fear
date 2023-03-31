using Systems.Interfaces;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class LifeUI : MonoBehaviour
{
    [Inject] private IUserSystem _userSystem;

    [SerializeField] private Text _lifeText;

    void Update()
    {
        _lifeText.text = _userSystem.GetUserHP().ToString();
    }
}
