using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCloseButton : MonoBehaviour {
    void OnMouseDown() {
        Application.OpenURL("https://www.quebon.tv/game/triFunction/exit");
    }
}
