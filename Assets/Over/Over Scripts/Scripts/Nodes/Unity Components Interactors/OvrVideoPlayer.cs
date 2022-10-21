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
using UnityEngine.Video;
using UnityEngine.Events;
using System;

namespace Over
{
    public enum OvrVideoPlayerActionType { EnableAudioTrack, SetDirectAudioMute, SetDirectAudioVolume, SetTargetAudioSource, UnityAction }

    [System.Serializable]
    public class OvrVideoPlayer : OvrNode
    {
        public VideoPlayer videoPlayer;
        public OvrVideoPlayerActionType actionType;

        [OvrVariable]
        public OvrInt trackIndex;
        //Enable Audio Track
        [OvrVariable]
        public OvrBool enable;
        //Set Direct Audio Mute
        [OvrVariable]
        public OvrBool mute;
        //Set Direct Audio Volume
        [OvrVariable]
        public OvrFloat volume;
        //Set Target Audio Source
        [OvrVariable]
        public AudioSource audioSource;

        //UnityEvent
        public UnityEvent unityAction;

        protected override void Execution()
        {
            switch (actionType)
            {
                case OvrVideoPlayerActionType.UnityAction:
                    break;
                default:
                    if (videoPlayer == null)
                    {
                        if (Application.isEditor)
                            Debug.LogError("Null reference at gameObject " + gameObject.name);
                        return;
                    }
                    break;
            }

            switch (actionType)
            {
                case OvrVideoPlayerActionType.EnableAudioTrack:

                    if (trackIndex != null && enable != null)
                    {
                        videoPlayer.EnableAudioTrack(Convert.ToUInt16(trackIndex.TypedVariable), enable);
                        videoPlayer.Stop();
                        videoPlayer.Play();
                    }
                    else if (Application.isEditor)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }

                    break;
                case OvrVideoPlayerActionType.SetDirectAudioMute:

                    if (trackIndex != null && mute != null)
                    {
                        videoPlayer.SetDirectAudioMute(Convert.ToUInt16(trackIndex.TypedVariable), mute);
                        videoPlayer.Stop();
                        videoPlayer.Play();
                    }
                    else if (Application.isEditor)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }

                    break;
                case OvrVideoPlayerActionType.SetDirectAudioVolume:

                    if (trackIndex != null && volume != null)
                    {
                        videoPlayer.SetDirectAudioVolume(Convert.ToUInt16(trackIndex.TypedVariable), Mathf.Clamp01(volume.TypedVariable));
                        videoPlayer.Stop();
                        videoPlayer.Play();
                    }
                    else if (Application.isEditor)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }

                    break;
                case OvrVideoPlayerActionType.SetTargetAudioSource:

                    if (trackIndex != null && audioSource != null)
                    {
                        videoPlayer.SetTargetAudioSource(Convert.ToUInt16(trackIndex.TypedVariable), audioSource);
                        videoPlayer.Stop();
                        videoPlayer.Play();
                    }
                    else if (Application.isEditor)
                    {
                        Debug.LogError("Null reference at gameObject " + gameObject.name);
                    }
                    break;
                case OvrVideoPlayerActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }
        }

        //public void IsAudioTrackEnabled(ushort trackIndex) { videoPlayer.IsAudioTrackEnabled(trackIndex); }
    }
}