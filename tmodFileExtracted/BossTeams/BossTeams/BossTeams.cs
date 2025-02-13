using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace BossTeams
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
	public class BossTeams : Mod
	{
		public static Vector2 ModifiedNPCTeamIconDrawPos(NPC npc)
		{
			switch (npc.type)
			{
				case NPCID.EyeofCthulhu:
					return new Vector2(-6, 0);
                case NPCID.SkeletronHead:
                    return new Vector2(-6, 0);
                case NPCID.MoonLordCore:
					return new Vector2(-8, -25);
				case NPCID.KingSlime or NPCID.QueenSlimeBoss:
					return new Vector2(-8, -50);
				case NPCID.SkeletronPrime:
					return new Vector2(-6, -20);
				case NPCID.QueenBee:
					return new Vector2(-12, 0);
				case NPCID.Golem:
					return new Vector2(0, -30);
				case NPCID.Plantera:
					return new Vector2(-4, 0);
				case NPCID.BrainofCthulhu:
					return new Vector2(-8, 0);
				case NPCID.MartianSaucerCore:
					return new Vector2(-6, -40);
				case NPCID.HallowBoss:
					return new Vector2(-8, 0);
				case NPCID.Deerclops:
					return new Vector2(-4, -50);
			}

			return Vector2.Zero;
		}
    }
}
