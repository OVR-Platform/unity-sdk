# OVER Unity SDK

## Unity Setup

1. Download Unity Hub (not the beta version).
2. After downloading Unity Hub, download Unity version 6000.3.18f1 from the Unity archive.

![Unity Hub archive](.github/images/unity-hub-archive.png)
_Fig 1.1: Unity Hub archive_

3. **On Windows**: install Android, iOS and WebGL Build Support (required). Add Mac Build Support (Mono) for OVER Console support (optional).

![Unity modules on Windows](.github/images/unity-modules-windows.png)
_Fig 1.2: Unity modules on Windows_

4. **On Mac**: install Android, iOS and WebGL Build Support (required). Add Windows Build Support (Mono) for OVER Console support (optional).

![Unity modules on Mac](.github/images/unity-modules-mac.png)
_Fig 1.3: Unity modules on Mac_

5. Create a new project with Unity 6000.3.18f1, using the 3D (Built-In Render Pipeline) template. URP and HDRP are not supported.

## Install OVER Editor Package

The OVER Editor Package can be installed via the Unity Package Manager, by following these steps:

- Open `Window > Package Management > Package Manager`
- Click the `+` button
- You can either install the package from a Git URL (Recommended), by pasting the following git url `https://github.com/OVR-Platform/unity-sdk.git#upm`, or from a tarball file (You can download the latest version [here](https://github.com/OVR-Platform/unity-sdk/releases)).

##### If you have problems downloading the package from git, try the following steps
###### For Mac Users:
- `brew install git` (https://git-scm.com/download/mac)
- `brew install git-lfs`
- `git lfs install`
- `sudo git lfs install --system`
- (Important) `sudo ln -s "$(which git-lfs)" /usr/local/bin/`
- (Otherwise) `sudo ln -s "$(which git-lfs)" "$(git --exec-path)/git-lfs"`
###### For Windows Users:
- Install git (https://git-scm.com/download/win)

## Create and use your OVER API Keys

1. Log in to the OVER Web Builder using your credentials or your Metamask wallet and click on "Unity SDK" in the left menu (or go directly to [builder.ovr.ai/dash/unity-sdk](https://builder.ovr.ai/dash/unity-sdk)).
2. In the Access Tokens section, click on the "New Token" button.

![Unity SDK page in the Web Builder](.github/images/webbuilder-unity-sdk.png)
_Fig 2.1: Unity SDK page and New Token button_

3. Type a name as a reference. The expiration date is optional: leave it empty for a token that does not expire.

![New Access Token](.github/images/webbuilder-new-token.png)
_Fig 2.2: Create your token_

4. Select and copy the newly created token to use it in Unity.

![Copy your token](.github/images/webbuilder-copy-token.png)
_Fig 2.3: Copy your token_

## Getting Started

To create a new experience, follow the Getting Started steps in the [Quick Start](https://docs.overthereality.ai/over-sdk-manual/setting-up/quick-start), or start from one of the [Jump Start Samples](https://docs.overthereality.ai/over-sdk-manual/setting-up/jump-start-samples).

# OVER SDK MANUAL
[Visit SDK Manual](https://docs.overthereality.ai/over-sdk-manual/)
