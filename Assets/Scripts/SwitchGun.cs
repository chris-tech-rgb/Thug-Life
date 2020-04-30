using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchGun : MonoBehaviour
{
    // Start is called before the first frame update
    public Button switchButton;


    // Update is called once per frame
    void Update()
    {
        switchButton.onClick.AddListener(OnClickSwitch);
    }
    private void OnClickSwitch()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<FinalController>().ChangeGunType();
    }
}
