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

using System;
using UnityEngine;
using UnityEngine.Events;

namespace Over
{
    public enum OvrAudioActionType { PlayClipAtPoint, PlayScheduled, UnityAction };

    [System.Serializable]
    public class OvrAudio : OvrNode
    {
        //OvrAudio
        public AudioSource audioSource;
        public OvrAudioActionType actionType;

        //Play Clip At Point
        public AudioClip clip;

        [OvrVariable]
        public OvrVector3 position;
        [OvrVariable]
        public OvrFloat volume;

        //Play Scheduled
        [OvrVariable]
        public OvrFloat time;

        //UnityAction
        public UnityEvent unityAction;

        protected override void Execution()
        {
            switch (actionType)
            {
                case OvrAudioActionType.PlayClipAtPoint:

                    if (clip != null && position != null && volume != null)
                        AudioSource.PlayClipAtPoint(clip, position.TypedVariable, Mathf.Clamp01(volume.TypedVariable));
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrAudioActionType.PlayScheduled:

                    if (audioSource != null && time != null)
                        audioSource.PlayScheduled(time.TypedVariable);
                    else if (Application.isEditor)
                        Debug.LogError("Null reference at gameObject " + gameObject.name);

                    break;
                case OvrAudioActionType.UnityAction:
                    unityAction?.Invoke();
                    break;
            }

        }

        //public void SetAmbisonicDecoderFloat(int index, float value) { audioSource.SetAmbisonicDecoderFloat(index, value); }
        //public void SetCustomCurve(AudioSourceCurveType type, AnimationCurve curve) { audioSource.SetCustomCurve(type, curve); }
        //public void SetScheduledEndTime(double time) { audioSource.SetScheduledEndTime(time); }
        //public void SetScheduledStartTime(double time) { audioSource.SetScheduledStartTime(time); }
        //public void SetSpatializerFloat(int index, float value) { audioSource.SetSpatializerFloat(index, value); }
    }
}