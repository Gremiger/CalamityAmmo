# Handoff: Calamity 2.2.2 compatibility fix + perf/correctness sweep

## Branches (test in this order)

1. **`Levantine`** — just the Calamity 2.2.2 crash fix (commit `b636bce`).
   Test this first since it's what actually unblocks your save.
2. **`perf-and-correctness-fixes`** — branched off `Levantine`, adds a
   separate commit (`659ab7e`) fixing 6 correctness bugs and 4 performance
   issues found by a code-review sweep (see its own section below). Kept
   separate on purpose so a problem in the perf/correctness pass can't
   block the crash fix — test it independently after `Levantine` is
   confirmed working, then merge if it's good.

## Context

Calamity Mod updated to 2.2.2. This addon (last adapted for 2.2.1, commit `4aa1086`)
was still referencing a field Calamity removed, which crashed the addon on load
(tModLoader JIT-verifies the whole assembly at load time, so one missing
reference anywhere kills the whole mod) and broke saves that depend on it.

This repo is a private fork of `gugudao/CalamityAmmo`:
- `origin` → `Gremiger/CalamityAmmo` (this fork, private)
- `upstream` → `gugudao/CalamityAmmo` (original, for pulling future updates)

## What was found and fixed here (on this Mac, code-only — not build-tested)

**Root cause:** `CalamityMod.CalPlayer.CalamityPlayer.gloveOfPrecision` (a `bool`
field) was removed from Calamity's `CalamityPlayer` class in 2.2.2. The Glove of
Precision accessory itself still exists, but Calamity reworked it to apply its
bonus directly inside the item's own `UpdateAccessory`, instead of setting a
shared player flag other mods could read.

This addon read that field every frame in `Modplayer.cs` (`CaePlayer.PostUpdate`)
to grant its own Ranged-damage bonus when the player had the glove equipped.

Exact crash (from tModLoader's error screen):
```
An error occurred while loading CalamityAmmo v1.2.2.0
Terraria.ModLoader.Exceptions.JITException:
In CalamityAmmo.CaePlayer.PostUpdate, Field not found: 'CalamityMod.CalPlayer.CalamityPlayer.gloveOfPrecision'.
```

**Fix (2 files, both committed):**
- `CAEUtils.cs` — added `HasAccessoryEquipped(Player player, int itemType)`,
  which scans equipped accessory slots directly (same pattern Calamity itself
  uses, e.g. `SpringStool.cs`'s `IsVanillaStoolEquipped`).
- `Modplayer.cs` — replaced the dead field read with
  `CAEUtils.HasAccessoryEquipped(Player, ModContent.ItemType<GloveOfPrecision>())`,
  preserving identical behavior. Added the missing
  `using CalamityMod.Items.Accessories;`.
- `build.txt` — bumped addon version `1.2.2.0` → `1.2.2.1` to mark this fix.
  `modReferences = CalamityMod@2.2` was left as-is; it's a major.minor floor
  and 2.2.2 still satisfies it.

**Verification done so far:** A full cross-reference of every other
`CalamityMod.*` type/member this addon touches (26 files reference Calamity
namespaces) against the actual Calamity 2.2.2 source found nothing else
broken — `gloveOfPrecision` appears to be the only stale reference for this
update cycle. This was static analysis only (grepping/reading source); it
cannot catch behavioral changes that don't involve a removed/renamed member,
and **none of this has been compiled or run in-game**, since this Mac has no
Terraria/tModLoader install and no `dotnet` SDK.

## What you need to do on the Windows machine

1. **Clone your fork there** (not the original `gugudao` repo):
   ```
   git clone git@github.com:Gremiger/CalamityAmmo.git
   ```
   Or if you already have a local clone of the original pointed at
   `gugudao/CalamityAmmo`, just repoint its `origin` remote to your fork the
   same way this Mac's copy is set up:
   ```
   git remote rename origin upstream
   git remote add origin git@github.com:Gremiger/CalamityAmmo.git
   git fetch origin
   git checkout Levantine
   git reset --hard origin/Levantine
   ```

2. **Place it in tModLoader's `ModSources` folder** if it isn't already there
   (typically
   `Documents\My Games\Terraria\tModLoader\ModSources\CalamityAmmo`), so the
   `..\tModLoader.targets` import in `CalamityAmmo.csproj` resolves.

3. **Make sure Calamity Mod is updated to 2.2.2** in tModLoader's Mod Browser
   / Workshop subscription.

4. **Check `CalamityAmmo.csproj`'s `HintPath`.** It currently points to:
   ```
   ..\..\..\..\..\..\SteamLibrary\steamapps\workshop\content\1281930\2824688072\2026.4\CalamityMod\CalamityMod.dll
   ```
   That `2026.4` subfolder was manually bumped by the original author on the
   last two Calamity updates (see `git log -p -- CalamityAmmo.csproj`), so it
   likely needs to change again for 2.2.2 — but only you can see what that
   path actually resolves to on your machine. If tModLoader's own
   Build+Reload doesn't need this manual reference at all (it may resolve
   Calamity automatically via `modReferences` in `build.txt`), you may not
   need to touch it — try building first and only fix this if the build
   fails on missing `CalamityMod` types.

5. **Build:** in tModLoader, enable Developer Mode, then use the in-game
   **Build + Reload** button for CalamityAmmo (Mod Sources menu). Watch for
   compile errors — if the HintPath is stale, you'll get "type or namespace
   CalamityMod could not be found"-style errors, not the JIT field error from
   before.

6. **Load your save** and confirm the mod loads without the JITException.

7. If it builds and loads cleanly, that's the fix confirmed end-to-end. Push
   any additional changes to `origin` (your fork) — `upstream` stays as a
   reference to pull future updates from `gugudao/CalamityAmmo` if they ever
   update it themselves.

## If the build turns up more errors

The static cross-reference we did should have caught anything else broken,
but the compiler is the real ground truth. If Build+Reload surfaces more
missing-type/member errors, they'll point at the exact file/line — bring
those back and we'll fix them the same way (find what Calamity renamed/moved
it to in the current source, adjust the addon to match).

## Second branch: `perf-and-correctness-fixes`

Once `Levantine` is confirmed working (mod loads, save is playable), check
out this branch to test the second round of fixes:
```
git checkout perf-and-correctness-fixes
git reset --hard origin/perf-and-correctness-fixes
```
Build + Reload again and play a bit, focusing on the things this branch
changed gameplay-wise:

- **Napalm Bullet** — bonus ignite (extra fire damage) should now only
  proc on a target that was *already* oiled from a previous hit, not on
  every single hit.
- **Mushroom United Nations' Shroomere shot** — should now home toward the
  *nearest* enemy in range (and respect line-of-sight/walls), not the
  farthest.
- **Glove of Recklessness + a ranged weapon** — use speed should be
  *faster* (was a dead branch making it always slower with no upside
  before this fix).
- **Transformer Coil** (electric coil chain lightning) and **Arcane
  Quiver / arrow mana cost** — these had shared-state bugs that mostly
  bite in multiplayer; single-player behavior should look unchanged if
  it looked fine before.
- **Lightning Vortex** (post-Moon Lord projectile) — should target the
  *nearest* enemy on its initial acquire, not whichever one happened to
  be last in the internal list.
- General performance: harder to observe directly, but the fixes target
  real per-tick hot paths (`autoSelectNPC`, `SporeSac`, the electric coil
  chain's `ElectricStream` projectile) that could cause frame-time spikes,
  especially with multiple stacked accessory effects or in multiplayer.

This was also code-only, not build-tested — same caveat as above applies.
If Build+Reload turns up errors here, bring them back too.

### New content on this same branch: Pritty Blaster + Pritty/Fernet Rounds

Also on `perf-and-correctness-fixes` (committed directly onto it, not a
separate branch — see `docs/superpowers/specs/2026-08-09-pritty-fernet-gun-design.md`
and `docs/superpowers/plans/2026-08-10-pritty-fernet-gun.md` for the full
design/plan): a new whimsical Argentina-themed weapon line.

- **Pritty Blaster** (`Weapons/PrittyBlaster.cs`) — a Pre-Hardmode gun,
  craftable day one (3 Bottle + 10 Wood at a Work Bench). Fires any
  Bullet-type ammo.
- **Pritty Rounds** (`Ammos/Pre_Hardmode/PrittyRounds.cs`) — the starter
  ammo tier (damage 7), themed on *vino con Pritty*.
- **Fernet Rounds** (`Ammos/Hardmode/FernetRounds.cs`) — the Hardmode
  upgrade (damage 13), themed on *Fernet con Coca*.
- Both ammo tiers shatter into a small AoE "fizzy splash"
  (`Projectiles/FizzySplash.cs`) on hit, dealing 40% of the hit's damage to
  anything nearby — check that this actually procs and looks reasonable in
  combat (a burst of green dust for Pritty Rounds, amber/orange for Fernet
  Rounds).
- Sprites are script-generated pixel art (not hand-drawn, not borrowed from
  elsewhere) — take a look at how `Weapons/PrittyBlaster.png`,
  `Ammos/Pre_Hardmode/PrittyRounds.png`, and `Ammos/Hardmode/FernetRounds.png`
  actually render in-game; they were only visually checked as static PNGs on
  this Mac, never seen rendered by the actual game engine/UI scaling.
- Confirm Pritty Blaster actually fires Pritty Rounds/Fernet Rounds
  correctly when loaded (it intentionally has no `Shoot()` override, unlike
  this addon's other guns, so it should respect whatever ammo is in your
  ammo slot — worth double-checking this works as expected since it departs
  from this codebase's usual gun pattern).
- None of this new code references anything under the `CalamityMod`
  namespace (only vanilla Terraria/tModLoader APIs), so it shouldn't be
  exposed to any Calamity-2.2.2-style breakage — but same caveat as
  everything else here: code-only, never compiled.

Once everything checks out, merge `perf-and-correctness-fixes` into
`Levantine` (or open a PR from one to the other in your fork) and push.
