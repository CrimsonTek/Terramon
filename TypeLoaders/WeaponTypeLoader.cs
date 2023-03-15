﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.TypeLoaders;

public class WeaponTypeLoader : TypeLoader
{
    Dictionary<int, WeaponTypeInfo> typeInfos;

    protected override string CSVFileName => CSVFileNames.Weapons;

    public static WeaponTypeLoader Instance { get; private set; }

    public static ElementArray GetElements(Item item)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out WeaponTypeInfo weaponTypeInfo))
        {
            return weaponTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }

    public static ElementArray GetElements(int itemType)
    {
        if (Instance.typeInfos.TryGetValue(itemType, out WeaponTypeInfo weaponTypeInfo))
        {
            return weaponTypeInfo.elements;
        }
        else
        {
            return ElementArray.Default;
        }
    }

    public static SpecialTooltip[] GetSpecialTooltips(Item item, out bool overrideTypeTooltip)
    {
        if (item is not null && Instance.typeInfos.TryGetValue(item.type, out WeaponTypeInfo weaponTypeInfo))
        {
            overrideTypeTooltip = weaponTypeInfo.overrideTypeTooltip;
            return weaponTypeInfo.specialTooltips;
        }
        else
        {
            overrideTypeTooltip = false;
            return Array.Empty<SpecialTooltip>();
        }
    }

    public static bool GetsStab(Item item)
    {
        return item is not null && Instance.typeInfos.ContainsKey(item.type);
    }

    protected override void InitTypeInfoCollection()
    {
        typeInfos = new Dictionary<int, WeaponTypeInfo>();
    }

    protected override void ParseLine(string line, string[] cells, int lineCount)
    {
        if (cells.Length <= 3)
        {
            return;
        }

        int itemID = int.Parse(cells[0]);

        SpecialTooltip[] specialTooltips = Array.Empty<SpecialTooltip>();
        bool overrideSpecialTooltip = false;
        if (!string.IsNullOrWhiteSpace(cells[ColumnToIndex.D]))
        {
            specialTooltips = SpecialTooltip.Parse(cells[ColumnToIndex.D], out overrideSpecialTooltip);
        }

        typeInfos[itemID] = new WeaponTypeInfo(ParseAtLeastOneElement(cells[ColumnToIndex.B..ColumnToIndex.D]), specialTooltips, overrideSpecialTooltip);
    }

    public override void Load()
    {
        Instance = this;
    }

    public override void Unload()
    {
        Instance = null;
    }

    class WeaponTypeInfo
    {
        public readonly ElementArray elements;
        public readonly SpecialTooltip[] specialTooltips;
        public readonly bool overrideTypeTooltip;

        public WeaponTypeInfo(ElementArray elements, SpecialTooltip[] specialTooltips, bool overrideTypeTooltip)
        {
            this.elements = elements;
            this.specialTooltips = specialTooltips;
            this.overrideTypeTooltip = overrideTypeTooltip;
        }
    }
}
