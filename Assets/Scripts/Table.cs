using Commons;
using UnityEngine;

public class Table : MonoBehaviour
{
    public TableType TableType;

    public int UsedSlots = 0;

    public bool TryUseTable()
    {
        if (!IsTableFree())
            return false;
        UsedSlots += 1;
        return true;
    }

    public bool IsTableFree() => UsedSlots < Slots();

    private int Slots()
    {
        switch(TableType)
        {
            case TableType.A:
                return 1;
            case TableType.B:
                return 2;
            default:
                return 1;
        }
    }
}