# Handoff: Calamity 2.2.2 compatibility fix

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
