using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField] private Gun Gun;
    private TMP_Text Text;
    // Start is called before the first frame update
    void Start()
    {
        Text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Text == null)
            return;
        if(!Gun.gameObject.activeSelf)
        {
            Text.text = "";
            return;
        }
        string ammotext = Gun.Ammo + "/" + Gun.AmmoCapacity;
        Text.text = ammotext;
    }
}
