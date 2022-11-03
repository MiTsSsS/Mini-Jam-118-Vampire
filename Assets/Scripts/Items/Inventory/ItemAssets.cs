using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAssets : MonoBehaviour {
    public static ItemAssets instance { get; private set; }

    private void Awake() {
        instance = this;
    }

    public Transform itemPrefab;

    public Sprite shortSwordSprite;
    public Sprite pureBloodSprite;
    public Sprite bowSprite;
    public Sprite crossbowSprite;

    public GameObject arrowPrefab;
    public GameObject boltPrefab;
}
