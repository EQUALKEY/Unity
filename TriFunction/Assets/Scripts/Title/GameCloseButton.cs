using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCloseButton : MonoBehaviour {
    public void OnMouseDown() {
        Application.OpenURL("https://www.quebon.tv/game/triFunction/exit");
    }
}
