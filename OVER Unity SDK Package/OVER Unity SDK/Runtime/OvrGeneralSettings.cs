/**
 * OVER Unity SDK License
 *
 * Copyright 2021 Over The Realty
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * 1. The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * 2. All copies of substantial portions of the Software may only be used in connection
 * with services provided by OVER.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NON INFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */

using UnityEngine;

namespace OverSDK
{
    public struct GeneralSettings
    {
        //AR Experience
        public bool environmentOcclusionAR;
        public bool automaticStartEnvironmentOcclusionAR;

        public bool humanOcclusionAR;
        public bool automaticStartHumanOcclusionAR;

        public bool meshOcclusionAR;
        public bool automaticStartMeshOcclusionAR;

        //Remote Experience
        public bool environmentOcclusionRemote;
        public bool automaticStartEnvironmentOcclusionRemote;

        public bool humanOcclusionRemote;
        public bool automaticStartHumanOcclusionRemote;

        public bool meshOcclusionRemote;
        public bool automaticStartMeshOcclusionRemote;

        public bool walkModeButton;
        public bool automaticWalkModeButton;


        public GeneralSettings(bool environmentOcclusionAR, bool automaticStartEnvironmentOcclusionAR, bool humanOcclusionAR, bool automaticStartHumanOcclusionAR, bool meshOcclusionAR, bool automaticStartMeshOcclusionAR, bool environmentOcclusionRemote, bool automaticStartEnvironmentOcclusionRemote, bool humanOcclusionRemote, bool automaticStartHumanOcclusionRemote, bool meshOcclusionRemote, bool automaticStartMeshOcclusionRemote, bool walkModeButton, bool automaticWalkModeButton)
        {
            this.environmentOcclusionAR = environmentOcclusionAR;
            this.automaticStartEnvironmentOcclusionAR = automaticStartEnvironmentOcclusionAR;
            this.humanOcclusionAR = humanOcclusionAR;
            this.automaticStartHumanOcclusionAR = automaticStartHumanOcclusionAR;
            this.meshOcclusionAR = meshOcclusionAR;
            this.automaticStartMeshOcclusionAR = automaticStartMeshOcclusionAR;
            this.environmentOcclusionRemote = environmentOcclusionRemote;
            this.automaticStartEnvironmentOcclusionRemote = automaticStartEnvironmentOcclusionRemote;
            this.humanOcclusionRemote = humanOcclusionRemote;
            this.automaticStartHumanOcclusionRemote = automaticStartHumanOcclusionRemote;
            this.meshOcclusionRemote = meshOcclusionRemote;
            this.automaticStartMeshOcclusionRemote = automaticStartMeshOcclusionRemote;
            this.walkModeButton = walkModeButton;
            this.automaticWalkModeButton = automaticWalkModeButton;
        }
    }

    public class OvrGeneralSettings : MonoBehaviour
    {
        //AR Experience
        public bool environmentOcclusionAR;
        public bool automaticStartEnvironmentOcclusionAR;

        public bool humanOcclusionAR;
        public bool automaticStartHumanOcclusionAR;

        public bool meshOcclusionAR;
        public bool automaticStartMeshOcclusionAR;

        //Remote Experience
        public bool environmentOcclusionRemote;
        public bool automaticStartEnvironmentOcclusionRemote;

        public bool humanOcclusionRemote;
        public bool automaticStartHumanOcclusionRemote;

        public bool meshOcclusionRemote;
        public bool automaticStartMeshOcclusionRemote;

        public bool walkModeButton;
        public bool automaticWalkModeButton;

        public void ImportSettings(GeneralSettings generalSettings)
        {
            this.environmentOcclusionAR = generalSettings.environmentOcclusionAR;
            this.automaticStartEnvironmentOcclusionAR = generalSettings.automaticStartEnvironmentOcclusionAR;
            this.humanOcclusionAR = generalSettings.humanOcclusionAR;
            this.automaticStartHumanOcclusionAR = generalSettings.automaticStartHumanOcclusionAR;
            this.meshOcclusionAR = generalSettings.meshOcclusionAR;
            this.automaticStartMeshOcclusionAR = generalSettings.automaticStartMeshOcclusionAR;
            this.environmentOcclusionRemote = generalSettings.environmentOcclusionRemote;
            this.automaticStartEnvironmentOcclusionRemote = generalSettings.automaticStartEnvironmentOcclusionRemote;
            this.humanOcclusionRemote = generalSettings.humanOcclusionRemote;
            this.automaticStartHumanOcclusionRemote = generalSettings.automaticStartHumanOcclusionRemote;
            this.meshOcclusionRemote = generalSettings.meshOcclusionRemote;
            this.automaticStartMeshOcclusionRemote = generalSettings.automaticStartMeshOcclusionRemote;
            this.walkModeButton = generalSettings.walkModeButton;
            this.automaticWalkModeButton = generalSettings.automaticWalkModeButton;
        }
    }
}
