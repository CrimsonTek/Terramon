﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Terraria.ModLoader;
using Terraria.ModLoader.Config;

namespace TerraTyping.Common.Configs;

public class ServerConfig : ModConfig
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2211:Non-constant fields should not be visible", Justification = "<Pending>")]
    public static ServerConfig Instance;

    public override ConfigScope Mode => ConfigScope.ServerSide;

    public override void OnChanged()
    {
        Table.NewMultiplierAndDivisorValues(this);
    }

    private float multiplier = 2;
    [Label("Multiplier")]
    [Tooltip("The increased damage of super effective moves.")]
    [Range(1f, 5f)]
    [Increment(0.1f)]
    [DefaultValue(2)]
    public float Multiplier
    {
        get => multiplier;
        [Obsolete("Only the user should use this.")]
        set
        {
            if (multiplier != value)
            {
                multiplier = value;
                if (DivisorIsInverseOfMultiplier && value != 0)
                {
                    divisor = 1 / value;
                }
            }
        }
    }

    private float divisor = 0.5f;
    [Label("Divisor")]
    [Tooltip("The decreased damage of not very effective moves.")]
    [Range(0f, 1f)]
    [Increment(0.1f)]
    [DefaultValue(0.5)]
    public float Divisor
    {
        get => divisor;
        [Obsolete("Only the user should use this.")]
        set
        {
            if (divisor != value)
            {
                divisor = value;
                divisorIsInverseOfMultiplier = false;
            }
        }
    }

    private bool divisorIsInverseOfMultiplier = true;
    [Label("Divisor is Inverse of Multiplier")]
    [Tooltip("If the divisor value should be locked to the inverse of the multiplier value.")]
    [DefaultValue(true)]
    public bool DivisorIsInverseOfMultiplier
    {
        get => divisorIsInverseOfMultiplier;
        [Obsolete("Only the user should use this.")]
        set
        {
            if (divisorIsInverseOfMultiplier != value)
            {
                divisorIsInverseOfMultiplier = value;
                if (value)
                {
                    divisor = 1 / Multiplier;
                }
            }
        }
    }

    [Label("STAB")]
    [Tooltip("The increased damage when wearing the same armor type as your weapon type.")]
    [Range(1f, 5f)]
    [Increment(0.1f)]
    [DefaultValue(1.5)]
    public float STAB { get; set; } = 1.5f;

    [Label("Weather Multiplier")]
    [Tooltip("The increased damage when using a weapon that's super effective in a specific type of weather.\n" +
        "This affects water type weapons in the rain, ground type weapons in sandstorms, and more.")]
    [Range(1f, 5f)]
    [Increment(0.05f)]
    [DefaultValue(1.25)]
    public float WeatherMultiplier { get; set; } = 1.25f;

    [Label("Weather Multiplier in Expert Only")]
    [Tooltip("Whether or not the weather multiplier should only apply in expert.")]
    [DefaultValue(false)]
    public bool WeatherMultOnlyExpert { get; set; } = false;

    [Label("Weather Multiplier for Enemies")]
    [Tooltip("Whether or not the weather multiplier should apply to enemies.")]
    [DefaultValue(true)]
    public bool WeatherMultForEnemies { get; set; } = true;

    [Label("Hidden Ability Chance in Percentage")]
    [Tooltip("The chance of an NPC having a rarer hidden ability.")]
    [Range(0f, 100f)]
    [DefaultValue(0.5f)]
    public float HiddenAbilityChancePercent { get; set; } = 0.5f;

    [Label("Stab Diminishing Return Scalar")]
    [Tooltip("The amount multiple STABs will benefit you. Having multiple of the same types as a weapon you're using will result in multiple STABs.\n0 will disable multiple STABs.\n1 will disable any diminishing return.")]
    [Range(0, 1f)]
    [DefaultValue(0.75f)]
    public float STABDiminishingReturnScalar { get; set; } = 0.75f;

    [Label("Ability Config")]
    [Tooltip("The values used for abilities.\nNote: some values may not currently be used.")]
    [SeparatePage]
    public AbilityConfig AbilityConfigInstance { get; set; } = new AbilityConfig();

    public class AbilityConfig
    {
        [Header("Lightning Rod")]

        [ReloadRequired]
        [Label("Lightning Rod Damage Boost (Player)")]
        [Tooltip("The damage boost the player will receive from the ability Lightning Rod.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float LightningRodDamageBoostPlayer { get; set; } = 1.2f;

        [ReloadRequired]
        [Label("Lightning Rod Damage Boost (NPC)")]
        [Tooltip("The damage boost an NPC will receive from the ability Lightning Rod.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float LightningRodDamageBoostNPC { get; set; } = 1.5f;

        [ReloadRequired]
        [Label("Lightning Rod Duration (Player)")]
        [Tooltip("The duration the player will receive the damage boost from the ability Lightning Rod.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float LightningRodDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Lightning Rod Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the damage boost from the ability Lightning Rod.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float LightningRodDurationNPC { get; set; } = 6f;

        [Header("Storm Drain")]

        [ReloadRequired]
        [Label("Storm Drain Damage Boost (Player)")]
        [Tooltip("The damage boost the player will receive from the ability Storm Drain.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float StormDrainDamageBoostPlayer { get; set; } = 1.2f;

        [ReloadRequired]
        [Label("Storm Drain Damage Boost (NPC)")]
        [Tooltip("The damage boost an NPC will receive from the ability Storm Drain.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float StormDrainDamageBoostNPC { get; set; } = 1.5f;

        [ReloadRequired]
        [Label("Storm Drain Duration (Player)")]
        [Tooltip("The duration the player will receive the damage boost from the ability Storm Drain.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float StormDrainDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Storm Drain Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the damage boost from the ability Storm Drain.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float StormDrainDurationNPC { get; set; } = 6f;

        [Header("Flash Fire")]

        [ReloadRequired]
        [Label("Flash Fire Damage Boost (Player)")]
        [Tooltip("The damage boost the player will receive from the ability Flash Fire.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float FlashFireDamageBoostPlayer { get; set; } = 1.2f;

        [ReloadRequired]
        [Label("Flash Fire Damage Boost (NPC)")]
        [Tooltip("The damage boost an NPC will receive from the ability Flash Fire.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float FlashFireDamageBoostNPC { get; set; } = 1.5f;

        [ReloadRequired]
        [Label("Flash Fire Duration (Player)")]
        [Tooltip("The duration the player will receive the damage boost from the ability Flash Fire.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float FlashFireDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Flash Fire Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the damage boost from the ability Flash Fire.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float FlashFireDurationNPC { get; set; } = 6f;

        [Header("Motor Drive")]

        [ReloadRequired]
        [Label("Motor Drive Speed Boost (Player)")]
        [Tooltip("The speed boost the player will receive from the ability Motor Drive.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float MotorDriveSpeedBoostPlayer { get; set; } = 1.15f;

        [ReloadRequired]
        [Label("Motor Drive Speed Boost (NPC)")]
        [Tooltip("The speed boost an NPC will receive from the ability Motor Drive.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float MotorDriveSpeedBoostNPC { get; set; } = 1.15f;

        [ReloadRequired]
        [Label("Motor Drive Duration (Player)")]
        [Tooltip("The duration the player will receive the speed boost from the ability Motor Drive.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float MotorDriveDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Motor Drive Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the speed boost from the ability Motor Drive.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float MotorDriveDurationNPC { get; set; } = 6f;

        [Header("Justified")]

        [ReloadRequired]
        [Label("Justified Damage Boost (Player)")]
        [Tooltip("The damage boost the player will receive from the ability Justified.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.2f)]
        public float JustifiedDamageBoostPlayer { get; set; } = 1.15f;

        [ReloadRequired]
        [Label("Justified Damage Boost (NPC)")]
        [Tooltip("The damage boost an NPC will receive from the ability Justified.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.5f)]
        public float JustifiedDamageBoostNPC { get; set; } = 1.15f;

        [ReloadRequired]
        [Label("Justified Duration (Player)")]
        [Tooltip("The duration the player will receive the damage boost from the ability Justified.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float JustifiedDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Justified Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the damage boost from the ability Justified.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float JustifiedDurationNPC { get; set; } = 6f;

        [Header("Water Compaction")]

        [ReloadRequired]
        [Label("Water Compaction Defense Boost (Player)")]
        [Tooltip("The defense boost the player will receive from the ability Water Compaction.")]
        [Range(1, 40)]
        [Increment(1)]
        [DefaultValue(20)]
        [Slider]
        public int WaterCompactionDefenseBoostPlayer { get; set; } = 20;

        [ReloadRequired]
        [Label("Water Compaction Defense Boost (NPC)")]
        [Tooltip("The defense boost an NPC will receive from the ability Water Compaction.")]
        [Range(1, 40)]
        [Increment(1)]
        [DefaultValue(20)]
        [Slider]
        public int WaterCompactionDefenseBoostNPC { get; set; } = 20;

        [ReloadRequired]
        [Label("Water Compaction Duration (Player)")]
        [Tooltip("The duration the player will receive the defense boost from the ability Water Compaction.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float WaterCompactionDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Water Compaction Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the defense boost from the ability Water Compaction.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float WaterCompactionDurationNPC { get; set; } = 6f;

        [Header("Steam Engine")]

        [ReloadRequired]
        [Label("Steam Engine Speed Boost (Player)")]
        [Tooltip("The speed boost the player will receive from the ability Steam Engine.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.4f)]
        public float SteamEngineSpeedBoostPlayer { get; set; } = 1.4f;

        [ReloadRequired]
        [Label("Steam Engine Speed Boost (NPC)")]
        [Tooltip("The speed boost an NPC will receive from the ability Steam Engine.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.4f)]
        public float SteamEngineSpeedBoostNPC { get; set; } = 1.4f;

        [ReloadRequired]
        [Label("Steam Engine Duration (Player)")]
        [Tooltip("The duration the player will receive the speed boost from the ability Steam Engine.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(3f)]
        public float SteamEngineDurationPlayer { get; set; } = 3f;

        [ReloadRequired]
        [Label("Steam Engine Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the speed boost from the ability Steam Engine.")]
        [Range(0f, 10f)]
        [Increment(0.5f)]
        [DefaultValue(6f)]
        public float SteamEngineDurationNPC { get; set; } = 6f;

        [Header("Mummy")]

        [ReloadRequired]
        [Label("Mummy Duration (Player)")]
        [Tooltip("The duration the player will receive the Mummy Debuff from the ability Mummy.")]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(10f)]
        public float MummyDurationPlayer { get; set; } = 10f;

        [ReloadRequired]
        [Label("Mummy Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the Mummy Debuff from the ability Mummy.")]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(15f)]
        public float MummyDurationNPC { get; set; } = 15f;

        [Header("Color Change")]

        [ReloadRequired]
        [Label("Color Change Duration (Player)")]
        [Tooltip("The duration the player will receive the Color Change Buff from the ability Color Change.")]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(8)]
        public float ColorChangeDurationPlayer { get; set; } = 8;

        [ReloadRequired]
        [Label("Color Change Duration (NPC)")]
        [Tooltip("The duration an NPC will receive the Color Change Buff from the ability Color Change.")]
        [Range(0f, 20f)]
        [Increment(0.5f)]
        [DefaultValue(10)]
        public float ColorChangeDurationNPC { get; set; } = 10;

        [Header("Sand Force")]

        [ReloadRequired]
        [Label("Sand Force Damage Boost (Player)")]
        [Tooltip("The damage boost the player will receive from the ability Sand Force.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.3f)]
        public float SandForceDamageBoostPlayer { get; set; } = 1.3f;

        [ReloadRequired]
        [Label("Sand Force Damage Boost (NPC)")]
        [Tooltip("The damage boost an NPC will receive from the ability Sand Force.")]
        [Range(1f, 2f)]
        [Increment(0.05f)]
        [DefaultValue(1.3f)]
        public float SandForceDamageBoostNPC { get; set; } = 1.3f;
    }
}