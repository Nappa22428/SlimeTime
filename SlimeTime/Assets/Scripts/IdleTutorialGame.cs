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
    public double SlimeUpgrade2Power;
    public double SlimeEssence;
    public double SlimeEssenceBoost;
    public double SlimeEssenceToGet;

    //int
    public int SlimeUpgrade1Level;
    public int SlimeUpgrade2Level;
    public int SlimePerSec1Level;
    public int SlimePerSec2Level;

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
        SlimeUpgrade2Power = 5;
        //levels
        SlimeUpgrade1Level = 0;
        SlimeUpgrade2Level = 0;
        SlimePerSec1Level = 0;
        SlimePerSec2Level = 0;
        
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
        SlimeEssenceText,
        SlimeEssenceBoostText,
        //SlimeEssenceToGetText,
        SlimeUpgrade1MaxText,
        SlimeUpgrade2MaxText,
        SlimePerSec1MaxText,
        SlimePerSec2MaxText
       ;
    //tabText


    //images
    public Image UpgradeBar1;
    public Image UpgradeBar2;
    public Image UpgradeBar3;
    public Image UpgradeBar4;
    public Image slimeImage;

    //double

    //float

    //int

    public int tabSwitcher;

    //bool
    

    //canvas
   // public CanvasGroup mainMenuGroup;
   // public CanvasGroup upgradesGroup;
   // public CanvasGroup settingsGroup;

    //sprites
    //public Sprite slime;
    
    

    //game objects
    public GameObject slimeGO;

    //public SpriteRenderer slimeRenderer;

    //Colors array for color changer
    public static int numColors = 1;
    public Color[] colors = new Color[numColors];

    public void Start()
    {
        Application.targetFrameRate = 60;

        //CanvasGroupChanger(true, mainMenuGroup);
        //CanvasGroupChanger(false, upgradesGroup);
        //CanvasGroupChanger(false, settingsGroup);

        //slimeGO.GetComponent<SpriteRenderer>().spriter = slimeRenderer;

        tabSwitcher = 0;

        SaveSystem.LoadPlayer(ref d);
        //Load();

        //Set colors in colors array (it wouldn't work otherwise, questions will remain unanswered)
        colors[0] = new Color(115 / 255f, 238 / 255f, 109 / 255f, 1); //Light green
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
        UnityEditor.EditorApplication.isPlaying = false;
    }
   
    public void Update()
    {
        d.SlimeEssenceToGet = ( 150 * System.Math.Sqrt(d.SlimeAmount / 1e7) );
        d.SlimeEssenceBoost = (d.SlimeEssence * 0.01) + 1;
        d.SlimePerSec = (d.SlimePerSec1Level + (d.SlimeUpgrade2Power * d.SlimePerSec2Level)) * d.SlimeEssenceBoost;
        d.SlimeAmount += d.SlimePerSec * Time.deltaTime;

        //value of a click
        SlimeValueText.text = "Squish\n+ " + NotationMethod(d.SlimeValue, "F0") + " Slime";

        //amount
        SlimeText.text = "Slime: " + NotationMethod(d.SlimeAmount, "F0");

        //per sec
        SlimePerSecText.text = NotationMethod(d.SlimePerSec, "F2") + " Slime/s";

        //upgrades Slime clicks
        #region
        //level 1
        var SlimeUpgrade1Cost = 10 * System.Math.Pow(1.07, d.SlimeUpgrade1Level);
        SlimeUpgrade1.text = "Slime Prodcution +1\nCost: " + NotationMethod(SlimeUpgrade1Cost, "F0") + " Slime\nlevel " + NotationMethod(d.SlimeUpgrade1Level, "F0");

        //level 2
        var SlimeUpgrade2Cost = 250 * System.Math.Pow(1.07, d.SlimeUpgrade2Level);
        SlimeUpgrade2.text = "Slime Prodcution +5\nCost: " + NotationMethod(SlimeUpgrade2Cost, "F0") + " Slime\nlevel " + NotationMethod(d.SlimeUpgrade2Level, "F0");  
        #endregion

        //upgrade Slime per sec
        #region
        //level 1
        var SlimePerSec1Cost = 50 * System.Math.Pow(1.07, d.SlimePerSec1Level);
        SlimeProduction1.text = "Slime Per Sec +" + NotationMethod(d.SlimeEssenceBoost, "F0") + "\nCost: " + NotationMethod(SlimePerSec1Cost, "F0") + " Slime\nlevel " + NotationMethod(d.SlimePerSec1Level, "F0");
        //level 2
        var SlimePerSec2Cost = 500 * System.Math.Pow(1.07, d.SlimePerSec2Level);
        SlimeProduction2.text = "Slime Per Sec +" + NotationMethod((d.SlimeUpgrade2Power * d.SlimeEssenceBoost), "F0")+ "\nCost: " + NotationMethod(SlimePerSec2Cost, "F0") + " Slime\nlevel " + NotationMethod(d.SlimePerSec2Level, "F0");
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
        SlimePerSec1MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(50, d.SlimeAmount, d.SlimePerSec1Level), "F0") + ")";
        //qi per sec 2
        SlimePerSec2MaxText.text = "Buy Max (" + NotationMethod(buyMaxCount(500, d.SlimeAmount, d.SlimePerSec2Level), "F0") + ")";

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
        #endregion

        //test color changing
        if (Input.GetKeyDown("c"))
        {
            ChangeColor(0);
        }
   
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
            d.SlimeUpgrade2Power = 5;

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
    public void ChangeColor(int color)
    {
        Debug.Log("reached method to change color " + colors[color]);
        slimeImage.color = colors[color];
    }

    #region
    public void SlimeClicker()
    {
        d.SlimeAmount += d.SlimeValue;
        slimeGO.transform.Rotate(0, 0, (transform.rotation.z - 5));
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
                var cost3 = 50 * System.Math.Pow(1.07, d.SlimePerSec1Level);
                if (d.SlimeAmount >= cost3)
                {
                    d.SlimePerSec1Level++;
                    d.SlimeAmount -= cost3;
                    cost3 *= 1.07;
                }
                break;
            case "SP1Max":
                var b3 = 50;
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
                var cost4 = 500 * System.Math.Pow(1.07, d.SlimePerSec2Level);
                if (d.SlimeAmount >= cost4)
                {
                    d.SlimePerSec2Level++;
                    d.SlimeAmount -= cost4;
                    cost4 *= 1.07;
                }
                break;
            case "SP2Max":
                var b4 = 500;
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
            default:
                Debug.Log("Bruh");
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

    public void FullReset()
    {
        d.FullReset();
    }
    #endregion
}
