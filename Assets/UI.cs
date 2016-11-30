using UnityEngine;
using System.Collections;

public class UI : MonoBehaviour {
    public Texture2D crosshairTexture;
    private const float CrosshairSize = 32;
    private Vector2 position = Vector3.zero;
    private Vector2 size = new Vector2(CrosshairSize, CrosshairSize);

    void OnGUI() {
        Vector2 mousePos = Input.mousePosition;
        position.x = mousePos.x - CrosshairSize / 2;
        position.y = Screen.height - mousePos.y - CrosshairSize / 2;
        Rect rect = new Rect(position, size);
        GUI.DrawTexture(rect, crosshairTexture);
    }
}
