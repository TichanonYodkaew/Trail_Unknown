using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_KeyHolder : MonoBehaviour
{
    [SerializeField] private KeyHolder keyHolder;

    private Transform container;
    private Transform keyTemplate;

    private void Awake()
    {
        container = transform.Find("container");
        keyTemplate = container.Find("keyTemplate");
        keyTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        keyHolder.OnKeysChanged += KeyHolder_OnKeysChanged;
    }

    private void KeyHolder_OnKeysChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        // Clean up old keys
        foreach (Transform child in container)
        {
            if (child == keyTemplate) continue;
            Destroy(child.gameObject);
        }

        // Instantiate current key list
        List<Key.KeyType> keyList = keyHolder.GetKeyList();
        container.GetComponent<RectTransform>().anchoredPosition = new Vector2(-(keyList.Count - 1) * 60 / 2f, 0);
        for (int i = 0; i < keyList.Count; i++)
        {
            Key.KeyType keyType = keyList[i];
            Transform keyTransform = Instantiate(keyTemplate, container);
            keyTransform.gameObject.SetActive(true);
            keyTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(75 * i, 0);
            Image keyImage = keyTransform.Find("image").GetComponent<Image>();
            switch (keyType)
            {
                default:
                case Key.KeyType.Red:
                    Color color1 = keyImage.color;
                    color1 = Color.red; 
                    color1.a = 0.5f;
                    keyImage.color = color1;
                    break;
                case Key.KeyType.Green:
                    Color color2 = keyImage.color;
                    color2 = Color.green;
                    color2.a = 0.5f;
                    keyImage.color = color2;
                    break;
                case Key.KeyType.Blue:
                    Color color3 = keyImage.color;
                    color3 = Color.blue;
                    color3.a = 0.5f;
                    keyImage.color = color3;
                    break;
                case Key.KeyType.Gray:
                    Color color4 = keyImage.color;
                    color4 = Color.gray;
                    color4.a = 0.5f;
                    keyImage.color = color4;
                    break;
                case Key.KeyType.Cyan:
                    Color color5 = keyImage.color;
                    color5 = Color.cyan;
                    color5.a = 0.5f;
                    keyImage.color = color5;
                    break;
                case Key.KeyType.Yellow:
                    Color color6 = keyImage.color;
                    color6 = Color.yellow;
                    color6.a = 0.5f;
                    keyImage.color = color6;
                    break;
                case Key.KeyType.Magenta:
                    Color color7 = keyImage.color;
                    color7 = Color.magenta;
                    color7.a = 0.5f;
                    keyImage.color = color7;
                    break;
                case Key.KeyType.Black:
                    Color color8 = keyImage.color;
                    color8 = Color.black;
                    color8.a = 0.5f;
                    keyImage.color = color8;
                    break;
            }
        }
    }
}
