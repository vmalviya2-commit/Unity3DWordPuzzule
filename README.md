# WorddyBuddy Splash Screen

This repository contains Unity scripts and documentation for a kid-friendly 3D splash screen called **"WorddyBuddy"**. The setup includes animated TextMeshPro elements, balloons, particles, and a scripted transition into your main scene.

## Contents

* `Assets/Scripts/Splash/SplashController.cs` – Drives the splash reveal, confetti burst, skip hint, and scene transition.
* `Assets/Scripts/Splash/BalloonBobbing.cs` – Gives balloon props a playful float and sway.
* `Assets/Scripts/Splash/SlowRotator.cs` – Adds subtle rotation to props such as the floor ring.
* `Docs/WorddyBuddySplashSetup.md` – Step-by-step guide to constructing the scene inside Unity.

## Getting Started

1. Create or open a Unity 2022 LTS (or newer) project.
2. Copy the `Assets` folder contents into your project to import the scripts.
3. Follow `Docs/WorddyBuddySplashSetup.md` to build the scene hierarchy, assign materials, and wire the scripts.
4. Add a target scene (e.g., `Home`) to Build Settings so the splash can transition once it finishes or the player taps to skip.

Feel free to extend the splash with additional props, post-processing, or loading logic as needed for your project.
