# Pritty Blaster + Pritty/Fernet Rounds Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Add a whimsical Argentina-themed Pre-Hardmode gun (Pritty Blaster) plus a tiered ammo pair (Pritty Rounds, Fernet Rounds) that each shatter into a small AoE "fizzy splash" on hit, following the design in `docs/superpowers/specs/2026-08-09-pritty-fernet-gun-design.md`.

**Architecture:** Standard tModLoader `ModItem`/`ModProjectile` pattern already used throughout this codebase. One shared AoE trigger projectile (`FizzySplash`) is spawned from each bullet's own `OnHitNPC`, parameterized by damage and dust color via `Projectile.ai[0]`/spawn args — this mirrors the self-contained on-hit-spawn pattern already used by `HydrothermicBullet_Proj.ModifyHitNPC` (spawns `HydrothermicVolcano`) in this exact codebase, NOT the accessory-triggered `GlobalProjectiles.cs` coil pattern (that one is keyed on a player flag, not an ammo type, so it's the wrong precedent here). The gun itself deliberately does **not** override `Shoot()` — unlike this codebase's other three guns (`ChewingGun`, `DeathMarkMagnum`, `SpectreRifle`), which all hard-code their own signature projectile regardless of loaded ammo, Pritty Blaster's whole point is that swapping ammo changes what fires, so it needs standard tModLoader ammo-driven projectile selection (no `Shoot()` override → the engine spawns whatever `Item.shoot` the loaded ammo declares).

**Tech Stack:** C# (tModLoader 1.4.4 API), Python 3 + Pillow (one-off sprite generation script, not shipped with the mod).

## Global Constraints

- No new debuff/buff/status effect — splash is pure AoE damage, no lingering effect (per spec's "Explicitly NOT doing").
- No interaction with Calamity's Alcohol buff/potion system beyond namespace coexistence.
- No changes to `build.txt` or `CalamityAmmo.csproj` — none of the new code references any `CalamityMod.*` type, only vanilla Terraria/tModLoader APIs, so there's no risk of hitting a Calamity-version landmine like the one fixed on this same branch.
- **No compiler is available on this machine** (no `dotnet` SDK, no local Terraria/tModLoader install — see `HANDOFF.md`). Every task's "verification" step is therefore static review (grep-based structural checks, cross-file consistency checks, and — for sprites — visual inspection via the Read tool) rather than a build/test run. This matches the discipline already used on this branch for the perf/correctness fixes. Actual compilation and in-game testing happens on the user's Windows machine per the existing handoff workflow.
- All new C# files use tabs for indentation, matching the prevailing style in this repo's `Ammos/`, `Weapons/`, and `Projectiles/` folders.

---

### Task 1: Sprite generation script + all three PNGs

**Files:**
- Create (scratch, not committed): `generate_sprites.py` in a scratch/temp directory of your choice outside the repo (e.g. your environment's designated scratchpad directory if one is provided, otherwise `/tmp/calamityammo-sprites/`) — never inside the git working tree, so it can't get accidentally committed.
- Create (committed): `Weapons/PrittyBlaster.png`
- Create (committed): `Ammos/Pre_Hardmode/PrittyRounds.png`
- Create (committed): `Ammos/Hardmode/FernetRounds.png`

**Interfaces:**
- Produces: three PNG files at the exact paths above, which Tasks 4, 6, and 7 reference as each `ModItem`'s implicit texture (tModLoader auto-loads `<ClassFolder>/<ClassName>.png` unless `Texture` is overridden — none of our `ModItem`s override `Texture`, so the file path must exactly match the class's namespace-relative path).
- Sizes (measured from existing comparable items in this repo): `PrittyBlaster.png` 54×36px (matches `Weapons/ChewingGun.png`), `PrittyRounds.png` and `FernetRounds.png` 16×36px (matches `Ammos/Pre_Hardmode/RottenBullet.png`).

- [ ] **Step 1: Check for / install Pillow**

Run: `pip3 install --quiet Pillow || pip install --quiet Pillow`
Expected: exits 0. If pip is unavailable entirely, use `python3 -m pip install --user Pillow` instead.

- [ ] **Step 2: Write the sprite generation script**

```python
#!/usr/bin/env python3
"""One-off pixel-art generator for the Pritty Blaster / Pritty & Fernet Rounds
sprites. Not shipped with the mod — run once, inspect the output, iterate on
this script directly if a sprite doesn't read clearly, then re-run."""

from PIL import Image

TRANSPARENT = (0, 0, 0, 0)
OUTLINE = (20, 16, 12, 255)  # near-black outline, standard Terraria item-icon style


def new_canvas(w, h):
    return Image.new("RGBA", (w, h), TRANSPARENT)


def rect(img, x0, y0, x1, y1, color):
    px = img.load()
    for x in range(x0, x1 + 1):
        for y in range(y0, y1 + 1):
            px[x, y] = color


def outline_rect(img, x0, y0, x1, y1, fill, outline=OUTLINE):
    rect(img, x0, y0, x1, y1, fill)
    px = img.load()
    for x in range(x0, x1 + 1):
        px[x, y0] = outline
        px[x, y1] = outline
    for y in range(y0, y1 + 1):
        px[x0, y] = outline
        px[x1, y] = outline


def bottle_sprite(body_color, label_color, cap_color):
    """16x36 bottle silhouette: cap, neck, shoulders, body, label stripe."""
    img = new_canvas(16, 36)
    # Cap (top)
    outline_rect(img, 6, 0, 9, 3, cap_color)
    # Neck
    outline_rect(img, 6, 4, 9, 9, body_color)
    # Shoulders (bottle widens)
    outline_rect(img, 4, 10, 11, 13, body_color)
    # Body
    outline_rect(img, 2, 14, 13, 33, body_color)
    # Label stripe across the body
    rect(img, 3, 20, 12, 26, label_color)
    px = img.load()
    for x in range(3, 13):
        px[x, 20] = OUTLINE
        px[x, 26] = OUTLINE
    return img


def gun_sprite():
    """54x36 improvised bottle-gun: a stubby frame with a bottle-shaped
    barrel/reservoir on top, matching ChewingGun.png's canvas size."""
    img = new_canvas(54, 36)
    # Grip
    outline_rect(img, 4, 18, 13, 33, (92, 61, 33, 255))       # brown wood grip
    # Frame/body
    outline_rect(img, 10, 14, 34, 22, (90, 90, 96, 255))      # gunmetal frame
    # Trigger guard
    outline_rect(img, 14, 22, 20, 27, (60, 60, 64, 255))
    # Barrel
    outline_rect(img, 32, 15, 50, 20, (70, 70, 76, 255))
    # Bottle reservoir sitting on top of the frame (the "Pritty" identity)
    outline_rect(img, 16, 2, 19, 5, (200, 210, 90, 255))      # cap
    outline_rect(img, 15, 6, 20, 9, (60, 140, 70, 255))       # neck, green glass
    outline_rect(img, 12, 10, 23, 19, (60, 140, 70, 255))     # body, green glass
    rect(img, 13, 12, 22, 15, (230, 220, 150, 255))           # label
    return img


if __name__ == "__main__":
    gun_sprite().save("PrittyBlaster.png")
    bottle_sprite((150, 30, 40, 255), (230, 220, 150, 255), (60, 45, 30, 255)).save("PrittyRounds.png")   # red wine bottle
    bottle_sprite((40, 30, 20, 255), (200, 160, 60, 255), (20, 15, 10, 255)).save("FernetRounds.png")     # dark bitter-liqueur bottle
    print("Wrote PrittyBlaster.png, PrittyRounds.png, FernetRounds.png")
```

Save this to the scratchpad path given in Files above.

- [ ] **Step 3: Run the script**

Run (from the scratch directory chosen above): `python3 generate_sprites.py`
Expected: prints `Wrote PrittyBlaster.png, PrittyRounds.png, FernetRounds.png` and creates all three files in the current directory.

- [ ] **Step 4: Visually inspect each sprite**

Use the Read tool on each generated PNG (Read supports viewing images directly). Check: does `PrittyRounds.png` read clearly as a red-wine-style bottle with a visible label stripe? Does `FernetRounds.png` read as a distinctly darker, different-capped bottle? Does `PrittyBlaster.png` read as a stubby handgun with a bottle mounted on top, at a similar visual weight to `Weapons/ChewingGun.png`?

If any sprite looks wrong (proportions off, colors muddy, outline missing), edit the corresponding function in the script directly (adjust the rect coordinates/colors) and re-run Step 3. Repeat until all three sprites look like intentional, distinct pixel art — not a rough blob.

- [ ] **Step 5: Copy sprites into the repo and commit**

From the repo root, copy the three PNGs from the scratch directory chosen in Step 2 to their destination paths listed in Files above (e.g. `cp <scratch-dir>/PrittyBlaster.png Weapons/PrittyBlaster.png`, and similarly for the other two), then:

```bash
git add Weapons/PrittyBlaster.png Ammos/Pre_Hardmode/PrittyRounds.png Ammos/Hardmode/FernetRounds.png
git commit -m "Add sprites for Pritty Blaster, Pritty Rounds, Fernet Rounds"
```

---

### Task 2: FizzySplash shared AoE trigger projectile

**Files:**
- Create: `Projectiles/FizzySplash.cs`

**Interfaces:**
- Produces: `CalamityAmmo.Projectiles.FizzySplash : ModProjectile`. Spawned via
  `Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FizzySplash>(), damage, knockback, owner, ai0, ai1)`
  where **ai0 = dust type to burst (as a float-cast int)**, ai1 unused (pass `0f`). Damage passed in is dealt as a single friendly AoE hit to anything overlapping its 64×64 hitbox during its ~10-tick life.
- Consumes: nothing from other new files (self-contained, only vanilla Terraria/tModLoader APIs).

- [ ] **Step 1: Write `Projectiles/FizzySplash.cs`**

```csharp
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class FizzySplash : ModProjectile
	{
		public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

		public override void SetDefaults()
		{
			Projectile.width = 64;
			Projectile.height = 64;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 10;
			Projectile.tileCollide = false;
			Projectile.knockBack = 0f;
			Projectile.usesLocalNPCImmunity = true;
			Projectile.localNPCHitCooldown = 10;
		}

		public override void AI()
		{
			if (Projectile.localAI[0] == 0f)
			{
				Projectile.localAI[0] = 1f;
				int dustType = (int)Projectile.ai[0];
				for (int i = 0; i < 12; i++)
				{
					Dust dust = Dust.NewDustDirect(Projectile.position, Projectile.width, Projectile.height, dustType, 0f, 0f, 0, default(Color), 1.2f);
					dust.velocity = Main.rand.NextVector2Circular(4f, 4f);
					dust.noGravity = true;
				}
			}
		}
	}
}
```

- [ ] **Step 2: Static verification**

Run: `grep -c "{" Projectiles/FizzySplash.cs; grep -c "}" Projectiles/FizzySplash.cs`
Expected: both counts equal (braces balanced). Also confirm by eye: `namespace CalamityAmmo.Projectiles` matches the folder (`Projectiles/`), matching the convention already used by `Projectiles/DeathMarkRound.cs`, `Projectiles/rottenBulletProj.cs` (both live at `Projectiles/` root with namespace `CalamityAmmo.Projectiles`).

- [ ] **Step 3: Commit**

```bash
git add Projectiles/FizzySplash.cs
git commit -m "Add FizzySplash shared AoE trigger projectile"
```

---

### Task 3: Pritty Round bullet projectile

**Files:**
- Create: `Projectiles/PrittyRoundProj.cs`

**Interfaces:**
- Consumes: `CalamityAmmo.Projectiles.FizzySplash` (Task 2), same namespace so no `using` needed.
- Produces: `CalamityAmmo.Projectiles.PrittyRoundProj : ModProjectile`, referenced by Task 4's `Item.shoot = ModContent.ProjectileType<PrittyRoundProj>()`.
- Dust type used for its splash: `DustID.GemEmerald` (bright green, already a standard long-established vanilla `DustID` name — matches the green-glass "Pritty" bottle sprite from Task 1).

- [ ] **Step 1: Write `Projectiles/PrittyRoundProj.cs`**

```csharp
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles
{
	public class PrittyRoundProj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			int splashDamage = (int)(damageDone * 0.4f);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<FizzySplash>(), splashDamage, 0f, Projectile.owner, DustID.GemEmerald, 0f);
		}
	}
}
```

- [ ] **Step 2: Static verification**

Run: `grep -c "{" Projectiles/PrittyRoundProj.cs; grep -c "}" Projectiles/PrittyRoundProj.cs`
Expected: both counts equal.
Run: `grep -n "class FizzySplash" Projectiles/FizzySplash.cs`
Expected: one match — confirms the type this file references actually exists before Task 4 depends on this file in turn.

- [ ] **Step 3: Commit**

```bash
git add Projectiles/PrittyRoundProj.cs
git commit -m "Add Pritty Round bullet projectile"
```

---

### Task 4: Pritty Rounds ammo item

**Files:**
- Create: `Ammos/Pre_Hardmode/PrittyRounds.cs`

**Interfaces:**
- Consumes: `CalamityAmmo.Projectiles.PrittyRoundProj` (Task 3, via `using CalamityAmmo.Projectiles;`); `Ammos/Pre_Hardmode/PrittyRounds.png` (Task 1, implicit texture).
- Produces: `CalamityAmmo.Ammos.Pre_Hardmode.PrittyRounds : ModItem`, an `AmmoID.Bullet`-type item any bullet-using gun (including Task 7's Pritty Blaster) can load.

- [ ] **Step 1: Write `Ammos/Pre_Hardmode/PrittyRounds.cs`**

```csharp
using CalamityAmmo.Projectiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Pre_Hardmode
{
	public class PrittyRounds : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 7;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 1f;
			Item.value = Item.buyPrice(0, 0, 0, 60);
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<PrittyRoundProj>();
			Item.shootSpeed = 6f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.EmptyBullet, 50)
				.AddIngredient(ItemID.Bottle, 1)
				.AddIngredient(ItemID.Gel, 5)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
```

- [ ] **Step 2: Static verification**

Run: `grep -c "{" Ammos/Pre_Hardmode/PrittyRounds.cs; grep -c "}" Ammos/Pre_Hardmode/PrittyRounds.cs`
Expected: both counts equal.
Run: `find . -iname "PrittyRounds.png"`
Expected: one match at `Ammos/Pre_Hardmode/PrittyRounds.png` (from Task 1) — this file's class is `PrittyRounds` in folder `Ammos/Pre_Hardmode/`, so tModLoader's implicit texture lookup needs exactly that path.

- [ ] **Step 3: Commit**

```bash
git add Ammos/Pre_Hardmode/PrittyRounds.cs
git commit -m "Add Pritty Rounds ammo item"
```

---

### Task 5: Fernet Round bullet projectile

**Files:**
- Create: `Projectiles/Hardmode/FernetRoundProj.cs`

**Interfaces:**
- Consumes: `CalamityAmmo.Projectiles.FizzySplash` (Task 2, via `using CalamityAmmo.Projectiles;` since this file's own namespace is `CalamityAmmo.Projectiles.Hardmode`).
- Produces: `CalamityAmmo.Projectiles.Hardmode.FernetRoundProj : ModProjectile`, referenced by Task 6's `Item.shoot`.
- Dust type used for its splash: `DustID.Torch` (warm amber/orange — matches the dark bitter-liqueur bottle sprite, and is already a proven-valid `DustID` in this exact codebase, used in `Projectiles/Hardmode/_NapalmBullet.cs`).

- [ ] **Step 1: Write `Projectiles/Hardmode/FernetRoundProj.cs`**

```csharp
using CalamityAmmo.Projectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Projectiles.Hardmode
{
	public class FernetRoundProj : ModProjectile
	{
		public override void SetDefaults()
		{
			Projectile.width = 8;
			Projectile.height = 8;
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.hostile = false;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.penetrate = 1;
			Projectile.timeLeft = 300;
			Projectile.light = 0f;
			Projectile.tileCollide = true;
		}

		public override void AI()
		{
			Projectile.rotation = Projectile.velocity.ToRotation();
		}

		public override bool OnTileCollide(Vector2 oldVelocity)
		{
			Collision.HitTiles(Projectile.position, Projectile.velocity, Projectile.width, Projectile.height);
			SoundEngine.PlaySound(SoundID.Item8, new Vector2?(Projectile.position));
			return true;
		}

		public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
		{
			int splashDamage = (int)(damageDone * 0.4f);
			Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Center, Vector2.Zero, ModContent.ProjectileType<FizzySplash>(), splashDamage, 0f, Projectile.owner, DustID.Torch, 0f);
		}
	}
}
```

- [ ] **Step 2: Static verification**

Run: `grep -c "{" Projectiles/Hardmode/FernetRoundProj.cs; grep -c "}" Projectiles/Hardmode/FernetRoundProj.cs`
Expected: both counts equal.
Run: `grep -n "^namespace" Projectiles/Hardmode/_NapalmBullet.cs Projectiles/Hardmode/FernetRoundProj.cs`
Expected: both show `namespace CalamityAmmo.Projectiles.Hardmode` — confirms this new file matches the folder's established namespace convention exactly.

- [ ] **Step 3: Commit**

```bash
git add Projectiles/Hardmode/FernetRoundProj.cs
git commit -m "Add Fernet Round bullet projectile"
```

---

### Task 6: Fernet Rounds ammo item

**Files:**
- Create: `Ammos/Hardmode/FernetRounds.cs`

**Interfaces:**
- Consumes: `CalamityAmmo.Projectiles.Hardmode.FernetRoundProj` (Task 5, via `using CalamityAmmo.Projectiles.Hardmode;`); `Ammos/Hardmode/FernetRounds.png` (Task 1, implicit texture).
- Produces: `CalamityAmmo.Ammos.Hardmode.FernetRounds : ModItem`, an `AmmoID.Bullet`-type item, the Hardmode upgrade to Pritty Rounds.

- [ ] **Step 1: Write `Ammos/Hardmode/FernetRounds.cs`**

```csharp
using CalamityAmmo.Projectiles.Hardmode;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Ammos.Hardmode
{
	public class FernetRounds : ModItem
	{
		public override void SetStaticDefaults()
		{
			Terraria.GameContent.Creative.CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 99;
		}

		public override void SetDefaults()
		{
			Item.damage = 13;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 8;
			Item.height = 8;
			Item.maxStack = 9999;
			Item.consumable = true;
			Item.knockBack = 1.5f;
			Item.value = Item.buyPrice(0, 0, 2, 0);
			Item.rare = ItemRarityID.Orange;
			Item.shoot = ModContent.ProjectileType<FernetRoundProj>();
			Item.shootSpeed = 7f;
			Item.ammo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe(50)
				.AddIngredient(ItemID.EmptyBullet, 50)
				.AddIngredient(ItemID.Bottle, 1)
				.AddIngredient(ItemID.HallowedBar, 1)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
```

- [ ] **Step 2: Static verification**

Run: `grep -c "{" Ammos/Hardmode/FernetRounds.cs; grep -c "}" Ammos/Hardmode/FernetRounds.cs`
Expected: both counts equal.
Run: `find . -iname "FernetRounds.png"`
Expected: one match at `Ammos/Hardmode/FernetRounds.png`.

- [ ] **Step 3: Commit**

```bash
git add Ammos/Hardmode/FernetRounds.cs
git commit -m "Add Fernet Rounds ammo item"
```

---

### Task 7: Pritty Blaster gun

**Files:**
- Create: `Weapons/PrittyBlaster.cs`

**Interfaces:**
- Consumes: `Weapons/PrittyBlaster.png` (Task 1, implicit texture). Does **not** consume `PrittyRoundProj`/`FernetRoundProj` directly — it deliberately has no `Shoot()` override, so it fires whatever `Item.shoot` the currently-loaded ammo declares (standard tModLoader ammo-selection behavior).
- Produces: `CalamityAmmo.Weapons.PrittyBlaster : ModItem`, a Pre-Hardmode `AmmoID.Bullet`-consuming gun.

- [ ] **Step 1: Write `Weapons/PrittyBlaster.cs`**

```csharp
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityAmmo.Weapons
{
	public class PrittyBlaster : ModItem
	{
		public override void SetStaticDefaults()
		{
			Item.ResearchUnlockCount = 1;
		}

		public override void SetDefaults()
		{
			Item.damage = 5;
			Item.DamageType = DamageClass.Ranged;
			Item.width = 54;
			Item.height = 36;
			Item.useTime = 20;
			Item.useAnimation = 20;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.noMelee = true;
			Item.knockBack = 1.5f;
			Item.value = Item.buyPrice(0, 0, 50, 0);
			Item.rare = ItemRarityID.White;
			Item.UseSound = SoundID.Item41;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 8f;
			Item.useAmmo = AmmoID.Bullet;
		}

		public override void AddRecipes()
		{
			CreateRecipe(1)
				.AddIngredient(ItemID.Bottle, 3)
				.AddIngredient(ItemID.Wood, 10)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
```

Note: no `Shoot()` override is intentional — see this plan's Architecture section.

- [ ] **Step 2: Static verification**

Run: `grep -c "{" Weapons/PrittyBlaster.cs; grep -c "}" Weapons/PrittyBlaster.cs`
Expected: both counts equal.
Run: `grep -n "override bool Shoot" Weapons/PrittyBlaster.cs`
Expected: no match — confirms the intentional absence of a `Shoot()` override.

- [ ] **Step 3: Commit**

```bash
git add Weapons/PrittyBlaster.cs
git commit -m "Add Pritty Blaster gun"
```

---

### Task 8: Cross-file consistency check

**Files:** none created; this task only reads/verifies files from Tasks 1–7.

**Interfaces:** none — this is a whole-feature integration check.

- [ ] **Step 1: Confirm every referenced type is defined exactly once**

Run:
```bash
for t in FizzySplash PrittyRoundProj FernetRoundProj PrittyRounds FernetRounds PrittyBlaster; do
  echo "== $t =="
  grep -rn "class $t" --include="*.cs" .
done
```
Expected: exactly one `class` definition line per name, in the file each task above created.

- [ ] **Step 2: Confirm no filename collides with an existing item/projectile in this repo**

Run: `find . -iname "PrittyBlaster.*" -o -iname "PrittyRounds.*" -o -iname "FernetRounds.*" -o -iname "FizzySplash.*" -o -iname "PrittyRoundProj.*" -o -iname "FernetRoundProj.*" | sort`
Expected: only the 6 new `.cs` files plus the 3 new `.png` files from Task 1 (9 total) — no pre-existing file with any of these names.

- [ ] **Step 3: Confirm `Item.ammo`/`Item.useAmmo` pairing is coherent across the three item files**

Run: `grep -n "Item.ammo\|Item.useAmmo" Weapons/PrittyBlaster.cs Ammos/Pre_Hardmode/PrittyRounds.cs Ammos/Hardmode/FernetRounds.cs`
Expected: `PrittyBlaster.cs` shows `Item.useAmmo = AmmoID.Bullet;`; both ammo files show `Item.ammo = AmmoID.Bullet;` — same enum value (`AmmoID.Bullet`) on both sides, confirming Pritty Blaster can actually load Pritty Rounds and Fernet Rounds.

- [ ] **Step 4: Confirm the splash damage ratio is applied identically in both bullet projectiles**

Run: `grep -n "damageDone \* 0.4f" Projectiles/PrittyRoundProj.cs Projectiles/Hardmode/FernetRoundProj.cs`
Expected: one match in each file, confirming both tiers use the same splash-damage convention documented in the spec.

- [ ] **Step 5: Final full-branch diff review**

Run: `git log --oneline -9` (should show the 8 feature commits from Tasks 1–7 plus this task if it produces one) and `git diff $(git merge-base Levantine HEAD)..HEAD --stat` to see the full set of files this feature touched (diffing from the actual fork point off `Levantine`, not `perf-and-correctness-fixes~1` — since we're on that branch, its ref moves with every commit, so `~1` would only show the last commit). Confirm nothing outside `Weapons/`, `Ammos/`, `Projectiles/`, and `docs/` was modified.

No commit needed for this task unless Step 1–4 turn up a fix — in that case, fix inline, re-run the relevant check, then commit the fix with a message describing what was inconsistent.
