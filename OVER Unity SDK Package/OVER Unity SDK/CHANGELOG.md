# Changelog

All notable changes to the OVER Unity SDK. Newest release first.

## 0.5.2-beta - 2026-10-27

### Improvements

- **General cleanup and reorganisation** — every SDK command now lives in a single `OVER`
  menu in the menu bar, and menu entries, prefab names and samples have been tidied up and
  made consistent. Existing projects are unaffected.
- **Updated manual** — the SDK manual has been reviewed and rewritten page by page, and is
  reachable from `OVER > Documentation`.
- Various bug fixes and performance improvements.

## 0.5.1-beta - 2026-10-19

### New Features

- **WebGPU support** — WebGL bundles now ship shader variants for both WebGPU and WebGL2.
- **Per-scene settings** — land selection, account and publication settings are now saved
  per scene instead of per project, and follow the scene even if you rename or move it.

### Improvements

- **Redesigned interface** — the Assets and Publication sections have been rebuilt around
  a new card layout, with cleaner lists and a more consistent look across the whole SDK.
- Various bug fixes, UI polish and performance improvements.

## 0.5.0-beta - 2026-09-15

### ⚠️ Breaking Changes

- **Unity 6 is now required** — the SDK targets Unity `6000.3.18f1`. Projects still on
  Unity 2022.3 must be upgraded before updating the SDK.

### New Features

- **Gaussian Splatting from local files** — drop a `.ply` or `.splat` file into your
  Assets folder and the SDK offers to convert it and spawn it in the scene as a Gaussian
  Splat.
- **Gaussian Splatting for external assets** — `.ply` and `.splat` files uploaded to the
  OVER Builder can now be imported directly into Unity from the Imported Assets panel,
  just like any other external asset.
- **New samples** — AR Navigation and Zebra, showing AR pathfinding and image tracking.

### Major Refactor

- **Land and Multi-Land selection** — completely redesigned selection flow, faster and
  clearer when working with multiple adjacent lands.
- **OVRMap repositioning** — reworked repositioning system: the map keeps its reference
  position, and an automatic restart resumes from the map you were working on.
- **Publishing pipeline** — stricter pre-publish validation and automatic generation of
  the app-side assets during publication.

### Bug Fixes and Improvements

- Various bug fixes, UI polish and performance improvements.

## 0.4.31-beta - 2026-02-24

### Bug Fixes and Improvements

- Bug fixes and performance enhancements
- Added new Unity Visual Scripting nodes

## 0.4.30-beta - 2025-11-20

### Bug Fixes and Improvements

- Bug fixes and performance enhancements
- Unity version update

## 0.4.21-beta - 2025-06-30

### Bug Fixes and Improvements

- Bug fixes and performance enhancements

## 0.4.12-beta - 2025-06-04

### New Features

- Advanced Publish (Single Build Upload): Added a new Advanced Publish mode that allows uploading a single build directly to Staging OVRLand, streamlining the publication process.

- Custom Animations for Controllable Objects: Controllable objects now support custom idle and walk animations, offering greater flexibility for interactive content.

- New AR Events: Added support for new AR-related events:
   - ArImageTargetFound
   - ArImageTargetLost
   - UserEnteredExperience
   
- Assets Section Integration: Introduced a new Assets section, tightly connected with the Builder, to better manage and organize assets used in experiences.

### Big Refactor

- Complete Refactor of the Publication Flow: The entire publish flow has been fully refactored. All legacy code has been removed, bringing this section up to speed with the rest of the SDK improvements.

### Bug Fixes and Improvements

- Fixed crash during publication caused by interactions with OverGateConsole and other related components.

- Various bug fixes and performance enhancements across the SDK.

## 0.4.9-beta - 2025-04-30

### Bug Fixes

- Fixes: Publication panel hotfixes

## 0.4.8-beta - 2025-03-20

### Bug Fixes

- Fixes: Publication panel hotfixes

## 0.4.7-beta - 2025-03-19

### New Features

- Unity AI Navigation Support: Added support for Unity's AI Navigation, enabling more dynamic and intelligent movement in AR experiences.
- Unity Splines Support: Implemented support for Unity Splines, allowing for smoother and more precise path-based animations.
- New Publication Setting for "Main Plane" and "AR Image Targets": Introduced an option to manage the detection type of the main AR plane and the UI support for AR Image Targets, offering more customization.
- Real-Time Online Synchronization for Controllable Objects and NFTs: Controllable objects and NFTs now support real-time online synchronization, enabling more interactive and dynamic experiences.

### Big Refactor

- Complete Refactor of the Publication Panel: The entire publication panel has been completely redesigned, making it significantly faster and more efficient.

### Bug Fixes and Improvements

- Various bug fixes and performance enhancements.

## 0.4.2-beta - 2025-02-14

### New Features

- AR Image Targets Support: Added the ability to recognize images as targets for augmented reality experiences. Use specific images to trigger AR content and elevate the interactivity of your applications.

### Bug Fixes and Improvements

- Various bug fixes and performance enhancements.

## 0.4.1-beta - 2024-11-13

### Bug Fixes

- Fixes: Video generation bug fix

## 0.4.0-beta - 2024-11-11

### New Features

- Support for Unity Visual Scripting: Added support for Unity Visual Scripting, enabling the full potential of Unity’s visual scripting capabilities for easier and more dynamic workflows.

- In-App Controllable Assets: All assets can now be transformed into controllable assets within the app.

### Bug Fixes and Improvements

- Various bug fixes and user experience enhancements: Addressed multiple bugs and implemented improvements to further enhance system stability and user experience.

## 0.3.2-beta - 2024-08-01

### Bug Fixes

- Fixes: Publication bug fix

## 0.3.1-beta - 2024-07-29

### Bug Fixes

- Fixes: Video generation bug fix

## 0.3.0-beta - 2024-07-25

### New Features

- Support for ARlayer and Virtual layer: Introduced new support for publishing content in different layers: AR layer and Virtual layer, expanding publishing possibilities.

- Assisted thumbnail and video generation: Added the feature for assisted generation of thumbnails and videos, simplifying the process of creating visual content.

### Improvements

- Improved publishing editor: The publishing editor has received significant improvements, with organized settings and new features to better manage the publishing of experiences.

### Bug Fixes

- Various bug fixes: Resolved numerous bugs to enhance system stability and performance.

## 0.2.6-beta - 2024-05-23

### New Features

New sublist system for variables: Introduced a system that allows the creation and management of sublists for a more organized and flexible handling of variables.

### Improvements

New examples for using nodes in web communication: Added practical examples to facilitate the implementation of nodes in web communication contexts.

### Bug Fixes

Various bug fixes: Resolved numerous bugs to enhance the system's stability and performance.

## 0.2.5-beta - 2024-03-25

- New Unity 2022.3.13f

New Features:
- Added new node to fire animation events
- Added a new node to get the gameobject that has its OverScript
- Added a new variable type for Text Mesh Pro in the scene (not UGUI)
- Added Gaussian Splatting support for the Over lands
- Added object to rigidbody cast node.
- Added gated access to allow only users with a specific NFT to enter the experience

Improvements:
- Changed Addition Publication text from window to OVER CONSOLE
- Changed min and max size editor window to 800x700
- Improved GltFast missing package managing logic
- Improved overall memory allocation SDK editor window performance
- Changed window editor header Over logo
- Added ping object every time an Over element is created in the scene with right click
- Changed "Over SDK" around the SDK to "OVER Unity SDK" to have a standardization
- Temporary disabled "Publish Remote Experience" tab
- Added IsKinematic to the rigidbody node
- Added rigidbody variables output for rigidbody exposer
- Changed "Additional Publication Platform" to a single toggle "Over Console Support" enable/disable
- Removed GUID header from variables
- Added a search window for variable type selection
- Renamed, moved and remade OVER SDK editor window GUI
- Improved window editor performance
- Moved logout button
- Added hyperlink icon to links
- The "Enabled By Default" toggle for each Occlusion Setting is enabled initially
- Changed OVRMaps list formatting for a more readable version
- Moved the check validity button after the asset picker element
- Removed ignore from the light settings dropdown and set the default values to Off, On, Off
- If an object is static, batching static is disabled when publishing to preserve the object's position.

Bug Fixes:
- Fix editor window keeps popping up when the user closes it
- Fix app breaks if a playerprefs is not found
- Removed OverLight and OverSettings components from the OverAsset prefab
- Fixed over script nullification overwrite when the user pressed esc when loading a new over script resulting in resetting all variables
- Fix bug when the scene was continuously updated and dirty every time an OverScript gameobject was selected
- Fixed NFT pagination
- Fixed error Failed to download texture: Cannot resolve the destination host
- Fixed error [Over] Failed to download texture: Request timeout
- Fixed thumbnail NFT refresh not loaded when going to play mode
- Fixed JSON saving
- Added check to prevent the OnClickUp event button from being fired when the cursor(or finger) is not on the button itself when releasing the screen
- Updated GltFast package URL
- Fixed error when multiple overScript of the same occurrences are in the scene.
- Changed publication tooltip text and label for remote experience.
- Changed helpbox text for publishing settings.
- Fixed GLTFast installed package if an older version is present.
- Fixed crash when publishing a scene.
- Fixed base plane spawned in the scene when publishing when the user does not own lands.
- Fixed light settings and global settings not
Security Updates:
- Fixed check scene button to not check only the root objects in the scene but also nested objects
- Added NavMesh Surface as an allowed component

Known Issues:
- Ctrl + z most of the time breaks the links between nodes, temporarily added a check to always remove the link between nodes and added a warning to notify the user

## 0.1.4-beta - 2023-07-27

- New Unity 2022.3.1f
- New Screen Share Preset
- Bug Fixes

## 0.1.3-beta - 2023-06-16

- Bug fix
- Mapping in folders publication

## 0.1.2-beta - 2023-06-06

- New NFT import
- Bug Fix

## 0.1.1-beta - 2023-05-05

- New Visual Scripting System

## 0.0.7-alpha - 2022-12-14

- New import package flow
- NFT ticket publication
- Bug fix

## 0.0.6-alpha - 2022-11-15

- Scene publication
- Hierarchy right click OVER label

## 0.0.5-alpha - 2022-10-21

- Map2Earn publication flow refactoring.

- POAP Publication support

## 0.0.4-alpha - 2022-10-13

bug fixes and improvements

## 0.0.3-alpha - 2022-08-12

- Added the support for the **Map2Earn**.
- Inserted the support to allow the user to activate or deactivate the Occlusion in the Experiences, which, within the OVER App, is located at the top right.
- Added support function to turn **Walk Mode** on or off. Walk Mode setting enables the user to toggle on and off walk mode with a button.
- Enabled the **OVER Canvas**. OVER Canvas allows you to create customized features with which interact in the Experiences. This way, the end user can find the one-of-a-kind experience.
- Inserted the **Chroma Key Video Player**. Through this function you can add a video in the Experience and remove the background, so you can see the main subject in transparency, inserted in the context. The green screen used for films allows you to better understand this concept: once the video is made, in post production the green background is removed.
- Support for publishing the same experience on multiple contiguous OVRLands

## 0.0.2-alpha - 2022-05-02

Thanks to our plugin, you can take advantage of all the Unity Engine features as it is the same development environment used in the OVER app. You can integrate major 3D formats like fbx, dae, 3ds.

In case of Version Update, please delete any previously installed version within the project.

For More Info https://docs.overthereality.ai/over-sdk-manual/

## 0.0.1-alpha - 2021-12-30

Here is the list of scripts available in this first release of OvrModSystem:

- **OVRClickableObject**: With this script, you can trigger events by clicking on 3D objects.
- **OvrColliderTriggerEvent**: With this script, you can trigger Unity events by user or object interaction with colliders.
- **OvrTriggerModEvents**: With this script, you can trigger scripts that are part of the OvrModSystem events through user or object interaction with colliders.
- **OvrModTriggerBtn**: With this script, you can trigger events by clicking on buttons in a canvas, for example, two-dimensional buttons.
- **OvrAudio**: This script allows you to modify audio sources by increasing or decreasing their volume, modifying the position in space, or changing the source.
- **OvrVideoPlayer**: This script allows you to modify the audio of the videos inserted in the scene, such as increasing or decreasing the volume or changing the audio source.
- **OvrCharacterController**: It allows you to connect a pre-existing CharacterController and move it in space.
- **OvrLight**: With this script, you can control the color of the lights in the scene.
- **OvrLineRenderer**: This script allows you to draw two-dimensional lines in three-dimensional space.
- **OvrRigidBody**: Thanks to this script, you can modify the force applied to an object using the rules of the physics engine.
- **OvrArWorldCanvas**: It allows you to enable the canvas click-in app integrated into the scene.
- **OvrAssets**: Defines the prefab of the scene to be published in Ovr.
