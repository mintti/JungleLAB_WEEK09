using System.Collections.Generic;
using UnityEngine;

public class UICommonTile : MonoBehaviour, ITile
{
    [SerializeField] private int _index;

    #region ITile
    /// <summary>
    /// 타일 인덱스
    /// </summary>
    public int Index { get => _index; set => _index = value; }
    
    public TileType Type { get; set; }
    
    public void OnEvent()
    {
        UIManager.I.ActivePlayerActionSelector(); 
    }
    
    public void ExitEvent()
    {
        UIManager.I.DisabledPlayerActionSelector();
    }

    public void PassEvent()
    {
        
    }
    #endregion
    
    public IUnit OnUnit { get; set; }

    public void Employ(ClassType type)
    {
        var obj = Instantiate(ResourceManager.I.UnitPrefab, transform);
        var unit = ResourceManager.I.GetUnit(type);
        obj.GetComponent<UIUnit>().Init(unit);

        OnUnit = unit;
        unit.Init(UnitLevel.One);
    }

    public void Upgrade()
    {
        OnUnit.Level = (UnitLevel)((int)OnUnit.Level + 1);
    }
        
    public void ChangeClass(ClassType type)
    {
        OnUnit.ClassType = type;
    }

    public void Destroy()
    {
        Destroy(OnUnit.UIUnit.gameObject);
        OnUnit = null;
        GameManager.I.CalculateSynergy();
    }
}