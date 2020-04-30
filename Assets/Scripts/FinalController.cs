using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class FinalController : MonoBehaviour
{
    public bool test_gun;
    public Text TimerText;
    public SimpleTouchController leftController;
    public SimpleTouchController rightController;
    public float Speed;
    public int kills = 0;
    private float GunTypeTimer;
    private float sniperTimer;
    private bool sniperPrepared;
    private float otherTimer;
    private float reloadTimer;
    private float speedMovements;
    private Rigidbody2D _rigidbody;
    private bool fire = false;
    private AudioSource source;
    private Vector2 aimDirection;
    public SaveData scData;
    private GameObject[] GunType;
    public int gunType;
    public int gunNumbers;
    private int pre;
    public LineRenderer lineRenderer;


    private int counting = 0;
    [Header("Text")]
    public Text countText;
    public Text goalText;

    [Header("Gun")]
    public GameObject tps;
    public GameObject xm;
    public GameObject ak47;
    public GameObject de;
    public GameObject awm;
    public GameObject p90;
    public GameObject m4a1;
    public GameObject mac10;
    public GameObject aug;
    public GameObject ump;
    public GameObject mp5;
    public GameObject mp40;
    public GameObject m4a4;
    public GameObject mx;
    public GameObject fn;
    public GameObject glk;
    public GameObject m9;
    public GameObject rev;
    public GameObject m24;
    public GameObject ssg;
    public GameObject nv;
    public GameObject m1;
    public GameObject brt;
    public GameObject gtl;
    public GameObject g11;
    public GameObject mk5;
    public GameObject r93;
    public GameObject m1911;
    public GameObject svd;
    public GameObject mg4;
    public GameObject aw50;
    public GameObject gm6;
    public GameObject m1919;
    public GameObject bm2;
    public GameObject cht;
    void Start()
    {
        if (File.Exists(Application.persistentDataPath + "/TLDemo.data"))
        {
            string jsonStr = JsonUtility.ToJson(scData);
            PlayerPrefs.SetString("SAVE_DATA", jsonStr);
            string temString;
            string PATH1 = Application.persistentDataPath + "/TLDemo.data";
            StreamReader streamReader1 = new StreamReader(PATH1);
            string sc = streamReader1.ReadToEnd();
            streamReader1.Close();
            if (sc.Length > 0)
            {
                scData = JsonUtility.FromJson<SaveData>(sc);
                temString = scData.ToString();
                temString = temString.Remove(0, 1);
                kills = IsNumeric(temString);
            }
        }
        _rigidbody = GetComponent<Rigidbody2D>();
        source = GetComponent<AudioSource>();
        GunType = new GameObject[gunNumbers];
        GunType[0] = tps;
        GunType[1] = xm;
        GunType[2] = ak47;
        GunType[3] = de;
        GunType[4] = awm;
        GunType[5] = p90;
        GunType[6] = m4a1;
        GunType[7] = mac10;
        GunType[8] = aug;
        GunType[9] = ump;
        GunType[10] = mp5;
        GunType[11] = mp40;
        GunType[12] = m4a4;
        GunType[13] = mx;
        GunType[14] = fn;
        GunType[15] = glk;
        GunType[16] = m9;
        GunType[17] = rev;
        GunType[18] = m24;
        GunType[19] = ssg;
        GunType[20] = nv;
        GunType[21] = m1;
        GunType[22] = brt;
        GunType[23] = gtl;
        GunType[24] = g11;
        GunType[25] = mk5;
        GunType[26] = r93;
        GunType[27] = m1911;
        GunType[28] = svd;
        GunType[29] = mg4;
        GunType[30] = aw50;
        GunType[31] = gm6;
        GunType[32] = m1919;
        GunType[33] = bm2;
        GunType[34] = cht;
        SwitchGun(GunType, gunType);
        if(!test_gun)
        {
            gunType = ((int)Random.Range(0.01f, (float)gunNumbers - 0.01f) % gunNumbers);
            pre = gunType;
            SwitchGun(GunType, gunType);
        }
        speedMovements = GunType[gunType].GetComponent<Gun>().runSpeedCoefficient * Speed;
        counting = GunType[gunType].GetComponent<Gun>().ammunition;
        GunTypeTimer = Random.Range(20, 60);
        sniperTimer = 5f;
        reloadTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        TimerText.text = GunTypeTimer.ToString("f2") + "s";
        sniperTimer += Time.deltaTime;
        GunTypeTimer -= Time.deltaTime;
        if (GunTypeTimer < 0)
        {
            if (!test_gun)
            {
                gunType = ((int)Random.Range(0.01f, (float)gunNumbers - 0.01f) % gunNumbers);
                if (gunType == pre)
                {
                    if (gunType < gunNumbers - 1)
                    {
                        gunType++;
                    }
                    if (gunType == gunNumbers - 1)
                    {
                        gunType = 0;
                    }
                }
                pre = gunType;
                pre = gunType;
                SwitchGun(GunType, gunType);
                speedMovements = GunType[gunType].GetComponent<Gun>().runSpeedCoefficient * Speed;
                counting = GunType[gunType].GetComponent<Gun>().ammunition;
                sniperTimer = 5f;
                reloadTimer = 0;
            }
            GunTypeTimer = Random.Range(20, 60);
        }
        if (test_gun)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                if (gunType < gunNumbers - 1)
                {
                    gunType++;
                }
                if (gunType == gunNumbers - 1)
                {
                    gunType = 0;
                }
                pre = gunType;
                SwitchGun(GunType, gunType);
                speedMovements = GunType[gunType].GetComponent<Gun>().runSpeedCoefficient * Speed;
                counting = GunType[gunType].GetComponent<Gun>().ammunition;
                sniperTimer = 5f;
                reloadTimer = 0;
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                test_gun = false;
            }
        }
        if (!test_gun)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                test_gun = true;
            }
        }

        if((int)GunType[gunType].GetComponent<Gun>().gunType !=3)
        {
            lineRenderer.SetPosition(0, _rigidbody.position);
            lineRenderer.SetPosition(1, _rigidbody.position);
        }

        scData.kill = kills;
        string jsonStr = JsonUtility.ToJson(scData);
        PlayerPrefs.SetString("SAVE_DATA", jsonStr);
        string PATH = Application.persistentDataPath + "/TLDemo.data";
        StreamWriter streamWriter = new StreamWriter(PATH);
        streamWriter.Write(jsonStr);
        streamWriter.Close();

        float moveX = leftController.GetTouchPosition.x;
        float moveY = leftController.GetTouchPosition.y;
        Vector2 moveVector = new Vector2(moveX, moveY);
        Vector2 position = _rigidbody.position;
        position += moveVector * speedMovements * Time.deltaTime;
        _rigidbody.MovePosition(position);

        countText.text = "Kills: " + kills;
        goalText.text = "Goal: " + (50 * (kills / 50) + 50);
    }

    private void FixedUpdate()
    {
        if (rightController.GetTouchPosition.x == 0 && rightController.GetTouchPosition.y == 0)
        {
            fire = false;
            otherTimer = -1;
            sniperPrepared = true;
            lineRenderer.SetPosition(0, _rigidbody.position);
            lineRenderer.SetPosition(1, _rigidbody.position);
        }
        else
        {
            fire = true;
            otherTimer++;
        }
        if (counting == 0)
        {
            lineRenderer.SetPosition(0, _rigidbody.position);
            lineRenderer.SetPosition(1, _rigidbody.position);
        }
        if (fire == true)
        {
            Fire();
        }
        if (counting <= 0 && ((int)GunType[gunType].GetComponent<Gun>().gunType == 1
            || (int)GunType[gunType].GetComponent<Gun>().gunType == 2
            || (int)GunType[gunType].GetComponent<Gun>().gunType == 3
            || (int)GunType[gunType].GetComponent<Gun>().gunType == 4))
        {
            if (reloadTimer == 0)
            {
                source.PlayOneShot(GunType[gunType].GetComponent<Gun>().reloadSound, 1F);
            }
            reloadTimer += Time.deltaTime;
            if (reloadTimer >= GunType[gunType].GetComponent<Gun>().reloadTime)
            {
                counting = GunType[gunType].GetComponent<Gun>().ammunition;
                reloadTimer = 0;
            }
        }
    }

    public void Fire()
    {
        if (otherTimer % GunType[gunType].GetComponent<Gun>().fireGap == 0 && (int)GunType[gunType].GetComponent<Gun>().gunType == 1 && counting > 0)
        {
            if (leftController.GetTouchPosition.x != 0 || leftController.GetTouchPosition.y != 0)
            {
                aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange));
            }
            else
            {
                aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient));
            }
            if (aimDirection.x != 0 || aimDirection.y != 0)
            {
                GameObject bullet = Instantiate(GunType[gunType].GetComponent<Gun>().bulletType, _rigidbody.position, Quaternion.identity);
                source.PlayOneShot(GunType[gunType].GetComponent<Gun>().fireSound, 1F);
                counting--;
                BulletController bc = bullet.GetComponent<BulletController>();
                if (bc != null)
                {
                    bc.Move(aimDirection / rightController.GetTouchPosition.magnitude);
                }
            }
        }
        if (otherTimer % GunType[gunType].GetComponent<Gun>().fireGap == 0 && (int)GunType[gunType].GetComponent<Gun>().gunType == 2 && counting > 0)
        {
            for (int i = 0; i < GunType[gunType].GetComponent<Gun>().shotGunNum; i++)
            {
                if (aimDirection.x != 0 || aimDirection.y != 0)
                {
                    GameObject bullet1 = Instantiate(GunType[gunType].GetComponent<Gun>().bulletType, _rigidbody.position, Quaternion.identity);
                    BulletController bc1 = bullet1.GetComponent<BulletController>();
                    if (bc1 != null)
                    {
                        bc1.Move(aimDirection / rightController.GetTouchPosition.magnitude);
                    }
                }
                aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange));
            }
            source.PlayOneShot(GunType[gunType].GetComponent<Gun>().fireSound, 1F);
            counting--;
        }
        if ((int)GunType[gunType].GetComponent<Gun>().gunType == 3 && counting > 0)
        {
            aimDirection = new Vector2(rightController.GetTouchPosition.x, rightController.GetTouchPosition.y);
            RaycastHit2D hitInfo = Physics2D.Raycast(_rigidbody.position, aimDirection, 2000f, LayerMask.GetMask("Aim"));
            lineRenderer.SetPosition(0, _rigidbody.position);
            if (hitInfo.collider == null && sniperPrepared == true && sniperTimer >= GunType[gunType].GetComponent<Gun>().sniperFireGap)
            {
                lineRenderer.SetPosition(1, (_rigidbody.position + aimDirection * 2000));
            }
            if (hitInfo.collider != null && sniperPrepared == true && sniperTimer >= GunType[gunType].GetComponent<Gun>().sniperFireGap)
            {
                lineRenderer.SetPosition(1, hitInfo.point);
            }
            if (sniperPrepared == false || sniperTimer < GunType[gunType].GetComponent<Gun>().sniperFireGap)
            {
                lineRenderer.SetPosition(0, _rigidbody.position);
                lineRenderer.SetPosition(1, _rigidbody.position);
            }
            if (hitInfo.collider != null)
            {
                if (hitInfo.collider.tag == "Enemy")
                {
                    if (sniperPrepared == true && sniperTimer >= GunType[gunType].GetComponent<Gun>().sniperFireGap)
                    {
                        GameObject bullet = Instantiate(GunType[gunType].GetComponent<Gun>().bulletType, _rigidbody.position, Quaternion.identity);
                        source.PlayOneShot(GunType[gunType].GetComponent<Gun>().fireSound, 1F);
                        counting--;
                        BulletController bc = bullet.GetComponent<BulletController>();
                        if (bc != null)
                        {
                            bc.Move(aimDirection / rightController.GetTouchPosition.magnitude);
                        }
                        Destroy(hitInfo.collider.gameObject);
                        kills++;
                        sniperPrepared = false;
                        sniperTimer = 0;
                    }
                }
            }
        }
        if (otherTimer % GunType[gunType].GetComponent<Gun>().fireGap == 0 && (int)GunType[gunType].GetComponent<Gun>().gunType == 4 && counting > 0)
        {
            if (leftController.GetTouchPosition.x != 0 || leftController.GetTouchPosition.y != 0)
            {
                aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange));
            }
            else
            {
                aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange / GunType[gunType].GetComponent<Gun>().staticHorizontalRangeCoefficient));
            }
            if ((aimDirection.x != 0 || aimDirection.y != 0) && sniperTimer >= GunType[gunType].GetComponent<Gun>().rifleFireGap)
            {
                GameObject bullet = Instantiate(GunType[gunType].GetComponent<Gun>().bulletType, _rigidbody.position, Quaternion.identity);
                source.PlayOneShot(GunType[gunType].GetComponent<Gun>().fireSound, 1F);
                counting--;
                BulletController bc = bullet.GetComponent<BulletController>();
                if (bc != null)
                {
                    bc.Move(aimDirection / rightController.GetTouchPosition.magnitude);
                }
                RaycastHit2D hitInfo = Physics2D.Raycast(_rigidbody.position, aimDirection, 2000f, LayerMask.GetMask("Aim"));
                if (hitInfo.collider != null)
                {
                    if (hitInfo.collider.tag == "Enemy")
                    {
                        Destroy(hitInfo.collider.gameObject);
                        kills++;
                    }
                }
                sniperTimer = 0;
            }
        }
        if (otherTimer % GunType[gunType].GetComponent<Gun>().fireGap == 0 && (int)GunType[gunType].GetComponent<Gun>().gunType == 5)
        {
            aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange));
            if (aimDirection.x != 0 || aimDirection.y != 0)
            {
                GameObject bullet = Instantiate(GunType[gunType].GetComponent<Gun>().bulletType, _rigidbody.position, Quaternion.identity);
                source.PlayOneShot(GunType[gunType].GetComponent<Gun>().fireSound, 1F);
                BulletController bc = bullet.GetComponent<BulletController>();
                if (bc != null)
                {
                    bc.Move(aimDirection / rightController.GetTouchPosition.magnitude);
                }
            }
        }
        if (otherTimer % GunType[gunType].GetComponent<Gun>().fireGap == 0 && (int)GunType[gunType].GetComponent<Gun>().gunType == 6)
        {
            for (int i = 0; i < GunType[gunType].GetComponent<Gun>().shotGunNum; i++)
            {
                if (aimDirection.x != 0 || aimDirection.y != 0)
                {
                    GameObject bullet1 = Instantiate(GunType[gunType].GetComponent<Gun>().bulletType, _rigidbody.position, Quaternion.identity);
                    BulletController bc1 = bullet1.GetComponent<BulletController>();
                    if (bc1 != null)
                    {
                        bc1.Move(aimDirection / rightController.GetTouchPosition.magnitude);
                    }
                }
                aimDirection = new Vector2(rightController.GetTouchPosition.x * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange), rightController.GetTouchPosition.y * Random.Range(1 - GunType[gunType].GetComponent<Gun>().horizontalRange, 1 + GunType[gunType].GetComponent<Gun>().horizontalRange));
            }
            source.PlayOneShot(GunType[gunType].GetComponent<Gun>().fireSound, 1F);
        }
    }

    public int IsNumeric(string str)
    {
        int i;
        if (str != null && System.Text.RegularExpressions.Regex.IsMatch(str, @"^-?\d+$"))
            i = int.Parse(str);
        else
            i = -1;
        return i;
    }

    public void SwitchGun(GameObject[] gunTypeArr, int gunType)
    {
        for(int i = 0; i < gunTypeArr.Length; i++)
        {
            if(gunType % gunNumbers == i)
            {
                gunTypeArr[i].SetActive(true);
            }
            else
            {
                gunTypeArr[i].SetActive(false);
            }
        }
    }

    public void ChangeGunType()
    {
        if (!test_gun)
        {
            gunType = ((int)Random.Range(0.01f, (float)gunNumbers - 0.01f) % gunNumbers);
            if (gunType == pre)
            {
                if (gunType < gunNumbers - 1)
                {
                    gunType++;
                }
                if (gunType == gunNumbers - 1)
                {
                    gunType = 0;
                }
            }
            pre = gunType;
            SwitchGun(GunType, gunType);
        }
        speedMovements = GunType[gunType].GetComponent<Gun>().runSpeedCoefficient * Speed;
        counting = GunType[gunType].GetComponent<Gun>().ammunition;
        sniperTimer = 5f;
        reloadTimer = 0;
    }
}

[System.Serializable]

public class SaveData
{
    public int kill;

    public override string ToString()
    {
        return "L" + kill;
    }
}
