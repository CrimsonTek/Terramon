﻿using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using TerraTyping.DataTypes;

namespace TerraTyping.Abilities.Buffs.TypeAdd
{
    public class BuffAddType : ModBuff, IUseModifiedType
    {
        public Element MyElement { get; set; }
        public ElementArray Elements { get; set; }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault($"Adding {LangHelper.MultipleElements(Elements)}");
            Description.SetDefault($"You are now {LangHelper.MultipleElements(Elements)} in addition to your armor's type.");
            Main.debuff[Type] = true;
        }

        public override bool IsLoadingEnabled(Mod mod)
        {
            return false;
        }
    }
}
