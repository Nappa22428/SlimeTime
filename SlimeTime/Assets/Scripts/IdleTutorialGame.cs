using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class PlayerData
{
    //doubles
    public double SlimeAmount;
    public double SlimePerSec;
    public double SlimeValue;
    //public double SlimeUpgrade2Power;
    public double SlimeEssence;
    public double SlimeEssenceBoost;
    public double SlimeEssenceToGet;

    //int
    public int SlimeUpgrade1Level;
    public int SlimeUpgrade2Level;
    public int SlimePerSec1Level, SlimePerSec2Level, SlimePerSec3Level, SlimePerSec4Level, SlimePerSec5Level, SlimePerSec6Level, SlimePerSec7Level, SlimePerSec8Level;


    public int evolutionSlime;

    public bool iSlime = false, fSlime = false, mSlime = false, cSlime = false, dSlime = false, aSlime = false, pSlime = false;
    public PlayerData()
    {
        FullReset();
    }

    public void FullReset()
    {
        //starting values
        SlimeAmount = 0;
        SlimeValue = 1;
        SlimeEssence = 0;
        SlimeEssenceBoost = 1;
        //qipersec
        //SlimeUpgrade2Power = 5;
        //levels
        SlimeUpgrade1Level = 0;
        SlimeUpgrade2Level = 0;
        SlimePerSec1Level = 0;
        SlimePerSec2Level = 0;
        SlimePerSec3Level = 0;
        SlimePerSec4Level = 0;
        SlimePerSec5Level = 0;
        SlimePerSec6Level = 0;
        SlimePerSec7Level = 0;
        SlimePerSec8Level = 0;
        evolutionSlime = 0;

        iSlime = false;
        fSlime = false;
        mSlime = false;
        cSlime = false;
        aSlime = false;
        dSlime = false;
        pSlime = false;

    }
}


public class IdleTutorialGame : MonoBehaviour
{ 
    public PlayerData d;
    //text
    public TextMeshProUGUI SlimeText, 
        SlimeValueText, 
        SlimePerSecText, 
        SlimeUpgrade1, 
        SlimeUpgrade2, 
        SlimeProduction1, 
        SlimeProduction2,
        SlimeProduction3,
        SlimeProduction4,
        SlimeProduction5,
        SlimeProduction6,
        SlimeProduction7,
        SlimeProduction8,
        SlimeEssenceText,
        SlimeEssenceBoostText,
        //SlimeEssenceToGetText,
        SlimeUpgrade1MaxText,
        SlimeUpgrade2MaxText,
        SlimePerSec1MaxText,
        SlimePerSec2MaxText,
        SlimePerSec3MaxText,
        SlimePerSec4MaxText,
        SlimePerSec5MaxText,
        SlimePerSec6MaxText,
        SlimePerSec7MaxText,
        SlimePerSec8MaxText
       ;
    //tabText


    //images
    public Image UpgradeBar1;
    public Image UpgradeBar2;
    public Image UpgradeBar3;
    public Image UpgradeBar4;
    public Image UpgradeBar5;
    public Image UpgradeBar6;
    public Image UpgradeBar7;
    public Image UpgradeBar8;
    public Image UpgradeBar9;
    public Image UpgradeBar10;

    public Image slimeImage;

    
    public Sprite Slime, Metal, Angel,Cowboy, Devil,Ice, Fire, Pudding;
    //double

    //float

    //int

    //public int tabSwitcher;

    //bool
    

    //canvas
    

    //sprites
    //public Sprite slime;
    
    

    //game objects
    public GameObject slimeGO;
    public GameObject upgrades;
    public GameObject evolution;

    public GameObject Sp1, Sp2, Sp3, Sp4, Sp5, Sp6, Sp7, Sp8;
    public bool upgradeSwitch = false;
    public bool evolutionSwitch = false;
    //public SpriteRenderer slimeRenderer;

    //Colors array for color changer

    //public Color[] colors;

    public void Start()
    {
        Application.targetFrameRate = 60;

        //CanvasGroupChanger(true, mainMenuGroup);
        //CanvasGroupChanger(false, upgradesGroup);
        //CanvasGroupChanger(false, settingsGroup);

        //slimeGO.GetComponent<SpriteRenderer>().spriter = slimeRenderer;

        //tabSwitcher = 0;

        SaveSystem.LoadPlayer(ref d);
        //Load();

        //Set colors in colors array (it wouldn't work otherwise, questions will remain unanswered)
        //colors[0] = new Color(90 / 255f, 154 / 255f, 43 / 255f, 1); //Green
        //colors[1] = new Color(161 / 255f, 25 / 255f, 17 / 255f, 1); //Red
        //colors[2] = new Color(15 / 255f, 100 / 255f, 140 / 255f, 1); //Blue
    }
    public void Upgrades()
    {
        upgradeSwitch ^= true;
        evolutionSwitch = false;
        upgrades.SetActive(upgradeSwitch);
        evolution.SetActive(evolutionSwitch);
    }
    public void Evolution()
    {
        evolutionSwitch ^= true;
        upgradeSwitch = false;
        upgrades.SetActive(upgradeSwitch);
        evolution.SetActive(evolutionSwitch);
    }
    public void CanvasGroupChanger(bool x, CanvasGroup y)
    {
        if (x)
        {
            y.alpha = 1;
            y.interactable = true;
            y.blocksRaycasts = true;
            return;
        }
        y.alpha = 0;
        y.interactable = false;
        y.blocksRaycasts = false;
    }

    /*public void ChangeTabs(int tS)
    {
        switch(tS)
        {
            case 0:
                //upgrades
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(true, upgradesGroup);
                CanvasGroupChanger(false, settingsGroup);


                break;
            case 1:
                //menu
                CanvasGroupChanger(true, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(false, settingsGroup);

                break;
            case 2:
                //settings
                CanvasGroupChanger(false, mainMenuGroup);
                CanvasGroupChanger(false, upgradesGroup);
                CanvasGroupChanger(true, settingsGroup);
                break;

        }
    }
   */ 
   
    public void SaveNQuit()
    {
        SaveSystem.SavePlayer(d);
        Application.Quit();
        //get rid of when published
       // UnityEditor.EditorApplication.isPlaying = false;
    }
   
    public void Update()
    {

        updateSlime();
        updateUpgrades();
        d.SlimeEssenceToGet = ( 150 * System.Math.Sqrt(d.SlimeAmount / 1e7) );
        d.SlimeEssenceBoost = (d.SlimeEssence * 0.01) + 1;
        d.SlimePerSec = ((0.1 *d.SlimePerSec1Level) + (5 * d.SlimePerSec2Level) + (25 * d.SlimePerSec3Level) + (50 * d.SlimePerSec4Level) + (100 * d.SlimePerSec5Level) + (250 * d.SlimePerSec6Level) + (500 * d.SlimePerSec7Level) + (1000 * d.SlimePerSec8Level)) ;
        d.SlimeAmount += d.SlimePerSec * Time.deltaTime;

        //value of a click
        SlimeValueText.text = "Squish\n+ " + NotationMethod(d.SlimeValue, "F0") + " SC";

        //amount
        SlimeText.text = "Slime Chunks: " + NotationMethod(d.SlimeAmount, "F0");

        //per sec
        SlimePerSecText.text = NotationMethod(d.SlimePerSec, "F2") + " SC/s";

        //upgrades Slime clicks
        #region
        //level 1
        var SlimeUpgrade1Cost = 10 * System.Math.Pow(1.07, d.SlimeUpgrade1Level);
        SlimeUpgrade1.text = "Slime Prodcution +1\nCost: " + NotationMethod(SlimeUpgrade1Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimeUpgrade1Level, "F0");

        //level 2
        var SlimeUpgrade2Cost = 250 * System.Math.Pow(1.07, d.SlimeUpgrade2Level);
        SlimeUpgrade2.text = "Slime Prodcution +5\nCost: " + NotationMethod(SlimeUpgrade2Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimeUpgrade2Level, "F0");  
        #endregion

        //upgrade Slime per sec
        #region
        //level 1
        var SlimePerSec1Cost = 25 * System.Math.Pow(1.07, d.SlimePerSec1Level);
        SlimeProduction1.text = "SC Per Sec + 0.1\nCost: " + NotationMethod(SlimePerSec1Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec1Level, "F0");
        //level 2
        var SlimePerSec2Cost = 75 * System.Math.Pow(1.07, d.SlimePerSec2Level);
        SlimeProduction2.text = "SC Per Sec + 5 \nCost: " + NotationMethod(SlimePerSec2Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec2Level, "F0");
        //level 3
        var SlimePerSec3Cost = 250 * System.Math.Pow(1.07, d.SlimePerSec1Level);
        SlimeProduction3.text = "SC Per Sec + 25\nCost: " + NotationMethod(SlimePerSec3Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec3Level, "F0");
        //level 4
        var SlimePerSec4Cost = 1000 * System.Math.Pow(1.07, d.SlimePerSec2Level);
        SlimeProduction4.text = "SC Per Sec + 50\nCost: " + NotationMethod(SlimePerSec4Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec4Level, "F0");
        //level 5
        var SlimePerSec5Cost = 2500 * System.Math.Pow(1.07, d.SlimePerSec1Level);
        SlimeProduction5.text = "SC Per Sec + 100\nCost: " + NotationMethod(SlimePerSec5Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec5Level, "F0");
        //level 6
        var SlimePerSec6Cost = 5000 * System.Math.Pow(1.07, d.SlimePerSec2Level);
        SlimeProduction6.text = "SC Per Sec + 250\nCost: " + NotationMethod(SlimePerSec6Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec6Level, "F0");
        //level 7
        var SlimePerSec7Cost = 10000 * System.Math.Pow(1.07, d.SlimePerSec1Level);
        SlimeProduction7.text = "SC Per Sec + 500\nCost: " + NotationMethod(SlimePerSec7Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec7Level, "F0");
        //level 8
        var SlimePerSec8Cost = 25000 * System.Math.Pow(1.07, d.SlimePerSec2Level);
        SlimeProduction8.text = "SC Per Sec + 1000\nCost: " + NotationMethod(SlimePerSec8Cost, "F0") + " SC\nlevel " + NotationMethod(d.SlimePerSec8Level, "F0");

        #endregion

        //karma
        #region
        SlimeEssenceText.text = "Slime Essence: " + NotationMethod(d.SlimeEssence, "F0");

        //karma to get
        //SlimeEssenceToGetText.text = "Reincarnate\n+" + NotationMethod(d.SlimeEssenceToGet, "F0") + " Slime Essence";
        
        //karma Boost 
        SlimeEssenceBoostText.text = NotationMethod(d.SlimeEssenceBoost, "F2") + "x Boost";
        #endregion

        //upgrade max
        #region
        //upgrade 1
        SlimeUpgrade1MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(10, d.SlimeAmount, d.SlimeUpgrade1Level), "F0") + ")";
        //upgrade 2
        SlimeUpgrade2MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(250, d.SlimeAmount, d.SlimeUpgrade2Level), "F0") + ")";

        //qi per sec 1
        SlimePerSec1MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(25, d.SlimeAmount, d.SlimePerSec1Level), "F0") + ")";
        //qi per sec 2
        SlimePerSec2MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(75, d.SlimeAmount, d.SlimePerSec2Level), "F0") + ")";
        //qi per sec 3
        SlimePerSec3MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(250, d.SlimeAmount, d.SlimePerSec3Level), "F0") + ")";
        //qi per sec 4
        SlimePerSec4MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(1000, d.SlimeAmount, d.SlimePerSec4Level), "F0") + ")";
        //qi per sec 5
        SlimePerSec5MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(2500, d.SlimeAmount, d.SlimePerSec5Level), "F0") + ")";
        //qi per sec 6
        SlimePerSec6MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(5000, d.SlimeAmount, d.SlimePerSec6Level), "F0") + ")";
        //qi per sec 7
        SlimePerSec7MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(10000, d.SlimeAmount, d.SlimePerSec7Level), "F0") + ")";
        //qi per sec 8
        SlimePerSec8MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(25000, d.SlimeAmount, d.SlimePerSec8Level), "F0") + ")";

        #endregion

        //bars for upgrade 
        #region
        //bar 1
        #region
        if (d.SlimeAmount / SlimeUpgrade1Cost < 0.01)
        {
            UpgradeBar1.fillAmount = 0;
        }
        else if(d.SlimeAmount / SlimeUpgrade1Cost > 10)
        {
            UpgradeBar1.fillAmount = 1;
        }
        else
        {
            UpgradeBar1.fillAmount = (float)(d.SlimeAmount / SlimeUpgrade1Cost);
        }
        #endregion
        //bar 2
        #region

        if (d.SlimeAmount / SlimeUpgrade2Cost < 0.01)
        {
            UpgradeBar2.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimeUpgrade2Cost > 10)
        {
            UpgradeBar2.fillAmount = 1;
        }
        else
        {
            UpgradeBar2.fillAmount = (float)(d.SlimeAmount / SlimeUpgrade2Cost);
        }
        #endregion
        //bar 3
        #region
        if (d.SlimeAmount / SlimePerSec1Cost < 0.01)
        {
            UpgradeBar3.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec1Cost > 10)
        {
            UpgradeBar3.fillAmount = 1;
        }
        else
        {
            UpgradeBar3.fillAmount = (float)(d.SlimeAmount / SlimePerSec1Cost);
        }
        #endregion
        //bar 4
        #region
        if (d.SlimeAmount / SlimePerSec2Cost < 0.01)
        {
            UpgradeBar4.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec2Cost > 10)
        {
            UpgradeBar4.fillAmount = 1;
        }
        else
        {
            UpgradeBar4.fillAmount = (float)(d.SlimeAmount / SlimePerSec2Cost);
        }
        #endregion
        //bar 3
        #region
        if (d.SlimeAmount / SlimePerSec3Cost < 0.01)
        {
            UpgradeBar5.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec3Cost > 10)
        {
            UpgradeBar5.fillAmount = 1;
        }
        else
        {
            UpgradeBar5.fillAmount = (float)(d.SlimeAmount / SlimePerSec3Cost);
        }
        #endregion
        //bar 4
        #region
        if (d.SlimeAmount / SlimePerSec4Cost < 0.01)
        {
            UpgradeBar6.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec4Cost > 10)
        {
            UpgradeBar6.fillAmount = 1;
        }
        else
        {
            UpgradeBar6.fillAmount = (float)(d.SlimeAmount / SlimePerSec4Cost);
        }
        #endregion
        //bar 3
        #region
        if (d.SlimeAmount / SlimePerSec5Cost < 0.01)
        {
            UpgradeBar7.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec5Cost > 10)
        {
            UpgradeBar7.fillAmount = 1;
        }
        else
        {
            UpgradeBar7.fillAmount = (float)(d.SlimeAmount / SlimePerSec5Cost);
        }
        #endregion
        //bar 4
        #region
        if (d.SlimeAmount / SlimePerSec6Cost < 0.01)
        {
            UpgradeBar8.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec6Cost > 10)
        {
            UpgradeBar8.fillAmount = 1;
        }
        else
        {
            UpgradeBar8.fillAmount = (float)(d.SlimeAmount / SlimePerSec6Cost);
        }
        #endregion
        //bar 3
        #region
        if (d.SlimeAmount / SlimePerSec7Cost < 0.01)
        {
            UpgradeBar9.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec7Cost > 10)
        {
            UpgradeBar9.fillAmount = 1;
        }
        else
        {
            UpgradeBar9.fillAmount = (float)(d.SlimeAmount / SlimePerSec7Cost);
        }
        #endregion
        //bar 4
        #region
        if (d.SlimeAmount / SlimePerSec8Cost < 0.01)
        {
            UpgradeBar10.fillAmount = 0;
        }
        else if (d.SlimeAmount / SlimePerSec8Cost > 10)
        {
            UpgradeBar10.fillAmount = 1;
        }
        else
        {
            UpgradeBar10.fillAmount = (float)(d.SlimeAmount / SlimePerSec8Cost);
        }
        #endregion
        #endregion

        //test color changing


        SaveSystem.SavePlayer(d);
    }
    //double
    public string NotationMethod(double x, string y)
    {
        if (x >= 1000000000000000000)
        {
            // x is larger than 1 Quintillion
            var exponenet = System.Math.Floor(System.Math.Log10(System.Math.Abs(x)));
            var mantissa = x / System.Math.Pow(10, exponenet);
            return mantissa.ToString("F2") + "e" + exponenet;
        }
        else if (x >= 1000000000000000)
        {
            // x Q (Quadrillion)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000000000000;
            return compressed.ToString("F1") + "Q";
        }
        else if (x >= 1000000000000)
        {
            // x T (Trillion)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000000000;
            return compressed.ToString("F1") + "T";
        }
        else if (x >= 1000000000)
        {
            // x B (Billion)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000000;
            return compressed.ToString("F1") + "B";
        }
        else if (x >= 1000000)
        {
            // x M (Million)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000;
            return compressed.ToString("F1") + "M";
        }
        else if (x >= 1000)
        {
            // x K (Thousand)
            var number = System.Math.Floor(x);
            var compressed = number / 1000;
            return compressed.ToString("F1") + "K";
        }
        else
        {
            // x is less than 1k
            return x.ToString(y);
        }
    }
    //float
    public string NotationMethod(float x, string y)
    {
        if (x >= 1000000000000000000)
        {
            // x is larger than 1 Quintillion
            var exponenet = System.Math.Floor(System.Math.Log10(System.Math.Abs(x)));
            var mantissa = x / System.Math.Pow(10, exponenet);
            return mantissa.ToString("F2") + "e" + exponenet;
        }
        else if (x >= 1000000000000000)
        {
            // x Q (Quadrillion)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000000000000;
            return compressed.ToString("F1") + "Q";
        }
        else if (x >= 1000000000000)
        {
            // x T (Trillion)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000000000;
            return compressed.ToString("F1") + "T";
        }
        else if (x >= 1000000000)
        {
            // x B (Billion)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000000;
            return compressed.ToString("F1") + "B";
        }
        else if (x >= 1000000)
        {
            // x M (Million)
            var number = System.Math.Floor(x);
            var compressed = number / 1000000;
            return compressed.ToString("F1") + "M";
        }
        else if (x >= 1000)
        {
            // x K (Thousand)
            var number = System.Math.Floor(x);
            var compressed = number / 1000;
            return compressed.ToString("F1") + "K";
        }
        else
        {
            // x is less than 1k
            return x.ToString(y);
        }
    }


    //Reincarnate
    public void Reincarnate()
    {
        if (d.SlimeEssenceToGet >= 1)
        {
            //starting values
            d.SlimeAmount = 0;
            d.SlimeValue = 1;

            //qipersec
            //d.SlimeUpgrade2Power = 5;

            //levels
            d.SlimeUpgrade1Level = 0;
            d.SlimeUpgrade2Level = 0;
            d.SlimePerSec1Level = 0;
            d.SlimePerSec2Level = 0;

            d.SlimeEssence += d.SlimeEssenceToGet;
        }
    }
    //buttons

    //Change slime color
    /*public void ChangeColor(int color)
    {
        Debug.Log("reached method to change color " + colors[color]);

        if (color < colors.Length && color >= 0)
        {
            slimeImage.color = colors[color];
        }
    }*/

    #region
    public void SlimeClicker()
    {
        d.SlimeAmount += d.SlimeValue;
        //slimeGO.transform.Rotate(0, 0, (transform.rotation.z - 5));
    }

    public void buySlimeUpgrade(string upgradeID)
    {
        switch (upgradeID)
        {
            case "S1":
                var cost1 = 10 * System.Math.Pow(1.07, d.SlimeUpgrade1Level);
                if (d.SlimeAmount >= cost1)
                {
                    d.SlimeUpgrade1Level++;
                    d.SlimeAmount -= cost1;
                    cost1 *= 1.07;
                    d.SlimeValue++;
                }
                break;
            case "S1Max":
                var b1 = 10;
                var c1 = d.SlimeAmount;
                var r1 = 1.07;
                var k1 = d.SlimeUpgrade1Level;
                var n1 = System.Math.Floor(System.Math.Log(((c1 * (r1 - 1)) / (b1 * System.Math.Pow(r1, k1))) + 1, r1));

                var cm1 = b1 * (System.Math.Pow(r1, k1) * (System.Math.Pow(r1, n1) - 1) / (r1 - 1));

                if (d.SlimeAmount >= cm1)
                {
                    d.SlimeUpgrade1Level += (int)n1;
                    d.SlimeAmount -= cm1;
                    d.SlimeValue += (int)n1;
                }
                break;
            case "S2":
                var cost2 = 250 * System.Math.Pow(1.07, d.SlimeUpgrade2Level);
                if (d.SlimeAmount >= cost2)
                {
                    d.SlimeUpgrade2Level++;
                    d.SlimeAmount -= cost2;
                    cost2 *= 1.07;
                    d.SlimeValue += 5;
                }
                break;
            case "S2Max":
                var b2 = 250;
                var c2 = d.SlimeAmount;
                var r2 = 1.07;
                var k2 = d.SlimeUpgrade2Level;
                var n2 = System.Math.Floor(System.Math.Log(((c2 * (r2 - 1)) / (b2 * System.Math.Pow(r2, k2))) + 1, r2));

                var cm2 = b2 * (System.Math.Pow(r2, k2) * (System.Math.Pow(r2, n2) - 1) / (r2 - 1));

                if (d.SlimeAmount >= cm2)
                {
                    d.SlimeUpgrade2Level += (int)n2;
                    d.SlimeAmount -= cm2;
                    d.SlimeValue += ((int)n2 *5);
                }
                break;
            case "SP1":
                var cost3 = 25 * System.Math.Pow(1.07, d.SlimePerSec1Level);
                if (d.SlimeAmount >= cost3)
                {
                    d.SlimePerSec1Level++;
                    d.SlimeAmount -= cost3;
                    cost3 *= 1.07;
                }
                break;
            case "SP1Max":
                var b3 = 25;
                var c3 = d.SlimeAmount;
                var r3 = 1.07;
                var k3 = d.SlimePerSec1Level;
                var n3 = System.Math.Floor(System.Math.Log(((c3 * (r3 - 1)) / (b3 * System.Math.Pow(r3, k3))) + 1, r3));

                var cm3 = b3 * (System.Math.Pow(r3, k3) * (System.Math.Pow(r3, n3) - 1) / (r3 - 1));

                if (d.SlimeAmount >= cm3)
                {
                    d.SlimePerSec1Level += (int)n3;
                    d.SlimeAmount -= cm3;
                }
                break;
            case "SP2":
                var cost4 = 75 * System.Math.Pow(1.07, d.SlimePerSec2Level);
                if (d.SlimeAmount >= cost4)
                {
                    d.SlimePerSec2Level++;
                    d.SlimeAmount -= cost4;
                    cost4 *= 1.07;
                }
                break;
            case "SP2Max":
                var b4 = 75;
                var c4 = d.SlimeAmount;
                var r4 = 1.07;
                var k4 = d.SlimePerSec2Level;
                var n4 = System.Math.Floor(System.Math.Log(((c4 * (r4 - 1)) / (b4 * System.Math.Pow(r4, k4))) + 1, r4));

                var cm4 = b4 * (System.Math.Pow(r4, k4) * (System.Math.Pow(r4, n4) - 1) / (r4 - 1));

                if (d.SlimeAmount >= cm4)
                {
                    d.SlimePerSec2Level += (int)n4;
                    d.SlimeAmount -= cm4;
                }
                break;
            case "SP3":
                var cost5 = 250 * System.Math.Pow(1.07, d.SlimePerSec1Level);
                if (d.SlimeAmount >= cost5)
                {
                    d.SlimePerSec3Level++;
                    d.SlimeAmount -= cost5;
                    cost5 *= 1.07;
                }
                break;
            case "SP3Max":
                var b5 = 250;
                var c5 = d.SlimeAmount;
                var r5 = 1.07;
                var k5 = d.SlimePerSec3Level;
                var n5 = System.Math.Floor(System.Math.Log(((c5 * (r5 - 1)) / (b5 * System.Math.Pow(r5, k5))) + 1, r5));

                var cm5 = b5 * (System.Math.Pow(r5, k5) * (System.Math.Pow(r5, n5) - 1) / (r5 - 1));

                if (d.SlimeAmount >= cm5)
                {
                    d.SlimePerSec3Level += (int)n5;
                    d.SlimeAmount -= cm5;
                }
                break;
            case "SP4":
                var cost6 = 1000 * System.Math.Pow(1.07, d.SlimePerSec4Level);
                if (d.SlimeAmount >= cost6)
                {
                    d.SlimePerSec4Level++;
                    d.SlimeAmount -= cost6;
                    cost6 *= 1.07;
                }
                break;
            case "SP4Max":
                var b6 = 1000;
                var c6 = d.SlimeAmount;
                var r6 = 1.07;
                var k6 = d.SlimePerSec4Level;
                var n6 = System.Math.Floor(System.Math.Log(((c6 * (r6 - 1)) / (b6 * System.Math.Pow(r6, k6))) + 1, r6));

                var cm6 = b6 * (System.Math.Pow(r6, k6) * (System.Math.Pow(r6, n6) - 1) / (r6 - 1));

                if (d.SlimeAmount >= cm6)
                {
                    d.SlimePerSec4Level += (int)n6;
                    d.SlimeAmount -= cm6;
                }
                break;
            case "SP5":
                var cost7 = 2500 * System.Math.Pow(1.07, d.SlimePerSec1Level);
                if (d.SlimeAmount >= cost7)
                {
                    d.SlimePerSec5Level++;
                    d.SlimeAmount -= cost7;
                    cost7 *= 1.07;
                }
                break;
            case "SP5Max":
                var b7 = 2500;
                var c7 = d.SlimeAmount;
                var r7 = 1.07;
                var k7 = d.SlimePerSec5Level;
                var n7 = System.Math.Floor(System.Math.Log(((c7 * (r7 - 1)) / (b7 * System.Math.Pow(r7, k7))) + 1, r7));

                var cm7 = b7 * (System.Math.Pow(r7, k7) * (System.Math.Pow(r7, n7) - 1) / (r7 - 1));

                if (d.SlimeAmount >= cm7)
                {
                    d.SlimePerSec5Level += (int)n7;
                    d.SlimeAmount -= cm7;
                }
                break;
            case "SP6":
                var cost8 = 5000 * System.Math.Pow(1.07, d.SlimePerSec6Level);
                if (d.SlimeAmount >= cost8)
                {
                    d.SlimePerSec6Level++;
                    d.SlimeAmount -= cost8;
                    cost8 *= 1.07;
                }
                break;
            case "SP6Max":
                var b8 = 5000;
                var c8 = d.SlimeAmount;
                var r8 = 1.07;
                var k8 = d.SlimePerSec6Level;
                var n8 = System.Math.Floor(System.Math.Log(((c8 * (r8 - 1)) / (b8 * System.Math.Pow(r8, k8))) + 1, r8));

                var cm8 = b8 * (System.Math.Pow(r8, k8) * (System.Math.Pow(r8, n8) - 1) / (r8 - 1));

                if (d.SlimeAmount >= cm8)
                {
                    d.SlimePerSec6Level += (int)n8;
                    d.SlimeAmount -= cm8;
                }
                break;
            case "SP7":
                var cost9 = 10000 * System.Math.Pow(1.07, d.SlimePerSec7Level);
                if (d.SlimeAmount >= cost9)
                {
                    d.SlimePerSec7Level++;
                    d.SlimeAmount -= cost9;
                    cost9 *= 1.07;
                }
                break;
            case "SP7Max":
                var b9 = 10000;
                var c9 = d.SlimeAmount;
                var r9 = 1.07;
                var k9 = d.SlimePerSec7Level;
                var n9 = System.Math.Floor(System.Math.Log(((c9 * (r9 - 1)) / (b9 * System.Math.Pow(r9, k9))) + 1, r9));

                var cm9 = b9 * (System.Math.Pow(r9, k9) * (System.Math.Pow(r9, n9) - 1) / (r9 - 1));

                if (d.SlimeAmount >= cm9)
                {
                    d.SlimePerSec7Level += (int)n9;
                    d.SlimeAmount -= cm9;
                }
                break;
            case "SP8":
                var cost10 = 25000 * System.Math.Pow(1.07, d.SlimePerSec8Level);
                if (d.SlimeAmount >= cost10)
                {
                    d.SlimePerSec8Level++;
                    d.SlimeAmount -= cost10;
                    cost10 *= 1.07;
                }
                break;
            case "SP8Max":
                var b10 = 25000;
                var c10 = d.SlimeAmount;
                var r10 = 1.07;
                var k10 = d.SlimePerSec8Level;
                var n10 = System.Math.Floor(System.Math.Log(((c10 * (r10 - 1)) / (b10 * System.Math.Pow(r10, k10))) + 1, r10));

                var cm10 = b10 * (System.Math.Pow(r10, k10) * (System.Math.Pow(r10, n10) - 1) / (r10 - 1));

                if (d.SlimeAmount >= cm10)
                {
                    d.SlimePerSec8Level += (int)n10;
                    d.SlimeAmount -= cm10;
                }
                break;
            default:
                Debug.Log("Bruh");
                break;
        }
    }

    public void buySlimeEvolution(string upgradeID)
    {
        switch (upgradeID) 
        {
            case "Ice":
                if (d.SlimeAmount >= 100)
                {
                    d.evolutionSlime = 1;
                    d.SlimeAmount -= 100;
                    d.SlimeValue = 2;
                    d.iSlime = true;
                }
                break;
            case "Fire":
                if (d.SlimeAmount >= 500)
                {
                    d.evolutionSlime = 2;
                    d.SlimeAmount -= 500;
                    d.SlimeValue = 5;
                    d.fSlime = true;
                }
                break;
            case "Metal":
                if (d.SlimeAmount >= 2500)
                {
                    d.evolutionSlime = 3;
                    d.SlimeAmount -= 2500;
                    d.SlimeValue = 25;
                    d.mSlime = true;
                }
                break;
            case "Cowboy":
                if (d.SlimeAmount >= 5000)
                {
                    d.evolutionSlime = 4;
                    d.SlimeAmount -= 5000;
                    d.SlimeValue = 50;
                    d.cSlime = true;
                }
                break;
            case "Devil":
                if (d.SlimeAmount >= 6666)
                {
                    d.evolutionSlime = 5;
                    d.SlimeAmount -= 6666;
                    d.SlimeValue = 66;
                    d.dSlime = true;
                }
                break;
            case "Angel":
                if (d.SlimeAmount >= 7777)
                {
                    d.evolutionSlime = 6;
                    d.SlimeAmount -= 7777;
                    d.SlimeValue = 77;
                    d.aSlime = true;
                }
                break;
            case "Pudding":
                if (d.SlimeAmount >= 100000)
                {
                    d.evolutionSlime = 7;
                    d.SlimeAmount -= 100000;
                    d.SlimeValue = 8008;
                    d.pSlime = true;
                }
                break;


        }

    }

    public double buyMaxCount(double cost ,double amount, int level)
    {
        var b = cost;
        var c = amount;
        var r = 1.07;
        var k = level;
        var n = System.Math.Floor(System.Math.Log(((c * (r - 1)) / (b * System.Math.Pow(r, k))) + 1, r));

        return n;
    }

    public void updateSlime()
    {
        switch (d.evolutionSlime) 
        {
            case 0:
                slimeImage.sprite = Slime;
                break;
            case 1:
                slimeImage.sprite = Ice;
                break;
            case 2:
                slimeImage.sprite = Fire;
                break;
            case 3:
                slimeImage.sprite = Metal;
                break;
            case 4:
                slimeImage.sprite = Cowboy;
                break;
            case 5:
                slimeImage.sprite = Devil;
                break;
            case 6:
                slimeImage.sprite = Angel;
                break;
            case 7:
                slimeImage.sprite = Pudding;
                break;
        }

    }
    public void updateUpgrades()
    {
        if (d.iSlime)
        {
            Sp2.SetActive(true);
        }
        else
        {
            Sp2.SetActive(false);
        }
        if (d.fSlime)
        {
            Sp3.SetActive(true);
        }
        else
        {
            Sp3.SetActive(false);
        }
        if (d.mSlime)
        {
            Sp4.SetActive(true);
        }
        else
        {
            Sp4.SetActive(false);
        }
        if (d.cSlime)
        {
            Sp5.SetActive(true);
        }
        else
        {
            Sp5.SetActive(false);
        }
        if (d.dSlime)
        {
            Sp6.SetActive(true);
        }
        else
        {
            Sp6.SetActive(false);
        }
        if (d.aSlime)
        {
            Sp7.SetActive(true);
        }
        else
        {
            Sp7.SetActive(false);
        }
        if (d.pSlime)
        {
            Sp8.SetActive(true);
        }
        else
        {
            Sp8.SetActive(false);
        }
    }

    public void FullReset()
    {
        d.FullReset();
    }
    #endregion
}
