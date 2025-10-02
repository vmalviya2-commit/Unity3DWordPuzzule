# WorddyBuddy Splash Screen Setup

This guide outlines how to assemble the "WorddyBuddy" kid-friendly splash screen inside Unity 2022 LTS (or newer). The instructions mirror the scene structure and motion logic provided by the included scripts.

## Scene Overview

Create a new scene called **`SplashScene`** and use the following hierarchy:

```
Main Camera
Directional Light
PostProcessVolume             (optional, URP post FX)
SplashRoot
  ├─ Title3D                  (TextMeshPro – 3D)
  ├─ SubTitle                 (TextMeshPro – UI or 3D)
  ├─ VersionText              (TextMeshPro – UI or 3D)
  ├─ BuddyBalloonL            (Mesh: Sphere + “string” cylinder)
  ├─ BuddyBalloonR            (Mesh: Sphere + “string” cylinder)
  ├─ FloorRing                (Torus mesh or Cylinder with donut texture)
  ├─ FloatingStars            (ParticleSystem)
  ├─ ConfettiBurst            (ParticleSystem)
  └─ TapToSkipHint            (TextMeshPro – UI)
```

### Camera

* Projection: **Perspective**
* Position: **(0, 1.5, -8)**
* Rotation: **(0, 0, 0)**
* Field of View: **45–55°**
* Background: soft sky blue (RGBA 120/190/255/255) or a gradient skybox.

### Lighting

* Directional light intensity ≈ **1.1**, warm tint (#FFF1CF), rotation about **(40, 30, 0)**.
* Add URP Bloom (≈0.5) and a very subtle Vignette if using the Universal Render Pipeline.

## Visual Direction

* **Color palette:** sky blue, sunshine yellow, candy pink, mint green, soft purple.
* **Fonts:** Rounded TMP font such as Nunito or Poppins Rounded.
* **Title3D:** Text "WORDDYBUDDY" with 0.1–0.2 extrusion, bevel, white face color, outline #3EC1FF, and a soft drop shadow (duplicate mesh offset slightly backward).
* **SubTitle:** "Let’s Find Some Words!" in matching font.
* **VersionText:** bottom-right, updated automatically by `SplashController`.
* **Balloons:** Pastel spheres with thin cylinders as strings and optional specular highlight quads.
* **FloorRing:** Torus or textured cylinder with a slow rotation.

## Particle Systems

### FloatingStars

* Shape: Box or cone above the title.
* Start Size: 0.1–0.2, Start Speed: 0.2–0.6.
* Gravity: 0, Emission: 10 per second.
* Color Over Lifetime: gentle pastel gradient.
* Use a small star sprite for the texture.

### ConfettiBurst

* Duration: 1 second, Burst: 60–100 particles.
* Gravity Modifier: 0.5, Start Speed: 2–4.
* Start Size: 0.08–0.14, Random rotation over lifetime.
* Use quads/triangles with colors from the palette.

## Included Scripts

All scripts live under `Assets/Scripts/Splash/`.

### `SplashController`

* Drives the entrance animation, audio cues, confetti burst, skip hint, and scene transition.
* Automatically fills in the app version (`Application.version`) on the `VersionText` component.
* Provides an adjustable idle hold duration (`idleHoldDuration`).
* Allows skipping with a tap/click once the hint is visible.

### `BalloonBobbing`

* Applies vertical bobbing and gentle sway to balloon props. Assign to both balloons and tweak amplitude/speed for variation.

### `SlowRotator`

* Adds a constant rotation to props such as the `FloorRing` for subtle motion.

## Wiring the Scene

1. Import TextMeshPro essentials (`Window → TextMeshPro → Import TMP Essentials`).
2. Create the hierarchy above and assign materials/fonts to match the palette.
3. Place the scripts on their respective objects:
   * `SplashController` on an empty GameObject; wire Title3D, VersionText, ConfettiBurst, AudioSource, clips, and TapToSkip CanvasGroup.
   * `BalloonBobbing` on each balloon mesh.
   * `SlowRotator` on the floor ring (set `eulerPerSecond.y` around 12).
4. Add the `SplashScene` and target scene (e.g., `Home`) to **Build Settings → Scenes In Build**. Set `SplashScene` as the first entry.
5. Provide two short SFX clips: a soft whoosh (`whooshIn`) and a sparkle (`sparkleHit`). Assign them to the `SplashController` AudioSource.
6. Play the scene. Tapping/clicking should skip the wait; otherwise, the controller automatically loads the next scene after `idleHoldDuration` seconds.

## Optional Enhancements

* Add a URP post-processing volume with Bloom and subtle Vignette.
* Duplicate Title3D with an offset for a soft drop shadow effect.
* Extend `SplashController` to pre-load Addressables or display a loading indicator during the idle hold period.
* Create a `Home` scene template for the next step in your onboarding flow.

Enjoy your sparkling WorddyBuddy splash screen!
