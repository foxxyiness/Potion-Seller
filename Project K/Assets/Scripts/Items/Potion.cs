using UnityEngine;
using Items;

public class Potion : MonoBehaviour
{
    [SerializeField] private GameObject toastedUpgrade;
    [SerializeField] private bool toastable;
    [SerializeField] private int roastLevel;
    [SerializeField] private string archetype;
    [SerializeField] private string baseText;
    [SerializeField] private string strengthText;
    [SerializeField] private string flavorText;
    [SerializeField] private Archetype archeType;
    [SerializeField] private Base baseType;
    [SerializeField] private Strength strengthType;
    [SerializeField] private Flavor flavorType;
    private void Start()
    {
        GetComponent<Item>();
        roastLevel = 2;
    }

    private enum Archetype
    {
        Light,
        Darkness,
        Harmony,
        Chaos,
        Coffee
    }
    private enum Base
    {
        Water,
        Light,
        Darkness,
        Harmony,
        Chaos,
        Coffee
        
    }
    private enum Strength
    {
        Sun,
        StormVine,
        PheonixBerry,
        AshCapMushroom,
        GoblinLeaf,
        Fireshot
    }
    private enum Flavor
    {
        Hair,
        IcePepper,
        BarbedBamboo,
        PixieCabbage,
        EagleBlossom,
        Toasted
    }

    public string GetArchetype()
    {
        return archetype.ToString();
    }
    public string GetToastable()
    {
        var answer = toastable ? "Yes" : "No";
        return answer;
    }
    public string GetBaseText()
    {
        return baseType.ToString();
    }
    public string GetStrengthText()
    {
        return strengthType.ToString();
    }
    public string GetFlavorText()
    {
        return flavorType.ToString();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Fire") && toastable)
        {
            roastLevel--;
            CheckRoast();
        }
    }

    private void CheckRoast()
    {
        if (roastLevel == 0)
        {
            GameObject toastedPotion = Instantiate(toastedUpgrade, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
    }
}
