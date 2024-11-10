using GridGambitProd;
using UnityEngine;

[CreateAssetMenu(fileName = "New Support Card", menuName = "Card/Support")]
public class SupportCard : Card
{
    public int supportAmount;
    public SupportType supportType;
    public int range;

    public enum SupportType
    {
        Buff,
        Debuff
    }
}