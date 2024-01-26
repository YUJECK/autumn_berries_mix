using autumn_berries_mix.EC;
using autumn_berries_mix.Grid;
using UnityEngine;

public sealed class Chainy : Entity, IOnTileSelected
{
    public void OnSelected()
    {
        Debug.Log("select");
    }

    public void OnDeselected()
    {
        Debug.Log("deselect");
    }
}
