﻿using CriteriaContextViewer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CriteriaContextViewer.Model.Readers;

namespace CriteriaContextViewer.Model.Files
{
    public class Criteria : IDBObjectReader
    {
        public const string FileName = @"Criteria.db2";

        public uint Id { get; set; }
        public uint Asset { get; set; }
        public uint StartAsset { get; set; }
        public uint FailAsset { get; set; }
        public ushort StartTimer { get; set; }
        public ushort ModifierTreeId { get; set; }
        public ushort EligibilityWorldStateId { get; set; }
        public CriteriaType Type { get; set; }
        public byte StartEvent { get; set; }
        public CriteriaCondition FailEvent { get; set; }
        public byte Flags { get; set; }
        public byte EligibilityWorldStateValue { get; set; }
        
        public void ReadObject(IWowClientDBReader dbReader, BinaryReader binaryReader)
        {
            using (BinaryReader br = binaryReader)
            {
                if (dbReader.HasSeparateIndexColumn)
                    Id = br.ReadUInt32();

                Asset = br.ReadUInt32();
                StartAsset = br.ReadUInt32();
                FailAsset = br.ReadUInt32();
                StartTimer = br.ReadUInt16();
                ModifierTreeId = br.ReadUInt16();
                EligibilityWorldStateId = br.ReadUInt16();
                Type = (CriteriaType)br.ReadByte();
                StartEvent = br.ReadByte();
                FailEvent = (CriteriaCondition)br.ReadByte();
                Flags = br.ReadByte();
                EligibilityWorldStateValue = br.ReadByte();
            }
        }
    }

    public enum CriteriaCondition : byte
    {
        None            = 0,
        NoDeath         = 1,
        Unk2            = 2,
        BGMap           = 3,
        NoLose          = 4,
        Unk5            = 5,
        Unk8            = 8,
        NoSpellHit      = 9,
        NotInGroup      = 10,
        Unk13           = 13
    };

    public enum CriteriaAdditionalCondition
    {
        [NYI]
        SourceDrunkValue = 1, // NYI
        Unk2 = 2,
        [NYI]
        ItemLevel = 3, // NYI
        TargetCreatureEntry = 4,
        TargetMustBePlayer = 5,
        TargetMustBeDead = 6,
        TargetMustBeEnemy = 7,
        SourceHasAura = 8,
        TargetHasAura = 10,
        TargetHasAuraType = 11,
        ItemQualityMin = 14,
        ItemQualityEquals = 15,
        Unk16 = 16,
        SourceAreaOrZone = 17,
        TargetAreaOrZone = 18,
        MaxDifficulty = 20,
        [NYI]
        TargetCreatureYieldsExperience = 21, // NYI
        ArenaType = 24,
        SourceRace = 25,
        SourceClass = 26,
        TargetRace = 27,
        TargetClass = 28,
        MaxGroupMembers = 29,
        TargetCreatureType = 30,
        SourceMap = 32,
        [NYI]
        ItemClass = 33, // NYI
        [NYI]
        ItemSubClass = 34, // NYI
        [NYI]
        CompleteQuestNotInGroup = 35, // NYI
        [NYI]
        MinPersonalRating = 37, // NYI (when implementing don't forget about CRITERIA_CONDITION_NO_LOSE)
        TitleBitIndex = 38,
        SourceLevel = 39,
        TargetLevel = 40,
        TargetZone = 41,
        TargetHealthPercentageBelow = 46,
        Unk55 = 55,
        [NYI]
        MinAchievementPoints = 56, // NYI
        [NYI]
        RequiresLFGGroup = 58, // NYI
        Unk60 = 60,
        [NYI]
        RequiresGuildGroup = 61, // NYI
        [NYI]
        GuildReputation = 62, // NYI
        [NYI]
        RatedBattleground = 63, // NYI
        ProjectRarity = 65,
        ProjectRace = 66,
        BattlePetSpecies = 91,
        GarrisonFollowerQuality = 145,
        GarrisonFollowerLevel = 146,
        [NYI]
        GarrisonRareMission = 147, // NYI
        [NYI]
        GarrisonBuildingLevel = 149, // NYI
        [NYI]
        GarrisonMissionType = 167, // NYI
        GarrisonFollowerItemlevel = 184,
    };

    public enum CriteriaFlags
    {
        CRITERIA_FLAG_SHOW_PROGRESS_BAR = 0x00000001,   // Show progress as bar
        CRITERIA_FLAG_HIDDEN = 0x00000002,   // Not show criteria in client
        CRITERIA_FLAG_FAIL_ACHIEVEMENT = 0x00000004,   // BG related??
        CRITERIA_FLAG_RESET_ON_START = 0x00000008,   //
        CRITERIA_FLAG_IS_DATE = 0x00000010,   // not used
        CRITERIA_FLAG_MONEY_COUNTER = 0x00000020    // Displays counter as money
    };

    public enum CriteriaTimedTypes
    {
        CRITERIA_TIMED_TYPE_EVENT = 1,    // Timer is started by internal event with id in timerStartEvent
        CRITERIA_TIMED_TYPE_QUEST = 2,    // Timer is started by accepting quest with entry in timerStartEvent
        CRITERIA_TIMED_TYPE_SPELL_CASTER = 5,    // Timer is started by casting a spell with entry in timerStartEvent
        CRITERIA_TIMED_TYPE_SPELL_TARGET = 6,    // Timer is started by being target of spell with entry in timerStartEvent
        CRITERIA_TIMED_TYPE_CREATURE = 7,    // Timer is started by killing creature with entry in timerStartEvent
        CRITERIA_TIMED_TYPE_ITEM = 9,    // Timer is started by using item with entry in timerStartEvent
        CRITERIA_TIMED_TYPE_UNK = 10,   // Unknown
        CRITERIA_TIMED_TYPE_UNK_2 = 13,   // Unknown
        CRITERIA_TIMED_TYPE_SCENARIO_STAGE = 14,   // Timer is started by changing stages in a scenario

        CRITERIA_TIMED_TYPE_MAX
    };
}