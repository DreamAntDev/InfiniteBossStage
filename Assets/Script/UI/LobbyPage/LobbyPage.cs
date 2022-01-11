using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Lobby
{
    public abstract class LobbyPage : MonoBehaviour
    {
        protected Lobby parent;
        public void SetParentLobby(Lobby lobby)
        {
            parent = lobby;
        }
        abstract public string GetTitle();
        virtual public bool IsVisibleTitle() { return true; }
        virtual public bool IsVisibleBackButton() { return true; }
    }
}
