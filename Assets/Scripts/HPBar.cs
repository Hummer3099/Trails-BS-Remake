using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public float currentHealth = 5;
    public float maxHealth = 10;
    public Image HealthBar;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHPBar();
    }
    void UpdateHPBar()
    {
        float calcHP = currentHealth / maxHealth;
        HealthBar.transform.localScale = new Vector3(Mathf.Clamp(calcHP, 0, 1), HealthBar.transform.transform.localScale.y, HealthBar.transform.transform.localScale.z);
    }
}
