using UnityEngine;

namespace SimplePopups
{
    public class DefaultPopupsProvider : MonoBehaviour, IPopupsProvider
    {
        [SerializeField] private PopupBase[] _popups;
        
        public PopupBase[] Popups => _popups;
    }
}