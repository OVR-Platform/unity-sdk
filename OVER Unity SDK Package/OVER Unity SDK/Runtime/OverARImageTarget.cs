using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace OverSDK
{

    public enum OverARImageTargetType { Movable, Static, Custom };


#if !APP_MAIN
    [ExecuteInEditMode]
#endif
    public class OverARImageTarget : MonoBehaviour
    {

        public OverARImageTargetType BehaviourType;

        //[OvrVariable]
        public bool hideIfLost = false;
        //[OvrVariable]
        public bool stopIfLost = false;
        //[OvrVariable]
        public bool assetFollowSize = false;

        [HideInInspector]
        public int id;

        [HideInInspector]
        private string title;

        [HideInInspector]
        public GameObject imageTarget;

        [HideInInspector]
        public float assetWidth;

        [HideInInspector]
        public Vector3 imageTargetPosition = Vector3.zero;
        [HideInInspector]
        public Quaternion imageTargetRotation = Quaternion.identity;
        [HideInInspector]
        public Vector3 imageTargetScale = Vector3.zero;

#if !APP_MAIN
        public void Init(int id, string title, float width, Texture texture)
        {
            this.gameObject.transform.position = new Vector3(0, 0.01f, 0);
            this.id = id;
            gameObject.name = gameObject.name + $" ({title})";
            this.title = title;
            assetWidth = width;
            transform.localScale = new Vector3(width, width, width);
            SetTexture(texture);
           
        }


        private void Update()
        {
            PreventUserChanges();
        }

        public void PreventUserChanges()
        {
            if (assetWidth > 0)
            {
                transform.localScale = new Vector3(assetWidth, assetWidth, assetWidth);
            }

            if (imageTarget != null && imageTargetPosition != Vector3.zero && imageTargetRotation != Quaternion.identity && imageTargetScale != Vector3.zero)
            {
                imageTarget.transform.localPosition = imageTargetPosition;
                imageTarget.transform.localRotation = imageTargetRotation;
                imageTarget.transform.localScale = imageTargetScale;
            }
        }


        public void SetTexture( Texture texture)
        {
            imageTarget = transform.Find("ImageTarget")?.gameObject;
            //imageTarget.transform.hideFlags = HideFlags.NotEditable | HideFlags.HideInInspector;
           
            float widthRatio = (float)texture.width / (float)texture.height;
            float HeightRatio = (float)texture.height / (float)texture.width;

            if(widthRatio > 1)
            {
                widthRatio = 1;
            }else if(HeightRatio > 1)
            {
                HeightRatio = 1;
            }

            imageTarget.transform.localScale = new Vector3(widthRatio, HeightRatio, 1);
            imageTarget.transform.rotation = Quaternion.Euler(90, 0, 0);



            imageTargetPosition = new Vector3(imageTarget.transform.position.x, imageTarget.transform.position.y, imageTarget.transform.position.z);
            imageTargetRotation = Quaternion.Euler(90, 0, 0);
            imageTargetScale = new Vector3(widthRatio, HeightRatio, 1);
            // Create a new material with the Unlit/Texture shader
            Material material = new Material(Shader.Find("Unlit/Texture"))
            {
                // Assign the texture to the material's main texture
                mainTexture = texture
            };

            // Set the material to the quad
            MeshRenderer meshRenderer = imageTarget.GetComponent<MeshRenderer>();
            meshRenderer.material = material;
        }
#endif
    }
}

