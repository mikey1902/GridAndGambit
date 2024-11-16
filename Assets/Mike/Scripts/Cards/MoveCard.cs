using GridGambitProd;
using UnityEngine;

[CreateAssetMenu(fileName = "New Move Card", menuName = "Card/Move")]
public class MoveCard : Card
{
    public int damage;
    public int moveDistance;
    public MoveType moveType;

    public enum MoveType
    {
        Orthogonal,
        Diagonal,
        LShape,
    }
}