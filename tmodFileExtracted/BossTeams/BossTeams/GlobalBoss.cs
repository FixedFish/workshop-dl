using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace BossTeams
{
	public class GlobalBoss : GlobalNPC
	{
        public int BossTeam;
        public override bool InstancePerEntity => true;
        public override bool AppliesToEntity(NPC entity, bool lateInstantiation)
        {
            return entity.boss;
        }

        public override void OnSpawn(NPC npc, IEntitySource source)
        {
            if (Main.netMode != NetmodeID.SinglePlayer && source is EntitySource_BossSpawn spawnSource && spawnSource.Target is Player player)
            {
                BossTeam = player.team;
                NetMessage.SendData(MessageID.SyncNPC, -1, -1, null, npc.whoAmI);
            }
        }

        public override void SendExtraAI(NPC npc, BitWriter bitWriter, BinaryWriter binaryWriter)
        {
            binaryWriter.Write(BossTeam);
        }

        public override void ReceiveExtraAI(NPC npc, BitReader bitReader, BinaryReader binaryReader)
        {
            BossTeam = binaryReader.ReadInt32();
        }

        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (BossTeam == 0 || npc.type == NPCID.TorchGod || Main.netMode == NetmodeID.SinglePlayer)
                return;

            if (npc.type == NPCID.CultistBoss && NPC.AnyNPCs(NPCID.CultistBossClone))
                return;

            Texture2D teamIcon = Terraria.GameContent.TextureAssets.Pvp[1].Value;
            var source = new Rectangle(BossTeam * 18, 0, 18, 18);
            float height = TextureAssets.Npc[npc.type].Height() / Main.npcFrameCount[npc.type];
            float width = TextureAssets.Npc[npc.type].Width() / Main.npcFrameCount[npc.type];
            Color drawCol = Lighting.GetColor((new Vector2(npc.position.X - 8 + width / 2, npc.Center.Y - height * 0.8f) + BossTeams.ModifiedNPCTeamIconDrawPos(npc)).ToTileCoordinates());

            spriteBatch.Draw(teamIcon, new Vector2(npc.Center.X, npc.Center.Y - height * 0.8f) - screenPos + BossTeams.ModifiedNPCTeamIconDrawPos(npc), source, drawCol);
        }

        public override bool? CanBeHitByItem(NPC npc, Player player, Item item)
        {
            return (BossTeam == player.team || BossTeam == 0) ? null : false;
        }

        public override bool? CanBeHitByProjectile(NPC npc, Projectile projectile)
        {
            return (BossTeam == 0 || BossTeam == Main.player[projectile.owner].team) ? null : false;
        }
    }
}
