# Pritty Blaster + Pritty/Fernet Rounds — Design

## Context

CalamityAmmo is a Terraria/tModLoader addon for Calamity Mod that adds ranged
weapons and ammo. The user wants a new, whimsical, Argentina-themed weapon
line: a gun plus a tiered pair of ammo, in the same "joke weapon" spirit as
the existing `ChewingGun` (Weapons/ChewingGun.cs).

Theme references:
- **Fernet con Coca** — Argentina's iconic bitter liqueur (Fernet Branca) mixed
  with Coca-Cola. The national drink.
- **Vino con Pritty** — cheap wine mixed with Pritty, a grapefruit-flavored
  soda. A budget staple, especially associated with Córdoba province
  "previas" (pregame gatherings).

## Scope

Three new items, following this codebase's existing tModLoader `ModItem` /
`ModProjectile` patterns (referenced throughout: `Weapons/ChewingGun.cs`,
`Ammos/Pre_Hardmode/RottenBullet.cs`, `Ammos/Hardmode/NapalmBullet.cs`,
`Global/GlobalProjectiles.cs`'s icyCoil→`IceBomb` spawn pattern):

1. **Pritty Blaster** — new gun, `Weapons/PrittyBlaster.cs`
2. **Pritty Rounds** — new Pre-Hardmode ammo, `Ammos/Pre_Hardmode/PrittyRounds.cs`
3. **Fernet Rounds** — new Hardmode ammo, `Ammos/Hardmode/FernetRounds.cs`

Explicitly out of scope: no new debuff/buff, no new tile/furniture, no
crafting-station gating beyond what's specified below, no multiplayer-specific
handling beyond what the existing `IceBomb`-style spawn pattern already
provides "for free" via the projectile system.

## 1. Pritty Blaster (gun)

An improvised bottle-gun. Fires any Bullet-type ammo (`Item.useAmmo =
AmmoID.Bullet`) — vanilla Musket Balls work in it day one; Pritty Rounds and
Fernet Rounds are its themed upgrades, but it's not gated to them.

- Pre-Hardmode, craftable immediately (day-one availability, like
  `ChewingGun`)
- Modest base stats — comparable to vanilla's Handgun (the ammo, not the gun,
  carries the interesting mechanics and power scaling)
- Recipe: cheap, thematic, early materials (e.g. `ItemID.Bottle` + `ItemID.Wood`,
  Work Bench) — exact quantities finalized during implementation following
  this tier's existing recipe conventions (see `ChewingGun.AddRecipes`)
- Sprite: new, ~54×36px (matches `Weapons/ChewingGun.png`'s dimensions),
  script-generated pixel art (see Sprites section)

## 2. Pritty Rounds (Pre-Hardmode ammo)

- `Item.ammo = AmmoID.Bullet`, usable in any bullet gun, not just Pritty
  Blaster
- Base damage ~7 (in line with `RottenBullet`'s 8, this tier's weaker-end
  bullet)
- **On-hit effect**: shatters into a small fizzy splash. Implemented as a
  short-lived AoE trigger projectile spawned in `OnHitNPC`
  (`Global/GlobalProjectiles.cs`, alongside the other coil-effect branches),
  following the exact pattern already used for Icy Coil's `IceBomb` spawn:
  `Projectile.NewProjectileDirect(..., splashDamage, ...)` where
  `splashDamage = (int)(damageDone * 0.4f)` — same 40% ratio this codebase
  already uses for that spawn-on-hit pattern.
  - Splash trigger projectile: small hitbox, brief `timeLeft`, reuses
    Calamity's `CalamityMod/Projectiles/InvisibleProj` texture (already
    referenced elsewhere in this codebase, e.g. `Ammos/Hardmode/
    HydrothermicArrow.cs`) — this is a functional invisible marker asset, not
    a borrowed visual identity, so it doesn't conflict with the "no shallow
    sprite copies" requirement.
  - Visual feedback for the splash is dust-only: a burst of drink-colored
    dust (exact `DustID` chosen during implementation) at the impact point,
    no new sprite required for the splash itself.
- Recipe: cheap Pre-Hardmode materials (e.g. `ItemID.Bottle` + `ItemID.Gel` +
  base ammo like `ItemID.MusketBall`), Work Bench — exact quantities
  finalized during implementation
- Sprite: new, ~16×36px (matches `RottenBullet.png`'s dimensions), wine-bottle
  silhouette, script-generated (see Sprites section)

## 3. Fernet Rounds (Hardmode ammo — the upgrade)

- Same shape as Pritty Rounds: `Item.ammo = AmmoID.Bullet`, same splash
  mechanic (own AoE trigger spawn, same 0.4x damage-ratio convention)
- Base damage ~13 (in line with `NapalmBullet`'s 12, this tier's bullet), with
  a larger and/or higher-damage splash than Pritty Rounds — exact numbers
  tuned during implementation, splash stays proportionally similar (~0.4x)
- Recipe: gated behind Hardmode materials — exact ingredients finalized
  during implementation, following this tier's existing recipe conventions
  (see `NapalmBullet.AddRecipes` / other `Ammos/Hardmode/*` recipes for the
  pattern)
- Sprite: new, ~16×36px, dark bitter-liqueur-bottle silhouette (visually
  distinct from Pritty Rounds' wine-bottle shape — darker glass, different
  cap), script-generated (see Sprites section)

## Sprites

No image-generation tool is available in this environment that produces
authentic Terraria-style pixel art, and the user explicitly ruled out both
generic placeholders (e.g. this codebase's existing 2×2px
`Rockets/RocketPlaceholder.png`) and shallow reuse of existing Calamity item
icons (e.g. directly reusing `RedWine.png`/`Whiskey.png` as our items'
primary visual identity).

Approach: write a Python (Pillow) script that draws each sprite
pixel-by-pixel/shape-by-shape with deliberate, hand-specified silhouettes and
a small, flat, outlined color palette (matching Terraria's general item-icon
style — solid fills, dark outline, no gradients/anti-aliasing noise), sized to
match the dimensions of comparable existing items in this codebase (measured
directly, see Scope section above). After generating each PNG, visually
inspect it (render + view) before treating it as final — if a sprite looks
bad, iterate on the script rather than ship it as-is. This is a best-effort
approach, not a substitute for real pixel-art skill; if the result isn't
good enough, fall back to asking the user for hand-made/commissioned art
before shipping.

## Explicitly NOT doing

- No new debuff/buff/status effect (splash is pure AoE damage, no lingering
  effect)
- No interaction with Calamity's existing Alcohol buff/potion system
  (`CalamityMod.Items.Potions.Alcohol` / `CalamityMod.Buffs.Alcohol`) beyond
  namespace coexistence — these are unrelated systems, not integrated
- No changes to the gun's own damage scaling beyond what vanilla ammo-swap
  mechanics already provide

## Verification

Since this repo can't be compiled on this machine (no `dotnet` SDK, no local
Terraria/tModLoader install — see `HANDOFF.md`), verification is: (1) static
review of the new C# against this codebase's existing patterns, matching the
same discipline used for the `perf-and-correctness-fixes` branch; (2) visual
inspection of the generated sprites; (3) actual in-game Build+Reload +
playtesting happens on the user's Windows machine per the existing handoff
workflow, same as the other two pending branches.
