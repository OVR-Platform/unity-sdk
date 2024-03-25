# OVER Unity SDK

## Unity Setup
1. Go to Unity Hub, and download Unity Hub (not the beta version)
2. After downloading Unity Hub, download Unity version 2022.3.13f1 from the Unity archive.

![Unity Version](https://assets.ovr.ai/images/github-sdk/unity-builder-tutorial-01-2.png)
_Fig 1.2: Unity Version_

3.	{For Windows} Select Android and iOS platform.
3.1	Select Mac platform for Over Console support. (Optional)
3.2	Select WebGL platform for Instant App support . (Optional)

![Unity platforms](https://assets.ovr.ai/images/github-sdk/unity-builder-tutorial-02-1-2.png)
_Fig 1.3: Unity platforms_

3.	{For Mac) Select Android and iOS platform.
3.1	Select Mac platform for Over Windows support. (Optional)
3.2	Select WebGL platform for Instant App support . (Optional)

![Unity platforms](https://assets.ovr.ai/images/github-sdk/unity-builder-tutorial-02-2.png)
_Fig 1.3: Unity platforms_

4.	Create new project in unity with the download version.


## Install OVER Editor Package
 The OVER Editor Package can be installed, via Unity Package Manager, by following these steps:

- Open `Window > Package Manager`
- Click the `+` button
- You can either choose to install the package from a Git URL (Recommended), by pasting the following git url `https://github.com/OVR-Platform/unity-sdk.git#upm`, or from a tarball file (You can download the last version [here](https://github.com/OVR-Platform/unity-sdk/releases)).

##### If you have problems downloading the package from git, try the following steps
###### For Mac Users:
- `brew install git` (https://git-scm.com/download/mac)
- `brew install git-lfs`
- `git lfs install`
- `sudo git lfs install --system`
- (Important) `sudo ln -s "$(which git-lfs)" /usr/local/bin/`
- (Otherwise) `sudo ln -s "$(which git-lfs)" "$(git --exec-path)/git-lfs"`
###### For Windows Users:
- install git (https://git-scm.com/download/win)

##### For Glb and Gltf support in Unity
- install glTFast package (https://github.com/atteneder/glTFast)

## Create and use your OVER API Keys
1.	Login on the OVER Web Builder https://builder.ovr.ai using your credentials or your Metamask wallet and click on "Api Keys" from the top menu.

![Create new project](https://assets.ovr.ai/images/github-sdk/sdk1-tutorial-2.png)
_Fig 3.1: Click on API Keys_

2. Click on the the button "Create token".

![Create new project](https://assets.ovr.ai/images/github-sdk/sdk2-tutorial.png)
_Fig 3.2: Click on Create token_

3.	Type a name as a reference and select an expiration date.

![Create new project](https://assets.ovr.ai/images/github-sdk/sdk3-tutorial-2.png)
_Fig 3.3: Create your token_

4.	Select and copy the newly created token to use it in Unity .

![Create new project](https://assets.ovr.ai/images/github-sdk/sdk4-tutorial.png)
_Fig 3.4: Get and use your token_

# OVER SDK MANUAL
[Visit SDK Manual](https://docs.overthereality.ai/over-sdk-manual/
)
